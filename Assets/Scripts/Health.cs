using UnityEngine;

public class Health : MonoBehaviour {

  [HideInInspector]
  public int health;
  public float invincibilityPeroid;
  public HealthBar hb;
  public AudioSource hitSound;
  public Vector2 hitForce;
  public GameOver go;

  private float invincibilityEndTime;
  private Rigidbody2D rb;
  private Player player;
  public Score score;

  public void Restart() {
    health = Globals.maxHealth; 
  }

  void Start() {
    rb = GetComponent<Rigidbody2D>();
    player = GetComponent<Player>();
    score = GetComponent<Score>();
    health = Globals.maxHealth;
  }

  void TakeDamage() {
    if(player.state == Player.State.Dead) {
      return;
    }
    hitSound.Play();
    Vector2 force = hitForce;
    if(GetComponent<SpriteRenderer>().flipX) {
      force.x = -force.x;
    }
    if(rb.velocity.y <= 0) {
      rb.AddForce(force);
    }
    health--;
    if(health <= 0) {
      player.state = Player.State.Dead;
      go.SetGameOver();
      score.CheckHighScore();
    } else {
      player.state = Player.State.Moving;
    }
    invincibilityEndTime = Time.time + invincibilityPeroid;
    hb.Set(health);
  }

  void OnTriggerStay2D(Collider2D other) {
    if(other.tag == "Zombie" && Time.time > invincibilityEndTime) {
      TakeDamage();
    }
  }
}
