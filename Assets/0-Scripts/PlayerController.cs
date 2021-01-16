using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    public float moveSpeed = 0.001f;
    public float jumpStrength = 100f;

    private Animator animator;
    private bool isJumping;
    private float timer;
    private bool runTimer;
    private int jumpNumber;
    private bool isCheckingGround;

    private void Start() {
        animator = GetComponent<Animator>();
    }

    private void Update() {
        float hor  = Input.GetAxis("Horizontal");
        float vertical  = Input.GetAxis("Vertical");
        float jumpValue = Input.GetAxis("Jump");

        // Debug.Log(jumpValue);
        
        animator.SetFloat("walkLeftRight", hor);
        transform.position += transform.right * (moveSpeed * hor);
        
        if (hor>0) {
            GetComponent<SpriteRenderer>().flipX = false;
        } else if (hor<0) {
            GetComponent<SpriteRenderer>().flipX = true;
        }

        if (jumpValue>0 && !isJumping && jumpNumber<2) {
            isJumping = true;
            runTimer = true;
            jumpNumber++;
            animator.SetBool("jump", true);
            GetComponent<Rigidbody2D>().AddForce(transform.up * jumpStrength);
        } else {
            animator.SetBool("jump", false);
        }

        if (runTimer) {
            timer+=Time.deltaTime;
            if (timer>0.25f) {
                isJumping = false;
                isCheckingGround = true;
                // timer = 0;
                // runTimer=false;
            }
            if (timer>1) {
                jumpNumber = 0;
                runTimer = false;
                timer = 0;
            }
        }
        
        if (isCheckingGround) {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, -transform.up, 0.2f, 1<<0);
            if (hit.collider!=null && hit.collider.tag == "Platform") {
                // Debug.Log(hit.collider.gameObject.name);
                // set animator to idle
                animator.SetBool("isGrounded", true);
                isCheckingGround = false;
            }
        }
        
    }
}
