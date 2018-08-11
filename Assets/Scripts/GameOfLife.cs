using UnityEngine;
using System.Collections;

public class GameOfLife : MonoBehaviour
{

    [SerializeField]
    private int m_Height;
    [SerializeField]
    private int m_Widht;

    byte[,] m_Update;
    byte[,] m_Old;
    byte[,] m_Draw;

    [SerializeField]
    bool m_IsProccesing = true;

    //Added Itterations for caves
    [SerializeField]
    private int m_Itterations = 10;

    [SerializeField]
    private int m_OreItterations = 25;

    private int m_ItterationsLeft;

    private int m_CurrentOres;
    private int m_MaxOres;

    [SerializeField]
    Texture2D m_Texture;

    [SerializeField]
    private Material m_Material;
    // Use this for initialization
    void Start()
    {

        //Setup Texture
        m_Texture = new Texture2D(m_Widht, m_Height);
        m_Texture.name = "GameOfLifeTexture";
        m_Texture.filterMode = FilterMode.Point;

        m_Old = new byte[m_Widht, m_Height];
        m_Update = (byte[,])m_Old.Clone();

        GenerateRandomGrid();
    }

    public void GenerateRandomGrid()
    {
        for (int x = 0; x < m_Widht; x++)
        {
            for (int y = 0; y < m_Height; y++)
            {
                float state = Random.Range(-40, 10);
                state = Mathf.Sign(state);
                if (state == 1)
                {
                    m_Update[x, y] = 16;
                }
                if (state == -1)
                {
                    m_Update[x, y] = 0;
                }
            }
        }

        m_Old = (byte[,])m_Update.Clone();
        m_Draw = (byte[,])m_Update.Clone();

        //Added Itterations
        m_ItterationsLeft = m_Itterations;
    }

    public byte[,] GenerateCave()
    {
        m_Update = new byte[m_Widht, m_Height];
        for (int x = 0; x < m_Widht; x++)
        {
            for (int y = 0; y < m_Height; y++)
            {
                float state = Random.Range(-40, 10);
                state = Mathf.Sign(state);
                if (state == 1)
                {
                    m_Update[x, y] = 16;
                }
                if (state == -1)
                {
                    m_Update[x, y] = 0;
                }
                
            }
        }

        m_Old = (byte[,])m_Update.Clone();
        m_Draw = (byte[,])m_Update.Clone();

        //Added Itterations
        m_ItterationsLeft = m_Itterations;
        StartCoroutine(UpdateGameOfLife());
        for (int i = 0; i < m_ItterationsLeft; i++)
        {
            //UpdateGameOfLife();
        }

        return m_Update;
    }

    public byte[,] GenerateOres(int maxStart)
    {
        m_Update = new byte[m_Widht, m_Height];
        m_MaxOres = maxStart;
        int amount = 0;
        while (amount < maxStart)
        {
            for (int x = 0; x < m_Widht; x++)
            {
                for (int y = 0; y < m_Height; y++)
                {

                    if (m_Update[x,y] == 0)
                    {
                        float state = Random.Range(-40, 10);
                        state = Mathf.Sign(state);
                        if (state == 1)
                        {
                            if (amount < maxStart)
                            {
                                m_Update[x, y] = 17;
                                amount++;
                            }
                        }
                        if (state == -1)
                        {
                            m_Update[x, y] = 1;
                        }
                    }
                    x = Random.Range(0, m_Widht);
                    y = Random.Range(0, m_Height);
                }
            }
        }
        m_CurrentOres = amount;

        m_Old = (byte[,])m_Update.Clone();
        m_Draw = (byte[,])m_Update.Clone();

        //Added Itterations
        m_ItterationsLeft = m_OreItterations + 1;

        StartCoroutine(UpdateGameOfLifeOres());
        for (int i = 0; i < m_ItterationsLeft; i++)
        {

            //for (int j = 0; j < m_MaxOres-m_CurrentOres; j++) {
            //    FillInGaps();
            //}
        }
        //UpdateGameOfLifeOres();

        return m_Update;
    }

    public void FillInGaps()
    {
        int x = Random.Range(0, m_Widht);
        int y = Random.Range(0, m_Height);

        if(m_Update[x,y] > 16) { FillInGaps(); }
        else
        {
            m_Update[x, y] += 16;
            m_CurrentOres++;
        }
    }

