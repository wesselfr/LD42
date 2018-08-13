using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NotEnoughText : MonoBehaviour
{

    public TextMeshProUGUI _Text;
    [SerializeField] float _LerpSpeed;
    public float _Alpha;

    void Start()
    {
        _Text = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        _Alpha = Mathf.Lerp(_Alpha, 0, _LerpSpeed * Time.deltaTime);
        _Text.color = new Color(_Text.color.r, _Text.color.g, _Text.color.b, _Alpha);
    }
}
