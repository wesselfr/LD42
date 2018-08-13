using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerContainer : MonoBehaviour
{
    public static PlayerContainer Instance;

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

    private enum Size
    {
        small,
        medium,
        large
    }

    [SerializeField] private GameObject[] _Players;
    [SerializeField] private GameObject _Camera;
    [SerializeField] private PlayerBehavior[] _PlayersScripts;
    [SerializeField] private int _Active;

    private Animator _Animator;
    private SpriteRenderer _Renderer;

    bool _IsTransfroming;

    Vector3 _TransformarionPos;
    bool _FlipX;

    AudioSource _AudioSource;

    private void Awake()
    {
        CreateInstance();
        _AudioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        _PlayersScripts = new PlayerBehavior[_Players.Length];
        for (int i = 0; i < 3; i++)
        {
            _PlayersScripts[i] = _Players[i].GetComponent<PlayerBehavior>();
            _Players[i].SetActive(false);
        }
        
        _Players[_Active].SetActive(true);
        _Animator = GetComponent<Animator>();
        _Renderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
#if UNITY_EDITOR
       

        if (Input.GetKeyDown(KeyCode.G) && _PlayersScripts[_Active]._Grounded && _IsTransfroming == false)
        {
            if (_Active < _Players.Length - 1)
                Grow();
        }
        if (Input.GetKeyDown(KeyCode.H) && _PlayersScripts[_Active]._Grounded && _IsTransfroming == false)
        {
            if (_Active > 0)
                ShrinkTest();
        }

        if (Input.GetKeyDown(KeyCode.K) && _PlayersScripts[_Active]._Grounded && _IsTransfroming == false)
        {
                SellGems();
        }
#endif

        if (ItemManager.Instance._Growth >= 100 && _PlayersScripts[_Active]._Grounded && _Active == (int)Size.small && _IsTransfroming == false )
        {
            if (_Active < _Players.Length - 1)
                Grow();
        }
        else if (ItemManager.Instance._Growth >= 300 && _PlayersScripts[_Active]._Grounded && _Active == (int)Size.medium && _IsTransfroming == false)
        {
            if (_Active < _Players.Length - 1)
                Grow();
        }

        if (!_IsTransfroming)
        {
            transform.position = _Players[_Active].transform.position;
            _Renderer.flipX = _PlayersScripts[_Active]._Renderer.flipX;
        }
        _Camera.transform.position = new Vector3(_Players[_Active].transform.position.x, _Players[_Active].transform.position.y, -10);
    }

    private void Grow()
    {
        if (_IsTransfroming == false && _PlayersScripts[_Active]._Grounded)
        {
            _IsTransfroming = true;
            _TransformarionPos = _Players[_Active].transform.position;
            transform.position = _TransformarionPos;
            _FlipX = _PlayersScripts[_Active]._Renderer.flipX;
            _Players[_Active].SetActive(false);

            if (_Active == (int)Size.small)
            {
                _Animator.Play("1");
                AudioManager.Instance.Play(Audio.Grow, PlayerSize.Small);
            }
            if (_Active == (int)Size.medium)
            {
                _Animator.Play("2");
                AudioManager.Instance.Play(Audio.Grower, PlayerSize.Small);
            }
        }
    }

    public void GrowEnd()
    {
        _Active++;
        _PlayersScripts[_Active].enabled = true;
        _Players[_Active].SetActive(true);
        _PlayersScripts[_Active].SwitchState(new Idle(_PlayersScripts[_Active]));
        _PlayersScripts[_Active]._Renderer.flipX = _FlipX;
        _PlayersScripts[_Active]._AxeRenderer.flipX = _FlipX;
        _Players[_Active].transform.position = _TransformarionPos;
        _IsTransfroming = false;
    }


    private void GrowTest()
    {
        if (_IsTransfroming == false && _PlayersScripts[_Active]._Grounded)
        {
            _TransformarionPos = _Players[_Active].transform.position;
            _FlipX = _PlayersScripts[_Active]._Renderer.flipX;
            _Players[_Active].SetActive(false);
            _Active++;
            _Players[_Active].SetActive(true);
            _PlayersScripts[_Active].SwitchState(new Idle(_PlayersScripts[_Active]));
            _PlayersScripts[_Active]._Renderer.flipX = _FlipX;
            _PlayersScripts[_Active]._AxeRenderer.flipX = _FlipX;
            _Players[_Active].transform.position = _TransformarionPos;
        }
    }

    private void ShrinkTest()
    {
        if (_IsTransfroming == false && _PlayersScripts[_Active]._Grounded)
        {
            _TransformarionPos = _Players[_Active].transform.position;
            _FlipX = _PlayersScripts[_Active]._Renderer.flipX;
            _Players[_Active].SetActive(false);
            _Active--;
            _Players[_Active].SetActive(true);
            _PlayersScripts[_Active].SwitchState(new Idle(_PlayersScripts[_Active]));
            _PlayersScripts[_Active]._Renderer.flipX = _FlipX;
            _PlayersScripts[_Active]._AxeRenderer.flipX = _FlipX;
            _Players[_Active].transform.position = _TransformarionPos;
        }
    }

    public void SellGems()
    {
        if (_IsTransfroming == false && _PlayersScripts[_Active]._Grounded && ItemManager.Instance._Money >= 1000)
        {
            ItemManager.Instance._Money -= 1000;
            _IsTransfroming = true;
            _TransformarionPos = _Players[_Active].transform.position;
            transform.position = _TransformarionPos;
            _FlipX = _PlayersScripts[_Active]._Renderer.flipX;
            _Animator.Play("R");
        }
    }
    public void EnterDoor()
    {
        AudioManager.Instance.Play(Audio.Jump, _PlayersScripts[_Active]._Size);
    }

    public void CloseDoor()
    {
        _TransformarionPos = _Players[_Active].transform.position;
        transform.position = _TransformarionPos;
        _FlipX = _PlayersScripts[_Active]._Renderer.flipX;
        _Players[_Active].SetActive(false);
    }

    public void ExitDoor()
    {
        ItemManager.Instance._Growth = 0;
        _Active = 0;
        _PlayersScripts[_Active].enabled = true;
        _Players[_Active].SetActive(true);
        _PlayersScripts[_Active].SwitchState(new Jump(_PlayersScripts[_Active], _PlayersScripts[_Active]._JumpForce));
        _PlayersScripts[_Active]._Renderer.flipX = _FlipX;
        _PlayersScripts[_Active]._AxeRenderer.flipX = _FlipX;
        _Players[_Active].transform.position = _TransformarionPos;
    }

    public void end()
    {
        _IsTransfroming = false;
    }

    public void WashSound()
    {
        _AudioSource.Play();
    }
}
