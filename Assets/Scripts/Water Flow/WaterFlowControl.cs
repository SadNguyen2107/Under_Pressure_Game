using UnityEngine;

public class WaterFlowControl : MonoBehaviour
{
    void OnParticleCollision(GameObject other)
    {
        if (other.CompareTag("Ball"))
        {
            SoundManager.Instance.PlayHitBallSound();
        }
    }
}
