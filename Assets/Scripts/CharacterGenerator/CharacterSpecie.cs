using System.Collections.Generic;
using System.Text;
using UnityEngine;


[CreateAssetMenu(fileName = "CharSpec_NEW", menuName = "Character/CharacterSpecie", order = 1)]

public class CharacterSpecie : ScriptableObject
{
    public string displayName;
    public List<string> firstNameList;
    public List<string> lastNameList;
    public List<Sprite> spriteList;

    public string GetRandomCharacterFullName()
    {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append(GetRandomCharacterFirstName());
        stringBuilder.Append(" ");
        stringBuilder.Append(GetRandomCharacterLastName());
        return stringBuilder.ToString();
    }

    public string GetRandomCharacterFirstName()
    {
        if (firstNameList == null || firstNameList.Count == 0) { return " "; }
        int index = Random.Range(0, firstNameList.Count);
        return firstNameList[index];
    }

    public string GetRandomCharacterLastName()
    {
        if (lastNameList == null || lastNameList.Count == 0) { return " "; }
        int index = Random.Range(0, lastNameList.Count);
        return lastNameList[index];
    }

    public Sprite GetRandomSprite()
    {
        int index = Random.Range(0, spriteList.Count);
        return spriteList[index];
    }
}
