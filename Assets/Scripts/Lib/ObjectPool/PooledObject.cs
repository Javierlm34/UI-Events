using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PooledGameObjectExtensions
{
    public static void ReturnToPool (this GameObject gameObject)
    {
        var PooledComponent = gameObject.GetComponent<PooledObject>();

        if(PooledComponent == null)
        {

            return;
        }

        PooledComponent.owner.ReturnObject(gameObject);
    }
}
public class PooledObject : MonoBehaviour
{
    public ObjectPool owner;
}
