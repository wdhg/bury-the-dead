using UnityEngine;

public class Grave : MonoBehaviour {

  public enum State {Empty, Zombie, Buried}
  [HideInInspector]
  public State state;

  private float escapePeroid = 3f;
  private float escapeTime;
  private float decomposePeroid = 60f;
  private float decomposeTime;
  private Animator anim;

  void Start() {
    anim = GetComponent<Animator>();
  }

  public void Bury() {
    state = State.Buried;
    decomposeTime = Time.time + decomposePeroid;
  }

  void ZombieConsume(GameObject zombie) {
    state = State.Zombie;
    Destroy(zombie);
    GameObject.FindGameObjectWithTag("Player").GetComponent<Score>().AddZombieTrapScore();
    escapeTime = Time.time + escapePeroid;
  }

  void ZombieEscape() {
    GameObject.FindGameObjectWithTag("ZombieSpawner").GetComponent<ZombieSpawner>().SpawnAt(transform.position.x, transform.position.x < Globals.mapWidth / 2f);
    Destroy(gameObject);
  }

  void OnTriggerEnter2D(Collider2D other) {
    if(other.tag == "Zombie" && state == State.Empty) {
      ZombieConsume(other.gameObject);
    }
  }

  void Update() {
    switch(state) {
      case State.Empty:
        anim.Play("Grave");
        break;
      case State.Zombie:
        anim.Play("Unburied");
        break;
      case State.Buried:
        anim.Play("Buried");
        break;
      default:
        break;
    }
    if(state == State.Zombie && Time.time > escapeTime) {
      ZombieEscape();
    }
    if(state == State.Buried && Time.time > decomposeTime) {
      Destroy(gameObject);
    }
  }
}