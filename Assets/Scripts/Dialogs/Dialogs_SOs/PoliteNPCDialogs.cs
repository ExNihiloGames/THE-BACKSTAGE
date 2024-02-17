using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "DIALOG_PoliteNPC", menuName = "Dialogs/NPC_Dialgos/PoliteNPC")]
public class PoliteNPCDialogs : ScriptableObject
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

    Dictionary<DialogStyle, List<string>> politeNPC = new Dictionary<DialogStyle, List<string>>();

    public string GetRandomDialog(DialogStyle dialogStyle)
    {
        GenerateDict();
        return politeNPC[dialogStyle][Random.Range(0, politeNPC[dialogStyle].Count)];
    }

    public string GetSpecificDialog(DialogStyle dialogStyle, int dialogIndex)
    {
        GenerateDict();
        return politeNPC[dialogStyle][dialogIndex];
    }

    void GenerateDict()
    {
        politeNPC[DialogStyle.Greetings] = greetings;
        politeNPC[DialogStyle.Acquiesce] = acquiesce;
        politeNPC[DialogStyle.Thanks] = thanks;
        politeNPC[DialogStyle.Insist] = insist;
        politeNPC[DialogStyle.Threats] = threats;
        politeNPC[DialogStyle.Supplication] = supplications;
        politeNPC[DialogStyle.NoIDCard] = noIDCard;
        politeNPC[DialogStyle.RefusalAlcoholTest] = refusalAlcoholTest;
        politeNPC[DialogStyle.RefusalDrugTest] = refusalDrugTest;
        politeNPC[DialogStyle.ProtestAlcoholTestResult] = protestAlcoholTestResult;
        politeNPC[DialogStyle.ProtestDrugTestResult] = protestDrugTestResult;
    }
}
