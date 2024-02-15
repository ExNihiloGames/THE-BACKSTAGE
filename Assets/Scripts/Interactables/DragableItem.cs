//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.EventSystems;
//using UnityEngine.TextCore.Text;


//public class IDCard : MonoBehaviour, IDragHandler
//{
//    Item type enum
//    public ItemType itemType;

//    Canvas space references
//    private Canvas canvas;
//    private RectTransform rectTransform;
//    private CanvasGroup canvasGroup;
//    private Vector2 initAnchorPosition;

//    Timer system to maitain item over character
//    private bool timerStarted;
//    private float timer = 2f;

//    Properties
//    public string firstName => m_firstName;
//    private string m_firstName;

//    public string lastName => m_lastName;
//    private string m_lastName;

//    public string species => m_species;
//    private string m_species;

//    public string birthDate => m_birthDate;
//    private string m_birthDate;

//    public bool isValid => m_isValid;
//    private bool m_isValid;

//    public bool isDrunk => m_isDrunk;
//    private bool m_isDrunk;

//    public bool isHigh => m_isHigh;
//    private bool m_isHigh;

//    private void Awake()
//    {
//        rectTransform = GetComponent<RectTransform>();
//        canvasGroup = GetComponent<CanvasGroup>();
//        canvas = FindObjectOfType<Canvas>();
//    }

//    private void Update()
//    {
//        if (timerStarted)
//        {
//            timer -= Time.deltaTime;
//        }
//    }

//    public void OnBeginDrag(PointerEventData eventData)
//    {
//        canvasGroup.alpha = .8f;
//        canvasGroup.blocksRaycasts = false;
//        initAnchorPosition = rectTransform.anchoredPosition;
//    }

//    public void OnEndDrag(PointerEventData eventData)
//    {
//        canvasGroup.alpha = 1f;
//        if (eventData.pointerEnter == null)
//        {
//            rectTransform.anchoredPosition = initAnchorPosition;
//        }
//        else
//        {
//            if (eventData.pointerEnter.GetComponent<CharacterHolder>() != null)
//            {
//                ReadCharacterSpecs(eventData.pointerEnter.gameObject.GetComponent<CharacterHolder>());
//            }
//        }
//        canvasGroup.blocksRaycasts = true;
//    }

//    public void OnDrag(PointerEventData eventData)
//    {
//        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
//        if (eventData.pointerEnter != null)
//        {
//            if (eventData.pointerEnter.GetComponent<TestCharacter>() != null)
//            {
//                if (!timerStarted) { timerStarted = true; }
//            }
//        }
//    }

//    private void ReadCharacterSpecs(CharacterHolder currentCharacter)
//    {
//        switch (itemType)
//        {
//            case ItemType.EthyloTest:
//                m_isDrunk = currentCharacter.character.isDrunk;
//                break;

//            case ItemType.DrugTest:
//                m_isHigh = currentCharacter.character.isHigh;
//                break;

//            case ItemType.IDCard:
//                m_firstName = currentCharacter.character.firstName;
//                m_lastName = currentCharacter.character.lastName;
//                m_species = currentCharacter.character.species;
//                m_birthDate = currentCharacter.character.birthDate;
//                m_isValid = currentCharacter.character.isValid;
//                break;
//        }
//    }
//}


//public enum ItemType
//{
//    EthyloTest,
//    DrugTest,
//    IDCard
//}
