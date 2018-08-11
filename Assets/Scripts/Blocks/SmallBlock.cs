using UnityEngine;
using System.Collections;

public enum BlockState
{
    Normal,
    Mined
}

public class SmallBlock : MonoBehaviour
{
    [SerializeField]
    private BigBlock m_BigBlock;

    private BlockState m_State = BlockState.Normal;

    // Use this for initialization
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void MineBlock()
    {
        m_State = BlockState.Mined;
        m_BigBlock.UpdateBlock();
    }

    public BlockState state { get { return m_State; } }
}
