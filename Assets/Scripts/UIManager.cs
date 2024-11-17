using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("Score Text")]
    [SerializeField] TextMeshProUGUI _player1ScoreText;
    [SerializeField] TextMeshProUGUI _player2ScoreText;

    public void UpdatePlayerScore(int playerNumber, int newScore)
    {
        if (playerNumber == 1)
        {
            _player1ScoreText.text = $"Player {playerNumber}\n{newScore}";
        }
        else
        {
            _player2ScoreText.text = $"Player {playerNumber}\n{newScore}";
        }
    }

}
