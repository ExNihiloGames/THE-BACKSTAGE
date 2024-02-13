using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PseudoRandomCharacterGenerator", menuName = "CharacterGenerator/PseudoRandom", order = 1)]
public class PseudoRandomGenerator : AbstractCharacterGenerator
{
    public List<CharacterConstructor> characterCreators = new List<CharacterConstructor>();

    public override Character Generate()
    {
        int characterIndex = Random.Range(0, characterCreators.Count);
        return characterCreators[characterIndex].Generate();
    }
}
