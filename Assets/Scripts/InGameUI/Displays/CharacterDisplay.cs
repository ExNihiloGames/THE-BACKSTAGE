using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CharacterDisplay : MonoBehaviour, IPointerDownHandler, IDropHandler
{
    public Sprite defaultCharacterSprite;
    public GameObject idCardTemplate;

    GameManager gameManager;
    DialogManager dialogManager;
    Character currentCharacter;
    RectTransform rectTransform;
    Image characterImage;

    bool hasHandedId;
    bool hasBeenAlcoholTested;
    bool hasBeenDrugTested;
    bool idHandedBack;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.Instance;
        dialogManager = DialogManager.Instance;

        GameManager.OnGuestShowUP += LoadCharacter;
        GameManager.OnGuestAccepted += UnloadCharacter;
        GameManager.OnGuestRejected += UnloadCharacter;
        
        rectTransform = GetComponent<RectTransform>();
        characterImage = GetComponent<Image>();
        gameManager.DebugCharacterDisplayState(true);
    }

    void LoadCharacter(Character character)
    {
        currentCharacter = character;
        characterImage.sprite = character.characterSprite;
        characterImage.enabled = true;
        gameManager.DebugCharacterDisplayState(false);
    }

    void UnloadCharacter()
    {
        characterImage.enabled = false;
        characterImage.sprite = defaultCharacterSprite;
        currentCharacter = null;
        hasHandedId = false;
        hasBeenAlcoholTested = false;
        hasBeenDrugTested = false;
        gameManager.DebugCharacterDisplayState(true);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!hasHandedId)
        {
            dialogManager.RequestPlayerDialog(DialogStyle.AskIDCard);
        }            
        if (currentCharacter.hasID)
        {
            if (!hasHandedId)
            {
                dialogManager.RequestNPCDialog(DialogStyle.Acquiesce);
                GenerateID();
                hasHandedId = true;
            }
            else
            {
                Debug.Log("Dude I Already gave it to you"); // Request DialogManager (???)
            }
        }
        else
        {
            dialogManager.RequestNPCDialog(DialogStyle.NoIDCard);
            StartCoroutine(DelayRequest(0.5f, DialogStyle.NoIDCard));
            //dialogManager.RequestPlayerDialog(DialogStyle.NoIDCard);
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            if (eventData.pointerDrag.GetComponent<TestingItem>() != null)
            {
                if (!hasBeenAlcoholTested || !hasBeenDrugTested)
                {
                    TestingItem testEquipment = eventData.pointerDrag.GetComponent<TestingItem>();
                    switch (testEquipment.testEquipmentType)
                    {
                        case TestEquipmentType.Alcohol:
                            if (!hasBeenAlcoholTested)
                            {
                                dialogManager.RequestNPCDialog(DialogStyle.Acquiesce);
                                StartCoroutine(ConductTest(eventData.pointerDrag, 1.5f));
                                testEquipment.isDrunk = currentCharacter.isDrunk;
                                hasBeenAlcoholTested = true;
                            }
                            else
                            {
                                dialogManager.RequestNPCDialog(DialogStyle.RefusalAlcoholTest);
                            }
                            break;

                        case TestEquipmentType.Drugs:
                            if (!hasBeenDrugTested)
                            {
                                dialogManager.RequestNPCDialog(DialogStyle.Acquiesce);
                                StartCoroutine(ConductTest(eventData.pointerDrag, 1.5f));
                                testEquipment.isHigh = currentCharacter.isHigh;
                                hasBeenDrugTested = true;
                            }
                            else
                            {
                                dialogManager.RequestNPCDialog(DialogStyle.RefusalDrugTest);
                            }
                            break;
                    }
                }
                else
                {
                    Debug.Log("Hey Listen I did all your dumb tests already. Just let me in");
                }
            }
            else if (eventData.pointerDrag.GetComponent<IDCard>() != null)
            {
                //dialogManager.RequestNPCDialog(DialogStyle.Thanks);
                idHandedBack = true;
                Destroy(eventData.pointerDrag);
            }
        }
    }

    private void GenerateID()
    {
        GameObject idCard = Instantiate(idCardTemplate, rectTransform.anchoredPosition, Quaternion.identity);
        RectTransform cardRectTransform = idCard.GetComponent<RectTransform>();

        cardRectTransform.SetParent(rectTransform.parent);
        cardRectTransform.anchoredPosition = rectTransform.anchoredPosition;

        IDCard idCardContent = idCard.GetComponent<IDCard>();

        idCardContent.isValid = currentCharacter.isIDValid;
        idCardContent.firstName = currentCharacter.firstName;
        idCardContent.lastName = currentCharacter.lastName;
        idCardContent.species = currentCharacter.characterSpecie.displayName;
        idCardContent.birthdate = currentCharacter.dateOfBirth.ToShortDateString();
        idCardContent.picture = currentCharacter.characterSprite;
    }

    private void CheckHasID()
    {
        if (!idHandedBack)
        {
            Debug.Log("Can I have my ID back?");
        }
        else
        {
            UnloadCharacter();
        }
    }

    IEnumerator ConductTest(GameObject testEquipment, float testDuration)
    {
        testEquipment.GetComponent<Image>().enabled = false;
        yield return new WaitForSeconds(testDuration);
        testEquipment.GetComponent<Image>().enabled = true;
    }

    IEnumerator DelayRequest(float s, DialogStyle dialogstyle)
    {
        yield return new WaitForSeconds(s);
        dialogManager.RequestNPCDialog(dialogstyle);
    }
}
