using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class NextGuestClickZone : MonoBehaviour, IPointerDownHandler
{
    public static event Action Clicked;

    public void OnPointerDown(PointerEventData eventData)
    {
        Clicked?.Invoke();
    }
}
