using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayMusicState : MonoBehaviour {

    [SerializeField]
    private Music m_Music;
    [SerializeField]
    private bool m_Force;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        MusicManager.instance.PlayMusic(m_Music, m_Force);   
    }
}
