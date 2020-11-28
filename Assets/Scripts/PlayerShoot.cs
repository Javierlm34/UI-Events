using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Contexts;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class PlayerShoot : MonoBehaviour
{
    public delegate void ReloadEvent(bool reloading);
    public delegate void AmmoEvent(int x);
    public event AmmoEvent OnAmmoChanged;
    public event ReloadEvent OnReload;

    [SerializeField]
    ObjectPool pool;

    [SerializeField]
    Transform shootpoint;

    [SerializeField]
    [Range (0, 100f)]
    float force;

    [SerializeField]
    [Range(0, 0.5f)]
    float rate = 0;

    [SerializeField]
    float rateAcc = 0;

    [SerializeField]
    Image panel;

    bool canShoot = true;

    bool isShooting = false;

    public int maxAmmo = 10;

    private int _ammo;

    public bool isReloading = false;

    public int ammo
    {
        get => _ammo;
        set
        {
            if(value!= _ammo)
            {
                _ammo = value;
                OnAmmoChanged?.Invoke(_ammo);
            }
        }
    }

    public void Start()
    {
        panel.color = UnityEngine.Color.green;
        ammo = maxAmmo;
    }
    private void Update()
    {
        rateAcc = Mathf.Max(0, rateAcc - rate);
        if (rateAcc == 0)
        {
            canShoot = true;
        }

        if (isShooting)
        {
            Shoot();
        }
    }

    public void HandleInteract(InputAction.CallbackContext callbackContext)
    {
        isShooting = callbackContext.ReadValue<float>() > 0; 
    }

    IEnumerator Reload()
    {
        var wait = new WaitForSeconds(0.3f);
        isReloading = true;
        OnReload?.Invoke(true);

        panel.color = UnityEngine.Color.red;
        while (ammo < maxAmmo)
        {
            ammo += 1;
            yield return wait;
        }
        yield return wait;
        OnReload?.Invoke(false);
        isReloading = false;
        panel.color = UnityEngine.Color.green;
    }
    void Shoot()
    {
        if (ammo <= 0)
        {
            StartCoroutine(Reload());
            return;
        }
        if (canShoot && !isReloading)
        {
            var bullet = pool.GetObject();
            bullet.transform.position = shootpoint.position;
            var rigidBody = bullet.GetComponent<Rigidbody>();
            rigidBody.AddForce(shootpoint.right * force, ForceMode.Impulse);
            canShoot = false;
            rateAcc = 1;
            ammo -= 1;
        }
    }
}
