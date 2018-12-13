using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "EnemyBaseStats", menuName = "Enemy/EnemyBaseStats/RangeStats", order = 0)]
public class RangeStats : ScriptableObject
{
    public float mintargetDist;
    public float forceAmount;
    public Transform bottle;
}
