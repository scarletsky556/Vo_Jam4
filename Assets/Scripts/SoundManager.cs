using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    public AudioMixer Master;

    public UnityEngine.UI.Slider MasterSlider;
    public UnityEngine.UI.Slider BGMSlider;

    public void MaterSliderChange()
    {
        Master.SetFloat("Master", MasterSlider.value);
    }

    public void BGMSliderChange()
    {
        Master.SetFloat("BGM", BGMSlider.value);
    }
}
