using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopButton : MonoBehaviour
{
    [SerializeField]
    private GenericItem m_ItemToSell;

    [SerializeField]
    private ShopHandler m_Store;

    [SerializeField]
    private BoxCollider2D m_Collider;
    public void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = 0;

            mousePosition = mousePosition - transform.position;

            Debug.Log("Mouse Position: " + mousePosition.x + "," + mousePosition.y + " Bounds: " + m_Collider.bounds.ClosestPoint(mousePosition));
            if (m_Collider.bounds.Contains(Camera.main.ScreenToWorldPoint(mousePosition)))
            {
                m_Store.BuyItem(m_ItemToSell);
            }
            
        }
    }
}

