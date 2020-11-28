using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (Rigidbody))]
public class Bullets : MonoBehaviour, IObjectPoolNotifier
{
    [SerializeField]
    [Range(0, 100f)]
    float limit;

    public Vector3 origin;

    private Rigidbody _rb;

    private Rigidbody rb
    {
        get
        {
            if (_rb == null)
            {
                _rb = gameObject.GetComponent<Rigidbody>();
            }
            return _rb;
        }
    }
    void onEnable()
    {
        origin = transform.position;
    }

    private void FixedUpdate()
    {
        float distance = Vector3.Distance(origin, transform.position);

        if (distance >= limit)
        {
            gameObject.ReturnToPool();
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        gameObject.ReturnToPool();
        var enemy = collision.other.GetComponent<Enemy>();
        if (enemy != null)
            enemy.Hit();
    }

    public void onCreatedOrDequeuedFromPool(bool created)
    {
    }
    public void onEnqueuedtoPool()
    {
        transform.rotation = Quaternion.identity;
        rb.velocity = Vector3.zero;

    }
}
