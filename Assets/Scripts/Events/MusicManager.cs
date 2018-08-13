using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Music
{
    Start,
    StartLoop,
    GameLoop1,
    GameLoop2,
    Ending,
    Credits
}

public class MusicManager : MonoBehaviour {

    public static MusicManager instance;

    [SerializeField]
    AudioClip m_Start, m_StartLoop, m_GameLoop, m_GameLoop2, m_Ending, m_Credits;

    [SerializeField]
    private AudioSource m_AudioSource;

    [SerializeField]
    private Music m_State;
    private Music m_LastState;

	// Use this for initialization
	void Start () {
        instance = this;
	}
	
	// Update is called once per frame
	void Update () {
        if (!m_AudioSource.isPlaying)
        {
            if(m_State == Music.Start && m_LastState == Music.Start)
            {
                m_State = Music.StartLoop;
            }
            if(m_State == Music.Ending && m_LastState == Music.Ending)
            {
                m_State = Music.Credits;
            }
            if(m_State == Music.GameLoop1 && m_LastState == Music.GameLoop1)
            {
                m_State = Music.GameLoop2;
                FadeAndStart();
            }
            if (m_State == Music.GameLoop2 && m_LastState == Music.GameLoop2)
            {
                m_State = Music.GameLoop1;
                FadeAndStart();
            }
            SwitchMusic();
        }

        if (Input.GetKeyDown(KeyCode.UpArrow)) { m_State++; }
        if (Input.GetKeyDown(KeyCode.DownArrow)) { m_State--; }
	}

    private void SwitchMusic()
    {
        switch (m_State)
        {
            case Music.Start:
                m_AudioSource.clip = m_Start;
                break;
            case Music.StartLoop:
                m_AudioSource.clip = m_StartLoop;
                break;
            case Music.GameLoop1:
                m_AudioSource.clip = m_GameLoop;
                break;
            case Music.GameLoop2:
                m_AudioSource.clip = m_GameLoop2;
                break;
            case Music.Ending:
                m_AudioSource.clip = m_Ending;
                break;
            case Music.Credits:
                m_AudioSource.clip = m_Credits;
                break;
        }
        m_AudioSource.Play();
    }

    IEnumerator FadeAndStart()
    {
        while (m_AudioSource.volume > 0)
        {
            m_AudioSource.volume -= 0.05f;
            yield return new WaitForSeconds(0.1f);
        }
        m_AudioSource.volume = 1;
        SwitchMusic();
    }

    public void PlayMusic(Music state, bool force)
    {
        m_LastState = state;
        m_State = state;
        if (force) { StartCoroutine(FadeAndStart()); }
    }


}
