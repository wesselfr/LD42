using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SellButton : MonoBehaviour
{
    [SerializeField] NotEnoughText _Text;
    public void Button()
    {
        if (ItemManager.Instance._Money >= 1000)
        {
            PlayerContainer.Instance.SellGems();
        }
        else
        {
            _Text._Alpha = 1;
        }
    }
}
