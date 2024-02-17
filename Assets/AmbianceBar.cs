using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmbianceBar : MonoBehaviour
{

    public Slider slider;

    public float ambiance_level;

    private void Update()
    {
        ambiance_level = slider.value;
    }

    public void SetMaxAmbiance()
    {
        slider.value = slider.maxValue;
    }

    public void SetMinAmbiance()
    {
        slider.value = slider.minValue;
    }

    public void AddAmbiance(float percentage)
    {
        slider.value += percentage;
    }

    public void SetAmbiance(float percentage)
    {
        slider.value = percentage;
    }

    public void SetAmbiance(int ambiance_level)
    {
        slider.value = ambiance_level;
    }
}
