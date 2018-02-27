using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformCollector:MonoBehaviour {

  private GameObject[] platforms;
  private float lastX;

  private ObsticleManager obsticleManager;

  private void Start() {
    GetPlatformsAndSetLastX();
    obsticleManager = FindObjectOfType<ObsticleManager>();
  }

  void GetPlatformsAndSetLastX() {
    platforms = GameObject.FindGameObjectsWithTag("Platform");
    GetLastX();
  }

  float GetLastX() {
    lastX = platforms[0].transform.position.x;

    for(int i = 1; i < platforms.Length; i++) {
      if(platforms[i].transform.position.x > lastX)
        lastX = platforms[i].transform.position.x;
    }
    return lastX;
  }

  void OnTriggerEnter2D(Collider2D target) {
    if(target.tag == "Platform") {
      RemoveObsticles(target.gameObject);
      float width = ((BoxCollider2D)target).size.x;
      Vector3 temp = target.transform.position;
      temp.x = GetLastX() + width;
      target.gameObject.transform.position = temp;

      AddObsticle(target.gameObject.transform);
    }
  }
  
  void AddObsticle(Transform parent) {
    int randy = Random.Range(0, 2);

    if(randy == 1) {
      GameObject obsticle = obsticleManager.GetObstacle();

      if(obsticle == null) return;

      Vector3 temp = parent.transform.position;
      temp.y += 1f;

      obsticle.transform.position = temp;
      obsticle.transform.parent = parent;
      obsticle.SetActive(true);
    }
  }

  void RemoveObsticles(GameObject go) {
    for(int i = go.transform.childCount - 1; i >= 0; --i) {
      var child = go.transform.GetChild(i).gameObject;
      if(child.gameObject.tag == "Obsticle" || child.gameObject.tag == "Coin") {
        child.SetActive(false);
      }
    }
  }
}
