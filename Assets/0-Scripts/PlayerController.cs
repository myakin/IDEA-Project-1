using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    public float moveSpeed = 0.001f;
    public float jumpStrength = 100f;
    public float velocityForceMagnitude = 1;

    private Animator animator;
    private bool isJumping;
    private bool isCheckingGround;
    private float jumpTimer;
    private Vector3 oldPosition;
    private float moveMultiplier = 1;

    private void Start() {
        animator = GetComponent<Animator>();
    }

    private void Update() {
        float hor  = Input.GetAxis("Horizontal");
        float vertical  = Input.GetAxis("Vertical");
        float jumpValue = Input.GetAxis("Jump");

        // Debug.Log(jumpValue);
        
        if (!isJumping) {
            if (Input.GetKey(KeyCode.LeftShift)) {
                moveMultiplier = 3;
                animator.SetBool("isRunning", true);
            } else {
                moveMultiplier = 1;
                animator.SetBool("isRunning", false);
            }

            animator.SetFloat("walkLeftRight", hor * moveMultiplier);
            transform.position += transform.right * (moveSpeed * hor * moveMultiplier);
            
            if (hor>0) {
                GetComponent<SpriteRenderer>().flipX = false;
            } else if (hor<0) {
                GetComponent<SpriteRenderer>().flipX = true;
            }
        }

        // jump trigger
        if (jumpValue>0) {
            if (!isJumping) {
                isJumping = true;
                animator.SetTrigger("triggerJump");

                transform.SetParent(null);

                Vector3 velocity = transform.position - oldPosition;
                GetComponent<Rigidbody2D>().AddForce(velocity * velocityForceMagnitude);

            }
            jumpTimer+=Time.deltaTime;
            if (jumpTimer<0.5f) {
                GetComponent<Rigidbody2D>().AddForce(transform.up * jumpStrength);
            }
        } 
        
        // ground checking
        if (isCheckingGround) {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, -transform.up, 0.2f, 1<<0);
            if (hit.collider!=null && hit.collider.tag == "Platform") {
                // Debug.Log(hit.collider.gameObject.name);
                // set animator to idle
                animator.SetBool("isGrounded", true);
                transform.SetParent(hit.transform);
            }

            // if (animator.GetCurrentAnimatorStateInfo(0).IsName("CharacterIdleAnimation")) {
            //     ResetJump();
            // }
        }

        oldPosition = transform.position;
        
    }

    // called from CharacterJumpEndAnimation animation
    public void ResetJump() {
        isCheckingGround = false;
        isJumping = false;
        animator.SetBool("isGrounded", false);
        jumpTimer = 0;
        GetComponent<Rigidbody2D>().velocity = Vector3.zero;
    }

    // called from CharacterJumpStartAnimation animation
    public void StartCheckingGround() {
        isCheckingGround = true;
    } 
}
