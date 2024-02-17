using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class GameManager : MonoBehaviour
{
    [Header("Guests Generation")]
    [SerializeField] int guestsNumber;
    [SerializeField] AbstractCharacterGenerator characterGenerator;
    [Space]
    [Header("Bar Settings")]
    [SerializeField] int maxGuests;
    [SerializeField] int ambianceJaugeRange;

    public static event Action<Character> OnGuestShowUP;
    public static event Action OnGuestAccepted;
    public static event Action OnGuestRejected;

    private static GameManager _instance;
    public static GameManager Instance { get { return _instance; } }

    List<Character> guestInQueue;
    List<Character> guestInBar;
    Character currentGuest;

    public float ambiance => m_ambiance;
    private float m_ambiance;

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
        AcceptRejectDebugPanel.accepted += OnChoice;
        NExtGuestClickZone.Clicked += CallNextGuest;

        guestInQueue = new List<Character>();
        guestInBar = new List<Character>();

        for (int i=0; i<guestsNumber; i++)
        {
            Character character = characterGenerator.Generate();
            guestInQueue.Add(character);
        }
    }

    public void CallNextGuest()
    {
        if (currentGuest == null)
        {
            OnGuestShowUP?.Invoke(guestInQueue[0]);
            currentGuest = guestInQueue[0];
            Debug.Log("New Guest at the bar: " + currentGuest.characterSpecie.displayName);
            Debug.Log("Ambiance influence: " + currentGuest.ambianceScore);
        }        
    }

    void OnChoice(bool accept)
    {
        if (currentGuest != null)
        {
            if (accept)
            {
                AddGuestToBar(currentGuest);
                OnGuestAccepted?.Invoke();
                Debug.Log("GUEST ACCEPTED");
            }
            else
            {
                Debug.Log("GUEST REJECTED");
                OnGuestRejected?.Invoke();
            }
            guestInQueue.RemoveAt(0);
            currentGuest = null;
            AddGuestToQueue();
        }
    }

    void AddGuestToBar(Character character)
    {
        guestInBar.Add(character);
        UpdateAmbianceLevel(character.ambianceScore);
        Debug.Log(guestInBar.Count + "Guests in Bar");
    }

    void AddGuestToQueue()
    {
        Character character = characterGenerator.Generate();
        guestInQueue.Add(character);

        // Animate new Sprite
    }

    void UpdateAmbianceLevel(float ambianceScore)
    {
        m_ambiance += ambianceScore;
        m_ambiance = m_ambiance > ambianceJaugeRange / 2 ? ambianceJaugeRange / 2 : m_ambiance;
        m_ambiance = m_ambiance < -ambianceJaugeRange / 2 ? -ambianceJaugeRange / 2 : m_ambiance;
        Debug.Log("Ambiance in bar: " + m_ambiance);
    }

    public void DebugCharacterDisplayState(bool characterDisplayState)
    {
        string isReady = characterDisplayState ? "READY" : "NOT READY";
        Debug.Log("Character Display is " + isReady);
    }
}
