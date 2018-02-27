using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController:MonoBehaviour {

  public float size;
  void FixedUpdate() {
    MovePlatform();
  }

  void MovePlatform() {
    Vector3 temp = gameObject.transform.position;
    temp.x -= (GameManager.Instance.scrollSpeed) * Time.deltaTime;
    gameObject.transform.position = temp;
  }
}
