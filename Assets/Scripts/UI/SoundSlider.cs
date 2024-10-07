using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SoundSlider : MonoBehaviour
{
    public void OnSliderValueChanged()
    {
        Slider slider=GetComponent<Slider>();
        AudioManager.Instance.SoundSldOnClick(null,slider);
    }
}
