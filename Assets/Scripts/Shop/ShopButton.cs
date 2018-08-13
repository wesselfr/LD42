using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopButton : MonoBehaviour
{
    [SerializeField]
    private GenericItemData m_ItemToSell;

    [SerializeField]
    private ShopHandler m_Store;

    [SerializeField]
    private BoxCollider m_Collider;
    public void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = 0;

            if (m_Collider.bounds.Contains(Camera.main.ScreenToWorldPoint(mousePosition)))
            {
                m_Store.BuyItem(m_ItemToSell);
                Debug.Log("BUY");
            }
            
        }
    }

    private void OnMouseDown()
    {
        m_Store.BuyItem(m_ItemToSell);
        Debug.Log("BUY");
    }
}

