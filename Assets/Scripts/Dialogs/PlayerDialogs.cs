using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "DIALOG_Player", menuName = "Dialogs/PlayerDialogs")]
public class PlayerDialogs : ScriptableObject
{
    public List<string> greetings = new List<string>();
    public List<string> acquiesce = new List<string>();
    public List<string> thanks = new List<string>();
    public List<string> insist = new List<string>();
    public List<string> threats = new List<string>();
    public List<string> apologies = new List<string>();
    public List<string> noIDCard = new List<string>();
    public List<string> refusalAlcoholTest = new List<string>();
    public List<string> refusalDrugTest = new List<string>();
    public List<string> protestAlcoholTestResult = new List<string>();
    public List<string> protestDrugTestResult = new List<string>();

    Dictionary<DialogStyle, List<string>> playerDialogs = new Dictionary<DialogStyle, List<string>>();

    public string GetRandomGreeting(DialogStyle dialogStyle)
    {
        playerDialogs[DialogStyle.Greetings] = greetings;
        playerDialogs[DialogStyle.Acquiesce] = acquiesce;
        playerDialogs[DialogStyle.Thanks] = thanks;
        playerDialogs[DialogStyle.Insist] = insist;
        playerDialogs[DialogStyle.Threats] = threats;
        playerDialogs[DialogStyle.Apologies] = apologies;
        playerDialogs[DialogStyle.NoIDCard] = noIDCard;
        playerDialogs[DialogStyle.RefusalAlcoholTest] = refusalAlcoholTest;
        playerDialogs[DialogStyle.RefusalDrugTest] = refusalDrugTest;
        playerDialogs[DialogStyle.ProtestAlcoholTestResult] = protestAlcoholTestResult;
        playerDialogs[DialogStyle.ProtestDrugTestResult] = protestDrugTestResult;

        return playerDialogs[dialogStyle][Random.Range(0, playerDialogs[dialogStyle].Count)];
    }
}
