using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Contexts;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShoot : MonoBehaviour
{

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

    bool canShoot = true;

    bool isShooting = false;

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


    void Shoot()
    {
        if (canShoot)
        {
            var bullet = pool.GetObject();
            bullet.transform.position = shootpoint.position;
            var rigidBody = bullet.GetComponent<Rigidbody>();
            rigidBody.AddForce(shootpoint.right * force, ForceMode.Impulse);
            canShoot = false;
            rateAcc = 1;
        }
    }
}
