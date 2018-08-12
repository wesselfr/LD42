using UnityEngine;
using System.Collections;

public struct BigBlockData
{
    SmallBlock[] m_SmallBlocks;
    private GenericBlockData m_Data;
    private Vector3 m_Position;
    public BigBlockData(SmallBlock[] smallBlocks, GenericBlockData data, Vector3 position)
    {
        m_SmallBlocks = smallBlocks;
        m_Data = data;
        m_Position = position;
    }

    public SmallBlock[] smallBlocks { get { return m_SmallBlocks; } set { m_SmallBlocks = value; } }
    public GenericBlockData data { get { return m_Data; } }
    public Vector3 position { get { return m_Position; } }
}

public class BigBlock : MonoBehaviour
{
    [SerializeField]
    SmallBlock[] m_SmallBlocks;
    // 0,1
    // 2,3

    [SerializeField]
    private GenericBlockData m_Data;

    [SerializeField]
    private SpriteRenderer m_Renderer;

    public void Initialize(GenericBlockData data)
    {
        m_Data = data;

    }

    public void SetBlockState(int index, BlockState state)
    {
        if (state == BlockState.Mined)
        {
            m_SmallBlocks[index].MineBlock();
        }
    }

    public GameObject GetDroppableItem()
    {
        return m_Data.dropItem;
    }

    public void EnableBlock(BigBlockData data)
    {
        m_SmallBlocks = data.smallBlocks;
        m_Data = data.data;
        transform.position = data.position;
    }

    public BigBlockData DisableBlock()
    {
        return new BigBlockData(m_SmallBlocks, m_Data, transform.position);
    }

    public void UpdateBlock()
    {
        // 0,1
        // 2,3
        BlockState leftTop = m_SmallBlocks[0].state;
        BlockState rightTop = m_SmallBlocks[1].state;
        BlockState leftBottom = m_SmallBlocks[2].state;
        BlockState rightBottom = m_SmallBlocks[3].state;

        int minedBlocks = 0;
        if(leftTop == BlockState.Mined) { minedBlocks++; }
        if(rightTop == BlockState.Mined) { minedBlocks++;}
        if(leftBottom == BlockState.Mined) { minedBlocks++; }
        if(rightBottom == BlockState.Mined) { minedBlocks++; }

        BlockStates currentState = BlockStates.Normal;

        switch (minedBlocks)
        {
            case 0:
                currentState = BlockStates.Normal;
                break;
            case 1:
                if(leftTop == BlockState.Mined)
                {
                    currentState = BlockStates.LeftTopMissing;
                }
                if(rightTop == BlockState.Mined)
                {
                    currentState = BlockStates.RightTopMissing;
                }
                if(leftBottom == BlockState.Mined)
                {
                    currentState = BlockStates.LeftBottomMissing;
                }
                if(rightBottom == BlockState.Mined)
                {
                    currentState = BlockStates.RightBottomMissing;
                }
                break;
            case 2:
                if(leftTop == BlockState.Mined && rightTop == BlockState.Mined)
                {
                    currentState = BlockStates.OnlyBottom;
                }
                if(leftBottom == BlockState.Mined && rightBottom == BlockState.Mined)
                {
                    currentState = BlockStates.OnlyTop;
                } 
                if(leftTop == BlockState.Mined && leftBottom == BlockState.Mined)
                {
                    currentState = BlockStates.OnlyRight;
                }
                if(rightTop == BlockState.Mined && rightBottom == BlockState.Mined)
                {
                    currentState = BlockStates.OnlyLeft;
                }
                if(leftBottom == BlockState.Normal && rightTop == BlockState.Normal)
                {
                    currentState = BlockStates.DiagonalLeftBottomRightTop;
                }
                if(rightBottom == BlockState.Normal && leftTop == BlockState.Normal)
                {
                    currentState = BlockStates.DiagonalRightBottomLeftTop;
                }
                break;
            case 3:
                if(leftTop == BlockState.Normal)
                {
                    currentState = BlockStates.OnlyLeftTop;
                }
                if(rightTop == BlockState.Normal)
                {
                    currentState = BlockStates.OnlyRightTop;
                }
                if(leftBottom == BlockState.Normal)
                {
                    currentState = BlockStates.OnlyLeftBottom;
                }
                if(rightBottom == BlockState.Normal)
                {
                    currentState = BlockStates.OnlyRightBottom;
                }
                break;
            case 4:
                currentState = BlockStates.Mined;
                break;
        }

        m_Renderer.sprite = m_Data.GetSprite(currentState);
    }
}
