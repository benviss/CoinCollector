using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.name == "Player") {
            GameManager.Instance.Spike();
            if (gameObject.tag == "Projectile") {
                Destroy(gameObject);
            }
        }
    }

    private void Update() {
        CheckRemove();
    }

    void CheckRemove() {
        if (transform.position.x < -20) {
            Destroy(this.gameObject);
        }
    }
}
