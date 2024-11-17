using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMovement : MonoBehaviour
{
    Camera _mainCamera; // Reference to the main camera.
    Vector2 _screenBounds; // Screen boundaries in world units.
    Vector3 _playerSize; // Size of the player (ball).

    void Start()
    {
        _mainCamera = Camera.main; // Get the main camera reference.
        _playerSize = GetComponent<SpriteRenderer>().bounds.extents; // Get half-size of the sprite (ball).
    }

    void Update()
    {
        // Check if the ball is out of bounds.
        bool isOutOfBounds = CheckOutOfBounds();

        // Prompt user to enter R
        if (isOutOfBounds)
        {
            FindObjectOfType<GameOverText>().StartBlinking("Ball fell out of bound!\nPlease enter R to restart Game!");
        }
    }

    bool CheckOutOfBounds()
    {
        // Get the camera bounds in world space.
        _screenBounds = _mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, _mainCamera.transform.position.z));

        // Check if the ball is outside the screen bounds.
        if (transform.position.x < _screenBounds.x * -1 - _playerSize.x ||
            transform.position.x > _screenBounds.x + _playerSize.x ||
            transform.position.y < _screenBounds.y * -1 - _playerSize.y ||
            transform.position.y > _screenBounds.y + _playerSize.y)
        {
            Debug.Log("Ball is out of bounds!");
            return true;
        }

        return false;
    }
}
