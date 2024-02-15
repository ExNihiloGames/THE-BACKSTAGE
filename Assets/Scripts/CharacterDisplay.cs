using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CharacterDisplay : MonoBehaviour, IPointerDownHandler, IDropHandler
{
    public GameObject idCardTemplate;

    Character currentCharater;
    RectTransform rectTransform;

    bool hasHandedId;
    bool hasBeenAlcoholTested;
    bool hasBeenDrugTested;

    // Start is called before the first frame update
    void Start()
    {
        GameManager.OnGuestShowUP += LoadCharacter;
        rectTransform = GetComponent<RectTransform>();
    }

    void LoadCharacter(Character character)
    {
        currentCharater = character;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("HI"); // Request DialogManager
        if (currentCharater.hasID)
        {
            if (!hasHandedId)
            {
                Debug.Log("ID? Sure. Here it is"); // Request DialogManager
                GenerateID();
                hasHandedId = true;
            }
            else
            {
                Debug.Log("Dude I Already gave it to you"); // Request DialogManager
            }
        }
        else
        {
            Debug.Log("Heu... I don't have it on me but if you let me pass I won't cause you trouble"); // Request DialogManager
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
                    Debug.Log("Pfff that's really not necessary..."); // Request DialogManager
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
                                Debug.Log("Again? What you like the smell of my breath so much ?"); // Request DialogManager
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
                                Debug.Log("Yes, This is my natural state, thanks. No need to try that again."); // Request DialogManager
                            }
                            break;
                    }
                }
                else
                {
                    Debug.Log("Hey Listen I did all your dumb tests already. Just let me in"); // Request DialogManager
                }
            }
            else if (eventData.pointerDrag.GetComponent<IDCard>() != null)
            {
                Debug.Log("Thanks");
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

    IEnumerator ConductTest(GameObject testEquipment, float testDuration)
    {
        testEquipment.GetComponent<Image>().enabled = false;
        yield return new WaitForSeconds(testDuration);
        testEquipment.GetComponent<Image>().enabled = true;
    }
}
