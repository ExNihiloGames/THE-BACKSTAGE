using System.Collections;
using UnityEngine;
using TMPro;

public class Bubble : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public float textSpeed = .05f;
    private bool isDialogueDisplaying = false;
    private Coroutine TL;
    private string text;

    public Bubble(string givenText) //Inutilisé
    {
        text = givenText;
    }

    public void SetText(string givenText)
    {
        text = givenText;
    }

    public void DisplayText()
    {
        textComponent.text = string.Empty;

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