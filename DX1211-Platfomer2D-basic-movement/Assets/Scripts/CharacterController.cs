using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

public class CharacterController : MonoBehaviour
{
    private Rigidbody2D _rb;
    private SpriteRenderer _sr;
    private Animator _a;

    [SerializeField] private bool isGrounded = true;
    [SerializeField] private float speed = 1f;
    [SerializeField] private float jumpStrength = 10f;
    
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _sr = GetComponent<SpriteRenderer>();
        _a = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            _rb.velocity = new Vector2(_rb.velocity.x,jumpStrength);
            _a.SetTrigger("GoJump");
        }

        if (Input.GetMouseButtonDown(0))
        {
            _a.SetTrigger("Attack");
        }
        
        _a.SetFloat("Speed", Math.Abs(_rb.velocity.x));
    }

    private void FixedUpdate()
    {
        float horizontalSpeed = Input.GetAxis("Horizontal");

        _rb.velocity = new Vector2(horizontalSpeed * speed, _rb.velocity.y);
        if (horizontalSpeed > 0)
            _sr.flipX = false;
        else if (horizontalSpeed < 0)
            _sr.flipX = true;
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
