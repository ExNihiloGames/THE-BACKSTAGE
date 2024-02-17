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
    private Queue<DialogInstruction> dialogStack = new Queue<DialogInstruction>();
    Speaker bubbleSide;
    Bubble previousBubble;
    public int marginBubbles = 20;
    private Color bubbleBGColorLeft = Color.red;
    private Color bubbleBGColorRight = Color.blue;
    private Color textColor = Color.white;

    void Start()
    {
        DialogManager.OnDialogBoxDisplayRequest += AddToQueue;
        DialogManager.OnDialogBoxClearRequest += ClearDialogBox;
        panelRectTransform = GetComponent<RectTransform>();
        BubbleBase.SetActive(false);
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
        DialogInstruction nextDialog;

        if (previousBubble == null)
        {
            if (dialogStack.TryDequeue(out nextDialog))
            {
                CreateNewBubble(nextDialog);
            }
        }
        else
        {
            if (!previousBubble.isDialogueDisplaying)
            {
                if (dialogStack.TryDequeue(out nextDialog))
                {
                    CreateNewBubble(nextDialog);
                }
            }
        }
    }

    void AddToQueue(DialogInstruction dialog)
    {
        dialogStack.Enqueue(dialog);
    }

    void ClearDialogBox()
    {
        dialogStack.Clear();
        foreach (GameObject aBubble in listOfBubbles)
        {
            Destroy(aBubble);
        }
        listOfBubbles.Clear();
    }

    void CreateNewBubble(DialogInstruction dialog)
    {
        bubbleSide = dialog.speaker;
        string text = dialog.text;

        GameObject newBubble = Instantiate(BubbleBase, panelRectTransform.position, Quaternion.identity); //copie du prefab Bubble (GameObject)

        Bubble newBubbleBubble = newBubble.GetComponent<Bubble>();   //on integre le composant Bubble
        newBubbleBubble.updateTextColor(textColor);
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
            // Debug.Log("distance to climb: " + newBubbleRectTransform.rect.height);
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
        if (bubbleSide == Speaker.NPC)
        {
            //gauche
            newBubbleBubble.Position("left");
            newBubbleBubble.GetComponent<Bubble>().updateBackgroundColor(bubbleBGColorLeft);
        }
        else
        {
            //droite
            newBubbleBubble.Position("right");
            newBubbleBubble.GetComponent<Bubble>().updateBackgroundColor(bubbleBGColorRight);
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
