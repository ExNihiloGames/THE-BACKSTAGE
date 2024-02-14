using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour, IDropHandler
{
    public TestEquipmentType readEquipmentsOfType;

    private bool testResult;

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            if(eventData.pointerDrag.gameObject.GetComponent<TestingItem>() != null)
            {
                if (eventData.pointerDrag.gameObject.GetComponent<TestingItem>().testEquipmentType == readEquipmentsOfType)
                {
                    eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;

                    TestEquipmentType itemInSlotType = eventData.pointerDrag.gameObject.GetComponent<TestingItem>().testEquipmentType;

                    switch (itemInSlotType)
                    {
                        case TestEquipmentType.Alcohol:
                            testResult = eventData.pointerDrag.gameObject.GetComponent<TestingItem>().isDrunk;
                            Debug.Log("Character is Drunk?" + testResult);
                            break;

                        case TestEquipmentType.Drugs:
                            testResult = eventData.pointerDrag.gameObject.GetComponent<TestingItem>().isHigh;
                            Debug.Log("Character is High?" + testResult);
                            break;
                    }
                }
                else
                {
                    Debug.Log("Wrong Equipment Type !");
                }
            }
            else
            {
                Debug.Log("Wrong Equipment Type !");
            }
        }
        else
        {
            Debug.Log("Wrong Equipment Type !");
        }
    }
}
