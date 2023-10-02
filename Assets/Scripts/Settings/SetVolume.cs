using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SetVolume : MonoBehaviour  
{
    public AudioMixer audioMixer;
    public Image img;
    public Sprite enabledSprite;
    public Sprite disabledSprite;
    private bool isEnabled = true;

    private void Update()
    {
        if (!isEnabled)
        {
            img.sprite = disabledSprite;
            audioMixer.SetFloat("volume", -80);
        }
        else
        {
            img.sprite = enabledSprite;
            audioMixer.SetFloat("volume", -5);
        }
    }

    public void toggleVolume()
    {
        isEnabled = !isEnabled;
    }
}
