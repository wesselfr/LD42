using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Item/New")]
public class GenericItemData : ScriptableObject
{

    [SerializeField]
    private Sprite m_Icon;

    [SerializeField]
    private string m_ItemName;

    [SerializeField]
    private int m_Price;

    [SerializeField]
    private int m_Id;

    public Sprite icon { get { return m_Icon; } }
    public string name { get { return m_ItemName; } }
    public int price { get { return m_Price; } }
    public int Id { get { return m_Id; } }

}
