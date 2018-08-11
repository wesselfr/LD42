using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockServiceProvider : MonoBehaviour {

    public static BlockServiceProvider instance;

    [SerializeField]
    private GenericBlockData[] m_Blocks;

    [SerializeField]
    private GenericOreData[] m_Ores;

	// Use this for initialization
	void Awake () {
        instance = this;
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
}
