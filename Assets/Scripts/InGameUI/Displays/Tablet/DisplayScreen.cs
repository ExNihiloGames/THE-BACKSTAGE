using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;
using JetBrains.Annotations;
using static CardReader;

[System.Serializable]
public class CardResultDisplay
{
    public TMP_Text firstNameTxt;
    public TMP_Text lastNameTxt;
    public TMP_Text speciesTxt;
    public TMP_Text birthDateTxt;
    public TMP_Text isValidText;
}

[System.Serializable]
public class TestResultDisplay
{
    public TMP_Text TestTypeText;
    public TMP_Text testResultText;
}


public class DisplayScreen : MonoBehaviour
{
    public CardResultDisplay cardResultDisplay;
    public TestResultDisplay testResultDisplay;

    private DialogManager dialogManager;

    private void Start()
    {
        ItemSlot itemSlot = FindObjectOfType<ItemSlot>();
        CardReader cardReader = FindObjectOfType<CardReader>();
        GameManager.OnGuestAccepted += ClearGuestInfoScreen;
        GameManager.OnGuestRejected += ClearGuestInfoScreen;
        itemSlot.OnTestEquipmentRead += DisplayTestResult;
        cardReader.OnCardRead += DisplayCard;
        dialogManager = DialogManager.Instance;
    }

    private void DisplayCard(object sender, OnCardReadEventArgs card)
    {
        cardResultDisplay.firstNameTxt.text = card.cardContent.firstName;
        cardResultDisplay.lastNameTxt.text = card.cardContent.lastName;
        cardResultDisplay.speciesTxt.text = card.cardContent.species;
        cardResultDisplay.birthDateTxt.text = card.cardContent.birthdate;
        cardResultDisplay.isValidText.text = card.cardContent.isValid ? "VALID" : "NOT VALID";
        cardResultDisplay.isValidText.color = card.cardContent.isValid ? Color.green : Color.red;
    }

    private void DisplayTestResult(TestEquipmentType testConducted, bool testResult)
    {
        testResultDisplay.TestTypeText.text = testConducted.ToString();
        testResultDisplay.testResultText.text = testResult? "POSITIVE": "NEGATIVE";
        testResultDisplay.testResultText.color = testResult? Color.red: Color.green;
        if (testResult)
        {
            if (testConducted == TestEquipmentType.Alcohol)
            {
                dialogManager.RequestPlayerDialog(DialogStyle.RefusalAlcoholTest);
                // dialogManager.RequestNPCDialog(DialogStyle.ProtestAlcoholTestResult);
                StartCoroutine(DelayRequest(0.5f, DialogStyle.ProtestAlcoholTestResult));
            }
            else
            {
                dialogManager.RequestPlayerDialog(DialogStyle.RefusalDrugTest);
                // dialogManager.RequestNPCDialog(DialogStyle.ProtestDrugTestResult);
                StartCoroutine(DelayRequest(0.5f, DialogStyle.ProtestDrugTestResult));
            }
        }
    }

        private void ClearGuestInfoScreen()
    {
        cardResultDisplay.firstNameTxt.text = "";
        cardResultDisplay.lastNameTxt.text = "";
        cardResultDisplay.speciesTxt.text = "";
        cardResultDisplay.birthDateTxt.text = "";
        cardResultDisplay.isValidText.text = "";
        cardResultDisplay.isValidText.color = Color.black;

        testResultDisplay.TestTypeText.text = "";
        testResultDisplay.testResultText.text = "";
        testResultDisplay.testResultText.color = Color.black;
    }

    IEnumerator DelayRequest(float s, DialogStyle dialogstyle)
    {
        yield return new WaitForSeconds(s);
        dialogManager.RequestNPCDialog(dialogstyle);
    }
}
