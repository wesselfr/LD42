using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Block", menuName = "Block/Create")]
public class GenericBlock : ScriptableObject {

    [Header("Basics")]
    [SerializeField]
    private string m_BlockName;
    [SerializeField]
    private Sprite m_Texture;


    public Sprite sprite { get { return m_Texture; } }
    public string blockName { get { return m_BlockName; } }
}
