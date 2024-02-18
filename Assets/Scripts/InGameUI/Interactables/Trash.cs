using UnityEngine;
using UnityEngine.EventSystems;

public class Trash : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            if (eventData.pointerDrag.GetComponent<IDCard>() == null)
            {
                Destroy(eventData.pointerDrag);
            }            
        }
    }
}
