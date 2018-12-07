using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyStats", menuName = "Enemy/EnemyStats", order = 0)]
public class EnemyStats : ScriptableObject
{
    public EnemyMovement enemyMovement;
    public enum EnemyMovement { ground, flying };
    public WaveMovement waveMovement;
    public enum WaveMovement { normal, swarm };
    public EnemyAttack enemyAttack;
    public enum EnemyAttack { melee, range };
    public int health;
    //other Stats
    public void AllEnums()
    {
        if (enemyMovement == EnemyMovement.ground)
        {

        }
        else if (enemyMovement == EnemyMovement.flying)
        {

        }


        if (waveMovement == WaveMovement.normal)
        {

        }
        else if(waveMovement == WaveMovement.swarm)
        {

        }

        if (enemyAttack == EnemyAttack.melee)
        {

        }
        else if (enemyAttack == EnemyAttack.range)
        {

        }
    }
}
