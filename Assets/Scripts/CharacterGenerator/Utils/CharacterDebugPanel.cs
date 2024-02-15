using TMPro;
using UnityEngine;

public class CharacterDebugPanel : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI firstNameText;
    [SerializeField] TextMeshProUGUI lastNameText;
    [SerializeField] TextMeshProUGUI specieText;
    [SerializeField] TextMeshProUGUI traitText;
    [SerializeField] TextMeshProUGUI ageText;
    [SerializeField] TextMeshProUGUI dateOfBirthText;
    [SerializeField] TextMeshProUGUI hasIDText;
    [SerializeField] TextMeshProUGUI isIDValidText;
    [SerializeField] TextMeshProUGUI effectText;

    public void Display(Character character)
    {
        firstNameText.text = character.firstName;
        lastNameText.text = character.lastName;

        specieText.text = character.characterSpecie.displayName;
        traitText.text = character.characterTrait.displayName;

        ageText.text = character.age.ToString();
        dateOfBirthText.text = character.dateOfBirth.ToShortDateString();

        hasIDText.text = character.hasID ? "Has ID" : "No ID";
        isIDValidText.text = character.isIDValid ? "Valid" : "Invalid";

        string effect = "";
        if (character.isDrunk)
        {
            effect += " Drunk";
        }
        if (character.isHigh)
        {
            effect += " High";
        }
        effectText.text = effect;
    }

    private void OnEnable()
    {
        CharacterQueue.characterShowUp += OnCharacterShowUp;
    }

    private void OnDisable()
    {
        CharacterQueue.characterShowUp -= OnCharacterShowUp;
    }

    void OnCharacterShowUp(Character character)
    {
        Display(character);
    }
}
