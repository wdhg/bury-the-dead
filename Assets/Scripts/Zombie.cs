using UnityEngine;

public class Zombie : MonoBehaviour {

  public bool goingRight;

  private float minSpeed = 1f;
  private float maxSpeed = 2f;
  private float speed;
  private SpriteRenderer sr;
  private Sounds sounds;
  private float minMoanInterval = 5f;
  private float maxMoanInterval = 10f;
  private float moanTime;

  void Start() {
    moanTime = Time.time + Random.Range(minMoanInterval, maxMoanInterval);
    speed = Random.Range(minSpeed, maxSpeed);
    sr = GetComponent<SpriteRenderer>();
    sounds = GameObject.FindGameObjectWithTag("Sounds").GetComponent<Sounds>();
  }

  void Move() {
    Vector2 direction = (goingRight)? Vector2.right : Vector2.left;
    sr.flipX = !goingRight;
    transform.position += (Vector3) direction * speed * Time.deltaTime;
  }

  void Moan() {
    sounds.ZombieMoan();
    moanTime = Time.time + Random.Range(minMoanInterval, maxMoanInterval);
  }

  void Update() {
    Move();
    if(transform.position.x < -2f || transform.position.x > Globals.mapWidth + 2f) {
      GameObject.FindGameObjectWithTag("Player").GetComponent<Score>().SubZombieMissedScore();
      Destroy(gameObject);
    }
    if(Time.time > moanTime) {
      Moan();
    }
  }
}
