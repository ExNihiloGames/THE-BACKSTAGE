using System.Collections;
using UnityEngine;
using TMPro;

public class Bubble : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public float textSpeed = .05f;
    private int currentLineIndex = 0; //indice du tableau ou se trouve le dialogue
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
            StopCoroutine(TL);
            textComponent.text = text;
            isDialogueDisplaying = false;
            currentLineIndex++;
        }
        else
        {
            TL = StartCoroutine(TypeLine());
        }
    }

    private void Start()
    {
        
    }

    IEnumerator TypeLine()
    {
        isDialogueDisplaying = true;

        foreach (char c in text.ToCharArray())
        {
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
        currentLineIndex++;
        isDialogueDisplaying = false;
    }
}