using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyMovement { ground, flying };
public enum WaveMovement { normal, swarm };
public enum EnemyAttack { melee, range };
[CreateAssetMenu(fileName = "EnemyBaseStats", menuName = "Enemy/EnemyBaseStats/BaseStats", order = 0)]
public class BaseStats : ScriptableObject
{
    [Header("TypeOf")]
    public EnemyMovement enemyMovement;
    public WaveMovement waveMovement;
    public EnemyAttack enemyAttack;
    [Header("SphereCollider")]
    public float viewRadius;
    public bool isTrigger;
    [Header("///")]
    public string tag;
    public LayerMask layer;
    public int health;
}
