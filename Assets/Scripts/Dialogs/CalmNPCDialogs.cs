using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "DIALOG_CalmNPC", menuName = "Dialogs/NPC_Dialgos/CalmNPC")]
public class CalmNPCDialogs : ScriptableObject
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

    Dictionary<DialogStyle, List<string>> calmNPC = new Dictionary<DialogStyle, List<string>>();

    public string GetRandomGreeting(DialogStyle dialogStyle)
    {
        calmNPC[DialogStyle.Greetings] = greetings;
        calmNPC[DialogStyle.Acquiesce] = acquiesce;
        calmNPC[DialogStyle.Thanks] = thanks;
        calmNPC[DialogStyle.Insist] = insist;
        calmNPC[DialogStyle.Threats] = threats;
        calmNPC[DialogStyle.Supplication] = supplications;
        calmNPC[DialogStyle.Apologies] = apologies;
        calmNPC[DialogStyle.NoIDCard] = noIDCard;
        calmNPC[DialogStyle.RefusalAlcoholTest] = refusalAlcoholTest;
        calmNPC[DialogStyle.RefusalDrugTest] = refusalDrugTest;
        calmNPC[DialogStyle.ProtestAlcoholTestResult] = protestAlcoholTestResult;
        calmNPC[DialogStyle.ProtestDrugTestResult] = protestDrugTestResult;

        return calmNPC[dialogStyle][Random.Range(0, calmNPC[dialogStyle].Count)];
    }
}
