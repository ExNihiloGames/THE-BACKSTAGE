using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MegaGenerator : AbstractCharacterGenerator
{
    public PseudoRandomGenerator pseudoRandomGenerator;
    public PseudoRandomGenerator randomGenerator;

    public override Character Generate()
    {
        return pseudoRandomGenerator.Generate();
    }
}
