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

    Size _Size;
    [SerializeField] private GameObject[] _Players;
    private int _Active;

    private void Start()
    {
        for (int i = 0; i < _Players.Length; i++)
        {
            _Players[i].SetActive(false);
        }

        _Players[_Active].SetActive(true);
    }

    private void Update()
    {
        transform.position = _Players[_Active].transform.position;
    }

    private void Grow()
    {

    }
}
