using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopHandler : MonoBehaviour {

    [Header("UI")]
    [SerializeField]
    private GameObject m_StoreUI, m_InteractKey;

    private bool m_StoreActive = false;
    private bool m_PlayerInBounds;

	// Use this for initialization
	void Start () {
        m_StoreUI.SetActive(false);
        m_InteractKey.SetActive(false);
	}

    public void ToggleStore()
    {
        //Switch UI
        m_StoreActive = !m_StoreActive;
        if (m_StoreActive) { EnterStore(); } else { LeaveStore(); }
    }
    public void EnterStore()
    {
        m_StoreActive = true;
        m_StoreUI.SetActive(true);
        m_InteractKey.SetActive(false);
    }
    public void LeaveStore()
    {
        m_StoreActive = false;
        m_StoreUI.SetActive(false);
        m_InteractKey.SetActive(false);
    }

    /// <summary>
    /// Sell Gems
    /// </summary>
    /// <param name="items"></param>
    public void SellInventory(DroppedItem[] items)
    {
        foreach(DroppedItem item in items)
        {
            //Add value of item
        }
    }

    public void BuyItem(GenericItemData item)
    {
        //Check money, than buy item.
        Debug.Log("Buy item");
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Contains("Player"))
        {
            m_InteractKey.SetActive(true);
            m_PlayerInBounds = true;
        }
    }

    public void Update()
    {
        if (m_PlayerInBounds)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                ToggleStore();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag.Contains("Player"))
        {
            LeaveStore();
            m_PlayerInBounds = false;
        }
    }
}
