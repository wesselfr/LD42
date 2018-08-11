using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chunk : MonoBehaviour {

    [SerializeField]
    private int m_Size;

    [SerializeField]
    private Grid m_Grid;

    [SerializeField]
    private ObjectPool m_Pool;

    [SerializeField]
    private GameObject m_Prefab;

    [SerializeField]
    private GameOfLife m_CaveGenerator;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.G))
        {
            GenerateChunk();
        }
	}

    public void GenerateChunk()
    {
        Stack<GenericBlockData> blocks = m_Pool.GetObjectData();

        byte[,] caves = m_CaveGenerator.GenerateCave();

        int Xmax = caves.GetLength(0);
        int Ymax = caves.GetLength(1);

        for(int x = 0; x < m_Size; x++)
        {
            for(int y = 0; y < m_Size; y++)
            {
                if (caves[x, y] < 16)
                {
                    Vector3 position = transform.position + new Vector3(x, y) * 1.5f;
                    BigBlock block = Instantiate(m_Prefab, position, Quaternion.identity).GetComponent<BigBlock>();

                    //LeftTop
                    if(x-1 > 0 && y+1 < Ymax)
                    {
                        if(caves[x-1, y+1] > 16)
                        {
                            block.SetBlockState(0, BlockState.Mined);
                        }
                    }
                    //Left
                    if(x-1 > 0)
                    {
                        if (caves[x - 1, y] > 16)
                        {
                            block.SetBlockState(0, BlockState.Mined);
                            block.SetBlockState(2, BlockState.Mined);
                        }
                    }
                    //LeftBottom
                    if (x - 1 > 0 && y - 1 > 0)
                    {
                        if (caves[x - 1, y - 1] > 16)
                        {
                            block.SetBlockState(2, BlockState.Mined);
                        }
                    }
                    //Top
                    if(y+1 < Ymax)
                    {
                        if (caves[x, y + 1] > 16)
                        {
                            block.SetBlockState(0, BlockState.Mined);
                            block.SetBlockState(1, BlockState.Mined);
                        }
                    }
                    //Bottom
                    if(y-1 > 0)
                    {
                        if (caves[x, y -1] > 16)
                        {
                            block.SetBlockState(2, BlockState.Mined);
                            block.SetBlockState(3, BlockState.Mined);
                        }
                    }
                    //RightTop
                    if (x + 1 < Xmax && y + 1 < Ymax)
                    {
                        if (caves[x + 1, y + 1] > 16)
                        {
                            block.SetBlockState(1, BlockState.Mined);
                        }
                    }
                    //Right
                    if (x + 1 < Xmax)
                    {
                        if (caves[x + 1, y] > 16)
                        {
                            block.SetBlockState(1, BlockState.Mined);
                            block.SetBlockState(3, BlockState.Mined);
                        }
                    }
                    //RightBottom
                    if (x + 1 < Xmax && y - 1 > 0)
                    {
                        if (caves[x + 1, y - 1] > 16)
                        {
                            block.SetBlockState(3, BlockState.Mined);
                        }
                    }

                    block.UpdateBlock();
                }
            }
        }
    }
}
