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

    private bool m_Done = false;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.G))
        {
            //StartCoroutine(GenerateChunk());
        }
	}

    public void StartGeneration()
    {
        StartCoroutine(GenerateChunk());
    }

    public IEnumerator GenerateChunk()
    {
        Stack<GenericBlockData> blocks = m_Pool.GetObjectData();

        byte[,] caves = m_CaveGenerator.GenerateCave();
        yield return new WaitForEndOfFrame();
        //byte[,] dirt = m_CaveGenerator.GenerateCave();

        byte[,] diamons = m_CaveGenerator.GenerateOres(15);
        yield return new WaitForEndOfFrame();
        byte[,] emeralds = m_CaveGenerator.GenerateOres(85);
        yield return new WaitForEndOfFrame();
        byte[,] gold = m_CaveGenerator.GenerateOres(50);
        yield return new WaitForEndOfFrame();
        byte[,] iron = m_CaveGenerator.GenerateOres(110);
        yield return new WaitForEndOfFrame();
        byte[,] ruby = m_CaveGenerator.GenerateOres(75);
        yield return new WaitForEndOfFrame();
        byte[,] sapphire = m_CaveGenerator.GenerateOres(75);
        yield return new WaitForEndOfFrame();

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

                    //if (dirt[x, y]>16)
                    //{
                    //    block.Initialize(BlockServiceProvider.instance.GetBlock(1));
                    //}

                    BigBlock ore = null;

                    //SpawnOre
                    if (iron[x, y] > 15)
                    {
                        ore = Instantiate(m_Prefab, position, Quaternion.identity).GetComponent<BigBlock>();
                        ore.Initialize(BlockServiceProvider.instance.GetOre(4));
                        ore.gameObject.name = "Iron";
                    }
                    else if(emeralds[x,y] > 15)
                    {
                        ore = Instantiate(m_Prefab, position, Quaternion.identity).GetComponent<BigBlock>();
                        ore.Initialize(BlockServiceProvider.instance.GetOre(2));
                        ore.gameObject.name = "Emeralds";
                    }
                    else if (ruby[x, y] > 15)
                    {
                        ore = Instantiate(m_Prefab, position, Quaternion.identity).GetComponent<BigBlock>();
                        ore.Initialize(BlockServiceProvider.instance.GetOre(5));
                        ore.gameObject.name = "Ruby";
                    }
                    else if (sapphire[x, y] > 15)
                    {
                        ore = Instantiate(m_Prefab, position, Quaternion.identity).GetComponent<BigBlock>();
                        ore.Initialize(BlockServiceProvider.instance.GetOre(6));
                        ore.gameObject.name = "Sapphire";
                    }
                    else if (gold[x, y] > 15)
                    {
                        ore = Instantiate(m_Prefab, position, Quaternion.identity).GetComponent<BigBlock>();
                        ore.Initialize(BlockServiceProvider.instance.GetOre(3));
                        ore.gameObject.name = "Gold";
                    }

                    else if (diamons[x, y] > 15)
                    {
                        ore = Instantiate(m_Prefab, position, Quaternion.identity).GetComponent<BigBlock>();
                        ore.Initialize(BlockServiceProvider.instance.GetOre(1));
                        ore.gameObject.name = "Diamond";
                    }

                    //LeftTop
                    if (x-1 > 0 && y+1 < Ymax)
                    {
                        if(caves[x-1, y+1] > 16)
                        {
                            block.SetBlockState(0, BlockState.Mined);
                            if(ore != null) { ore.SetBlockState(0, BlockState.Mined); }
                        }
                    }
                    //Left
                    if(x-1 > 0)
                    {
                        if (caves[x - 1, y] > 16)
                        {
                            block.SetBlockState(0, BlockState.Mined);
                            block.SetBlockState(2, BlockState.Mined);
                            if (ore != null)
                            {
                                ore.SetBlockState(0, BlockState.Mined);
                                ore.SetBlockState(2, BlockState.Mined);
                            }
                        }
                    }
                    //LeftBottom
                    if (x - 1 > 0 && y - 1 > 0)
                    {
                        if (caves[x - 1, y - 1] > 16)
                        {
                            block.SetBlockState(2, BlockState.Mined);
                            if (ore != null) { ore.SetBlockState(2, BlockState.Mined); }
                        }
                    }
                    //Top
                    if(y+1 < Ymax)
                    {
                        if (caves[x, y + 1] > 16)
                        {
                            block.SetBlockState(0, BlockState.Mined);
                            block.SetBlockState(1, BlockState.Mined);
                            if (ore != null)
                            {
                                ore.SetBlockState(0, BlockState.Mined);
                                ore.SetBlockState(1, BlockState.Mined);
                            }
                        }
                    }
                    //Bottom
                    if(y-1 > 0)
                    {
                        if (caves[x, y -1] > 16)
                        {
                            block.SetBlockState(2, BlockState.Mined);
                            block.SetBlockState(3, BlockState.Mined);
                            if (ore != null)
                            {
                                ore.SetBlockState(2, BlockState.Mined);
                                ore.SetBlockState(3, BlockState.Mined);
                            }
                        }
                    }
                    //RightTop
                    if (x + 1 < Xmax && y + 1 < Ymax)
                    {
                        if (caves[x + 1, y + 1] > 16)
                        {
                            block.SetBlockState(1, BlockState.Mined);
                            if (ore != null) { ore.SetBlockState(1, BlockState.Mined); }
                        }
                    }
                    //Right
                    if (x + 1 < Xmax)
                    {
                        if (caves[x + 1, y] > 16)
                        {
                            block.SetBlockState(1, BlockState.Mined);
                            block.SetBlockState(3, BlockState.Mined);
                            if (ore != null)
                            {
                                ore.SetBlockState(1, BlockState.Mined);
                                ore.SetBlockState(3, BlockState.Mined);
                            }
                        }
                    }
                    //RightBottom
                    if (x + 1 < Xmax && y - 1 > 0)
                    {
                        if (caves[x + 1, y - 1] > 16)
                        {
                            block.SetBlockState(3, BlockState.Mined);
                            if (ore != null) { ore.SetBlockState(3, BlockState.Mined); }
                        }
                    }

                    block.UpdateBlock();
                    block.transform.parent = this.transform;
                    if (ore != null)
                    {
                        ore.UpdateBlock();
                        ore.transform.parent = this.transform;
                        ore.gameObject.GetComponent<SpriteRenderer>().sortingOrder = 1;
                    }

                    yield return new WaitForEndOfFrame();
                    
                }
            }
        }
        m_Done = true;
    }

    public bool done { get { return m_Done; } }
}
