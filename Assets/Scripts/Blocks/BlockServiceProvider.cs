using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockServiceProvider : MonoBehaviour {

    public static BlockServiceProvider instance;

    [SerializeField]
    private GenericBlockData[] m_Blocks;

    [SerializeField]
    private GenericOreData[] m_Ores;

    [SerializeField]
    private BigBlock m_BlockGenerator;

    private Stack<BigBlock> m_BlockPool;

	// Use this for initialization
	void Awake () {
        instance = this;
        m_BlockPool = new Stack<BigBlock>();
        for(int i = 0; i < 10000; i++)
        {
            m_BlockPool.Push(Instantiate(m_BlockGenerator));
        }
	}

    public GenericBlockData GetBlock(int id)
    {
        for(int i = 0; i < m_Blocks.Length; i++)
        {
            if(m_Blocks[i].id == id)
            {
                return m_Blocks[i];
            }
        }
        return null;
    }

    public GenericOreData GetOre(int id)
    {
        for (int i = 0; i < m_Ores.Length; i++)
        {
            if (m_Ores[i].id == id)
            {
                return (GenericOreData)m_Ores[i];
            }
        }
        return null;
    }

    public BigBlock GetBlockCreator()
    {
        m_BlockGenerator.Initialize(GetBlock(2));
        return m_BlockGenerator;
    }

    public void AddToPool(BigBlock block)
    {
        block.transform.position =  Vector2.one * 99f;
        m_BlockPool.Push(block);
    }

    public BigBlock GetBlockFromPool()
    {
        return m_BlockPool.Pop();
    }
}
