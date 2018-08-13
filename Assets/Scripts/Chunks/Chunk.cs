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

    [SerializeField]
    private bool m_TopLayer;

    [SerializeField]
    private BigBlockData[,] m_Data;

    [SerializeField]
    private Stack<BigBlock> m_CachedBlocks;

    [SerializeField]
    private GameObject m_Shop;

    private Vector2 m_PositionFromOrigin;
    private Chunk m_TopChunk;
    private Chunk m_BottomChunk;
    private Chunk m_LeftChunk;
    private Chunk m_RightChunk;

    private bool m_Done = false;
	
    
    public IEnumerator Dispose()
    {
        int childs = transform.childCount;
        for(int i =0; i< childs; i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
        Destroy(this.gameObject);
        yield return new WaitForEndOfFrame();
    }

    public void StartGeneration(Vector2 distanceFromOrigin)
    {
        m_PositionFromOrigin = distanceFromOrigin;
        m_Data = new BigBlockData[m_Size, m_Size];
        m_CachedBlocks = new Stack<BigBlock>();
        if (!m_TopLayer)
        {
            StartCoroutine(GenerateChunk());
        }
        else
        {
            StartCoroutine(GenerateGroundLevel());
        }
    }

    public IEnumerator GenerateGroundLevel()
    {
        GenericBlockData dirt = BlockServiceProvider.instance.GetBlock(2);
        GenericBlockData grass = BlockServiceProvider.instance.GetBlock(4);
        for (int x = 0; x < m_Size; x++)
        {
            for (int y = 0; y < m_Size; y++)
            {
                if (y < 10)
                {
                    Vector3 position = transform.position + new Vector3(x, y) * 1.5f;
                    BigBlock block = Instantiate(m_Prefab, position, Quaternion.identity).GetComponent<BigBlock>();
                    if (y > 5)
                    {
                        if (y < 9)
                        {
                            //Dirt
                            block.Initialize(dirt);
                        }
                        else
                        {
                            //Grass
                            block.Initialize(grass);
                        }
                    }
                    block.UpdateBlock();
                    block.transform.parent = this.transform;

                    //m_Data[x, y] = block.DisableBlock(); ;

                    
                }
            }
        }
        m_Done = true;
        yield return new WaitForEndOfFrame();
    }


    public IEnumerator GenerateChunk()
    {
        Stack<GenericBlockData> blocks = m_Pool.GetObjectData();

        m_CaveGenerator.ResetGeneration();
        yield return StartCoroutine(m_CaveGenerator.CaveCourotine());
        while (!m_CaveGenerator.done) { yield return new WaitForEndOfFrame(); }
        byte[,] caves = m_CaveGenerator.ReturnData();

        Debug.Log("Caves Done");
        
        //byte[,] dirt = m_CaveGenerator.GenerateCave();

        m_CaveGenerator.SetMaxOres(40);
        yield return StartCoroutine(m_CaveGenerator.OresCourotine());
        while (!m_CaveGenerator.done) { yield return new WaitForEndOfFrame(); }
        byte[,] diamons = m_CaveGenerator.ReturnData();

        Debug.Log("Diamonds Done");

        m_CaveGenerator.SetMaxOres(85);
        yield return StartCoroutine(m_CaveGenerator.OresCourotine());
        while (!m_CaveGenerator.done) { yield return new WaitForEndOfFrame(); }
        byte[,] emeralds = m_CaveGenerator.ReturnData();

        Debug.Log("Emeralds Done");

        m_CaveGenerator.SetMaxOres(50);
        yield return StartCoroutine(m_CaveGenerator.OresCourotine());
        while (!m_CaveGenerator.done) { yield return new WaitForEndOfFrame(); }
        byte[,] gold = m_CaveGenerator.ReturnData();

        Debug.Log("Gold Done");

        m_CaveGenerator.SetMaxOres(110);
        yield return StartCoroutine(m_CaveGenerator.OresCourotine());
        while (!m_CaveGenerator.done) { yield return new WaitForEndOfFrame(); }
        byte[,] iron = m_CaveGenerator.ReturnData();

        Debug.Log("Iron Done");

        m_CaveGenerator.SetMaxOres(75);
        yield return StartCoroutine(m_CaveGenerator.OresCourotine());
        while (!m_CaveGenerator.done) { yield return new WaitForEndOfFrame(); }
        byte[,] ruby = m_CaveGenerator.ReturnData();

        Debug.Log("Ruby Done");

        m_CaveGenerator.SetMaxOres(75);
        yield return StartCoroutine(m_CaveGenerator.OresCourotine());
        while (!m_CaveGenerator.done) { yield return new WaitForEndOfFrame(); }
        byte[,] sapphire = m_CaveGenerator.ReturnData();

        Debug.Log("Sapphire Done");


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

                    //yield return new WaitForEndOfFrame();
                    
                }
            }
        }
        m_Done = true;
        yield return StartCoroutine(PopulateCaves(caves));
    }

    public IEnumerator PopulateGround()
    {
        yield return new WaitForEndOfFrame();
    }

    public IEnumerator PopulateCaves(byte[,] caveData)
    {
        byte[,] caves = caveData;
        Stack<Vector2> m_cachedCave = new Stack<Vector2>();
        for (int x = 0; x < m_Size; x++)
        {
            for (int y = 0; y < m_Size; y++)
            {
                if (caves[x, y] > 15)
                {
                    if (x - 1 > 0 && x + 1 < m_Size)
                    {
                        if(y-1 > 0)
                        {
                            if(caves[x-1,y]> 15 && caves[x+1, y] > 15)
                            {
                                
                                //Ground Check
                                if(caves[x - 1, y - 1] < 15 && caves[x,y-1] < 15 && caves[x + 1, y - 1] < 15)
                                {
                                    for (int heightCheck = 0; heightCheck < 4;)
                                    {
                                        if(caves[x-1, y+heightCheck] > 15 && caves[x, y + heightCheck] > 15 && caves[x + 1, y + heightCheck] > 15)
                                        {
                                            heightCheck++;
                                        }
                                        else
                                        {
                                            break;
                                        }
                                        yield return new WaitForEndOfFrame();
                                        if ( y + heightCheck < m_Size)
                                        {
                                            Debug.Log(heightCheck);
                                            if (heightCheck == 4)
                                            {

                                                //GenerateStore (Test)

                                                int RandomStore = Random.Range(0, 2);

                                                if (RandomStore > 0)
                                                {
                                                    GenericBlockData dirt = BlockServiceProvider.instance.GetBlock(2);

                                                    Vector3 leftPosition = transform.position + new Vector3(x - 1, y) * 1.5f;
                                                    Vector3 centerPosition = transform.position + new Vector3(x, y) * 1.5f;
                                                    Vector3 rightPosition = transform.position + new Vector3(x + 1, y) * 1.5f;

                                                    BigBlock leftBlock = Instantiate(m_Prefab, leftPosition, Quaternion.identity).GetComponent<BigBlock>();
                                                    BigBlock centerBlock = Instantiate(m_Prefab, centerPosition, Quaternion.identity).GetComponent<BigBlock>();
                                                    BigBlock rightBlock = Instantiate(m_Prefab, rightPosition, Quaternion.identity).GetComponent<BigBlock>();

                                                    leftBlock.Initialize(dirt);
                                                    centerBlock.Initialize(dirt);
                                                    rightBlock.Initialize(dirt);

                                                    leftBlock.UpdateBlock();
                                                    centerBlock.UpdateBlock();
                                                    rightBlock.UpdateBlock();

                                                    if(RandomStore == 1)
                                                    {
                                                        Vector3 storePosition = transform.position + new Vector3(x, y + 1) * 1.5f;
                                                        Instantiate(m_Shop, storePosition, Quaternion.identity);
                                                    }

                                                    yield return new WaitForEndOfFrame();
                                                }

                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }

                }
            }
        }
    }

    private struct Cave
    {
        private byte[,] m_Cave;
        public Cave(byte[,] cave)
        {
            m_Cave = cave;
        }
        public void AddToCave(byte[,] bit)
        {
           
        } 
        public byte[,] cave { get { return m_Cave; } }
    }

    public bool done { get { return m_Done; } }

    public void ShowChunk()
    {
        BlockServiceProvider provider = BlockServiceProvider.instance;
        for (int x = 0; x < m_Size; x++)
        {
            for (int y = 0; y < m_Size; y++)
            {
                BigBlock block = provider.GetBlockFromPool();
                block.EnableBlock(m_Data[x, y]);
                m_CachedBlocks.Push(block);
            }
        }
    }

    public void HideChunk()
    {
        BlockServiceProvider provider = BlockServiceProvider.instance;
        BigBlock[] blocks = m_CachedBlocks.ToArray();
        int x = 0;
        int y = -1;
        for(int i = 0; i < blocks.Length; i++)
        {
            y++;
            if (y > m_Size) { y = -1; x++; }

            m_Data[x, y] = blocks[i].DisableBlock();
            provider.AddToPool(blocks[i]);
        }
        m_CachedBlocks.Clear();

        
    }

    public Vector2 position { get { return m_PositionFromOrigin; } }

    public Chunk topChunk { get { return m_TopChunk; } }
    public Chunk bottomChunk { get { return m_BottomChunk; } }
    public Chunk leftChunk { get { return m_LeftChunk; } }
    public Chunk rightChunk { get { return m_RightChunk; } }

    public void AddNeighbour(Vector2 direction, Chunk chunkToAdd)
    {
        if(direction == Vector2.up)
        {
            m_TopChunk = chunkToAdd;
        }
        if(direction == Vector2.down)
        {
            m_BottomChunk = chunkToAdd;
        }
        if(direction == Vector2.left)
        {
            m_LeftChunk = chunkToAdd;
        }
        if(direction == Vector2.right)
        {
            m_RightChunk = chunkToAdd;
        }
    }
}
