using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockServiceProvider : MonoBehaviour {

    public static BlockServiceProvider instance;

    [SerializeField]
    private GenericBlockData[] m_Blocks;

	// Use this for initialization
	void Start () {
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
}
