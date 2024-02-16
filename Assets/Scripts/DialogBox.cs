using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UIElements;
using TMPro;
using UnityEditor;
using Unity.VisualScripting;

public class DialogBox : MonoBehaviour
{
    public GameObject BubbleBase; //Mettre la bulle qui sert de modèle depuis Unity
    private int bubblesDisplayed = 0;
    RectTransform panelRectTransform;
    private List<GameObject> listOfBubbles = new List<GameObject>();
    bool bubbleSide = false;
    Bubble previousBubble;
    public int marginBubbles = 20;

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
        "Oui",
        "Ca va très bien merci !Ca va très bien merci !Ca va très bien merci !",
        "CoolCoolCool !"
    };

    void Start()
    {
        panelRectTransform = GetComponent<RectTransform>();
        BubbleBase.SetActive(false);

        CreateNewBubble(dialogueLines[0]);
    }

    private Bubble GetPreviousBubble()
    {
        if (listOfBubbles.Count > 0)
        {
            return previousBubble = listOfBubbles[listOfBubbles.Count - 1].GetComponent<Bubble>();
        }
        else
        {
            return null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Lit la prochaine ligne de dialogue lorsque le joueur appuie sur une touche
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if(currentLineIndex < dialogueLines.Length)
            {
                previousBubble = GetPreviousBubble();
                if (previousBubble.GetIsDialogueDisplaying())
                {
                    previousBubble.Abbreviate();    //Abrège frère.
                } else
                {
                    CreateNewBubble(dialogueLines[currentLineIndex]);
                }
            } else
            {
                //todo: retirer toutes les bulles
            }
        }
    }

    void CreateNewBubble(string text = "")
    {

        GameObject newBubble = Instantiate(BubbleBase, panelRectTransform.position, Quaternion.identity); //copie du prefab Bubble (GameObject)

        Bubble newBubbleBubble = newBubble.GetComponent<Bubble>();   //on integre le composant Bubble
        newBubbleBubble.SetText(text); //on lui indique le texte

        RectTransform newBubbleRectTransform = newBubble.GetComponent<RectTransform>(); //on integre le rectTransform
        newBubbleRectTransform.SetParent(panelRectTransform); //On lui indique son parent

        //gestion de la taille
        newBubbleBubble.ForceScale(); //dû à un bug de Unity on force le scale initial

        PushToSide(newBubbleBubble);

        newBubble.name = "Bubble_" + ++bubblesDisplayed;
        newBubble.SetActive(true);

        //affichage du texte
        TextMeshProUGUI textMeshProComponent = newBubble.GetComponentInChildren<TextMeshProUGUI>();
        if (textMeshProComponent != null)
        {
            newBubbleBubble.DisplayText();
            //gestion des autres bubbles
            ClimbPreviousBubbles(newBubbleRectTransform.rect.height); //on remonte les autres bulles relativement a la hauteur de la nouvelle bulle
            listOfBubbles.Add(newBubble);   //on l'ajoute a la liste des Bubbles affichés
            Debug.Log("distance to climb: " + newBubbleRectTransform.rect.height);

            currentLineIndex++;
        }
        else
        {
            Debug.LogError("Le composant TextMeshPro n'a pas été trouvé dans newBubble.");
        }
    }

    /**
     * true = gauche; false = droite 
    */
    private void PushToSide(Bubble newBubbleBubble)
    {
        //gestion du coté 
        if (bubbleSide)
        {
            //gauche
            bubbleSide = !bubbleSide;
            newBubbleBubble.Position("left");
        }
        else
        {
            //droite
            bubbleSide = !bubbleSide;
            newBubbleBubble.Position("right");
        }
    }

    void ClimbPreviousBubbles(float distance)
    {
        //gestion des bulles déjà éxistante
        List<GameObject> bubblesToRemove = new List<GameObject>();
        if (listOfBubbles.Count > 0)
        {
            Bubble previousBubble = GetPreviousBubble();
            float rectPreviousBubbleHeight = previousBubble.GetComponent<RectTransform>().rect.height;

            foreach (GameObject aBubble in listOfBubbles)
            {
                Bubble theBubble = aBubble.GetComponent<Bubble>();
                RectTransform aBubbleRectTransform = theBubble.GetComponent<RectTransform>();
                theBubble.Climb(distance+ marginBubbles);

                if (aBubbleRectTransform.localPosition.y + marginBubbles * 2 > (panelRectTransform.sizeDelta.y / 2))
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
    }
}
