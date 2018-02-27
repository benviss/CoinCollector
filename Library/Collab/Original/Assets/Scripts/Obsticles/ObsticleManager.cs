using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObsticleManager:MonoBehaviour {

  public GameObject[] obsticles;

  // Use this for initialization
  void Start() {
    obsticles = GameObject.FindGameObjectsWithTag("Obsticle");	
  }

  // Update is called once per frame
  void Update() {

  }

  public GameObject GetObsticle() {
    int randy = Random.Range(0, obsticles.Length);
    if(obsticles.Length != 0 && !obsticles[randy].activeInHierarchy) {
      return obsticles[randy];
    }
    return null;
  }


}
