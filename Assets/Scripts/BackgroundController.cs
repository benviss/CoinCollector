using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundController : MonoBehaviour {

  void Update() {
    MoveBG();
  }

  void MoveBG() {
    Vector3 temp = gameObject.transform.position;
    temp.x -= (GameManager.Instance.scrollSpeed) * Time.deltaTime;
    gameObject.transform.position = temp;
  }

}
