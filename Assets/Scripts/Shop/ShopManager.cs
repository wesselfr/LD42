using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour {

    public static ShopManager instance;

    [SerializeField]
    private int m_Money;

	// Use this for initialization
	void Start () {
        instance = this;
	}
	
    /// <summary>
    /// Checks if player currently has enough money.
    /// </summary>
    /// <param name="needed"></param>
    /// <returns></returns>
    public bool CheckMoney(int needed)
    {
        if(needed  <= m_Money)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void ChangeMoney(int amount)
    {
        m_Money += amount;
    }
}
