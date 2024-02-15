using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TestDialogBox : MonoBehaviour, IPointerDownHandler
{
    public GameObject DialogBubble;

    RectTransform rectTransform;
    List<GameObject> allDialogBubbles;
    GameObject toDestroy;
    float prevDialogBubbleHeight;
    float gapBetweenBubbles=10f;
    bool leftOrRight = false;

    // Start is called before the first frame update
    void Start()
    {
        rectTransform= GetComponent<RectTransform>();
        allDialogBubbles = new List<GameObject>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (allDialogBubbles.Count > 0)
        {
            GameObject bubbleToRemove = null;

            foreach (GameObject dialogBubble in allDialogBubbles)
            {
                RectTransform currentRectTransform = dialogBubble.GetComponent<RectTransform>();
                currentRectTransform.localPosition = new Vector3(currentRectTransform.localPosition.x, currentRectTransform.localPosition.y + prevDialogBubbleHeight);
                if (currentRectTransform.localPosition.y > rectTransform.sizeDelta.y / 2)
                {
                    bubbleToRemove = dialogBubble;
                }
            }
            if(bubbleToRemove!= null)
            {
                removeDialogBubble(bubbleToRemove);
            }
        }
        GameObject newDialogBubble = Instantiate(DialogBubble, rectTransform.position, Quaternion.identity);
        RectTransform nDB_rectTransform = newDialogBubble.GetComponent<RectTransform>();

        nDB_rectTransform.SetParent(rectTransform);
        nDB_rectTransform.sizeDelta = new Vector2(2 * rectTransform.sizeDelta.x / 3, nDB_rectTransform.sizeDelta.y);
        if (!leftOrRight)
        {
            nDB_rectTransform.position = new Vector2(rectTransform.position.x + ((rectTransform.sizeDelta.x - nDB_rectTransform.sizeDelta.x) / 2), rectTransform.position.y - (rectTransform.sizeDelta.y  - nDB_rectTransform.sizeDelta.y) / 2); // Align Right
            newDialogBubble.GetComponent<Image>().color = Color.white;
        }
        else
        {
            nDB_rectTransform.position = new Vector2(rectTransform.position.x - ((rectTransform.sizeDelta.x - nDB_rectTransform.sizeDelta.x) / 2), rectTransform.position.y - (rectTransform.sizeDelta.y - nDB_rectTransform.sizeDelta.y) / 2); // Align Left
            newDialogBubble.GetComponent<Image>().color = Color.blue;
        }
        leftOrRight = !leftOrRight;

        allDialogBubbles.Add(newDialogBubble);
        prevDialogBubbleHeight = nDB_rectTransform.sizeDelta.y + gapBetweenBubbles;
    }

    private void removeDialogBubble(GameObject bubbleToRemove)
    {
        allDialogBubbles.Remove(bubbleToRemove);
        Destroy(bubbleToRemove);
    }
}
