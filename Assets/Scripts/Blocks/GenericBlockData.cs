using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Block", menuName = "Block/Create")]
public class GenericBlockData : ScriptableObject {

    [Header("Basics")]
    [SerializeField]
    private string m_BlockName;
    [SerializeField]
    private int m_ID;
    [SerializeField]
    private Sprite m_Texture;

    #region Accesors
    public Sprite sprite { get { return m_Texture; } }
    public string blockName { get { return m_BlockName; } }
    public int id { get { return m_ID; } }
    #endregion
}
