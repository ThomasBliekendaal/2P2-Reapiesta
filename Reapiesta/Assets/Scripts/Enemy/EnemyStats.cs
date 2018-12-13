using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyMovement { ground, flying };
public enum WaveMovement { normal, swarm };
public enum EnemyAttack { melee, range };
[CreateAssetMenu(fileName = "EnemyBaseStats", menuName = "Enemy/EnemyBaseStats/BaseStats", order = 0)]
public class EnemyStats : ScriptableObject
{
    public EnemyMovement enemyMovement;
    public WaveMovement waveMovement;
    public EnemyAttack enemyAttack;
    public float viewRadius;
    public int health;
}
