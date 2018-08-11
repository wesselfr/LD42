using UnityEngine;
using System.Collections;

public class BigBlock : MonoBehaviour
{
    [SerializeField]
    SmallBlock[] m_SmallBlocks;
    // 0,1
    // 2,3

    [SerializeField]
    private GenericBlockData m_Data;

    [SerializeField]
    private SpriteRenderer m_Renderer;

    public void Initialize(GenericBlockData data)
    {
        m_Data = data;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateBlock()
    {
        
    }
}
