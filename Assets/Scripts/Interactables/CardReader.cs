using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;

public class CardReader : MonoBehaviour, IDropHandler
{
    public event EventHandler<OnCardReadEventArgs> OnCardRead;
    public class OnCardReadEventArgs : EventArgs
    {
        public IDCard cardContent;
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            if (eventData.pointerDrag.GetComponent<IDCard>() != null)
            {
                GameObject cardGO = eventData.pointerDrag;
                cardGO.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
                OnCardRead?.Invoke(this, new OnCardReadEventArgs { cardContent = cardGO.GetComponent<IDCard>()});
            }
        }
    }
}
