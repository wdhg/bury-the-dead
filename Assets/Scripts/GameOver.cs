using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour {

  public GameController gc;
  public GameObject gameOverScreen;
  public Text highScoreText;

  private bool gameIsOver;

  public void SetGameOver() {
    gameOverScreen.SetActive(true);
    gameIsOver = true;
  }

  void Update() {
    if(gameIsOver && Input.anyKeyDown) {
      gc.Restart();
      gameOverScreen.SetActive(false);
      gameIsOver = false;
    }
  }
}