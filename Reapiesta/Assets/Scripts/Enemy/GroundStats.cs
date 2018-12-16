using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "EnemyBaseStats", menuName = "Enemy/EnemyBaseStats/GroundStats", order = 0)]
public class GroundStats : ScriptableObject
{
    public float walkSpeed;
    public float runSpeed;
}
