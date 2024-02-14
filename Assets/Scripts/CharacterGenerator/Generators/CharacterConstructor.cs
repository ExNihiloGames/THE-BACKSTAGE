using UnityEngine;

[CreateAssetMenu(fileName = "CharCon_NEW", menuName = "CharacterConstructor", order = 1)]
public class CharacterConstructor : AbstractCharacterGenerator
{
    public CharacterSpecie characterSpecie;
    public CharacterTrait characterTrait;
    [Space]
    public bool forceFullName = false;
    public string firstName;
    public string lastName;
    [Space]
    public bool forceID = false;
    public bool hasID;
    public bool isIDValid;
    [Space]
    public bool forceEffects;
    public CharacterEffect characterEffect;

    public override Character Generate()
    {
        Character character = new Character(characterSpecie, characterTrait);

        if (forceFullName)
        {
            character.SetName(firstName, lastName);
        }

        if (forceEffects)
        {
            character.SetEffects(characterEffect);
        }

        return character;
    }
}
