using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    public float speed = 4f;
    public float jumpForce = 20f;
    public float slideSpeed = 2f;
    public float dashForce = 200f;
    public LayerMask collisionLayer;
    public int totalJumps = 1;


    private Rigidbody2D rb;
    private Collider2D collider;
    private float wallJumpedTimer = 0f;
    private float dashTimer = 0f;
    private int currentJumps = 0;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.drag = 2f; // Just feels good
        collider = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // Inputs
        bool left = Input.GetKey(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow);
        bool right = Input.GetKey(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow);
        int moveDirection = (right ? 1 : 0) - (left ? 1 : 0);
        bool jump = Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space);;
        bool dash = Input.GetKeyDown(KeyCode.LeftShift);
        bool slideLeft = moveDirection == -1 && leftWallHit() && rb.velocity.y < 0;
        bool slideRight = moveDirection == 1 && rightWallHit() && rb.velocity.y < 0;

        // perform slides off walls, else only update x position if wallJumpedTimer <= 0
        if (slideLeft || slideRight) {
            DoSlide();
        } 
        else if (wallJumpedTimer <= 0 && dashTimer <= 0) { // else if because dont want to move while sliding
            DoMove(moveDirection);
        }
        if (dash && dashTimer <= 0) {
            DoDash(moveDirection);
        }
        if (jump) {
            DoJump(slideLeft, slideRight);
        }

        IncrementTimers();
    }

    void DoJump(bool slideLeft, bool slideRight) {
        // If sliding should jump left or right
        if (slideLeft || slideRight) {
            currentJumps = totalJumps;
            rb.AddForce(new Vector2((slideRight ? -1 : 1) * jumpForce, jumpForce), ForceMode2D.Impulse);
            wallJumpedTimer = 0.25f;
        }
        // If grounded should jump
        else if (IsGrounded()) {
            currentJumps = totalJumps;
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
        // If not grounded check for double jumps 
        else if (currentJumps > 0) {
            currentJumps--;
            rb.AddForce(Vector2.up * (jumpForce * 0.75f), ForceMode2D.Impulse);
        }
        // If none of these conditions are satisfied don't jump
    }

    void DoDash(int moveDirection) {
        rb.AddForce(new Vector2(moveDirection * dashForce, 0), ForceMode2D.Impulse);
        dashTimer = 0.25f;
    }

    void DoSlide() {
        rb.velocity = new Vector2(0, -slideSpeed);
    }

    void DoMove(int moveDirection) {
        rb.velocity = new Vector2(speed * moveDirection, rb.velocity.y);
    }

    void IncrementTimers() {
        if (wallJumpedTimer > 0) {
            if (IsGrounded()) {
                wallJumpedTimer = 0;
            }
            wallJumpedTimer -= Time.deltaTime;
        }
        dashTimer = dashTimer > 0 ? dashTimer -= Time.deltaTime : 0;
    }

    bool IsGrounded() {
        RaycastHit2D leftSide = Physics2D.Raycast(new Vector2(collider.bounds.max.x, collider.bounds.min.y), Vector2.down, 0.1f, collisionLayer);
        RaycastHit2D rightSide = Physics2D.Raycast(new Vector2(collider.bounds.min.x, collider.bounds.min.y), Vector2.down, 0.1f, collisionLayer);
        if (leftSide.collider != null || rightSide.collider != null) {
            return true;
        }
        return false;
    }

    bool leftWallHit() {
        RaycastHit2D upper = Physics2D.Raycast(new Vector2(collider.bounds.min.x, collider.bounds.max.y), Vector2.left, 0.1f, collisionLayer);
        RaycastHit2D lower = Physics2D.Raycast(new Vector2(collider.bounds.min.x, collider.bounds.min.y), Vector2.left, 0.1f, collisionLayer);
        if (upper.collider != null || lower.collider != null) {
            return true;
        }
        return false;
    }

    bool rightWallHit() {
        RaycastHit2D upper = Physics2D.Raycast(new Vector2(collider.bounds.max.x, collider.bounds.max.y), Vector2.right, 0.1f, collisionLayer);
        RaycastHit2D lower = Physics2D.Raycast(new Vector2(collider.bounds.max.x, collider.bounds.min.y), Vector2.right, 0.1f, collisionLayer);
        if (upper.collider != null || lower.collider != null) {
            return true;
        }
        return false;
    }
}
