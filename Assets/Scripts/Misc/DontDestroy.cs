using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.VisualScripting.Member;

public class DontDestroy : MonoBehaviour
{
    public AudioSource backgroundMusic;
    void Awake()
    {
       
        int MusicPlayingCount = 1;
        if (MusicPlayingCount > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}
