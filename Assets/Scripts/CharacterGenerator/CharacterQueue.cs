using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CharacterQueue : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] AbstractCharacterGenerator characterGenerator;
    Queue<Character> queue = new Queue<Character>();

//#if UNITY_EDITOR
//    [Space]
//    [Header("Debug")]
//    [SerializeField] int debugQueueCount;
//    [SerializeField] int debugValidated;
//    [SerializeField] int debugRefused;
//#endif

    public static Action<Character> characterShowUp;

    public void GenerateQueue(int count = 500)
    {
        if (characterGenerator == null) return;

        queue.Clear();

        for (int i = 0; i < count; i++)
        {
            Character character = characterGenerator.Generate();
            queue.Enqueue(character);
//#if UNITY_EDITOR
//            Debug.Log(character.characterEffect.ToString());
//#endif
        }
//#if UNITY_EDITOR
//        debugQueueCount = count;
//        debugValidated = 0;
//        debugRefused = 0;
//#endif
    }

    private void OnEnable()
    {
        AcceptRejectDebugPanel.accepted += OnAccept;
    }

    private void OnDisable()
    {
        AcceptRejectDebugPanel.accepted -= OnAccept;
    }

    private void Start()
    {
        GenerateQueue();
        if (queue.TryPeek(out Character character))
        {
            characterShowUp?.Invoke(character);
        }
    }

    void OnAccept(bool accept)
    {
        if (queue.Count == 0) return;

        queue.Dequeue();

//#if UNITY_EDITOR
//        debugQueueCount = queue.Count;
//        if (accept)
//        {
//            debugValidated++;
//        }
//        else
//        {
//            debugRefused++;
//        }
//#endif
        if (queue.TryPeek(out Character character))
        {
            characterShowUp?.Invoke(character);
        }
    }
}

//#if UNITY_EDITOR
//[CustomEditor(typeof(CharacterQueue))]
//public class MyScriptEditor : Editor
//{
//    public override void OnInspectorGUI()
//    {
//        var queue = target as CharacterQueue;
//        DrawDefaultInspector();
//        if (GUILayout.Button("Generate"))
//        {
//            queue.GenerateQueue();
//        }
//    }
//}
//#endif