using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogoFade : MonoBehaviour {

    [SerializeField]
    private SpriteRenderer m_Sprite;

	// Use this for initialization
	void Start () {
        StartCoroutine(FadeLogo());
        MusicManager.instance.PlayMusic(Music.Start, false);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator FadeLogo()
    {
        while (m_Sprite.color.a > 0)
        {
            m_Sprite.color = new Color(m_Sprite.color.r, m_Sprite.color.r, m_Sprite.color.b, m_Sprite.color.a - 0.03f);
            transform.position += Vector3.up * 0.05f;
            yield return new WaitForSeconds(0.1f);
        }
    }
}
