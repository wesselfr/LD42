using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public static ItemManager Instance;
    public float _Growth;

    private List<GenericItemData> _Cobbles;
    private List<GenericItemData> _Diamonds;
    private List<GenericItemData> _Dirts;
    private List<GenericItemData> _Emeralds;
    private List<GenericItemData> _Golds;
    private List<GenericItemData> _Irons;
    private List<GenericItemData> _Rubies;
    private List<GenericItemData> _Saphhires;


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
        _Cobbles = new List<GenericItemData>();
        _Diamonds = new List<GenericItemData>();
        _Dirts = new List<GenericItemData>();
        _Emeralds = new List<GenericItemData>();
        _Golds = new List<GenericItemData>();
        _Irons = new List<GenericItemData>();
        _Rubies = new List<GenericItemData>();
        _Saphhires = new List<GenericItemData>();
    }

    public void AddItem(GenericItemData data)
    {
        switch (data.Id)
        {
            case 0:
                _Cobbles.Add(data);
                break;
            case 1:
                _Diamonds.Add(data);
                break;
            case 2:
                _Dirts.Add(data);
                break;
            case 3:
                _Emeralds.Add(data);
                break;
            case 4:
                _Golds.Add(data);
                break;
            case 5:
                _Irons.Add(data);
                break;
            case 6:
                _Rubies.Add(data);
                break;
            case 7:
                _Saphhires.Add(data);
                break;


        }
    }

    public void AddGrowth(float value)
    {
        _Growth += value;
    }
}
