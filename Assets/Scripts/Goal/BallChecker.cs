using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallChecker : MonoBehaviour
{
    [Range(1, 2)]
    [SerializeField] int _goalPlayer = 1;

    void OnTriggerExit2D(Collider2D other)
    {
        // If ball go through this line 
        // ---> Score 1 point for the player
        if (other.CompareTag("Ball"))
        {
            GameManager.Instance.AddScore(_goalPlayer, 1);
        }
    }
}
