using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    public float speed = 30f;
    public Vector2 direction = new Vector2(0, 0);
        public LayerMask collisionLayer;

    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }


    void Update()
    {
        rb.velocity = direction * speed;
        if (scan()) {
            Destroy(gameObject);
        }
    }

    public void OnBecameInvisible()
    {
        DestroyObject(gameObject);
    }

    void OnCollisionEnter2D (Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Collision"))
        {
            Destroy(gameObject);
        }
    }

    bool scan() {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, 0.2f, collisionLayer);
        if (hit.collider != null) {
            return true;
        }
        return false;
    }
}
