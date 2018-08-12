using UnityEngine;

public class CameraMan : MonoBehaviour {

  public Transform player;

  void Update() {
    // this works. dont ask
    float cameraWidth = Camera.main.orthographicSize * Camera.main.aspect;
    Vector3 position = player.position;
    position.z = -10f;
    position.y = 2f;
    if(position.x - cameraWidth < 0) {
      position.x = cameraWidth;
    }
    if(position.x + cameraWidth > Globals.mapWidth) {
      position.x = Globals.mapWidth - cameraWidth;
    }
    transform.position = position;
  }
}