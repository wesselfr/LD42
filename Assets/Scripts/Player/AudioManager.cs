using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Audio
{
    dig,
    drop,
    eat,
    Grow,
    Grower,
    Jump
}

public class AudioManager : MonoBehaviour
{

    public static AudioManager Instance;
    [SerializeField] List<AudioClip> _Clips;
    AudioSource _AudioSourse;


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
        _AudioSourse = GetComponent<AudioSource>();
        CreateInstance();
    }

    public void Play(Audio audio, PlayerSize size)
    {
        _AudioSourse.pitch = 1;
        switch (size)
        {
            case PlayerSize.Small:
                switch (audio)
                {
                    case Audio.dig:
                        _AudioSourse.clip = _Clips[0];
                        _AudioSourse.pitch = (Random.Range(100, 200) * 0.01f);
                        _AudioSourse.Play();
                        break;
                    case Audio.drop:
                        _AudioSourse.clip = _Clips[1];
                        _AudioSourse.Play();
                        break;
                    case Audio.eat:
                        _AudioSourse.clip = _Clips[4];
                        _AudioSourse.pitch = (Random.Range(100, 200) * 0.01f);
                        _AudioSourse.Play();
                        break;
                    case Audio.Grow:
                        _AudioSourse.clip = _Clips[5];
                        _AudioSourse.Play();
                        break;
                    case Audio.Grower:
                        _AudioSourse.clip = _Clips[6];
                        _AudioSourse.Play();
                        break;
                        case Audio.Jump:
                        _AudioSourse.clip = _Clips[7];
                        _AudioSourse.Play();
                        break;
                }
                break;
            case PlayerSize.Meduim:
                switch (audio)
                {
                    case Audio.dig:
                        _AudioSourse.clip = _Clips[0];
                        _AudioSourse.pitch = (Random.Range(100, 200) * 0.01f);
                        _AudioSourse.Play();
                        break;
                    case Audio.drop:
                        _AudioSourse.clip = _Clips[2];
                        _AudioSourse.Play();
                        break;
                    case Audio.eat:
                        _AudioSourse.clip = _Clips[4];
                        _AudioSourse.pitch = (Random.Range(100, 200) * 0.01f);
                        _AudioSourse.Play();
                        break;
                    case Audio.Grow:
                        _AudioSourse.clip = _Clips[5];
                        _AudioSourse.Play();
                        break;
                    case Audio.Grower:
                        _AudioSourse.clip = _Clips[6];
                        _AudioSourse.Play();
                        break;
                    case Audio.Jump:
                        _AudioSourse.clip = _Clips[8];
                        _AudioSourse.Play();
                        break;
                }
                break;
            case PlayerSize.Big:
                switch (audio)
                {
                    case Audio.dig:
                        _AudioSourse.clip = _Clips[0];
                        _AudioSourse.pitch = (Random.Range(100, 200) * 0.01f);
                        _AudioSourse.Play();
                        break;
                    case Audio.drop:
                        _AudioSourse.clip = _Clips[3];
                        _AudioSourse.Play();
                        break;
                    case Audio.eat:
                        _AudioSourse.clip = _Clips[4];
                        _AudioSourse.pitch = (Random.Range(100, 200) * 0.01f);
                        _AudioSourse.Play();
                        break;
                    case Audio.Grow:
                        _AudioSourse.clip = _Clips[5];
                        _AudioSourse.Play();
                        break;
                    case Audio.Grower:
                        _AudioSourse.clip = _Clips[6];
                        _AudioSourse.Play();
                        break;
                    case Audio.Jump:
                        _AudioSourse.clip = _Clips[8];
                        _AudioSourse.Play();
                        break;
                }
                break;
        }
    }
}
