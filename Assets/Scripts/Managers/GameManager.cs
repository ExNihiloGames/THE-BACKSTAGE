using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    [Header("Game Settings")]
    [SerializeField] int guestsNumber;
    [SerializeField] AbstractCharacterGenerator characterGenerator;

    private static GameManager _instance;
    public static GameManager Instance { get { return _instance; } }

    List<Character> guestInQueue;
    List<Character> guestInBar;

    public static event Action<Character> OnGuestShowUP;


    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance= this;
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        guestInQueue = new List<Character>();

        for (int i=0; i<guestsNumber; i++)
        {
            Character character = characterGenerator.Generate();
            guestInQueue.Add(character);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    Character FirstGuestInLine() { return guestInQueue[0]; }

    void AcceptGuest()
    {

    }

    void RefuseGuest()
    {

    }

    void AddGuestToBar(Character character)
    {

    }

    void CalculateAmbianceLevel()
    {
        // 
    }

}
