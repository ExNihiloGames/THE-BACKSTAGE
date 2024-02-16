using System.Collections;
using UnityEngine;
using TMPro;

public class Bubble : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public float textSpeed = .05f;
    public bool adaptBubbleSize = true;
    private bool isDialogueDisplaying = false;
    private Coroutine TL;
    private string text;
    private RectTransform imageRectTransform;

    public void SetText(string givenText)
    {
        text = givenText;
    }

    public void DisplayText()
    {
        //l'effacer et recommencer

        textComponent.text = string.Empty;  //on retire le texte de la pr�fab (lorem ipsum)
        textComponent.text = text; //On �crit le texte pour que la bulle s'affiche entierement, comme ca l'espace n�cessaire est d�j� calcul�
        AdaptBubbleSize();
        textComponent.text = string.Empty; //On r�efface pour que le texte soit tap� via la coroutine

        if (isDialogueDisplaying)
        {
            Abbreviate();
        }
        else
        {
            TL = StartCoroutine(TypeLine());
        }
    }

    public bool Abbreviate()
    {
        StopCoroutine(TL);
        textComponent.text = text;
        isDialogueDisplaying = false;

        return true;
    }

    public bool GetIsDialogueDisplaying()
    {

        return isDialogueDisplaying;
    }

    private void AdaptBubbleSize()
    {
        Vector2 textSize = textComponent.GetPreferredValues();
        
        imageRectTransform = GetComponent<RectTransform>();
        RectTransform panelRectTransform = imageRectTransform.parent.GetComponent<RectTransform>();

        Debug.Log(panelRectTransform.name);

        //recherche de d�fauts
        float initialX = imageRectTransform.sizeDelta.x;
        float initialY = imageRectTransform.sizeDelta.y;

        Vector2 panelMin = panelRectTransform.anchoredPosition - panelRectTransform.sizeDelta;
        Vector2 panelMax = panelRectTransform.anchoredPosition + panelRectTransform.sizeDelta;
        Vector2 imageMin = imageRectTransform.anchoredPosition - imageRectTransform.sizeDelta;
        Vector2 imageMax = imageRectTransform.anchoredPosition + imageRectTransform.sizeDelta;

        bool leftOverflow = imageMin.x < panelMin.x;
        bool rightOverflow = imageMax.x > panelMax.x;

        Debug.Log(panelMin);
        Debug.Log(panelMax);


        // Afficher les r�sultats dans la console
        Debug.Log("D�passement du c�t� gauche : " + leftOverflow);
        Debug.Log("D�passement du c�t� droit : " + rightOverflow);

        //adaptation de la bulle

        imageRectTransform.sizeDelta = textSize;
    }

    public void ForceScale()
    {
        imageRectTransform = GetComponent<RectTransform>();
        imageRectTransform.localScale = new Vector3(1f, 1f, 1f); //d� � un bug de Unity on force le scale initial
    }

    public void Position(string position = "")
    {
        imageRectTransform = GetComponent<RectTransform>();
        switch (position)
        {
            case "right":
                //gauche

                // Modifier la position de l'image pour qu'elle se colle � gauche du parent
                imageRectTransform.anchorMin = new Vector2(0, 0); // Ancrage minimum � gauche
                imageRectTransform.anchorMax = new Vector2(0, 0); // Ancrage maximum � gauche
                imageRectTransform.pivot = new Vector2(0, 0); // Pivot � gauche

                // R�initialiser la position et la taille de l'image en fonction de celles du parent
                imageRectTransform.anchoredPosition = Vector2.zero;
                return;
            case "left":
            default:
                //droite
                imageRectTransform.anchorMin = new Vector2(1, 0); // Ancrage minimum � gauche
                imageRectTransform.anchorMax = new Vector2(1, 0); // Ancrage maximum � gauche
                imageRectTransform.pivot = new Vector2(1, 0); // Pivot � gauche

                // R�initialiser la position et la taille de l'image en fonction de celles du parent
                imageRectTransform.anchoredPosition = Vector2.zero;
                imageRectTransform.anchoredPosition = new Vector2(1, 0);
                return;
        }
    }

    public void SetHeight(float height)
    {
        //gere sa hauteur a l'aide du parametre
    }
    public void GoUp(float distance)
    {
        imageRectTransform = GetComponent<RectTransform>();
        imageRectTransform.localPosition = new Vector2(imageRectTransform.localPosition.x, imageRectTransform.localPosition.y + distance);
    }

    IEnumerator TypeLine()
    {
        isDialogueDisplaying = true;

        foreach (char c in text.ToCharArray())
        {
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
        isDialogueDisplaying = false;
    }
}