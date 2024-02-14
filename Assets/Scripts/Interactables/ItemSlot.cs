using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour, IPointerDownHandler, IDropHandler
{
    public TestEquipmentType readEquipmentsOfType;
    public GameObject TestEquipment;

    private RectTransform rectTransform;
    private bool testResult;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        GameObject newTestEquipment = Instantiate(TestEquipment, rectTransform.anchoredPosition, Quaternion.identity);
        RectTransform nT_Rect = newTestEquipment.GetComponent<RectTransform>();

        nT_Rect.SetParent(rectTransform.parent);
        nT_Rect.anchoredPosition= rectTransform.anchoredPosition;
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            if(eventData.pointerDrag.gameObject.GetComponent<TestingItem>() != null)
            {
                if (eventData.pointerDrag.gameObject.GetComponent<TestingItem>().testEquipmentType == readEquipmentsOfType)
                {
                    eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = rectTransform.anchoredPosition;

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
