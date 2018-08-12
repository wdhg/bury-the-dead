using UnityEngine;

public class Ground : MonoBehaviour {

  public int depth;
  public Transform collision;
  public GameObject grassPrefab;
  public GameObject dirtPrefab;

  void Start() {
    // plus 2f so we can spawn zombies out of the map
    collision.localScale = new Vector2(Globals.mapWidth, 1f);
    collision.localPosition = new Vector2(Globals.mapWidth / 2f, 0f);
    Generate();
  }

  void GenerateDirt(int x, int y) {
    GameObject prefab = dirtPrefab;
    if(y == 0) {
      prefab = grassPrefab;
    }
    Instantiate(prefab,
      new Vector2(transform.position.x + x, transform.position.y - y),
      prefab.transform.rotation,
      transform
    );
  }

  void Generate() {
    for(int x = 0; x < Globals.mapWidth; x++) {
      for(int y = 0; y < depth; y += 2) {
        GenerateDirt(x, y);
      }
    }
  }
}