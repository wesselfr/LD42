using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    private AudioSource _BackgroundMusic;

    public TextMesh text;

    public AudioClip _BGM;
    public int _BPM;

    public float _BeatTime;
    public float _CurrentBeatPos;
    public float _CurrentTime;
    public float _TimeInBeat;

    public bool _CanPunchNormal;
    public bool _CanPunchHard;

    public bool _CanKickNormal;
    public bool _CanKickHard;
    public bool _CanKickHarder;

    private void CreateInstance()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Awake()
    {
        CreateInstance();
    }

    void Start ()
    {
        _BackgroundMusic = gameObject.AddComponent<AudioSource>();
        _BackgroundMusic.clip = _BGM;
        _BackgroundMusic.loop = true;
        _BackgroundMusic.Play();
        _BackgroundMusic.pitch = 1f;

        _BeatTime = (60.0f / _BPM);
    }

    // Update is called once per frame
    void Update()
    {
        // Update music timer
        _CurrentTime = _BackgroundMusic.time;
        _CurrentBeatPos = _CurrentTime / _BeatTime;
        _TimeInBeat = _CurrentTime % 1;

        //  Check if player can attack
        _CanPunchNormal = (_TimeInBeat < 0.5f);
        _CanPunchHard = (_TimeInBeat > 0.5);

        _CanKickNormal = (_TimeInBeat < 0.333f);
        _CanKickHard = (_TimeInBeat > 0.333f && _TimeInBeat < 0.666f);
        _CanKickHarder = (_TimeInBeat > 0.666f);

        text.text = (_CurrentTime).ToString();
    }

}
