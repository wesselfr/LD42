using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndEventManager : MonoBehaviour {

    [SerializeField]
    private GameObject m_Face1, m_Face2;

    [SerializeField]
    private GameObject m_Credits;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Contains("Player"))
        {
            MusicManager.instance.PlayEndMusic();
            //MusicManager.instance.OnMusicDone += SlowMo;
            SlowMo();
            StartCoroutine(Credits());
            StartCoroutine(SwitchFace());
        }
    }

    public IEnumerator SwitchFace()
    {
        int i = 50;
        while (i > 0)
        {
            m_Face1.SetActive(true);
            m_Face2.SetActive(false);
            yield return new WaitForSecondsRealtime(0.50f);
            m_Face1.SetActive(false);
            m_Face2.SetActive(true);
            yield return new WaitForSecondsRealtime(0.50f);
            i--;
        }

        Application.Quit();
        Application.Quit();
    }

    public IEnumerator Credits()
    {
        m_Credits.transform.position += transform.up * 0.1f;
        yield return new WaitForSecondsRealtime(0.1f);
        yield return StartCoroutine(Credits());
    }

    public void SlowMo()
    {
        Time.timeScale = 0.2f;
        Debug.Log("End Event");
    }
}
