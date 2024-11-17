using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; } // Singleton instance.

    UIManager _UIManager;

    [Header("Timer")]
    [SerializeField] Timer _timer;

    // Ball
    [Header("Bal")]
    [SerializeField] GameObject _ball;
    [SerializeField] Vector3 _ballInitialPosition = Vector3.zero;

    // Player
    [Header("Player 1")]
    [SerializeField] GameObject _player1;
    [SerializeField] Vector3 _player1InitialPosition;

    [Header("Player 2")]
    [SerializeField] GameObject _player2;
    [SerializeField] Vector3 _player2InitialPosition;
    int _player1Score = 0;
    int _player2Score = 0;
    bool isGamePaused = false; // Tracks if the game is paused.

    // Getter
    public int Player1Score
    {
        get { return _player1Score; }
    }
    public int Player2Score
    {
        get { return _player2Score; }
    }

    void Awake()
    {
        // Ensure there is only one instance of GameManager.
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Persist across scenes.
        }
        else
        {
            Destroy(gameObject); // Destroy duplicate GameManagers.
        }

        // Get UI Manager
        _UIManager = FindObjectOfType<UIManager>();
    }

    void Update()
    {
        // Example: Restart the game if "R" is pressed.
        if (Input.GetKeyDown(KeyCode.R))
        {
            Debug.Log("Restart Game");
            RestartGame();
        }

        // Example: Pause or unpause the game if "P" is pressed.
        if (Input.GetKeyDown(KeyCode.P))
        {
            TogglePause();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            QuitGame();
        }
    }

    public void RestartGame()
    {
        // Reload the current scene to restart the game.
        // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1f; // Ensure time is running normally.

        // Reset the score of 2 players
        _player1Score = 0;
        _player2Score = 0;

        // Update to the UI
        _UIManager.UpdatePlayerScore(playerNumber: 1, newScore: 0);
        _UIManager.UpdatePlayerScore(playerNumber: 2, newScore: 0);

        // Reset the ball
        SetBallToInitialState();

        // Reset the Players
        ActivateAllPlayers();
        SetPlayersToInitialState();

        // Reset the Timer
        _timer.ResetTimer();
    }

    public void TogglePause()
    {
        isGamePaused = !isGamePaused;

        // Pause or unpause the game.
        if (isGamePaused)
        {
            Time.timeScale = 0f; // Pause time.
        }
        else
        {
            Time.timeScale = 1f; // Resume time.
        }
    }

    public void AddScore(int playerNumber, int scoreToAdd)
    {
        // Update the score for the specified player.
        if (playerNumber == 1)
        {
            _player1Score += scoreToAdd;

            // Update score to the UI
            _UIManager.UpdatePlayerScore(playerNumber, _player1Score);
        }
        else if (playerNumber == 2)
        {
            _player2Score += scoreToAdd;

            // Update score to the UI
            _UIManager.UpdatePlayerScore(playerNumber, _player2Score);
        }

        // Optional: Print updated scores for debugging.
        Debug.Log($"Player 1 Score: {_player1Score} | Player 2 Score: {_player2Score}");

        // Reset the ball
        SetBallToInitialState();

        // Reset the Players
        SetPlayersToInitialState();
    }

    public void QuitGame()
    {
        // Quit the game (works only in builds).
        Debug.Log("Quitting game...");
        Application.Quit();
    }

    void SetBallToInitialState()
    {
        // Reset the ball velocity 
        if (_ball.TryGetComponent<Rigidbody2D>(out Rigidbody2D ballRigidBody))
        {
            ballRigidBody.velocity = Vector3.zero;
        }

        // Set Ball to the original position
        _ball.transform.position = _ballInitialPosition;
    }

    void SetPlayersToInitialState()
    {
        // Set Player 1 back to initial state
        _player1.transform.position = _player1InitialPosition;

        // Set Player 2 back to initial state
        _player2.transform.position = _player2InitialPosition;
    }

    public void ActivateAllPlayers()
    {
        _player1.gameObject.SetActive(true);
        _player2.gameObject.SetActive(true);
    }
    public void DeactivateAllPlayers()
    {
        _player1.gameObject.SetActive(false);
        _player2.gameObject.SetActive(false);
    }

}
