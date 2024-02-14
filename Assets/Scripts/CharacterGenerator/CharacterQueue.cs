using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.TextCore.Text;
using static UnityEngine.GraphicsBuffer;

public class CharacterQueue : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] AbstractCharacterGenerator characterGenerator;
    Queue<Character> queue = new Queue<Character>();
#if UNITY_EDITOR
    [Space]
    [Header("Debug")]
    [SerializeField] int debugQueueCount;
    [SerializeField] int debugValidated;
    [SerializeField] int debugRefused;
#endif

    public void GenerateQueue(int count = 500)
    {
        if (characterGenerator == null) return;

        queue.Clear();

        for (int i = 0; i < count; i++)
        {
            Character character = characterGenerator.Generate();
            queue.Enqueue(character);
#if UNITY_EDITOR
            Debug.Log(character.characterEffect.ToString());
#endif
        }
#if UNITY_EDITOR
        debugQueueCount = count;
        debugValidated = 0;
        debugRefused = 0;
#endif
    }

    public void Validate()
    {
        queue.Dequeue();
#if UNITY_EDITOR
        debugQueueCount = queue.Count;
        debugValidated++;
#endif
    }

    public void Refuse()
    {
        queue.Dequeue();
#if UNITY_EDITOR
        debugQueueCount = queue.Count;
        debugRefused++;
#endif
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(CharacterQueue))]
public class MyScriptEditor : Editor
{
    public override void OnInspectorGUI()
    {
        var queue = target as CharacterQueue;
        DrawDefaultInspector();
        if (GUILayout.Button("Generate"))
        {
            queue.GenerateQueue();
        }
        if (GUILayout.Button("Validate"))
        {
            queue.Validate();
        }
        if (GUILayout.Button("Refuse"))
        {
            queue.Refuse();
        }
    }
}
#endif