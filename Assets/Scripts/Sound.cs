using UnityEngine;

public class Sound : MonoBehaviour {

  void Update() {
    if(!GetComponent<AudioSource>().isPlaying) {
      Destroy(gameObject);
    }
  }
}
