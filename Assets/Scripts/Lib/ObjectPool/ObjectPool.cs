using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface IObjectPoolNotifier
{
    void onCreatedOrDequeuedFromPool(bool created);
    void onEnqueuedtoPool();
}
public class ObjectPool : MonoBehaviour
{
    [SerializeField]
    GameObject prefab;

    private Queue<GameObject> inactiveObjects = new Queue<GameObject>();

    public GameObject GetObject ()
    {
        if(inactiveObjects.Count > 0)
        {
            var pooledObject = inactiveObjects.Dequeue();
            pooledObject.transform.parent = null;
            pooledObject.SetActive(true);

            var notifiers = pooledObject.GetComponents<IObjectPoolNotifier>();

            foreach (var notifier in notifiers)
            {
                notifier.onCreatedOrDequeuedFromPool(false);
            }
            return pooledObject;
        }
        else
        {
            var createdObject = Instantiate(prefab);
            var poolTag = createdObject.AddComponent<PooledObject>();
            poolTag.owner = this;
            poolTag.hideFlags = HideFlags.HideInInspector;
            var notifiers = createdObject.GetComponents<IObjectPoolNotifier>();

            foreach (var notifier in notifiers)
            {
                notifier.onCreatedOrDequeuedFromPool(true);
            }
            return createdObject;
        }
    }

    public void ReturnObject(GameObject gameObject)
    {
        var notifiers = gameObject.GetComponents<IObjectPoolNotifier>();

        foreach (var notifier in notifiers)
        {
            notifier.onEnqueuedtoPool();
        }

        gameObject.SetActive(false);
        gameObject.transform.parent = this.transform;
        inactiveObjects.Enqueue(gameObject);
    }
}
