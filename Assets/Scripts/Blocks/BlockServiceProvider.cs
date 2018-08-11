using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockServiceProvider : MonoBehaviour {

    public static BlockServiceProvider instance;



	// Use this for initialization
	void Start () {
        instance = this;
	}
}
