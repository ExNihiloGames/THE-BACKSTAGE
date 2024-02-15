using System;

public class Character
{
    static float hasIDProbability = 0.85f; 

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

    public DateTime dateOfBirth => m_dateOfBirth; //Generer une date en fonction de si l'id est valide ou non, un range (15-17) si invalide, un range (18-
    private DateTime m_dateOfBirth;

    public bool hasID => m_hasID;
    private bool m_hasID;
    public bool isIDValid => false; // 28eme siecle, majorite a 18 ans
    public bool isDrunk => m_characterEffect.HasFlag(CharacterEffect.Drunk);
    public bool isHigh => m_characterEffect.HasFlag(CharacterEffect.High);


    public Character(CharacterSpecie characterSpecie, CharacterTrait characterTrait)
    {
        m_firstName = characterSpecie.GetRandomCharacterFirstName();
        m_lastName = characterSpecie.GetRandomCharacterLastName();
        m_dateOfBirth = new DateTime(2800, UnityEngine.Random.Range(1,13), UnityEngine.Random.Range(1, 30));
        m_hasID = UnityEngine.Random.Range(0f, 1f) < 0.85f; 
        m_characterSpecie = characterSpecie;
        m_characterTrait = characterTrait;
        m_characterEffect = characterTrait.GetRandomEffect();
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
