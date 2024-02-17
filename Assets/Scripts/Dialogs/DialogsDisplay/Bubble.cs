using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Bubble : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public float textSpeed = .05f;
    private Coroutine TL;
    private string text;
    private RectTransform imageRectTransform;

    private bool m_isDialogueDisplaying = false;
    public bool isDialogueDisplaying { get { return m_isDialogueDisplaying; } }

    public void SetText(string givenText)
    {
        text = givenText;
    }

    public void updateBackgroundColor(Color color)
    {
        GetComponent<Image>().color = color;
    }
    public void updateTextColor(Color color)
    {
        textComponent.color = color;
    }
    public void DisplayText()
    {
        textComponent.text = string.Empty;  //on retire le texte de la pr�fab (lorem ipsum)
        textComponent.text = text; //On �crit le texte pour que la bulle s'affiche entierement, comme ca l'espace n�cessaire est d�j� calcul�
        AdaptBubbleSize();
        textComponent.text = string.Empty; //On r�efface pour que le texte soit tap� via la coroutine

        if (m_isDialogueDisplaying)
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
        m_isDialogueDisplaying = false;

        return true;
    }

    public bool GetIsDialogueDisplaying()
    {

        return m_isDialogueDisplaying;
    }

    private void AdaptBubbleSize()
    {
        Vector2 textNeededSpace = textComponent.GetPreferredValues();
        int margin = 20;
        imageRectTransform = GetComponent<RectTransform>();
        RectTransform panelRectTransform = imageRectTransform.parent.GetComponent<RectTransform>();
        float maxWidth = (panelRectTransform.rect.width * 2) / 3;   //la largeur d'une bulle ne peut etre > 2/3 de la largeur du panel
        float maxHeight = imageRectTransform.rect.height;

        if (textNeededSpace.x > maxWidth) { textNeededSpace.x = maxWidth - margin; };
        if(textNeededSpace.y > maxHeight) {  textNeededSpace.y = maxHeight - margin; };

        //adaptation de la bulle
        imageRectTransform.sizeDelta = textNeededSpace;
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

    public void Climb(float distance)
    {
        imageRectTransform = GetComponent<RectTransform>();
        imageRectTransform.localPosition = new Vector2(imageRectTransform.localPosition.x, imageRectTransform.localPosition.y + distance);
    }

    IEnumerator TypeLine()
    {
        m_isDialogueDisplaying = true;
        foreach (char c in text.ToCharArray())
        {
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
        m_isDialogueDisplaying = false;
    }
}