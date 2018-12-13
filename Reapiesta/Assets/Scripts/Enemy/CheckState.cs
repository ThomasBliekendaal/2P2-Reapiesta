using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckState : MonoBehaviour
{
    [SerializeField] GroundStats groundStats;
    [SerializeField] FlyingStats flyingStats;
    [SerializeField] MeleeStats meleeStats;
    [SerializeField] RangeStats rangeStats;
    public void Checkstate(EnemyStats stats, GameObject newEnemy)
    {
        switch (stats.enemyAttack)
        {
            case EnemyAttack.melee:
                //add the melee class to the gameobject
                Component newMelee = newEnemy.AddComponent(typeof(Melee));
                newMelee.GetComponent<Melee>().meleeStats = meleeStats;
                break;
            case EnemyAttack.range:
                //add the range class to the gameobject
                Component newRange = newEnemy.AddComponent(typeof(Range));
                newRange.GetComponent<Range>().rangeStats = rangeStats;
                break;
        }
        switch (stats.enemyMovement)
        {
            case EnemyMovement.ground:
                //add the ground class to the gameobject
                Component newGround = newEnemy.AddComponent(typeof(Ground));
                newGround.GetComponent<Ground>().groundStats = groundStats;
                break;
            case EnemyMovement.flying:
                //add the flying class to the gameobject
                Component newFlying = newEnemy.AddComponent(typeof(Flying));
                newFlying.GetComponent<Flying>().flyingStats = flyingStats;
                break;
        }
    }
}
