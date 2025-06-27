using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyFlee : MonoBehaviour, IFleeingEnemy
{
    [SerializeField] private int directionsCount = 16;
    [SerializeField] private float fleeDistance = 4f;
    [SerializeField] private float sampleRadius = 1.5f;

    private bool mustFlee;

    public bool ShouldFlee()
    {
        return mustFlee;
    }

    public Vector3 TryGetRadialFleePosition(Vector3 targetPosition, float currentDistanceToTarget)
    {         
        float angleStep = 360f / directionsCount;
        Vector3 idealFleeDirection = (transform.position - targetPosition).normalized;

        Vector3? bestFallback = null;
        float maxDistance = currentDistanceToTarget;
        float bestDot = -1f;

        for (int i = 0; i < directionsCount; i++)
        {
            float angle = angleStep * i;

            Vector2 direction = Quaternion.Euler(0, 0, angle) * Vector2.right;
            Vector3 candidate = this.transform.position + (Vector3)(direction * fleeDistance);

            if (NavMesh.SamplePosition(candidate, out NavMeshHit hit, sampleRadius, NavMesh.AllAreas))
            {
                Vector3 candidatePos = hit.position;
                float newDistance = Vector3.Distance(candidate, targetPosition);
                Vector3 candidateDirection = (hit.position - this.transform.position).normalized;               
                float dot = Vector3.Dot(idealFleeDirection, candidateDirection);

                if(newDistance > currentDistanceToTarget && dot > 0.5f)
                {
                    Vector3 fleePosition = hit.position;
                    return fleePosition;
                }                

                if(newDistance > maxDistance || (Mathf.Approximately(newDistance, maxDistance) && dot > bestDot))
                {
                    maxDistance = newDistance;
                    bestDot = dot;
                    bestFallback = candidatePos;
                }
            }
        }

        return bestFallback ?? this.transform.position;
    }
}
