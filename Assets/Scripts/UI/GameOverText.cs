using System.Collections;
using UnityEngine;
using TMPro;

public class GameOverText : MonoBehaviour
{
    TextMeshProUGUI _blinkingText; // Reference to the TextMeshPro text.
    [SerializeField] float blinkInterval = 1f; // Time in seconds between toggles.
    bool isBlinking = false; // Tracks whether the blinking is active.
    void Awake()
    {
        _blinkingText = GetComponent<TextMeshProUGUI>();
    }

    void Start()
    {
        // Ensure the text starts fully transparent.
        SetAlpha(0);
    }

    public void StartBlinking(string message)
    {
        if (!isBlinking)
        {
            _blinkingText.text = message;
            isBlinking = true;
            StartCoroutine(BlinkRoutine());
        }
    }

    public void StopBlinking()
    {
        isBlinking = false;
        SetAlpha(0); // Ensure the text becomes fully invisible.
        StopAllCoroutines();
    }

    IEnumerator BlinkRoutine()
    {
        while (isBlinking)
        {
            // Fade in.
            yield return StartCoroutine(FadeAlpha(0, 1, blinkInterval / 2));

            // Fade out.
            yield return StartCoroutine(FadeAlpha(1, 0, blinkInterval / 2));
        }
    }

    IEnumerator FadeAlpha(float startAlpha, float endAlpha, float duration)
    {
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            // Interpolate alpha value over time.
            float alpha = Mathf.Lerp(startAlpha, endAlpha, elapsedTime / duration);
            SetAlpha(alpha);

            elapsedTime += Time.deltaTime;
            yield return null; // Wait for the next frame.
        }

        // Ensure the final alpha is set accurately.
        SetAlpha(endAlpha);
    }

    void SetAlpha(float alpha)
    {
        // Modify the alpha of the text color.
        Color color = _blinkingText.color;
        color.a = alpha;
        _blinkingText.color = color;
    }
}
