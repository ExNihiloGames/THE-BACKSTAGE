using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TestCharacter : MonoBehaviour, IPointerDownHandler, IDropHandler
{
    public GameObject idCardTemplate;

    public string _firstName = "Jhon";
    public string _lastName = "Doe";
    public string _species = "Jawa";
    public string _birthdate = "01/11/2765";
    public bool _isValid = false;
    public bool _isDrunk = false;
    public bool _isHigh = true;
    public bool has_ID;

    private RectTransform rectTransform;
    private bool hasHandedId;
    private bool hasBeenAlcoholTested;
    private bool hasBeenDrugTested;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("HI");
        if (has_ID)
        {
            if (!hasHandedId)
            {
                Debug.Log("ID? Sure. Here it is");
                GenerateID();
                hasHandedId = true;
            }
            else
            {
                Debug.Log("Dude I Already gave it to you");
            }
        }
        else
        {
            Debug.Log("Heu... I don't have it on me but if you let me pass I won't cause you trouble");
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
                    TestingItem testEquipment = eventData.pointerDrag.GetComponent<TestingItem>();
                    switch (testEquipment.testEquipmentType)
                    {
                        case TestEquipmentType.Alcohol:
                            if(!hasBeenAlcoholTested)
                            {
                                StartCoroutine(ConductTest(eventData.pointerDrag, 1.5f));
                                testEquipment.isDrunk = _isDrunk;
                                hasBeenAlcoholTested = true;
                            }
                            else
                            {
                                Debug.Log("Again? What you like the smell of my breath so much ?");
                            }
                            break;

                        case TestEquipmentType.Drugs:
                            if (!hasBeenDrugTested)
                            {
                                StartCoroutine(ConductTest(eventData.pointerDrag, 1.5f));
                                testEquipment.isHigh = _isHigh;
                                hasBeenDrugTested = true;
                            }
                            else
                            {
                                Debug.Log("Yes, This is my natural state, thanks. No need to try that again.");
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
        idCardContent.firstName = _firstName;
        idCardContent.lastName = _lastName;
        idCardContent.species = _species;
        idCardContent.birthdate = _birthdate;
        idCardContent.isValid = _isValid;
    }

    IEnumerator ConductTest(GameObject testEquipment, float testDuration)
    {
        testEquipment.GetComponent<Image>().enabled = false;
        yield return new WaitForSeconds(testDuration);
        testEquipment.GetComponent<Image>().enabled = true;
    }
}
