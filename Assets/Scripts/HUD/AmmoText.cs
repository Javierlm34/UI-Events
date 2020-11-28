using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_Text))]
public class AmmoText : MonoBehaviour
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
    }
    private void OnEnable()
    {
        player.OnAmmoChanged += HandleFire;
    }

    void HandleFire(int ammo)
    {
        UpdateText(ammo, player.maxAmmo);
    }
 
    void UpdateText(int ammo, int maxAmmo)
    {
        text.text = $"{ammo}/{maxAmmo}";
    }
}
