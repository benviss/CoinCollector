using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour {

  public void PlayGame() {
    GameManager.Instance.playerIsAlive = true;
    SceneManager.LoadScene("Gameplay");

  }

	public void QuitGame() {
    Application.Quit();
  }
}
