using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

public class Character
{
    public string name => m_name;
    private string m_name;
    public CharacterSpecie characterSpecie => m_characterSpecie;
    private CharacterSpecie m_characterSpecie;

    public CharacterTrait characterTrait => m_characterTrait;
    private CharacterTrait m_characterTrait;

    public CharacterEffect characterEffect => m_characterEffect;
    private CharacterEffect m_characterEffect;

    public Character(CharacterSpecie characterSpecie, CharacterTrait characterTrait)
    {
        m_characterSpecie = characterSpecie;
        m_characterTrait = characterTrait;
        m_characterEffect = 0;
    }

    public Character(CharacterSpecie characterSpecie, CharacterTrait characterTrait, CharacterEffect characterEffect)
    {
        m_name = characterSpecie.GetRandomCharacterFullName();
        m_characterSpecie = characterSpecie;
        m_characterTrait = characterTrait;
        m_characterEffect = characterEffect;
    }

    public bool IsDrunk()
    {
        return m_characterEffect.HasFlag(CharacterEffect.Drunk);
    }

    public bool IsHigh()
    {
        return m_characterEffect.HasFlag(CharacterEffect.High);
    }
}
