using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioManager))]
[RequireComponent(typeof(PlayerScore))]
public class GameManager: Singleton<GameManager> {

  [SerializeField]
  private float initialScrollSpeed = 9.5f;
  [HideInInspector]
  public float scrollSpeed;

  [SerializeField]
  private float initialDeathWallSpeed = 7.5f;
  [HideInInspector]
  public float deathWallSpeed;

  public float coinSlowPercentage = .1f;
  public float coinsCollected = 0;
  public float lastSpikeTime = 0;
  public float minSpikeInterval = .5f;
  public bool playerIsAlive;

  Player player;
  PlayerScore playerScore;
  // Update is called once per frame
  void Update() {
    scrollSpeed = initialScrollSpeed * Mathf.Pow(1 - coinSlowPercentage, coinsCollected);
    deathWallSpeed = initialDeathWallSpeed - scrollSpeed;
    playerScore.AdjustScore(scrollSpeed * Time.deltaTime);
  }

  private void Awake() {
    scrollSpeed = initialScrollSpeed;
    deathWallSpeed = initialDeathWallSpeed;
  }
  private void Start() {
    player = FindObjectOfType<Player>();
    playerScore = GetComponent<PlayerScore>();
    playerIsAlive = true;
  }
  public void AddCoin() {
    AudioManager.Instance.PlayCoinSound();
    coinsCollected++;
    playerScore.AdjustCoins(1);
    playerScore.AdjustScore(10);
  }

  public void RemoveCoin() {
    AudioManager.Instance.PlayCoinSound();
    coinsCollected--;
    playerScore.AdjustCoins(-1);
  }

  public void Spike() {
    if(lastSpikeTime + minSpikeInterval < Time.time ) {
      lastSpikeTime = Time.time;
      if (coinsCollected > 0) {
        player.ThrowCoins();
      }
      else {
        PlayerDeath();
      }
    }
  }

  public void PlayerDeath() {
    playerIsAlive = false;
    Destroy(player.gameObject);
    playerScore.GameOver();
    StartCoroutine(GameOverLoadMainMenu());
  }

  IEnumerator GameOverLoadMainMenu() {
    yield return new WaitForSeconds(3f);
    Time.timeScale = 1;
    LoadMainMenu();
  }

  public void LoadMainMenu() {
    SceneManager.LoadScene("MainMenu");
  }
}
