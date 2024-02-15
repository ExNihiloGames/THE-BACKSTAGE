using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour, IDropHandler
{
    public Action<TestEquipmentType, bool> OnTestEquipmentRead;
    private bool testResult;

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            if(eventData.pointerDrag.gameObject.GetComponent<TestingItem>() != null)
            {
                eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;

                TestEquipmentType itemInSlotType = eventData.pointerDrag.gameObject.GetComponent<TestingItem>().testEquipmentType;

                switch (itemInSlotType)
                {
                    case TestEquipmentType.Alcohol:
                        testResult = eventData.pointerDrag.gameObject.GetComponent<TestingItem>().isDrunk;
                        break;

                    case TestEquipmentType.Drugs:
                        testResult = eventData.pointerDrag.gameObject.GetComponent<TestingItem>().isHigh;
                        break;
                }
                OnTestEquipmentRead?.Invoke(itemInSlotType, testResult);
            }
        }
    }
}
