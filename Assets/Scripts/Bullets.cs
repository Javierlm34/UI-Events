using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullets : MonoBehaviour
{
    [SerializeField]
    [Range(0, 100f)]
    float limit;

    Vector3 origin;

    void onEnable()
    {
        origin = transform.position;
    }

    private void FixedUpdate()
    {
        float distance = Vector3.Distance(origin, transform.position);

        if (distance >= limit)
        {
            Destroy(gameObject);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        var enemy = collision.other.GetComponent<Enemy>();
        enemy.Hit();
      
        Destroy(gameObject);
    }
}
