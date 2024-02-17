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
    [Space]
    [Header("NPC Dialog settings")]
    [SerializeField][Range(0f, 1f)] float richSaysHighProba;
    [SerializeField][Range(0f, 1f)] float angrySaysHighProba;
    [SerializeField][Range(0f, 1f)] float othersSaysHighProba;
    [SerializeField][Range(0f, 1f)] float guestProtestRejectionProba;


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
        dialogGenerator = new DialogGenerator(playerDialogs, calmNPCDialogs, politeNPCDialogs, angryNPCDialogs, drunkNPCDialogs, highNPCDialogs);
        GameManager.OnGuestShowUP += SetCurrentCharacter;
        GameManager.OnGuestAccepted += AcceptGuest;
        GameManager.OnGuestRejected += RefuseGuest;
    }

    void SetCurrentCharacter(Character character)
    {
        currentCharacter = character;
        string trait = currentCharacter.characterTrait.displayName;
        CharacterEffect effect = character.characterEffect;
        float randFloat = UnityEngine.Random.Range(0f, 1f);
        bool saysHi;

        saysHi = randFloat < othersSaysHighProba;
        if (trait == "isRich") { saysHi = randFloat <= richSaysHighProba; }
        if (effect.HasFlag(CharacterEffect.Angry)){ saysHi = randFloat <= angrySaysHighProba; }
        if (trait == "is Rich" && effect.HasFlag(CharacterEffect.Angry)) { saysHi = randFloat <= richSaysHighProba * angrySaysHighProba; }

        if (saysHi) { RequestNPCDialog(DialogStyle.Greetings); }
    }

    void AcceptGuest()
    {
        RequestPlayerDialog(DialogStyle.Accept);
        RequestNPCDialog(DialogStyle.Thanks);
    }

    void RefuseGuest()
    {
        RequestPlayerDialog(DialogStyle.Refuse);
        if (UnityEngine.Random.Range(0f, 1f) <= guestProtestRejectionProba)
        {
            RequestNPCDialog(DialogStyle.Threats);
        }
    }

    public void RequestPlayerDialog(DialogStyle dialogStyle)
    {
        string dialog = dialogGenerator.GetPlayerRandomDialog(dialogStyle);
        OnDialogBoxDisplayRequest?.Invoke(Speaker.Player, dialog);
    }

    public void RequestNPCDialog(DialogStyle dialogStyle)
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
    Accept,
    Refuse,
    Thanks,
    Insist,
    Threats,
    Supplication,
    Apologies,
    NoIDCard,
    PerformTest,
    RefusalAlcoholTest,
    RefusalDrugTest,
    ProtestAlcoholTestResult,
    ProtestDrugTestResult
}
