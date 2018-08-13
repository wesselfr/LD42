using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericItem : MonoBehaviour
{

}

public class ShopHandler : MonoBehaviour {

    [Header("UI")]
    [SerializeField]
    private GameObject m_StoreUI, m_InteractKey;

    private bool m_StoreActive = false;

	// Use this for initialization
	void Start () {
        m_StoreUI.SetActive(false);
        m_InteractKey.SetActive(false);
	}

    public void ToggleStore()
    {
        //Switch UI
        m_StoreActive = !m_StoreActive;
        m_StoreUI.SetActive(m_StoreActive);
        m_InteractKey.SetActive(!m_StoreActive);
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

    public void BuyItem(GenericItem item)
    {
        //Check money, than buy item.
        Debug.Log("Buy item");
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Contains("Player"))
        {
            m_InteractKey.SetActive(true);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Contains("Player"))
        {
            //Interact key. Toggles Store.
            if (Input.GetKeyDown(KeyCode.E))
            {
                ToggleStore();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Contains("Player"))
        {
            LeaveStore();
        }
    }
}
