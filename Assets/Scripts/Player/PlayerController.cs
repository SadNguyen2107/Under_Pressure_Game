using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Range(1, 2)]
    [SerializeField] int _playerNumber;

    [SerializeField] float _moveSpeed = 5f; // Speed at which the player moves.

    Rigidbody2D _rb; // Reference to the Rigidbody2D component.
    Vector2 _movement; // Movement direction.

    Camera _mainCamera; // Reference to the main camera.
    Vector2 _screenBounds; // Screen boundaries in world units.
    Vector2 _playerSize; // Half-size of the player sprite.

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _mainCamera = Camera.main;

        // Calculate the player's half size based on its collider or sprite.
        _playerSize = GetComponent<SpriteRenderer>().bounds.extents;
    }

    void FixedUpdate()
    {
        // Move the player based on input.
        // _rb.AddForce(_movement * _moveSpeed);
        _rb.velocity = _movement * _moveSpeed;

        // Clamp the player's position to keep it within the camera's boundaries.
        ClampPosition();
    }

    void Update()
    {
        // If Player 1
        if (_playerNumber == 1)
        {
            MovePlayer1();
        }
        // If Player 2
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
        _movement.x = Input.GetAxisRaw("Horizontal_P2");
        _movement.y = Input.GetAxisRaw("Vertical_P2");
    }

    void ClampPosition()
    {
        // Get the camera bounds in world space.
        _screenBounds = _mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, _mainCamera.transform.position.z));

        // Clamp the player's position based on camera boundaries.
        Vector3 clampedPosition = transform.position;
        clampedPosition.x = Mathf.Clamp(clampedPosition.x, _screenBounds.x * -1 + _playerSize.x, _screenBounds.x - _playerSize.x);
        clampedPosition.y = Mathf.Clamp(clampedPosition.y, _screenBounds.y * -1 + _playerSize.y, _screenBounds.y - _playerSize.y);

        // Apply the clamped position to the player's transform.
        transform.position = clampedPosition;
    }
}
