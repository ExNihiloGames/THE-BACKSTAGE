using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CharacterDebugPanel : MonoBehaviour
{
    [SerializeField] AbstractCharacterGenerator characterGenerator;
    [SerializeField] TextMeshProUGUI nameText;
    [SerializeField] TextMeshProUGUI specieText;
    [SerializeField] TextMeshProUGUI traitText;
    [SerializeField] TextMeshProUGUI effectText;

    public void Display(Character character)
    {
        nameText.text = character.name;
        specieText.text = character.characterSpecie.displayName;
        traitText.text = character.characterTrait.displayName;
        string effect = "";
        if (character.IsDrunk())
        {
            effect += " Drunk";
        }
        if (character.IsHigh())
        {
            effect += " High";
        }
        effectText.text = effect;
    }

    private void Start()
    {
        Display(characterGenerator.Generate());
    }
}
