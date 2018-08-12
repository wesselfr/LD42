using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour {

    [SerializeField]
    private List<Chunk> m_Chunks;
    private int m_Last;

    [SerializeField]
    private Chunk m_Prefab;

    private void Start()
    {
        FirstGeneration();
    }

    // Use this for initialization
    void FirstGeneration () {
        m_Last = 0;
        m_Chunks[0].StartGeneration(Vector2.zero);
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.G))
        {
            FirstGeneration();
        }
        if (m_Chunks[m_Last].done)
        {
           
            if (m_Last < m_Chunks.Count -1)
            {
                m_Last++;
                m_Chunks[m_Last].StartGeneration(m_Chunks[m_Last].transform.position);
            }
        }
	}

    public void CreateNewChunk()
    {

    }

    public void AddCunks(Chunk askChunk)
    {
        Chunk center = askChunk;

    }

}
