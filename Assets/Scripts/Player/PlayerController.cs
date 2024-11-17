using System.Collections;
using System.Collections.Generic;
using UnityEditor.Callbacks;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Range(1, 2)]
    [SerializeField] int _playerNumber;

    [SerializeField] float _moveSpeed = 5f; // Speed at which the player moves.

    Rigidbody2D _rb; // Reference to the Rigidbody2D component.
    Vector2 _movement; // Movement direction.

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        // Move the player based on input.
        // _rb.AddForce(_movement * _moveSpeed);
        _rb.velocity = _movement * _moveSpeed;
    }

    void Update()
    {
        // If Player 1
        if (_playerNumber == 1)
        {
            MovePlayer1();
        }
        // If Player2
        else
        {
            MovePlayer2();
        }

        // Normalize the movement vector.
        _movement = _movement.normalized;
    }

    void MovePlayer1()
    {
        _movement.x = Input.GetAxisRaw("Horizontal_P1");
        _movement.y = Input.GetAxisRaw("Vertical_P1");
    }

    void MovePlayer2()
    {
        Debug.Log("Move 2");
        _movement.x = Input.GetAxisRaw("Horizontal_P2");
        _movement.y = Input.GetAxisRaw("Vertical_P2");
    }
}
