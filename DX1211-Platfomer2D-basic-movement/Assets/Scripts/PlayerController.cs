using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float moveSpeed = 5f;
    private float jumpForce = 5f; // Adjust the jump force as needed.
    private bool isAttacking = false;

    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private Animator ar;

    private float dirH = 0.0f;

    private AudioSource sfxAudioSrc;
    public AudioClip walkAudioClip;
    //public AudioClip jumpAudioClip; // Add a jump sound effect.

    private bool isGrounded = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        ar = GetComponent<Animator>();
        sfxAudioSrc = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        dirH = Input.GetAxis("Horizontal");

        if (Mathf.Abs(dirH) > 0.0f)
        {
            if (!sfxAudioSrc.isPlaying)
            {
                sfxAudioSrc.clip = walkAudioClip;
                sfxAudioSrc.Play();
            }
        }

        // Allow the player to jump only when on the ground
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            //sfxAudioSrc.clip = jumpAudioClip;
            //sfxAudioSrc.Play();
        }

        if (!isAttacking && Input.GetMouseButtonDown(0))
        {
            Attack();
        }

    }

    private void LateUpdate()
    {
        UpdateAnimation();
        UpdateSpriteDirection();
    }

    private void FixedUpdate()
    {
        Vector2 vel = new Vector2(dirH * moveSpeed, rb.velocity.y);
        rb.velocity = vel;
    }

    void UpdateAnimation()
    {
        if (!isGrounded)
        {
            if (Mathf.Abs(rb.velocity.y) >= 0.001f)
            {
                ar.Play("Jumping");
            }
            else
            {
                ar.Play("IdleA");
            }
        }
        else // On the ground
        {
            if (Mathf.Abs(rb.velocity.x) <= 0.001f)
            {
                ar.Play("IdleA");
            }
            else
            {
                ar.Play("WalkA");
            }

            if (isAttacking)
            {
                ar.Play("AttackA");
            }
        }
    }

    void UpdateSpriteDirection()
    {
        if (dirH < 0f)
        {
            sr.flipX = true;
        }
        else if (dirH > 0f)
        {
            sr.flipX = false;
        }
    }

    void Attack()
    {
        isAttacking = true;
        ar.SetBool("isAttack", true);
        Debug.Log(">>>>START");

        StartCoroutine(ResetAttack());
    }
    IEnumerator ResetAttack()
    {
        yield return new WaitForSeconds(2f);
        isAttacking = false;
        ar.SetBool("isAttack", false);
        Debug.Log(">>>>END");
    }

    // Detect collision with the ground
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    // Detect when the player is no longer colliding with the ground
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
}
