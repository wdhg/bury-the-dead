using UnityEngine;

public class ScoreInfo : MonoBehaviour {

  private float speed = 200f;
  private float maxHeight = 150f;

  void Move() {
    transform.localPosition = Vector2.MoveTowards(
      transform.localPosition,
      new Vector2(transform.localPosition.x, maxHeight),
      speed * Time.deltaTime
    );
    if(maxHeight - transform.localPosition.y < 0.1f) {
      Destroy(gameObject);
    }
  }

  void Update() {
    Move();
  }

}
