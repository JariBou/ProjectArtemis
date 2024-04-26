using System;
using System.Collections;
using System.Collections.Generic;
using _project._Tests;
using UnityEngine;

public class EnemyTestScript : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.GetComponent<ProjectileTestScript>())
        {
            Destroy(other.gameObject);
        }
    }
}
