using UnityEngine;
using UnityEngine.UI;

public class ScoreLog : MonoBehaviour {

  public GameObject scoreInfoPrefab;
  public Transform canvas;

  public void Log(string text, bool bad) {
    GameObject scoreInfo = Instantiate(scoreInfoPrefab, canvas);
    scoreInfo.GetComponent<Text>().text = text;
    if(bad) {
      scoreInfo.GetComponent<Text>().color = Color.red;
    }
  }
}