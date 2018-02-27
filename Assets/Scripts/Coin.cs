using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour {

    public bool canCollect = true;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (canCollect) {
            if (collision.gameObject.name == "Player") {
                GameManager.Instance.AddCoin();
                Destroy(this.gameObject);
            }
        }
    }

    private void Update() {
        //Move();
        CheckRemove();
    }

    //void Move() {
    //    float deltaCurrentSpeed = GameManager.Instance.scrollSpeed * Time.deltaTime;
    //    transform.position = new Vector3(transform.position.x - deltaCurrentSpeed, transform.position.y, transform.position.z);
    //}

    void CheckRemove() {
        if (transform.position.x < -20) {
            Destroy(this.gameObject);
        }
    }
}
