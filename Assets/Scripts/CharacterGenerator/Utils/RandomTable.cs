using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RandomTable<T>
{
    [SerializeField] private List<WeightedObject<T>> weightedList = new List<WeightedObject<T>>();
    private List<int> weightSum = new List<int>();


    public T GetRandomObject()
    {
        if (weightSum.Count == 0) return default;
        int random = Random.Range(0, weightSum[weightSum.Count - 1]);
        for (int i = 0; i < weightSum.Count; i++)
        {
            if (random < weightSum[i])
            {
                return weightedList[i].value;
            }
        }
        return default;
    }

    public void ComputeWeights()
    {
        weightSum.Clear();
        int sum = 0;
        foreach (var item in weightedList)
        {
            sum += item.weight;
            weightSum.Add(sum);
        }
    }
}
