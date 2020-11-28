using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Reloading : MonoBehaviour
{
    private TMP_Text _text;

    TMP_Text text
    {
        get
        {
            if (_text == null)
            {
                _text = gameObject.GetComponent<TMP_Text>();
            }
            return _text;
        }

    }

    [SerializeField]
    PlayerShoot player;
    private void Start()
    {
        UpdateText(false);
    }
    private void OnEnable()
    {
        player.OnReload += UpdateText;
    }
    void UpdateText(bool reloading)
    {
        if (reloading)
        {
            text.text = "Reloading...";

        }
        else
        {
            text.text = "";
        }
    }
}
