using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class MusicSlider : MonoBehaviour
{
    public void OnSliderValueChanged()
    {
        Slider slider=GetComponent<Slider>();
        AudioManager.Instance.MusicSldOnClick(null,slider);
    }
}
