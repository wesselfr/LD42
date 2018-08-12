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

    [SerializeField]
    private BlockState m_State = BlockState.Normal;

    public void MineBlock()
    {
        m_State = BlockState.Mined;
        m_BigBlock.UpdateBlock();
        GetComponent<BoxCollider2D>().enabled = false;
        //GetComponent<CircleCollider2D>().enabled = false;
        GameObject itemToDrop = m_BigBlock.GetDroppableItem();
    }

    public void SpawnDroppedItem()
    {
        Instantiate(m_BigBlock.GetDroppableItem(), transform.position, Quaternion.identity);
    }

    public BlockState state { get { return m_State; } }
}
