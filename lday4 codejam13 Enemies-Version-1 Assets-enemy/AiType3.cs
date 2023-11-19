using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiType3 : MonoBehaviour
{
    public float speed = 3f;
    public float projectileSpeed = 3f;
    public LayerMask collisionLayer;
    public GameObject target;
    public GameObject projectile;

    private Rigidbody2D rb;
    private Collider2D collider;
    private EnemyShoot enemyShoot;

    // Start is called before the first frame update
    void Start()
    {

        rb = GetComponent<Rigidbody2D>();
        // Flying enemy
        rb.gravityScale = 0;
        collider = GetComponent<Collider2D>();
        enemyShoot = GetComponent<EnemyShoot>();
    }

    // Update is called once per frame
    void Update()
    {
        if (EnemyUtility.canSeeTarget(transform.position, target.transform.position, collisionLayer)) {
            rb.velocity = (target.transform.position - transform.position).normalized * speed;
            enemyShoot.shoot(transform.position, target.transform.position, projectile, projectileSpeed);
        } else {
            rb.velocity = new Vector2(0f, 0f);
        }
    }
}
