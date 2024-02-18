using System.Collections.Generic;
using UnityEngine;
using System;


public class GameManager : MonoBehaviour
{
    [Header("Guests Generation")]
    [SerializeField] AbstractCharacterGenerator characterGenerator;
    [SerializeField] int guestsInQueue;
    [SerializeField] [Range(0f, 1f)] float guestIsVIPProba;
    [SerializeField] List<GameObject> gameObjects;
    [Space]
    [Header("Bar Settings")]
    [SerializeField] int maxGuests;
    [SerializeField] GameObject anarchyBar;
    [SerializeField] int minAnarchy;
    [SerializeField] int maxAnarchy;

    public static event Action<Character> OnGuestIsVIP;
    public static event Action<Character> OnGuestShowUP;
    public static event Action OnGuestAccepted;
    public static event Action OnGuestRejected;

    private static GameManager _instance;
    public static GameManager Instance { get { return _instance; } }

    // List<Character> guestInQueue;
    Queue<Character> guestQueue;
    List<Character> guestsInBarList;
    List<Character> vipGuestsList;
    Character currentGuest;

    public float anarchy => m_anarchy;
    private float m_anarchy;

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
        NextGuestClickZone.Clicked += CallNextGuest;

        //guestInQueue = new List<Character>();
        guestQueue= new Queue<Character>();
        guestsInBarList = new List<Character>();
        vipGuestsList= new List<Character>();

        for (int i=0; i<guestsInQueue; i++)
        {
            Character generatedCharacter = characterGenerator.Generate();
            AddGuestToVIPList(generatedCharacter);
            // guestInQueue.Add(character);
            guestQueue.Enqueue(generatedCharacter);
        }
    }

    public void CallNextGuest()
    {
        if (currentGuest == null)
        {
            if (guestQueue.TryDequeue(out currentGuest))
            {
                OnGuestShowUP?.Invoke(currentGuest);
            }
            // OnGuestShowUP?.Invoke(guestInQueue[0]);
            //currentGuest = guestInQueue[0];
            Debug.Log("New Guest at the bar: " + currentGuest.characterSpecie.displayName);
            Debug.Log("Guest is:" + "Angry:" + currentGuest.isAngry + "Drunk:" + currentGuest.isDrunk);
            Debug.Log("Anarchy influence: " + currentGuest.anarchyScore);
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
            //guestInQueue.RemoveAt(0);
            currentGuest = null;
            AddGuestToQueue();
        }
    }

    void AddGuestToBar(Character character)
    {
        guestsInBarList.Add(character);
        UpdateAnarchyLevel(character.anarchyScore);
        Debug.Log(guestsInBarList.Count + "Guests in Bar");
    }

    void AddGuestToQueue()
    {
        Character character = characterGenerator.Generate();
        guestQueue.Enqueue(character);
        // guestInQueue.Add(character);

        // Animate new Sprite
    }

    void AddGuestToVIPList(Character character)
    {
        if (vipGuestsList.Count < 3)
        {
            float randfloat = UnityEngine.Random.Range(0f, 1f);
            if(randfloat < guestIsVIPProba) { vipGuestsList.Add(character); }
        }
    }

    void UpdateAnarchyLevel(float anarchyScore)
    {
        m_anarchy += anarchyScore;
        m_anarchy = m_anarchy > maxAnarchy ? maxAnarchy : m_anarchy;
        m_anarchy = m_anarchy < minAnarchy ? minAnarchy : m_anarchy;
        Debug.Log("Anarchy in bar: " + m_anarchy);

        //Todo: temporaire
        anarchyBar.GetComponent<AnarchyBar>().SetAnarchyLevel(m_anarchy);
    }

    public void DebugCharacterDisplayState(bool characterDisplayState)
    {
        string isReady = characterDisplayState ? "READY" : "NOT READY";
        Debug.Log("Character Display is " + isReady);
    }
}
