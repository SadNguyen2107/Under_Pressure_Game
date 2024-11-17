using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [Header("Game Over Text")]
    [SerializeField] GameOverText _gameOverText;

    Image _timerImage; // Reference to the Image UI component (set to Filled).
    TextMeshProUGUI _timerText;

    // Total duration of the timer in seconds.
    [Header("Timer")]
    [SerializeField] float _timerDuration = 60f;

    // Tracks the remaining time.
    float _timerValue;
    void Awake()
    {
        _timerImage = GetComponent<Image>();
        _timerText = GetComponentInChildren<TextMeshProUGUI>();
    }

    void Start()
    {
        // Initialize the timer.
        ResetTimer();
    }

    void Update()
    {
        // Update the timer every frame.
        if (_timerValue > 0)
        {
            _timerValue -= Time.deltaTime;

            // Update the fill amount based on the remaining time.
            _timerImage.fillAmount = _timerValue / _timerDuration;

            // Update time text
            UpdateTimerText();
        }
        else
        {
            // Timer has ended.
            _timerImage.fillAmount = 0;

            // Update time text
            UpdateTimerText();

            TimerEnd();
        }
    }

    public void ResetTimer()
    {
        // Reset the timer to its full duration.
        _timerValue = _timerDuration;
        _timerImage.fillAmount = 1;

        // Stop the blinking text
        _gameOverText.StopBlinking();
    }

    void TimerEnd()
    {
        // Logic for when the timer ends.
        Debug.Log("Timer has ended!");

        // Start blinking the text Game Over
        int dif = GameManager.Instance.Player1Score - GameManager.Instance.Player2Score;
        Debug.Log(dif);
        if (dif < -1)
        {
            _gameOverText.StartBlinking("Game Over!\nPlayer 2 won!\nPlease enter R to restart game!");
        }
        else if (dif > 1)
        {
            _gameOverText.StartBlinking("Game Over!\nPlayer 1 won!\nPlease enter R to restart game!");
        }
        else
        {
            _gameOverText.StartBlinking("Game Over!\nDraw!\nPlease enter R to restart game!");
        }
        
        // Stop all Player Interact
        // Until Hit R
        GameManager.Instance.DeactivateAllPlayers();
    }

    void UpdateTimerText()
    {
        int timerValueAsInt = Mathf.RoundToInt(_timerValue);
        _timerText.text = $"{timerValueAsInt} s";
        Debug.Log(_timerText.text);
    }
}
