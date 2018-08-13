using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour {

    [SerializeField]
    private GameObject m_Player;
	
	// Update is called once per frame
	void Update () {
        transform.position = new Vector3(Mathf.Round(m_Player.transform.position.x), transform.position.y, transform.position.z);
	}
}
