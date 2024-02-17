using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "DIALOG_AngryNPC", menuName = "Dialogs/NPC_Dialgos/AngryNPC")]
public class AngryNPCDialogs : ScriptableObject
{
    public List<string> greetings = new List<string>();
    public List<string> acquiesce = new List<string>();
    public List<string> thanks = new List<string>();
    public List<string> insist = new List<string>();
    public List<string> threats = new List<string>();
    public List<string> supplications = new List<string>();
    public List<string> noIDCard = new List<string>();
    public List<string> refusalAlcoholTest = new List<string>();
    public List<string> refusalDrugTest = new List<string>();
    public List<string> protestAlcoholTestResult = new List<string>();
    public List<string> protestDrugTestResult = new List<string>();

    Dictionary<DialogStyle, List<string>> angryNPC = new Dictionary<DialogStyle, List<string>>();

    public string GetRandomDialog(DialogStyle dialogStyle)
    {
        GenerateDict();
        return angryNPC[dialogStyle][Random.Range(0, angryNPC[dialogStyle].Count)];
    }

    public string GetSpecificDialog(DialogStyle dialogStyle, int dialogIndex)
    {
        GenerateDict();
        return angryNPC[dialogStyle][dialogIndex];
    }

    void GenerateDict()
    {
        angryNPC[DialogStyle.Greetings] = greetings;
        angryNPC[DialogStyle.Acquiesce] = acquiesce;
        angryNPC[DialogStyle.Thanks] = thanks;
        angryNPC[DialogStyle.Insist] = insist;
        angryNPC[DialogStyle.Threats] = threats;
        angryNPC[DialogStyle.Supplication] = supplications;
        angryNPC[DialogStyle.NoIDCard] = noIDCard;
        angryNPC[DialogStyle.RefusalAlcoholTest] = refusalAlcoholTest;
        angryNPC[DialogStyle.RefusalDrugTest] = refusalDrugTest;
        angryNPC[DialogStyle.ProtestAlcoholTestResult] = protestAlcoholTestResult;
        angryNPC[DialogStyle.ProtestDrugTestResult] = protestDrugTestResult;
    }
}
