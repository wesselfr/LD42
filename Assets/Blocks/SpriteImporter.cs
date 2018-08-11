using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class SpriteImporter : MonoBehaviour
{
    [SerializeField]
    private Sprite[] m_SpriteSheet;
    [SerializeField]
    private GenericBlockData m_Data;

    public Sprite[] sheet { get { return m_SpriteSheet; } }
    public GenericBlockData data { get { return m_Data; } }
}

[CustomEditor(typeof(SpriteImporter))]
public class CreateOreScript : Editor {

    public override void OnInspectorGUI()
    {
        base.DrawDefaultInspector();  
        if(GUILayout.Button("Import Art"))
        {
            Import();
        }
    }
    
    public void Import()
    {
        SpriteImporter importer = target as SpriteImporter;

        Sprite[] sprites = importer.sheet;
        EditorUtility.SetDirty(importer.data);
        importer.data.m_Normal = sprites[5];
        importer.data.m_LeftTopMissing = sprites[4];
        importer.data.m_RightTopMissing = sprites[6];
        importer.data.m_LeftBottomMissing = sprites[9];
        importer.data.m_RightBottomMissing = sprites[11];
        importer.data.m_OnlyBottom = sprites[1];
        importer.data.m_OnlyTop = sprites[14];
        importer.data.m_OnlyRight = sprites[3];
        importer.data.m_OnlyLeft = sprites[7];
        importer.data.m_DiagonalLeftBottomRightTop = sprites[13];
        importer.data.m_DiagonalRightBottomLeftTop = sprites[15];
        importer.data.m_OnlyLeftTop = sprites[12];
        importer.data.m_OnlyRightTop = sprites[8];
        importer.data.m_OnlyLeftBottom = sprites[2];
        importer.data.m_OnlyRightBottom = sprites[0];

       



    }
}
