using UnityEngine;

public class Sounds : MonoBehaviour {

  public GameObject zombieMoanSound;
  public float minPitchShift, maxPitchShift;

  public void ZombieMoan() {
    GameObject s = Instantiate(zombieMoanSound, transform);
    s.GetComponent<AudioSource>().pitch = Random.Range(1 - minPitchShift, 1 + maxPitchShift);
  }
}
