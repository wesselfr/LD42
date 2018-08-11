using UnityEngine;
using System.Collections;

public enum BlockState
{
    Normal,
    Eaten
}

public class SmallBlock : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer m_Renderer;

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

    public void EatBlock()
    {
        m_State = BlockState.Eaten;
        m_BigBlock.UpdateBlock();
    }

    public void SetSprite(Sprite sprite)
    {
        m_Renderer.sprite = sprite;
    }

    public BlockState state { get { return m_State; } }
}
