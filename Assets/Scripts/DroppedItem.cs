using UnityEngine;
using System.Collections;

public class DroppedItem : MonoBehaviour
{
    [SerializeField]
    private Sprite m_Sprite;
    [SerializeField]
    private int m_MoneyValue;
    [SerializeField]
    private float m_GrowMultiplier;

    public void DestroyItem()
    {
        Destroy(this.gameObject);
    }

    public Sprite sprite { get { return m_Sprite; } }
    public int moneyValue { get { return m_MoneyValue; } }
    public float growthMultiplier { get { return m_GrowMultiplier; } }
}
