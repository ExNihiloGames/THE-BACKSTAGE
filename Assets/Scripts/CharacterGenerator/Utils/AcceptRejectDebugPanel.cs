using System;
using UnityEngine;
using UnityEngine.UI;

public class AcceptRejectDebugPanel : MonoBehaviour
{
    [SerializeField] Button acceptButton;
    [SerializeField] Button rejectButton;

    public static Action<bool> accepted;

    private void OnEnable()
    {
        acceptButton.onClick.AddListener(() => accepted?.Invoke(true));
        rejectButton.onClick.AddListener(() => accepted?.Invoke(false));
    }

    private void OnDisable()
    {
        acceptButton.onClick.RemoveAllListeners();
        rejectButton.onClick.RemoveAllListeners();
    }
}
