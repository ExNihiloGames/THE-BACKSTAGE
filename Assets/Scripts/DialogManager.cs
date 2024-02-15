using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UIElements;
using TMPro;
using UnityEditor;

public class DialogManager : MonoBehaviour
{
    public GameObject BubbleBase; //Mettre la bulle qui sert de modèle depuis Unity
    private int bubblesDisplayed = 0;
    RectTransform panelRectTransform;
    private List<GameObject> listOfBubbles = new List<GameObject>();
/*    private static GameManager _instance;
    public static GameManager Instance { get { return _instance; } }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
        }
    }
*/

    // Tableau de lignes de dialogue à afficher
    private int currentLineIndex = 0;
    private string[] dialogueLines =
    {
        "Salut, ça va ?",
        "Ca va, et toi ?",
        "Ca va très bien merci !",
        "Cool !",
        "Oui",
        "Ca va très bien merci !Ca va très bien merci !Ca va très bien merci !",
        "CoolCoolCool !"
    };

    void Start()
    {
        panelRectTransform = GetComponent<RectTransform>();
        Debug.Log("DisplayManager");
        BubbleBase.SetActive(false);

        CreateNewBubble(dialogueLines[0]);
    }

    // Update is called once per frame
    void Update()
    {
        // Lit la prochaine ligne de dialogue lorsque le joueur appuie sur une touche
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if(currentLineIndex < dialogueLines.Length)
            {
                CreateNewBubble(dialogueLines[currentLineIndex]);
            } else
            {
                //todo: retirer toutes les bulles
            }
        }
    }

    void CreateNewBubble(string text = "")
    {
        float marginBottom = 20;
        List<GameObject> bubblesToRemove = new List<GameObject>();

        if(listOfBubbles.Count > 0)
        {
            foreach (GameObject aBubble in listOfBubbles)
            {
                RectTransform aBubbleRectTransform = aBubble.GetComponent<RectTransform>();
                aBubbleRectTransform.localPosition = new Vector3(aBubbleRectTransform.localPosition.x, aBubbleRectTransform.localPosition.y + aBubbleRectTransform.rect.height + marginBottom);

                if (aBubbleRectTransform.localPosition.y > panelRectTransform.sizeDelta.y / 2)
                {
                    bubblesToRemove.Add(aBubble);
                }
            }

            foreach (GameObject aBubble in bubblesToRemove)
            {
                listOfBubbles.Remove(aBubble);
                Destroy(aBubble);
            }
        }

        GameObject newBubble = Instantiate(BubbleBase, panelRectTransform.position, Quaternion.identity);
        listOfBubbles.Add(newBubble);
        Debug.Log(listOfBubbles.Count);
        Bubble bubble = newBubble.GetComponent<Bubble>();
        bubble.SetText(text);
        RectTransform newBubbleRectTransform = newBubble.GetComponent<RectTransform>();

        newBubbleRectTransform.SetParent(panelRectTransform); //On indique qui est le parent
        newBubbleRectTransform.position = panelRectTransform.position; //Panel
        newBubbleRectTransform.localScale = new Vector3(1f,1f,1f); //dû à un bug de Unity on force le scale initial
        newBubbleRectTransform.sizeDelta = new Vector2(panelRectTransform.sizeDelta.x, newBubbleRectTransform.sizeDelta.y);
        
        newBubble.name = "Bubble_" + ++bubblesDisplayed;
        newBubble.SetActive(true);

        TextMeshProUGUI textMeshProComponent = newBubble.GetComponentInChildren<TextMeshProUGUI>();

        if (textMeshProComponent != null)
        {
            bubble.DisplayText();
            currentLineIndex++;
        }
        else
        {
            Debug.LogError("Le composant TextMeshPro n'a pas été trouvé dans newBubble.");
        }
    }
}
