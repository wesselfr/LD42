using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour {

    [SerializeField]
    private Chunk[] m_Chunks;
    private int m_Last;
	// Use this for initialization
	void FirstGeneration () {
        m_Last = 0;
        m_Chunks[0].StartGeneration();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.G))
        {
            FirstGeneration();
        }
        if (m_Chunks[m_Last].done)
        {
           
            if (m_Last < m_Chunks.Length - 1)
            {
                m_Last++;
                m_Chunks[m_Last].StartGeneration();
            }
        }
	}

    public void CreateNewChunk()
    {

    }

}
