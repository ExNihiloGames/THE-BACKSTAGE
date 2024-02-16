using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "DIALOG_HighNPC", menuName = "Dialogs/NPC_Dialgos/HighNPC")]
public class HighNPCDialogs : ScriptableObject
{
    public List<string> greetings = new List<string>();
    public List<string> acquiesce = new List<string>();
    public List<string> thanks = new List<string>();
    public List<string> insist = new List<string>();
    public List<string> threats = new List<string>();
    public List<string> supplications = new List<string>();
    public List<string> apologies = new List<string>();
    public List<string> noIDCard = new List<string>();
    public List<string> refusalAlcoholTest = new List<string>();
    public List<string> refusalDrugTest = new List<string>();
    public List<string> protestAlcoholTestResult = new List<string>();
    public List<string> protestDrugTestResult = new List<string>();

    Dictionary<DialogStyle, List<string>> highNPC = new Dictionary<DialogStyle, List<string>>();

    public string GetRandomDialog(DialogStyle dialogStyle)
    {
        GenerateDict();
        return highNPC[dialogStyle][Random.Range(0, highNPC[dialogStyle].Count)];
    }

    public string GetSpecificDialog(DialogStyle dialogStyle, int dialogIndex)
    {
        GenerateDict();
        return highNPC[dialogStyle][dialogIndex];
    }

    void GenerateDict()
    {
        highNPC[DialogStyle.Greetings] = greetings;
        highNPC[DialogStyle.Acquiesce] = acquiesce;
        highNPC[DialogStyle.Thanks] = thanks;
        highNPC[DialogStyle.Insist] = insist;
        highNPC[DialogStyle.Threats] = threats;
        highNPC[DialogStyle.Supplication] = supplications;
        highNPC[DialogStyle.Apologies] = apologies;
        highNPC[DialogStyle.NoIDCard] = noIDCard;
        highNPC[DialogStyle.RefusalAlcoholTest] = refusalAlcoholTest;
        highNPC[DialogStyle.RefusalDrugTest] = refusalDrugTest;
        highNPC[DialogStyle.ProtestAlcoholTestResult] = protestAlcoholTestResult;
        highNPC[DialogStyle.ProtestDrugTestResult] = protestDrugTestResult;
    }
}
