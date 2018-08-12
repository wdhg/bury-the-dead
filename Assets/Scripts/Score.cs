using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour {

  public int digScore;
  public int zombieTrapScore;
  public int buryScore;
  public int zombieMissedScore;
  public Text scoreText;
  public ScoreLog scoreLog;
  public Player player;
  public Text highScoreText;

  private int score;

  public void Restart() {
    score = 0;
  }
  
  public void CheckHighScore() {
    if(score > PlayerPrefs.GetInt("highscore", 0)) {
      PlayerPrefs.SetInt("highscore", score);
    }
    highScoreText.text = "HIGH SCORE: " + PlayerPrefs.GetInt("highscore", 0).ToString();
  }

  public void AddDigScore() {
    if(player.state == Player.State.Dead) {
      return;
    }
    score += digScore;
    scoreLog.Log("GRAVE DUG +" + digScore.ToString(), false);
  }

  public void AddZombieTrapScore() {
    if(player.state == Player.State.Dead) {
      return;
    }
    score += zombieTrapScore;
    scoreLog.Log("ZOMBIE TRAPPED +" + zombieTrapScore.ToString(), false);
  }

  public void AddBuryScore() {
    if(player.state == Player.State.Dead) {
      return;
    }
    score += buryScore;
    scoreLog.Log("ZOMBIE BURIED +" + buryScore.ToString(), false);
  }

  public void SubZombieMissedScore() {
    if(player.state == Player.State.Dead) {
      return;
    }
    score -= zombieMissedScore;
    if(score < 0) {
      score = 0;
    }
    scoreLog.Log("ZOMBIE MISSED -" + zombieMissedScore.ToString(), true);
  }

  void SetScoreText() {
    scoreText.text = score.ToString();
  }

  void Update() {
    SetScoreText();
  }
}