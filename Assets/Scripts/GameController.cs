using UnityEngine;

public class GameController : MonoBehaviour {

  public Player player;
  public ZombieSpawner zs;
  public Graves graves;

  public void Restart() {
    player.Restart();
    zs.Restart();
    graves.Restart();
  }

  void Update() {
    if(Input.GetKeyDown(KeyCode.Escape)) {
      Application.Quit();
    }
  }
}
