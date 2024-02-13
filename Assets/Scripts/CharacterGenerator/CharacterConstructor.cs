using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CharCon_NEW", menuName = "CharacterConstructor", order = 1)]
public class CharacterConstructor : AbstractCharacterGenerator
{
    public bool forceFullName;
    public string fullName;

    public CharacterSpecie characterSpecie;
    public CharacterTrait characterTrait;

    public bool forceEffects;
    public CharacterEffect characterEffect;

    public override Character Generate()
    {
        if (forceEffects)
        {
            return new Character(characterSpecie, characterTrait, characterEffect);
        }
        else
        {
            return new Character(characterSpecie, characterTrait);
        }
    }
}
