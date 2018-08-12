using UnityEngine;

public class Cloud : MonoBehaviour {

  public bool goingRight;
  public float minX, maxX;
  public float speed;

  void Move() {
    if(transform.localPosition.x < minX) {
      transform.localPosition = new Vector2(maxX, transform.localPosition.y);
    }
    if(transform.localPosition.x > maxX) {
      transform.localPosition = new Vector2(minX, transform.localPosition.y);
    }
    Vector3 dir = goingRight ? Vector3.right : Vector3.left;
    transform.Translate(dir * speed * Time.deltaTime);
  }

  void Update() {
    Move();
  }
}