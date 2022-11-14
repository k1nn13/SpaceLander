using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class setUI : MonoBehaviour
{
    public Slider slider;

    public void SetMaxValueInt(int maxValue)
    {
        slider.maxValue = maxValue;
        slider.value = maxValue;
    }

    public void SetMaxValueFloat(float maxValue)
    {
        slider.maxValue = maxValue;
        slider.value = maxValue;
    }

    public void SetValueInt(int currentValue)
    {
        slider.value = currentValue;
    }

    public void SetValueFloat(float currentValue)
    {
        slider.value = currentValue;
    }

}
