using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayerX : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(PlayerContainer.Instance.transform.position.x, transform.position.y, transform.position.z);
    }
}
