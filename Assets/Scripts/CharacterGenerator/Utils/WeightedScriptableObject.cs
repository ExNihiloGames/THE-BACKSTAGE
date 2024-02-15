using UnityEngine;

[System.Serializable]
public struct WeightedObject<T>
{
    public T value;
    public int weight;
}
