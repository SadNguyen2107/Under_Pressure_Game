using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterSprayer : MonoBehaviour
{
    [SerializeField] Vector3 longScale = new Vector3(2f, 2f, 1f); // The "long" size.
    [SerializeField] Vector3 shortScale = new Vector3(1f, 1f, 1f); // The "short" size.

    [SerializeField] float _duration = 1f; // Time it takes to scale between sizes.
    [SerializeField] float _interval = 2f; // Time to wait between scaling cycles.

    bool _isScalingToLong = true; // Tracks the current scaling direction.

    // Update is called once per frame
     void Start()
    {
        // Start the scaling cycle.
        StartCoroutine(ScaleRoutine());
    }

    private IEnumerator ScaleRoutine()
    {
        while (true)
        {
            // Determine target scale based on the direction.
            Vector3 targetScale = _isScalingToLong ? longScale : shortScale;

            // Animate the scale change.
            yield return StartCoroutine(ScaleOverTime(targetScale));

            // Wait before scaling again.
            yield return new WaitForSeconds(_interval);

            // Toggle the direction for the next cycle.
            _isScalingToLong = !_isScalingToLong;
        }
    }

    private IEnumerator ScaleOverTime(Vector3 targetScale)
    {
        Vector3 startScale = transform.localScale; // Store the starting scale.
        float elapsedTime = 0f; // Track time elapsed during scaling.

        while (elapsedTime < _duration)
        {
            // Interpolate the scale based on time.
            transform.localScale = Vector3.Lerp(startScale, targetScale, elapsedTime / _duration);
            elapsedTime += Time.deltaTime;
            yield return null; // Wait for the next frame.
        }

        // Ensure the scale reaches the target exactly at the end.
        transform.localScale = targetScale;
    }
}
