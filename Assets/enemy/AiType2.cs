using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiType2 : MonoBehaviour
{

    public float projectileSpeed;
    public LayerMask collisionLayer;
    public GameObject target;
    public GameObject projectile;

    private EnemyShoot enemyShoot;

    // Start is called before the first frame update
    void Start()
    {
        enemyShoot = GetComponent<EnemyShoot>();
    }

    // Update is called once per frame
    void Update()
    {
        if (EnemyUtility.canSeeTarget(transform.position, target.transform.position, collisionLayer)) {
            enemyShoot.shoot(transform.position, target.transform.position, projectile, projectileSpeed);
        }
    }
}
