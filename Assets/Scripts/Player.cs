using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private float _yVelocity;
    private CharacterController _characterController;
    private bool _canDoubleJump = true;
    private Vector3 _start_position;
    private Vector3 velocity;
    bool respawn = false;

    public float moveSpeed = 10;
    public float jumpForce = 10;
    public float gravityForce = 1;

    public delegate void PlayerCollect();
    public static event PlayerCollect onPlayerCollect;

    public delegate void PlayerDeath();
    public static event PlayerDeath onPlayerDeath;


    // Start is called before the first frame update
    void Start()
    {
        _start_position = transform.position;
        _characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        PlayerMovement();
    }

    private void PlayerMovement()
    {
        float HorizontalAxis = Input.GetAxis("Horizontal");
        velocity = new Vector3(HorizontalAxis, 0, 0) * moveSpeed;

        if (_characterController.isGrounded)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _yVelocity = jumpForce;
                _canDoubleJump = true;
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Space) && _canDoubleJump)
            {
                _yVelocity = jumpForce;
                _canDoubleJump = false;
            }
            else
            {
                _yVelocity -= gravityForce;
            }
        }
        
        velocity.y = _yVelocity;
        if (!respawn)
        {
            _characterController.Move(velocity * Time.deltaTime);
        }
        else
        {
            respawn = false;
        }
        
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Coin")
        {
            onPlayerCollect?.Invoke();
            Destroy(other.gameObject);
        }

        if(other.tag == "Death")
        {
            onPlayerDeath?.Invoke();
            Respawn(_start_position);
        }
    }

    private void Respawn(Vector3 position)
    {
        transform.position = position;
        respawn = true;
    }



}
