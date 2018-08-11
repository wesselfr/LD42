using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BlockStates
{
    Normal,
    LeftTopMissing,
    RightTopMissing,
    LeftBottomMissing,
    RightBottomMissing,
    OnlyBottom,
    OnlyTop,
    OnlyRight,
    OnlyLeft,
    DiagonalLeftBottomRightTop,
    DiagonalRightBottomLeftTop,
    OnlyLeftTop,
    OnlyRightTop,
    OnlyLeftBottom,
    OnlyRightBottom,
    Mined
}

[CreateAssetMenu(fileName = "New Block", menuName = "Block/Create")]
public class GenericBlockData : ScriptableObject {

    [Header("Basics")]
    [SerializeField]
    private string m_BlockName;
    [SerializeField]
    private int m_ID;
    //[SerializeField]
    //private Sprite m_Texture;
    [SerializeField]
    private int m_HealthPoints;

    [Header("Art")]
    [SerializeField]
    private Sprite m_Normal;
    [SerializeField]
    private Sprite m_LeftTopMissing;
    [SerializeField]
    private Sprite m_RightTopMissing;
    [SerializeField]
    private Sprite m_LeftBottomMissing;
    [SerializeField]
    private Sprite m_RightBottomMissing;
    [SerializeField]
    private Sprite m_OnlyBottom;
    [SerializeField]
    private Sprite m_OnlyTop;
    [SerializeField]
    private Sprite m_OnlyRight;
    [SerializeField]
    private Sprite m_OnlyLeft;
    [SerializeField]
    private Sprite m_DiagonalLeftBottomRightTop;
    [SerializeField]
    private Sprite m_DiagonalRightBottomLeftTop;
    [SerializeField]
    private Sprite m_OnlyLeftTop;
    [SerializeField]
    private Sprite m_OnlyRightTop;
    [SerializeField]
    private Sprite m_OnlyLeftBottom;
    [SerializeField]
    private Sprite m_OnlyRightBottom;


    #region Accesors
    public string blockName { get { return m_BlockName; } }
    public int id { get { return m_ID; } }
    public int health { get { return m_HealthPoints; } }
    #endregion

    public Sprite GetSprite(BlockStates state)
    {
        switch (state)
        {
            default:
                return null;
                
            case BlockStates.Normal:
                return m_Normal;
            case BlockStates.LeftTopMissing:
                return m_LeftTopMissing;
            case BlockStates.RightTopMissing:
                return m_RightTopMissing;
            case BlockStates.LeftBottomMissing:
                return m_LeftBottomMissing;
            case BlockStates.RightBottomMissing:
                return m_RightBottomMissing;
            case BlockStates.OnlyBottom:
                return m_OnlyBottom;
            case BlockStates.OnlyTop:
                return m_OnlyTop;
            case BlockStates.OnlyLeft:
                return m_OnlyLeft;
            case BlockStates.OnlyRight:
                return m_OnlyRight;
            case BlockStates.DiagonalLeftBottomRightTop:
                return m_DiagonalLeftBottomRightTop;
            case BlockStates.DiagonalRightBottomLeftTop:
                return m_DiagonalRightBottomLeftTop;
            case BlockStates.OnlyLeftTop:
                return m_OnlyLeftTop;
            case BlockStates.OnlyRightTop:
                return m_OnlyRightTop;
            case BlockStates.OnlyLeftBottom:
                return m_OnlyLeftBottom;
            case BlockStates.OnlyRightBottom:
                return m_OnlyRightBottom;

        }
    }
}
