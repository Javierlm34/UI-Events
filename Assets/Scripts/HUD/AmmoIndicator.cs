using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoIndicator : MonoBehaviour
{
    [SerializeField]
    GameObject ammoSlot;

    [SerializeField]
    PlayerShoot player;

    List<GameObject> ammoSlots = new List<GameObject>();
    private void Start()
    {
        for (int i = 0; i < player.maxAmmo; i++)
        {
            var newSlot = Instantiate(ammoSlot);
            newSlot.transform.SetParent(this.transform);
            ammoSlots.Add(newSlot);
        }
    }

    private void OnEnable()
    {
        player.OnAmmoChanged += HandleAmmoChanged;
    }
    private void OnDisable()
    {
        player.OnAmmoChanged -= HandleAmmoChanged;
    }
    void HandleAmmoChanged(int ammo)
    {
        for (int i = 0; i < ammoSlots.Count; i++)
        {
            var canvasGroup = ammoSlots[i].GetComponent<CanvasGroup>();
            canvasGroup.alpha = i < ammo ? 1 : 0.5f;
        }
        
    }
    private void FixedUpdate()
    {
        
    }
}
