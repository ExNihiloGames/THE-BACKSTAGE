using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "DIALOG_Player", menuName = "Dialogs/PlayerDialogs")]
public class PlayerDialogs : ScriptableObject
{
    public List<string> greetings = new List<string>();
    public List<string> acquiesce = new List<string>();
    public List<string> thanks = new List<string>();
    public List<string> insist = new List<string>();
    public List<string> accept = new List<string>();
    public List<string> refuse = new List<string>();
    public List<string> threats = new List<string>();
    public List<string> askIDCard = new List<string>();
    public List<string> noIDCard = new List<string>();
    public List<string> performTest = new List<string>();
    public List<string> refusalAlcoholTest = new List<string>();
    public List<string> refusalDrugTest = new List<string>();

    Dictionary<DialogStyle, List<string>> playerDialogs = new Dictionary<DialogStyle, List<string>>();

    public string GetRandomDialog(DialogStyle dialogStyle)
    {
        GenerateDict();
        return playerDialogs[dialogStyle][Random.Range(0, playerDialogs[dialogStyle].Count)];
    }

    public string GetSpecificDialog(DialogStyle dialogStyle, int dialogIndex)
    {
        GenerateDict();
        return playerDialogs[dialogStyle][dialogIndex]; 
    }

    void GenerateDict()
    {
        playerDialogs[DialogStyle.Greetings] = greetings;
        playerDialogs[DialogStyle.Acquiesce] = acquiesce;
        playerDialogs[DialogStyle.Thanks] = thanks;
        playerDialogs[DialogStyle.Insist] = insist;
        playerDialogs[DialogStyle.Accept] = accept;
        playerDialogs[DialogStyle.Refuse] = refuse;
        playerDialogs[DialogStyle.Threats] = threats;
        playerDialogs[DialogStyle.AskIDCard] = askIDCard;
        playerDialogs[DialogStyle.NoIDCard] = noIDCard;
        playerDialogs[DialogStyle.PerformTest] = performTest;
        playerDialogs[DialogStyle.RefusalAlcoholTest] = refusalAlcoholTest;
        playerDialogs[DialogStyle.RefusalDrugTest] = refusalDrugTest;
    }
}
