using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestingItem : MonoBehaviour
{
    public TestEquipmentType testEquipmentType;
    public bool isDrunk;
    public bool isHigh;
}

public enum TestEquipmentType
{
    Alcohol,
    Drugs,
}
