using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour {

    [SerializeField]
    private List<Chunk> m_Chunks;
    private int m_Last;

    [SerializeField]
    private GameObject m_VerticleChunkPrefab;

    [SerializeField]
    private GameObject m_Player;

    private float MaxX = 0;
    private float MinX = 0;

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
        if (m_Chunks[m_Last].done)
        {
           
            if (m_Last < m_Chunks.Count -1)
            {
                m_Last++;
                m_Chunks[m_Last].StartGeneration(m_Chunks[m_Last].transform.position);
            }
        }

        if(m_Player.transform.position.x - 37.5f< MinX && m_Chunks[m_Last].done)
        {
            float newX = MinX - 37.5f;
            GameObject verticleChunkObject = Instantiate(m_VerticleChunkPrefab, new Vector3(newX, 0), Quaternion.identity);
            verticleChunkObject.transform.parent = this.gameObject.transform;
            VerticleChunkHandler chunkHandler = verticleChunkObject.GetComponent<VerticleChunkHandler>();
            m_Chunks.AddRange(chunkHandler.chunks);
            MinX = newX;
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
