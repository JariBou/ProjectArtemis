using _project._Tests;
using UnityEngine;

public class EnemyTestScript : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<ProjectileTestScript>())
        {
            other.gameObject.GetComponent<ProjectileTestScript>().Release();
        }
    }
}
