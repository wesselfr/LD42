using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField] private TextMeshProUGUI _GemWorth;
    private int _Counter;
    
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
       
    }

    // Update is called once per frame
    void Update()
    {
        if (_Counter > ItemManager.Instance._Money)
        {
            _Counter--;
        }
        if (_Counter < ItemManager.Instance._Money)
        {
            _Counter++;
        }

        _GemWorth.text = _Counter.ToString();
    }

}
