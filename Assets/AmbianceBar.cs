using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmbianceBar : MonoBehaviour
{

    public Slider slider;
    public Gradient gradient; //optionel
    public Image fill; //optionel

    private void Awake()
    {
        SetMinAmbianceLevel();
    }

    public void SetMaxAmbianceLevel()
    {
        slider.value = slider.maxValue;
    }

    public void SetMinAmbianceLevel()
    {
        slider.value = slider.minValue;
    }

    public void SetMinAmbianceValue(float value)
    {
        slider.minValue = value;
    }

    public void SetMaxAmbianceValue(float value)
    {
        slider.maxValue = value;
    }

    public void AddAmbiance(float percentage)
    {
        slider.value += percentage;
    }

    public void SetAmbianceLevel(float percentage)
    {
        slider.value = percentage;

        FillAutoUpdate();
    }

    private void FillAutoUpdate() //optionel
    {
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }

}
