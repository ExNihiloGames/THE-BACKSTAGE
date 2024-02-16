using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogManager : MonoBehaviour
{
    public static event Action<Speaker, string> DisplayDialogInstruction;
    public static event Action ClearDialogBoxInstruction;

    private static DialogManager _instance;
    public static DialogManager Instance { get { return _instance; } }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    private void Start()
    {
        GameManager.OnGuestShowUP += OnNPCDialogRequest;
    }

    void OnPlayerDialogRequest()
    {

    }

    void OnNPCDialogRequest(Character character)
    {

    }
}

public enum Speaker
{
    Player,
    NPC
}

public enum DialogStyle
{
    Greetings,
    Acquiesce,
    Thanks,
    Insist,
    Threats,
    Supplication,
    Apologies,
    NoIDCard,
    RefusalAlcoholTest,
    RefusalDrugTest,
    ProtestAlcoholTestResult,
    ProtestDrugTestResult
}
