using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player:MonoBehaviour {

  public float jumpForce = 500;
  public float jumpTime = .1f;
  public float playerHeight = 1;
  public float playerWidth = 1;
  private float curJumpTime = 0;

  Rigidbody2D myRigidbody;

  public float throwCoinsPercent = .5f;

  public Coin coinPrefab;

  private float speed = 0;

  void Start() {
    myRigidbody = GetComponent<Rigidbody2D>();
  }

  void FixedUpdate() {
    GetInput();
    ReCenter();
  }

  private void Update() {
    if(transform.position.y < -6) {
      GameManager.Instance.PlayerDeath();
    }
  }

  void GetInput() {
    if(Input.GetKeyDown(KeyCode.Escape)) {
      GameManager.Instance.LoadMainMenu();
    }
    else if(Input.anyKey) {
      Jump();
    }
    else {
      curJumpTime = 1;
    }
  }

  void ReCenter() {
    Vector3 trans = transform.position;
    if(Mathf.Abs(trans.x) < .05f) {
      trans.x = 0;
    }
    else {
      speed -= trans.x * .3f;
      speed *= .5f;
      trans.x += speed * Time.deltaTime;
    }
    transform.position = trans;
  }

  bool IsGrounded() {
    Debug.DrawLine(transform.position - (.5f * playerWidth * Vector3.right) - (playerHeight * Vector3.up), transform.position - (.5f * playerWidth * Vector3.right) - (playerHeight * Vector3.up) + Vector3.down * .03f, Color.red);
    if(Physics2D.Raycast(transform.position - (.5f * playerWidth * Vector3.right) - (playerHeight * Vector3.up), Vector2.down, .05f, 1 << 8) ||
        Physics2D.Raycast(transform.position + (.5f * playerWidth * Vector3.right) - (playerHeight * Vector3.up), Vector2.down, .05f, 1 << 8)) {
      return true;
    }
    return false;
  }

  bool IsWalled() {
    if(Physics2D.Raycast(transform.position + (.5f * playerWidth * Vector3.right) - (playerHeight * Vector3.up), Vector2.right, .05f, 1 << 8) ||
        Physics2D.Raycast(transform.position + (.5f * playerWidth * Vector3.right) - (.2f * playerHeight * Vector3.up), Vector2.right, .05f, 1 << 8)) {
      return true;
    }
    return false;
  }

  void Jump() {

    if(IsGrounded() && curJumpTime == 1) {
      curJumpTime = 0;
    }
    else if(IsWalled() && curJumpTime == 1) {
      curJumpTime = jumpTime * .5f;
    }

    if(curJumpTime < jumpTime) {
      curJumpTime += Time.deltaTime;
      myRigidbody.AddForce(Vector2.up * jumpForce * (Time.deltaTime / jumpTime));
    }
  }

  public void ThrowCoins() {
    int throwCoinsNum = Mathf.CeilToInt(GameManager.Instance.coinsCollected * throwCoinsPercent);
    for(int i = 0; i < throwCoinsNum && GameManager.Instance.coinsCollected > 0; i++) {
      Vector3 offset = Random.onUnitSphere;
      offset.z = 0;
      GameManager.Instance.RemoveCoin();
      Coin newCoin = Instantiate(coinPrefab, transform.position + offset.normalized * .3f, Quaternion.identity);
      Rigidbody2D body = newCoin.GetComponent<Rigidbody2D>();
      body.AddForce(offset.normalized * 300);
      body.gravityScale = -.5f;
      newCoin.canCollect = false;
      Destroy(newCoin.gameObject, 10);
    }
  }

  private void OnTriggerEnter2D(Collider2D collision) {
    if(collision.gameObject.tag == "Deadly") {
      GameManager.Instance.PlayerDeath();
      
    }
  }
}
