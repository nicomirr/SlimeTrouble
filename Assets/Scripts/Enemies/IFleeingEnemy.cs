using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IFleeingEnemy 
{
    public bool ShouldFlee();

    public Vector3 TryGetRadialFleePosition(Vector3 targetPosition, float currentDistanceToTarget);
}
