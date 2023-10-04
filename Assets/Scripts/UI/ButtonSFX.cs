using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSFX : MonoBehaviour
{

    public AudioSource buttonSound;

    public void PlayButtonSFX()
    {
        buttonSound.Play();
    }
}
