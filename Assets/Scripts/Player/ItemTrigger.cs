using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemTrigger : MonoBehaviour {

    public Animator _TransformAnim;
    public LayerMask _ItemLayer;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Item"))
        {
            collision.gameObject.GetComponent<DroppedItem>().DestroyItem();
            _TransformAnim.Play("Eating");
        }
    }
}
