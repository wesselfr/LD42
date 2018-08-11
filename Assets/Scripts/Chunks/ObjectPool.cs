using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour {

    [SerializeField]
    private string[] m_BlockAmounts; // "1 = 255". Creates 255 blocks of id 1

    private GenericBlockData[] m_Blocks;

    private void Start()
    {
        
    }

}
