using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerScore : MonoBehaviour {

  private float score;
  private int coins;

  [SerializeField]
  GameObject scorePanel;
  [SerializeField]
  private Text scoreText;
  [SerializeField]
  private Text coinText;
  [SerializeField]
  private Text finalScoreText;

  [SerializeField]
  private GameObject gameOverPanel;

	void Start () {
    score = 0f;
	}

  public void AdjustScore(float value) {
    score += value;
    if(scoreText != null)
      scoreText.text = "" + Mathf.RoundToInt(score);
  }

  public void AdjustCoins(int value) {
    coins += value;
    if(coinText != null)
      coinText.text = "" + Mathf.RoundToInt(coins);
  }

  public void GameOver() {
    if(scorePanel != null) {
      scorePanel.SetActive(false);
    }
    if(gameOverPanel != null) {
      gameOverPanel.SetActive(true);
      finalScoreText.text = "" + Mathf.RoundToInt(score);
    }
  }
}
