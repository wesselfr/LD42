using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour {

    [SerializeField]
    private string[] m_BlockAmounts; // "1 = 255". Creates 255 blocks of id 1

    private GenericBlockData[] m_Blocks;
    private Stack<GenericBlockData> m_BlockData;

    private void Start()
    {
        m_BlockData = new Stack<GenericBlockData>();
        m_Blocks = new GenericBlockData[m_BlockAmounts.Length];
        foreach(string str in m_BlockAmounts)
        {
            string[] data = str.Split('=');
            int id = int.Parse(data[0]);
            for(int i = 0; i < int.Parse(data[1]); i++)
            {
                m_BlockData.Push(BlockServiceProvider.instance.GetBlock(id));
            }
        }
    }

    public Stack<GenericBlockData> GetObjectData()
    {
        m_BlockData.Clear();
        m_Blocks = new GenericBlockData[m_BlockAmounts.Length];
        foreach (string str in m_BlockAmounts)
        {
            string[] data = str.Split('=');
            int id = int.Parse(data[0]);
            for (int i = 0; i < int.Parse(data[1]); i++)
            {
                m_BlockData.Push(BlockServiceProvider.instance.GetBlock(id));
            }
        }
        return m_BlockData;
    }



}
