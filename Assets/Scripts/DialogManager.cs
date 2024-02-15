using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UIElements;
using TMPro;

public class DialogManager : MonoBehaviour
{
    public GameObject BubbleBase; //Mettre la bulle qui sert de modèle depuis Unity
    private int bubblesDisplayed = 0;
    RectTransform panelRectTransform;

    // Tableau de lignes de dialogue à afficher
    private int currentLineIndex = 0;
    private string[] dialogueLines =
    {
        "Salut, ça va ?",
        "Ca va, et toi ?",
        "Ca va très bien merci !",
        "Cool !"
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
        Debug.Log("Creating New Bubble");

        GameObject newBubble = Instantiate(BubbleBase, panelRectTransform.position, Quaternion.identity);
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
