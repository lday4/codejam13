using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EnemyUtility
{
    public static bool canSeeTarget(Vector2 position, Vector2 targetPosition, LayerMask collisionLayer) {
        Vector2 directionToTarget = targetPosition - position;
        float distanceToTarget = directionToTarget.magnitude;
        directionToTarget.Normalize();
        RaycastHit2D[] hits = Physics2D.RaycastAll(position + directionToTarget, directionToTarget, distanceToTarget - 1f, collisionLayer);
         if (hits.Length == 0) {
            return true;
        }
        return false;
    }

    
}
