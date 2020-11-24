using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    float hp = 10;

    public void Hit()
    {
        hp -= 1;
        if (hp <= 0)
        {
            Destroy(gameObject);
        }
    }
}