    void GenerateGlider()
    {
        for (int x = 0; x < m_Widht; x++)
        {
            for (int y = 0; y < m_Height; y++)
            {
                m_Update[x, y] = 0;
            }
        }

        //Glider
        m_Update[13, 12] = 16;
        m_Update[14, 13] = 16;
        m_Update[12, 14] = 16;
        m_Update[13, 14] = 16;
        m_Update[14, 14] = 16;


        m_Old = (byte[,])m_Update.Clone();
        m_Draw = (byte[,])m_Update.Clone();
    }

    void GenerateWinder()
    {
        for (int x = 0; x < m_Widht; x++)
        {
            for (int y = 0; y < m_Height; y++)
            {
                m_Update[x, y] = 0;
            }
        }

        //Straight Line

        m_Update[10, 10] = 17;
        m_Update[10, 11] = 17;
        m_Update[10, 12] = 17;

        m_Old = (byte[,])m_Update.Clone();
        m_Draw = (byte[,])m_Update.Clone();
    }

    //// Update is called once per frame
    //void Update()
    //{

    //    //if (Input.GetKeyDown(KeyCode.R))
    //    //{
    //    //    GenerateRandomGrid();
    //    //}
    //    //if (Input.GetKeyDown(KeyCode.N))
    //    //{
    //    //    m_IsProccesing = !m_IsProccesing;
    //    //}
    //    //if (Input.GetKeyDown(KeyCode.G))
    //    //{
    //    //    //GenerateGlider();
    //    //}
    //    //if (Input.GetKeyDown(KeyCode.Space))
    //    //{
    //    //    m_Draw = GenerateCave();
    //    //}
    //    //if (Input.GetKeyDown(KeyCode.H))
    //    //{
    //    //    GenerateWinder();
    //    //}

    //    if (m_IsProccesing)
    //    {
    //        //GenerateRandomGrid();
    //        UpdateGameOfLife();
    //        m_ItterationsLeft--;
    //        if(m_ItterationsLeft == 0) { m_IsProccesing = false; }
    //    }
    //    Draw();
    //}

    IEnumerator UpdateGameOfLife()
    {
        for (int x = 0; x < m_Widht; x++)
        {
            for (int y = 0; y < m_Height; y++)
            {
                byte neighbourAmount = 0;

                if (x - 1 >= 0 && x + 1 < m_Widht)
                {
                    if (y - 1 >= 0 && y + 1 < m_Height)
                    {
                        //LeftUp
                        if (m_Old[x - 1, y + 1] >= 16)
                        {
                            neighbourAmount++;
                        }
                        //Up
                        if (m_Old[x, y + 1] >= 16)
                        {
                            neighbourAmount++;
                        }
                        //RightUp
                        if (m_Old[x + 1, y + 1] >= 16)
                        {
                            neighbourAmount++;
                        }
                        //Left
                        if (m_Old[x - 1, y] >= 16)
                        {
                            neighbourAmount++;
                        }
                        //Right
                        if (m_Old[x + 1, y] >= 16)
                        {
                            neighbourAmount++;
                        }
                        //LeftDown
                        if (m_Old[x - 1, y - 1] >= 16)
                        {
                            neighbourAmount++;
                        }
                        //Down
                        if (m_Old[x, y - 1] >= 16)
                        {
                            neighbourAmount++;
                        }
                        //RightDown
                        if (m_Old[x + 1, y - 1] >= 16)
                        {
                            neighbourAmount++;
                        }
                        Draw();
                    }
                }


                if (neighbourAmount == 2)
                {
                    if (m_Old[x, y] >= 16)
                    {
                        byte alive = 16;
                        int value = neighbourAmount + alive;
                        m_Update[x, y] = (byte)value;
                    }
                    else if (m_Old[x, y] < 16)
                    {
                        int value = neighbourAmount;
                        m_Update[x, y] = (byte)value;
                    }
                }

                if (neighbourAmount == 3)
                {
                    byte alive = 16;
                    int value = neighbourAmount + alive;
                    m_Update[x, y] = (byte)value;

                }

                //Removed >4 for cave generator
                if (neighbourAmount <= 2)
                {
                    int value = neighbourAmount;
                    m_Update[x, y] = (byte)value;
                }

                //yield return new WaitForEndOfFrame();
            }
        }
        m_Draw = (byte[,])m_Update.Clone();
        if (m_ItterationsLeft > 0)
        {
            m_ItterationsLeft--;
            yield return StartCoroutine(UpdateGameOfLife());
        }
    }

