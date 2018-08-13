using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteImporter : MonoBehaviour
{
    [SerializeField]
    private Sprite[] m_SpriteSheet;
    [SerializeField]
    private GenericBlockData m_Data;

    public Sprite[] sheet { get { return m_SpriteSheet; } }
    public GenericBlockData data { get { return m_Data; } }
}
