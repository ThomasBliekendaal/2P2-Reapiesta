using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    [SerializeField] EnemyStats stats;
    void Start()
    {
        print(stats.enemyAttack);
        print(stats.enemyMovement);
        print(stats.waveMovement);
    }

    void Update()
    {
        
    }

    void Walking()
    {

    }

    void Event()
    {

    }

    void Melee()
    {

    }

    void Range()
    {

    }
}
