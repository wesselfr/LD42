using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticleChunkHandler : MonoBehaviour {

    [SerializeField]
    private Chunk[] m_Chunks;

    private float m_HorizontalValue;

    public void Initialize(float x)
    {
        m_HorizontalValue = x;
    }

    public Chunk[] chunks { get { return m_Chunks; } }
    public float x { get { return m_HorizontalValue; } }
}
