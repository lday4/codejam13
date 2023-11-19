using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{

    public float cooldown = 1.5f;
    private float currentCooldown = 0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        currentCooldown = currentCooldown > 0 ? currentCooldown -= Time.deltaTime : 0;
    }

    public void shoot(Vector2 position, Vector2 targetPosition, GameObject projectile, float speed) {
        
        if (currentCooldown <= 0) {
            Vector2 directionToTarget = targetPosition - position;
            Vector2 normalizedDirectionToTarget = directionToTarget.normalized;
            Quaternion rotation = Quaternion.LookRotation(Vector3.forward, directionToTarget);

            GameObject proj = Instantiate(projectile, position + normalizedDirectionToTarget, rotation);
            Projectile projectileScript = proj.GetComponent<Projectile>();

            if (projectileScript != null)
            {
                projectileScript.direction = normalizedDirectionToTarget;
                projectileScript.speed = speed;
            }
            
            currentCooldown = cooldown;
        }
    }
}
