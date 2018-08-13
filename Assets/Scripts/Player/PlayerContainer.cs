using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerContainer : MonoBehaviour
{
    private enum Size
    {
        small,
        medium,
        large
    }

    [SerializeField] private GameObject[] _Players;
    [SerializeField] private PlayerBehavior[] _PlayersScripts;
    private int _Active;

    private Animator _Animator;
    private SpriteRenderer _Renderer;


    Vector3 _TransformarionPos;
    bool _FlipX;

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
        transform.position = _Players[_Active].transform.position;
        _Renderer.flipX = _PlayersScripts[_Active]._Renderer.flipX;
        if (Input.GetKeyDown(KeyCode.G) && _PlayersScripts[_Active]._Grounded)
        {
            if (_Active < _Players.Length - 1)
                Grow();
        }
        if (Input.GetKeyDown(KeyCode.H) && _PlayersScripts[_Active]._Grounded)
        {
            if(_Active > 0)
            ShrinkTest();
        }
    }

    private void Grow()
    {
        _TransformarionPos = _Players[_Active].transform.position;
        _FlipX = _PlayersScripts[_Active]._Renderer.flipX;
        _Players[_Active].SetActive(false);
        

        if (_Active == (int)Size.small)
        {
            _Animator.Play("1");
        }
        if (_Active == (int)Size.medium)
        {
            _Animator.Play("2");
        }
    }

    public void GrowEnd()
    {
        _Active++;
        _Players[_Active].SetActive(true);
        _PlayersScripts[_Active].SwitchState(new Idle(_PlayersScripts[_Active]));
        _PlayersScripts[_Active]._Renderer.flipX = _FlipX;
        _PlayersScripts[_Active]._AxeRenderer.flipX = _FlipX;
        _Players[_Active].transform.position = _TransformarionPos;
    }


    private void GrowTest()
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

    private void ShrinkTest()
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
