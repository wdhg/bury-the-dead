using UnityEngine;

public class Player : MonoBehaviour {

  public float speed;
  public float jumpVelocity;
  public float digTime;
  public float buryTime;
  public GameObject gravePrefab;
  public enum State {Moving, Digging, Burying, Dead}
  [HideInInspector]
  public State state;
  public AudioSource digSound;
  public Transform graves;

  private Rigidbody2D rb;
  private Animator anim;
  private float unfreezeTime;
  private GameObject targetGrave;
  private Health health;
  private Score score;

  void Start() {
    rb = GetComponent<Rigidbody2D>();
    anim = GetComponent<Animator>();
    health = GetComponent<Health>();
    score = GetComponent<Score>();
    Restart();
  }

  public void Restart() {
    state = State.Moving;
    transform.position = new Vector2(Globals.mapWidth / 2, 0f);
    health.Restart();
    score.Restart();
  }

  public bool IsGrounded() {
    return Physics2D.Linecast(transform.position + Vector3.down * 0.01f, transform.position + Vector3.down * 0.2f, 1<<8);
  }

  void Move() {
    if(transform.position.x < 0) {
      transform.position = new Vector2(0, transform.position.y);
    } else if(transform.position.x > Globals.mapWidth) {
      transform.position = new Vector2(Globals.mapWidth, transform.position.y);
    }
    Vector2 velocity = new Vector2(
      Input.GetAxisRaw("Horizontal") * speed,
      rb.velocity.y
    );
    if(Input.GetButton("Jump") && IsGrounded()) {
      velocity.y = jumpVelocity;
    }
    if(velocity.x == 0) {
      anim.Play("Idle");
    } else {
      anim.Play("Walking");
    }
    if(velocity.x < 0) {
      GetComponent<SpriteRenderer>().flipX = true;
    } else if(velocity.x > 0) {
      GetComponent<SpriteRenderer>().flipX = false;
    }
    rb.velocity = velocity;
  }

  void DigGrave() {
    digSound.Play();
    Vector3 position = new Vector3(Mathf.Floor(transform.position.x), 0f, -1f);
    Instantiate(gravePrefab, position, gravePrefab.transform.rotation, graves);
    state = State.Moving;
    GetComponent<Score>().AddDigScore();
  }

  void BuryGrave() {
    if(!targetGrave) {
      return;
    }
    digSound.Play();
    targetGrave.GetComponent<Grave>().Bury();
    state = State.Moving;
    GetComponent<Score>().AddBuryScore();
  }

  void UseSpade() {
    float xPos = Mathf.Floor(transform.position.x);
    foreach(GameObject graveObj in GameObject.FindGameObjectsWithTag("Grave")) {
      if(graveObj.transform.position.x != xPos) {
        continue;
      }
      Grave grave = graveObj.GetComponent<Grave>();
      if(grave.state == Grave.State.Zombie) {
        // if grave contains a zombie, bury it
        targetGrave = graveObj;
        unfreezeTime = Time.time + buryTime;
        state = State.Burying;
        return;
      } else {
        // if grave is empty or burried, dont do anything
        return;
      }
    }
    // no graves exist here, dig a grave
    unfreezeTime = Time.time + digTime;
    state = State.Digging;
  }

  void Update() {
    if(state == State.Dead) {
      Vector2 velocity = rb.velocity;
      velocity.x = 0;
      rb.velocity = velocity;
      anim.Play("Death");
      return;
    }
    if((state == State.Digging || state == State.Burying) && (Input.GetAxisRaw("Horizontal") != 0 || Input.GetButtonDown("Jump"))) {
      state = State.Moving;
    }
    if(state == State.Moving) {
      Move();
      if(Input.GetButtonDown("Fire1")) {
        UseSpade();
      }
    } else if(state == State.Digging && Time.time > unfreezeTime) {
      DigGrave();
    } else if(state == State.Burying && Time.time > unfreezeTime) {
      BuryGrave();
    } else {
      rb.velocity = Vector2.zero;
      anim.Play("Digging");
    }
    if(state == State.Burying && !targetGrave) {
      state = State.Moving;
    }
  }
}