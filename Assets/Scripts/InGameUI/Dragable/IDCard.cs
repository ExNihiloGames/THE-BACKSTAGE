using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IDCard : MonoBehaviour
{
    [HideInInspector] public bool isValid;
    [HideInInspector] public string firstName;
    [HideInInspector] public string lastName;
    [HideInInspector] public string species;
    [HideInInspector] public string birthdate;
    [HideInInspector] public Sprite picture;
}
