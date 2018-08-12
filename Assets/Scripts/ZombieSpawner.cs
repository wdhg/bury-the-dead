using UnityEngine;

public class ZombieSpawner : MonoBehaviour {

  public GameObject zombiePrefab;
  public float minSpawnInterval;
  public float maxSpawnInterval;

  private float nextSpawnTime;

  public void Restart() {
    int children = transform.childCount;
    for(int i = children - 1; i >= 0; i--) {
      Destroy(transform.GetChild(i).gameObject);
    }
  }

  public void SpawnAt(float xPos, bool goingRight) {
    Vector3 position = new Vector3(xPos, -0.1f, 1f);
    GameObject zombie = Instantiate(zombiePrefab, position, zombiePrefab.transform.rotation, transform);
    zombie.GetComponent<Zombie>().goingRight = goingRight;
  }

  void Spawn() {
    bool goingRight = Random.value < 0.5f;
    SpawnAt(goingRight? -1f : Globals.mapWidth + 1f, goingRight);
  }

  void Update() {
    if(Time.time > nextSpawnTime) {
      Spawn();
      nextSpawnTime = Time.time + Random.Range(minSpawnInterval, maxSpawnInterval);
    }
  }

}