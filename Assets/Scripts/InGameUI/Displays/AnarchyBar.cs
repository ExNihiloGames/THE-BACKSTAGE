using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnarchyBar : MonoBehaviour
{

    public Slider slider;
    public Gradient gradient; //optionel
    public Image fill; //optionel

    public void SetMaxAnarchyLevel()
    {
        slider.value = slider.maxValue;
    }

    public void SetMinAnarchyLevel()
    {
        slider.value = slider.minValue;
    }

    public void AddAnarchy(float percentage)
    {
        slider.value += percentage;
    }

    public void SetAnarchyLevel(float percentage)
    {
        slider.value = percentage;

        FillAutoUpdate();
    }

    private void FillAutoUpdate() //optionel
    {
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }

}
