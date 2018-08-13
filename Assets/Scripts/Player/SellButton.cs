using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SellButton : MonoBehaviour
{
    public void Button()
    {
        PlayerContainer.Instance.SellGems();
    }
}
