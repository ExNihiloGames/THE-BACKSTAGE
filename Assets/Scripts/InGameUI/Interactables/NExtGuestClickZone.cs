using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class NExtGuestClickZone : MonoBehaviour, IPointerDownHandler
{
    public static event Action Clicked;

    public void OnPointerDown(PointerEventData eventData)
    {
        Clicked?.Invoke();
    }
}
