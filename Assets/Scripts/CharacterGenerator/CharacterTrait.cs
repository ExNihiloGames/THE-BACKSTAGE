using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CharTrait_NEW", menuName = "Character/CharacterTrait", order = 1)]
public class CharacterTrait : ScriptableObject
{
    public string displayName;
    public RandomTable<CharacterEffect> effectsTable;

    public CharacterEffect GetRandomEffect()
    {
        return effectsTable.GetRandomObject();
    }

    private void OnEnable()
    {
        Debug.Log($"ComputeWeights for {this.name}");
        effectsTable.ComputeWeights();
    }
}