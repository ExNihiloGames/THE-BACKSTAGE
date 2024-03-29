using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TestsDistributor : MonoBehaviour, IPointerDownHandler
{
    public GameObject TestEquipment;
    private DialogManager dialogManager;
    private RectTransform rectTransform;

    private void Start()
    {
        dialogManager = FindObjectOfType<DialogManager>();
        rectTransform = GetComponent<RectTransform>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        GameObject newTestEquipment = Instantiate(TestEquipment, rectTransform.anchoredPosition, Quaternion.identity);
        RectTransform nT_Rect = newTestEquipment.GetComponent<RectTransform>();

        nT_Rect.SetParent(rectTransform.parent);
        nT_Rect.anchoredPosition = rectTransform.anchoredPosition;
        dialogManager.RequestPlayerDialog(DialogStyle.PerformTest);
    }
}
