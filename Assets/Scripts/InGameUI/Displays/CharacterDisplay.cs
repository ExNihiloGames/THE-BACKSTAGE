using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CharacterDisplay : MonoBehaviour, IPointerDownHandler, IDropHandler
{
    public GameObject idCardTemplate;

    GameManager gameManager;
    DialogManager dialogManager;
    Character currentCharater;
    RectTransform rectTransform;

    bool hasHandedId;
    bool hasBeenAlcoholTested;
    bool hasBeenDrugTested;
    bool idHandedBack;

    // Start is called before the first frame update
    void Start()
    {
        GameManager.OnGuestShowUP += LoadCharacter;
        GameManager.OnGuestAccepted += UnloadCharacter;
        GameManager.OnGuestRejected += UnloadCharacter;
        gameManager = GameManager.Instance;
        dialogManager= DialogManager.Instance;
        rectTransform = GetComponent<RectTransform>();
        gameManager.DebugCharacterDisplayState(true);
    }

    void LoadCharacter(Character character)
    {
        currentCharater = character;
        GetComponent<Image>().enabled = true;
        gameManager.DebugCharacterDisplayState(false);
    }

    void UnloadCharacter()
    {
        GetComponent<Image>().enabled = false;
        currentCharater = null;
        hasHandedId = false;
        hasBeenAlcoholTested = false;
        hasBeenDrugTested = false;
        gameManager.DebugCharacterDisplayState(true);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (currentCharater.hasID)
        {
            if (!hasHandedId)
            {
                Debug.Log("ID? Sure. Here it is");
                dialogManager.RequestNPCDialog(DialogStyle.Acquiesce);
                GenerateID();
                hasHandedId = true;
            }
            else
            {
                Debug.Log("Dude I Already gave it to you"); // Request DialogManager (???)
            }
        }
        else
        {
            Debug.Log("Heu... I don't have it on me but if you let me pass I won't cause you trouble");
            dialogManager.RequestNPCDialog(DialogStyle.NoIDCard);
            dialogManager.RequestPlayerDialog(DialogStyle.NoIDCard);
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            if (eventData.pointerDrag.GetComponent<TestingItem>() != null)
            {
                if (!hasBeenAlcoholTested || !hasBeenDrugTested)
                {

                    Debug.Log("Pfff that's really not necessary...");
                    dialogManager.RequestNPCDialog(DialogStyle.Acquiesce);
                    TestingItem testEquipment = eventData.pointerDrag.GetComponent<TestingItem>();
                    switch (testEquipment.testEquipmentType)
                    {
                        case TestEquipmentType.Alcohol:
                            if (!hasBeenAlcoholTested)
                            {
                                StartCoroutine(ConductTest(eventData.pointerDrag, 1.5f));
                                testEquipment.isDrunk = currentCharater.isDrunk;
                                hasBeenAlcoholTested = true;
                            }
                            else
                            {
                                Debug.Log("Again? What you like the smell of my breath so much ?");
                                dialogManager.RequestNPCDialog(DialogStyle.RefusalAlcoholTest);

                            }
                            break;

                        case TestEquipmentType.Drugs:
                            if (!hasBeenDrugTested)
                            {
                                StartCoroutine(ConductTest(eventData.pointerDrag, 1.5f));
                                testEquipment.isHigh = currentCharater.isHigh;
                                hasBeenDrugTested = true;
                            }
                            else
                            {
                                Debug.Log("Yes, This is my natural state, thanks. No need to try that again.");
                                dialogManager.RequestNPCDialog(DialogStyle.RefusalDrugTest);
                            }
                            break;
                    }
                }
                else
                {
                    Debug.Log("Hey Listen I did all your dumb tests already. Just let me in");
                }
            }
            else if (eventData.pointerDrag.GetComponent<IDCard>() != null)
            {
                Debug.Log("Thanks");
                dialogManager.RequestNPCDialog(DialogStyle.Thanks);
                idHandedBack = true;
                Destroy(eventData.pointerDrag);
            }
        }
    }

    private void GenerateID()
    {
        GameObject idCard = Instantiate(idCardTemplate, rectTransform.anchoredPosition, Quaternion.identity);
        RectTransform cardRectTransform = idCard.GetComponent<RectTransform>();

        cardRectTransform.SetParent(rectTransform.parent);
        cardRectTransform.anchoredPosition = rectTransform.anchoredPosition;

        IDCard idCardContent = idCard.GetComponent<IDCard>();
        idCardContent.firstName = currentCharater.firstName;
        idCardContent.lastName = currentCharater.lastName;
        idCardContent.species = currentCharater.characterSpecie.displayName;
        idCardContent.birthdate = currentCharater.dateOfBirth.ToShortDateString();
        idCardContent.isValid = currentCharater.isIDValid;
    }

    private void CheckHasID()
    {
        if (!idHandedBack)
        {
            Debug.Log("Can I have my ID back?");
        }
        else
        {
            UnloadCharacter();
        }
    }

    IEnumerator ConductTest(GameObject testEquipment, float testDuration)
    {
        testEquipment.GetComponent<Image>().enabled = false;
        yield return new WaitForSeconds(testDuration);
        testEquipment.GetComponent<Image>().enabled = true;
    }
}
