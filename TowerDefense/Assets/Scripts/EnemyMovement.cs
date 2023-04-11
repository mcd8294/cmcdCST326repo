using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Enemy))]
public class EnemyMovement : MonoBehaviour
{
    private Enemy enemy;
    private Transform target;
    private int wavePointIndex = 0;
    private NavMeshAgent navAgent;

    void Awake()
    {
        navAgent = GetComponent<NavMeshAgent>();
        enemy = GetComponent<Enemy>();
        target = Waypoints.points[0];
        navAgent.SetDestination(target.position);
    }
    void Update()
    {
        Vector3 dir = target.position - transform.position;
        if (navAgent.remainingDistance <= 0.2f)
        {
            EndPath();
        }
    }
    void EndPath()
    {
        PlayerStats.Lives--;
        WaveSpawner.EnemiesAlive--;
        Destroy(gameObject);
    }
}
