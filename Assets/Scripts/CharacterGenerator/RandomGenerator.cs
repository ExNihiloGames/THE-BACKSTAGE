using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RandomCharacterGenerator", menuName = "CharacterGenerator/Random", order = 1)]
public class RandomGenerator : AbstractCharacterGenerator
{
    public override Character Generate()
    {
        return new Character(null, 0);
    }
}
