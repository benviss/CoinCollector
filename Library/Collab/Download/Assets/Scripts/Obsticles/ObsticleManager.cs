using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObsticleManager:MonoBehaviour {

  private GameObject[] obstacles;

  // Use this for initialization
  void Start() {
    obstacles = GameObject.FindGameObjectsWithTag("Obsticle");

    for(int i = 0; i < obstacles.Length; i++) {
      obstacles[i].SetActive(false);
    }
  }

  // Update is called once per frame
  void Update() {

  }

  public GameObject GetObstacle() {
    int randy = Random.Range(0, obstacles.Length);
    if(!obstacles[randy].activeInHierarchy) {
      return obstacles[randy];
    }
    return null;
  }


}
