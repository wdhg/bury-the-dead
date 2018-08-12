using UnityEngine;

public class Graves : MonoBehaviour {

  public void Restart() {
    int children = transform.childCount;
    for(int i = children - 1; i >= 0; i--) {
      Destroy(transform.GetChild(i).gameObject);
    }
  }
}
