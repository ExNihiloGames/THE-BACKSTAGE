using System;
using System.Collections.Generic;
using UnityEngine;

public class Character
{
    static DateTime m_today = new DateTime(2826, 02, 04);
    public static DateTime today {get{return m_today;}}
    static int majorityAge = 18;
    static int maxAgeDifferenceAboveMajority = 25;
    static int minAgeDifferenceBelowMajority = 2;
    static int dayInAYear = 365;


    static float hasIDProbability = 0.85f;
    static float isIDValidProbability = 0.85f;
    static float isValidAgeProbability = 0.9f;

    static float isUnderAgeAnarchyMalus = 32f;
    static float iDInvalidAnarchyMalus = 18f;
    static float isDrunkAnarchyMalus = 25f;
    static float isHighAnarchyMalus = 25f;
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

    public Sprite characterSprite => m_characterSprite;
    private Sprite m_characterSprite;

    public DateTime dateOfBirth => m_dateOfBirth;
    private DateTime m_dateOfBirth;

    public bool hasID => m_hasID;
    private bool m_hasID;
    public bool isIDValid => m_isIDValid;
    private bool m_isIDValid;
    public bool isValidAge => m_isValidAge;
    private bool m_isValidAge;

    public int age => (m_today - m_dateOfBirth).Days / 365;

    public bool isDrunk => m_characterEffect.HasFlag(CharacterEffect.Drunk);
    public bool isHigh => m_characterEffect.HasFlag(CharacterEffect.High);
    public bool isAngry => m_characterEffect.HasFlag(CharacterEffect.Angry);
    public bool isPolite => m_characterEffect.HasFlag(CharacterEffect.Polite);

    public float anarchyScore => m_anarchyScore;
    private float m_anarchyScore;

    public Character(CharacterSpecie characterSpecie, CharacterTrait characterTrait)
    {
        m_firstName = characterSpecie.GetRandomCharacterFirstName();
        m_lastName = characterSpecie.GetRandomCharacterLastName();

        m_characterSpecie = characterSpecie;
        m_characterTrait = characterTrait;
        m_characterEffect = characterTrait.GetRandomEffect();
        m_characterSprite = characterSpecie.GetRandomSprite();

        m_hasID = UnityEngine.Random.Range(0f, 1f) < hasIDProbability;
        m_isIDValid = UnityEngine.Random.Range(0f, 1f) < isIDValidProbability;
        m_isValidAge = UnityEngine.Random.Range(0f, 1f) < isValidAgeProbability;

        m_dateOfBirth = m_today - (m_isValidAge ? new TimeSpan((UnityEngine.Random.Range(majorityAge, majorityAge + maxAgeDifferenceAboveMajority + 1) * dayInAYear + UnityEngine.Random.Range(0, dayInAYear + 1)), 0, 0, 0) :
        //                                                   random year number between majorityAge and majorityAge + maxAgeDifferenceAboveMajority * dayInAYear - random day offset   
                                               new TimeSpan((UnityEngine.Random.Range(majorityAge - minAgeDifferenceBelowMajority, majorityAge + 1) * dayInAYear) - UnityEngine.Random.Range(1, dayInAYear + 1), 0, 0, 0));
        //                                                   random year number between majorityAge - minAgeDifferenceBelowMajority and majorityAge * dayInAYear + random day offset (added so it becomes less than majority                                                              
        int isDrunkInt = isDrunk ? 1 : 0;
        int isHighInt = isHigh ? 1 : 0;
        int idInvalidOrNoIdInt = (hasID && isIDValid) ? 0 : 1;
        int isUnderAgeInt = m_isValidAge? 0 : 1;

        m_anarchyScore = isDrunkInt * isDrunkAnarchyMalus + isHighInt * isHighAnarchyMalus + idInvalidOrNoIdInt * iDInvalidAnarchyMalus + isUnderAgeInt * isUnderAgeAnarchyMalus;
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
