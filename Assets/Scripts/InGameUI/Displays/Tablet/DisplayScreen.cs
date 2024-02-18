using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using static CardReader;


[System.Serializable]
public class CardsList
{
    public List<Sprite> validCards = new List<Sprite>();
    public List<Sprite> invalidCards = new List<Sprite>();
}


[System.Serializable]
public class CardResultDisplay
{
    [Header("Text Displays")]
    public TMP_Text firstnameTxtValue;
    public TMP_Text lastnameTxtValue;
    public TMP_Text speciesTxtValue;
    public TMP_Text birthDateTxtValue;
    [Space]
    [Header("Image Displays")]
    public Image cardImage;
    public Image characterImage;
    [Space]
    [Header("Default Sprites")]
    public Sprite defaultCardSprite;
    public Sprite defaultCharacterSprite;
}


[System.Serializable]
public class TestResultDisplay
{
    [Header("Alcohol test")]
    public Sprite p_AlcoholTestSprite;
    public Sprite n_AlcoholTestSprite;
    [Space]
    [Header("Drugs test")]
    public Sprite p_DrugsTestSprite;
    public Sprite n_DrugsTestSprite;
    [Space]
    [Header("Default Sprites")]
    public Sprite defaultTestResultSprite;
    [Space]
    [Header("Result display")]
    public TMP_Text TestTypeText;
    public Image testResultImage;
}


public class DisplayScreen : MonoBehaviour
{
    public CardsList cardsList;
    public CardResultDisplay cardResultDisplay;
    public TestResultDisplay testResultDisplay;

    public TMP_Text dateTextValue;
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
        dateTextValue.text = Character.today.ToShortDateString();
    }

    private void DisplayCard(object sender, OnCardReadEventArgs card)
    {
        if(card.cardContent.isValid)
        {
            cardResultDisplay.cardImage.sprite = cardsList.validCards[UnityEngine.Random.Range(0, cardsList.validCards.Count)];
        }
        else
        {
            cardResultDisplay.cardImage.sprite = cardsList.invalidCards[UnityEngine.Random.Range(0, cardsList.invalidCards.Count)];
        }

        cardResultDisplay.firstnameTxtValue.text = card.cardContent.firstName;
        cardResultDisplay.lastnameTxtValue.text = card.cardContent.lastName;
        cardResultDisplay.speciesTxtValue.text = card.cardContent.species;
        cardResultDisplay.birthDateTxtValue.text = card.cardContent.birthdate;
        cardResultDisplay.characterImage.sprite = card.cardContent.picture;

        cardResultDisplay.cardImage.enabled = true;
    }

    private void DisplayTestResult(TestEquipmentType testConducted, bool testResult)
    {
        testResultDisplay.TestTypeText.text = testConducted.ToString();
        switch (testConducted)
        {
            case TestEquipmentType.Alcohol:
                if (testResult)
                {
                    testResultDisplay.testResultImage.sprite = testResultDisplay.p_AlcoholTestSprite;
                    dialogManager.RequestPlayerDialog(DialogStyle.RefusalAlcoholTest);
                    StartCoroutine(DelayRequest(0.5f, DialogStyle.ProtestAlcoholTestResult));
                    break;
                }
                else
                {
                    testResultDisplay.testResultImage.sprite = testResultDisplay.n_AlcoholTestSprite;
                    dialogManager.RequestPlayerDialog(DialogStyle.RefusalDrugTest);
                    StartCoroutine(DelayRequest(0.5f, DialogStyle.ProtestDrugTestResult));
                    break;
                }

            case TestEquipmentType.Drugs:
                if (testResult)
                {
                    testResultDisplay.testResultImage.sprite = testResultDisplay.p_DrugsTestSprite;
                    break;
                }
                else
                {
                    testResultDisplay.testResultImage.sprite = testResultDisplay.n_DrugsTestSprite;
                    break;
                }

            default:
                Debug.Log("Invalid object presented");
                break;

                // NOTE Coroutines can be replaced with old call: dialogManager.RequestNPCDialog(DialogStyle.ProtestAlcoholTestResult);
        }
        testResultDisplay.testResultImage.enabled = true;
    }

    private void ClearGuestInfoScreen()
    {        
        cardResultDisplay.firstnameTxtValue.text = "";
        cardResultDisplay.lastnameTxtValue.text = "";
        cardResultDisplay.speciesTxtValue.text = "";
        cardResultDisplay.birthDateTxtValue.text = "";
        cardResultDisplay.cardImage.sprite = cardResultDisplay.defaultCardSprite;
        cardResultDisplay.cardImage.enabled = false;

        testResultDisplay.TestTypeText.text = "";
        testResultDisplay.testResultImage.sprite = testResultDisplay.defaultTestResultSprite;
        testResultDisplay.testResultImage.enabled = false;

    }

    IEnumerator DelayRequest(float s, DialogStyle dialogstyle)
    {
        yield return new WaitForSeconds(s);
        dialogManager.RequestNPCDialog(dialogstyle);
    }
}
