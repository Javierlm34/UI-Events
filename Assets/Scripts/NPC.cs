using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(Rigidbody))]
public class NPC : MonoBehaviour
{
    [SerializeField]
    Transform[] targetList;

    [SerializeField]
    Transform target;

    [SerializeField]
    [Range(0, 10)]
    float tolerance = 0.2f;

    [SerializeField]
    [Range(.1f, 2)]
    float patrolSpeed;

    [SerializeField]
    [Range(.1f, 10)]
    float followSpeed;

    [SerializeField]
    string followTag;

    IEnumerator behaviour;

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
    private void Start()
    {
        behaviour = Patrol(Random.Range(0,10));
        StartCoroutine(behaviour);

    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == followTag)
        {
            if (behaviour != null)
            {
                StopCoroutine(behaviour);
            }
            target = other.gameObject.transform;
            behaviour = Follow();
            StartCoroutine(behaviour);

        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == followTag)
        {
            if (other.gameObject.tag == followTag)
            {
                if (behaviour != null)
                {
                    StopCoroutine(behaviour);
                }
                behaviour = Patrol(Random.Range(0, 10));
                StartCoroutine(behaviour);

            }
        }
    }


    Transform adquireTarget()
    {
        return targetList[Random.Range(0,targetList.Length)];
    }

    IEnumerator Follow()
    {
        var wait = new WaitForFixedUpdate();
        while (true)
        {
            float distance = Vector3.Distance(transform.position, target.position);

            while (distance > tolerance)
            {
                distance = Vector3.Distance(transform.position, target.position);

                MoveToTarget(followSpeed);
                yield return wait;
            }
            yield return wait;
        }
    }
    IEnumerator Patrol(float waitTime)
    {
        var wait = new WaitForFixedUpdate();
        while (true)
        {
            target = adquireTarget();


            float distance = Vector3.Distance(transform.position, target.position);


            while (distance > tolerance)
            {
                distance = Vector3.Distance(transform.position, target.position);
                //Debug.Log($"Distance: {distance}");
                MoveToTarget(patrolSpeed);
                yield return wait;
            }
            yield return wait;
        }
       
    }
    void MoveToTarget(float speed)
    {
        Vector3 movePosition = Vector3.Lerp(transform.position, target.position, speed * Time.fixedDeltaTime);
        rb.MovePosition(movePosition);
    }
}
