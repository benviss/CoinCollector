using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundCollector:MonoBehaviour {

  GameObject[] backgrounds;
  private float lastX;

  private void Start() {
    GetPlatformsAndSetLastX();
  }


  void GetPlatformsAndSetLastX() {
    backgrounds = GameObject.FindGameObjectsWithTag("Background");
    GetLastX();
  }

  private void OnTriggerEnter2D(Collider2D target) {
    if(target.gameObject.tag == "Background") {
      float width = ((BoxCollider2D)target).size.x;
      Vector3 temp = target.transform.position;
      temp.x = GetLastX() + width;
      target.gameObject.transform.position = temp;
    }
  }

  float GetLastX() {
    lastX = backgrounds[0].transform.position.x;

    for(int i = 1; i < backgrounds.Length; i++) {
      if(backgrounds[i].transform.position.x > lastX)
        lastX = backgrounds[i].transform.position.x;
    }
    return lastX;
  }
}
