using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogSystem : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public float textSpeed = .05f;
    private int currentLineIndex = 0; //indice du tableau ou se trouve le dialogue
    private bool isDialogueDisplaying = false;
    private Coroutine TL;

    // Tableau de lignes de dialogue à afficher
    private string[] dialogueLines =
    {
        "Salut, ça va ?",
        "Ca va, et toi ?",
        "Ca va très bien merci !",
        "Cool !"
    };

    void Start()
    {
        Debug.Log("Start!");

        DisplayDialogue();
    }

    void Update()
    {
        // Lit la prochaine ligne de dialogue lorsque le joueur appuie sur une touche
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Bouton espace préssé !");

            if (currentLineIndex < dialogueLines.Length)
            {
                DisplayDialogue();
            }
            else
            {
                //gameObject.SetActive(false);
                currentLineIndex = 0;
                Debug.Log("Fin des dialogues");
            }
        }
    }

    void DisplayDialogue()
    {
        textComponent.text = string.Empty;

        if (isDialogueDisplaying)
        {
            StopCoroutine(TL);
            textComponent.text = dialogueLines[currentLineIndex];
            isDialogueDisplaying = false;
            currentLineIndex++;
        }
        else
        {
            TL = StartCoroutine(TypeLine());
        }
    }

    IEnumerator TypeLine()
    {
        isDialogueDisplaying = true;

        foreach (char c in dialogueLines[currentLineIndex].ToCharArray())
        {
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
        currentLineIndex++;
        isDialogueDisplaying = false;
    }
}