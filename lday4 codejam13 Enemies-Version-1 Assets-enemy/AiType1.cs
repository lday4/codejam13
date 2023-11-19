using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiType11 : MonoBehaviour
{

    public float speed = 3f;
    public float projectileSpeed = 3f;
    public LayerMask collisionLayer;
    public GameObject target;
    public GameObject projectile;
    public float previousXCheck = 1f;

    private float moveDirection = 1f;
    private float previousX;
    private Collider2D collider;
    private Rigidbody2D rb;
    private float previousXCheckTimer = 0;
    private EnemyShoot enemyShoot;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        previousX = transform.position.x;
        collider = GetComponent<Collider2D>();
        enemyShoot = GetComponent<EnemyShoot>();
    }

    // Update is called once per frame
    void Update()
    {
        if (EnemyUtility.canSeeTarget(transform.position, target.transform.position, collisionLayer)) {
            enemyShoot.shoot(transform.position, target.transform.position, projectile, projectileSpeed);
        } else {
            previousXCheckTimer = previousXCheckTimer > 0 ? previousXCheckTimer -= Time.deltaTime : 0;
            move();
        }
    }

    void move() {
        rb.velocity = new Vector2(speed * moveDirection, rb.velocity.y);
        if (previousXCheckTimer <= 0 && previousX == transform.position.x) {
            moveDirection = -moveDirection;
            previousXCheckTimer = previousXCheck;
        } else if (previousXCheckTimer <= 0) {
            previousX = transform.position.x;
            previousXCheckTimer = previousXCheck;
        }
    }
}
