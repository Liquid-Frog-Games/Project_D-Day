using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.VisualScripting.Member;

public class DontDestroy : MonoBehaviour
{
    void Awake()
    {
        int MusicPlayingCount = FindObjectsOfType<AudioSource>().Length;
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
