using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreen : MonoBehaviour {

  public GameObject title;
  public GameObject howToPlay;

  void Start() {
    title.SetActive(true);
    howToPlay.SetActive(false);
  }

  void Update() {
    if(Input.anyKeyDown) {
      if(!howToPlay.activeSelf) {
        title.SetActive(false);
        howToPlay.SetActive(true);
      } else {
        SceneManager.LoadScene("Main");
      }
    }
  }
}