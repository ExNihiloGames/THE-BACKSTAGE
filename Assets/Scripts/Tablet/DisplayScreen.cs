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

    private void Start()
    {
        ItemSlot itemSlot = FindObjectOfType<ItemSlot>();
        CardReader cardReader = FindObjectOfType<CardReader>();
        GameManager.OnGuestAccepted += ClearGuestInfoScreen;
        GameManager.OnGuestRejected += ClearGuestInfoScreen;
        itemSlot.OnTestEquipmentRead += DisplayTestResult;
        cardReader.OnCardRead += DisplayCard;
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
}
