using System;
using System.Collections.Generic;
using UnityEngine;

public class Character
{
    static DateTime today = new DateTime(2826, 02, 04);
    static int majorityAge = 18;
    static int maxAgeDifferenceAboveMajority = 25;
    static int minAgeDifferenceBelowMajority = 2;
    static int dayInAYear = 365;


    static float hasIDProbability = 0.85f;
    static float isIDValidProbability = 0.85f;

    public string firstName => m_firstName;
    private string m_firstName;

    public string lastName => m_lastName;
    private string m_lastName;

    public CharacterSpecie characterSpecie => m_characterSpecie;
    private CharacterSpecie m_characterSpecie;

    public CharacterTrait characterTrait => m_characterTrait;
    private CharacterTrait m_characterTrait;

    public CharacterEffect characterEffect => m_characterEffect;
    private CharacterEffect m_characterEffect;

    public DateTime dateOfBirth => m_dateOfBirth;
    private DateTime m_dateOfBirth;

    public bool hasID => m_hasID;
    private bool m_hasID;
    public bool isIDValid => m_isIDValid;
    private bool m_isIDValid;

    public int age => (today - m_dateOfBirth).Days / 365;

    public bool isDrunk => m_characterEffect.HasFlag(CharacterEffect.Drunk);
    public bool isHigh => m_characterEffect.HasFlag(CharacterEffect.High);

    public Character(CharacterSpecie characterSpecie, CharacterTrait characterTrait)
    {
        m_firstName = characterSpecie.GetRandomCharacterFirstName();
        m_lastName = characterSpecie.GetRandomCharacterLastName();

        m_characterSpecie = characterSpecie;
        m_characterTrait = characterTrait;
        m_characterEffect = characterTrait.GetRandomEffect();

        m_hasID = UnityEngine.Random.Range(0f, 1f) < hasIDProbability;
        m_isIDValid = UnityEngine.Random.Range(0f, 1f) < hasIDProbability * isIDValidProbability;
        // isIDValidAge et isIDValidLogo


        m_dateOfBirth = today - (m_isIDValid ? new TimeSpan((UnityEngine.Random.Range(majorityAge, majorityAge + maxAgeDifferenceAboveMajority + 1) * dayInAYear + UnityEngine.Random.Range(0, dayInAYear + 1)), 0, 0, 0) :
        //                                                   random year number between majorityAge and majorityAge + maxAgeDifferenceAboveMajority * dayInAYear - random day offset   
                                               new TimeSpan((UnityEngine.Random.Range(majorityAge - minAgeDifferenceBelowMajority, majorityAge + 1) * dayInAYear) - UnityEngine.Random.Range(1, dayInAYear + 1), 0, 0, 0));
        //                                                   random year number between majorityAge - minAgeDifferenceBelowMajority and majorityAge * dayInAYear + random day offset (added so it becomes less than majority                                                              
    }
    public void SetName(string firstName, string lastName)
    {
        m_firstName = firstName;
        m_lastName = lastName;
    }

    public void SetEffects(CharacterEffect effect)
    {
        m_characterEffect = effect;
    }
}
