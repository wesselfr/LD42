using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerText : MonoBehaviour {
    [SerializeField]
    private GameObject m_TextObject;
	// Use this for initialization
	void Start () {
        m_TextObject.SetActive(false);
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        m_TextObject.SetActive(true);
    }
}