    IEnumerator UpdateGameOfLifeOres()
    {
        for (int x = 0; x < m_Widht; x++)
        {
            for (int y = 0; y < m_Height; y++)
            {
                byte neighbourAmount = 0;

                if (x - 1 >= 0 && x + 1 < m_Widht)
                {
                    if (y - 1 >= 0 && y + 1 < m_Height)
                    {
                        //LeftUp
                        if (m_Old[x - 1, y + 1] >= 16)
                        {
                            neighbourAmount++;
                        }
                        //Up
                        if (m_Old[x, y + 1] >= 16)
                        {
                            neighbourAmount++;
                        }
                        //RightUp
                        if (m_Old[x + 1, y + 1] >= 16)
                        {
                            neighbourAmount++;
                        }
                        //Left
                        if (m_Old[x - 1, y] >= 16)
                        {
                            neighbourAmount++;
                        }
                        //Right
                        if (m_Old[x + 1, y] >= 16)
                        {
                            neighbourAmount++;
                        }
                        //LeftDown
                        if (m_Old[x - 1, y - 1] >= 16)
                        {
                            neighbourAmount++;
                        }
                        //Down
                        if (m_Old[x, y - 1] >= 16)
                        {
                            neighbourAmount++;
                        }
                        //RightDown
                        if (m_Old[x + 1, y - 1] >= 16)
                        {
                            neighbourAmount++;
                        }
                        Draw();
                    }
                }





                if (neighbourAmount == 2)
                {
                    if (m_Old[x, y] >= 16)
                    {
                        byte alive = 0;
                        if (m_Old[x, y] < 15)
                        {
                            //Set Alive if not all ores are filled.
                            if (m_CurrentOres < m_MaxOres)
                            {
                                m_CurrentOres++;
                                alive = 16;
                            }
                        }
                        if (m_Old[x, y] > 16) { alive = 16; }
                        int value = neighbourAmount + alive;
                        m_Update[x, y] = (byte)value;
                    }
                    else if (m_Old[x, y] < 16)
                    {
                        int value = neighbourAmount;
                        m_Update[x, y] = (byte)value;
                    }
                }

                if (neighbourAmount == 3)
                {
                    byte alive = 0;
                    if (m_Old[x, y] < 15)
                    {
                        //Set Alive if not all ores are filled.
                        if(m_CurrentOres < m_MaxOres)
                        {
                            m_CurrentOres++;
                            alive = 16;
                        }
                    }
                    if(m_Old[x,y] > 16) { alive = 16; }
                    int value = neighbourAmount + alive;
                    m_Update[x, y] = (byte)value;


                }

                //Removed >4 for cave generator
                if (neighbourAmount <= 2)
                {
                    if(m_Old[x,y] > 16)
                    {
                        m_CurrentOres--;
                    }
                    int value = neighbourAmount;
                    m_Update[x, y] = (byte)value;
                }

                //yield return new WaitForEndOfFrame();

            }
        }
        m_Draw = (byte[,])m_Update.Clone();
        if(m_ItterationsLeft > 0)
        {
            m_ItterationsLeft--;
            yield return StartCoroutine(UpdateGameOfLifeOres());
        }
    }

    void Draw()
    {

        for (int x = 0; x < m_Widht; x++)
        {
            for (int y = 0; y < m_Height; y++)
            {
                if (m_Draw[x, y] >= 16)
                {
                    m_Texture.SetPixel(x, y, Color.white);
                }
                if (m_Draw[x, y] <= 15)
                {
                    m_Texture.SetPixel(x, y, Color.black);
                }
            }
        }

        m_Texture.Apply();
        m_Old = (byte[,])m_Draw.Clone();

        m_Material.SetTexture("_MainTex", m_Texture);

    }

    public byte[,] data { get { return m_Draw; } }
}