using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "PRWCharGen_New", menuName = "CharacterGenerator/PseudoRandomWeighted", order = 2)]
public class PseudoRandomWeightedGenerator : AbstractCharacterGenerator
{
    [SerializeField] RandomTable<CharacterConstructor> characterCreators;

    public override Character Generate()
    {
        return characterCreators.GetRandomObject().Generate();
    }

    private void OnValidate()
    {
        characterCreators.ComputeWeights();
    }

    private void OnEnable()
    {
        characterCreators.ComputeWeights();
    }
}
