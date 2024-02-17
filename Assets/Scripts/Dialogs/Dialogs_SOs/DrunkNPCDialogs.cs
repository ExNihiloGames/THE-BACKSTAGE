using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "DIALOG_DrunkNPC", menuName = "Dialogs/NPC_Dialgos/DrunkNPC")]
public class DrunkNPCDialogs : ScriptableObject
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

    Dictionary<DialogStyle, List<string>> drunkNPC = new Dictionary<DialogStyle, List<string>>();

    public string GetRandomDialog(DialogStyle dialogStyle)
    {
        GenerateDict();
        return drunkNPC[dialogStyle][Random.Range(0, drunkNPC[dialogStyle].Count)];
    }

    public string GetSpecificDialog(DialogStyle dialogStyle, int dialogIndex)
    {
        GenerateDict();
        return drunkNPC[dialogStyle][dialogIndex];
    }

    void GenerateDict()
    {
        drunkNPC[DialogStyle.Greetings] = greetings;
        drunkNPC[DialogStyle.Acquiesce] = acquiesce;
        drunkNPC[DialogStyle.Thanks] = thanks;
        drunkNPC[DialogStyle.Insist] = insist;
        drunkNPC[DialogStyle.Threats] = threats;
        drunkNPC[DialogStyle.Supplication] = supplications;
        drunkNPC[DialogStyle.NoIDCard] = noIDCard;
        drunkNPC[DialogStyle.RefusalAlcoholTest] = refusalAlcoholTest;
        drunkNPC[DialogStyle.RefusalDrugTest] = refusalDrugTest;
        drunkNPC[DialogStyle.ProtestAlcoholTestResult] = protestAlcoholTestResult;
        drunkNPC[DialogStyle.ProtestDrugTestResult] = protestDrugTestResult;
    }
}
