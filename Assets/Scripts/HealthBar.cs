using UnityEngine;

public class HealthBar : MonoBehaviour {

  public GameObject heart;
  public Sprite heartSprite;
  public Sprite deadHeartSprite;
  public float seperation;
  public float showPeriod;

  private float disappearTime;

  void Start() {
    for(int i = 0; i < Globals.maxHealth; i++) {
      GameObject newHeart = Instantiate(heart, transform);
      newHeart.transform.localPosition = new Vector2(seperation * i, 0);
    }
    transform.position -= new Vector3(((Globals.maxHealth - 1) * seperation) / 2, 0, 0);
  }

  public void Set(int health) {
    if(health > Globals.maxHealth) {
      return;
    }
    for(int i = 0; i < (Globals.maxHealth - health); i++) {
      Transform child = transform.GetChild(i);
      child.GetComponent<SpriteRenderer>().sprite = deadHeartSprite;
    }
    for(int i = 0; i < health; i++) {
      Transform child = transform.GetChild(transform.childCount - 1 - i);
      child.GetComponent<SpriteRenderer>().sprite = heartSprite;
    }
    disappearTime = Time.time + showPeriod;
    SetVisible(true);
  }

  void SetVisible(bool isVisible) {
    for(int i = 0; i < transform.childCount; i++) {
      transform.GetChild(i).GetComponent<SpriteRenderer>().enabled = isVisible;
    }
  }

  void Update() {
    if(Time.time > disappearTime) {
      SetVisible(false);
    }
  }
}
