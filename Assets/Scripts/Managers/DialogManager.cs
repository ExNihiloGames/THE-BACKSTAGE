using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogManager : MonoBehaviour
{
    [Header("Player Dialogs")]
    [SerializeField] PlayerDialogs playerDialogs;
    [Space]
    [Header("NPC Dialogs")]
    [SerializeField] CalmNPCDialogs calmNPCDialogs;
    [SerializeField] PoliteNPCDialogs politeNPCDialogs;
    [SerializeField] AngryNPCDialogs angryNPCDialogs;
    [SerializeField] DrunkNPCDialogs drunkNPCDialogs;
    [SerializeField] HighNPCDialogs highNPCDialogs;

    public static event Action<Speaker, string> OnDialogBoxDisplayRequest;
    public static event Action OnDialogBoxClearRequest;

    private static DialogManager _instance;
    public static DialogManager Instance { get { return _instance; } }

    DialogGenerator dialogGenerator;
    private Character currentCharacter;

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
        GameManager.OnGuestShowUP += SetCurrentCharacter;
        dialogGenerator = new DialogGenerator(playerDialogs, calmNPCDialogs, politeNPCDialogs, angryNPCDialogs, drunkNPCDialogs, highNPCDialogs);
    }

    void SetCurrentCharacter(Character character)
    {
        currentCharacter = character;
    }

    void GeneratePlayerDialog(DialogStyle dialogStyle)
    {
        string dialog = dialogGenerator.GetPlayerRandomDialog(dialogStyle);
        OnDialogBoxDisplayRequest?.Invoke(Speaker.Player, dialog);
    }

    void GenerateNPCDialog(DialogStyle dialogStyle)
    {
        string dialog = dialogGenerator.GetNPCRandomDialog(currentCharacter.characterEffect, dialogStyle);
        OnDialogBoxDisplayRequest?.Invoke(Speaker.NPC, dialog);
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
