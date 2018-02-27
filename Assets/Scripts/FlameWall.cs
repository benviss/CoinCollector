using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameWall : MonoBehaviour {
    
    public List<Transform> sprites = new List<Transform>();

    public Player player;
    public float animateDist = 1f;
    public float minX = -15;

    private List<float> offsets = new List<float>();

    // Use this for initialization
    void Start() {
        player = FindObjectOfType<Player>();
        for (int i = 0; i < sprites.Count; i++) {
            offsets.Add(i);
        }
    }

    // Update is called once per frame
    void Update() {
        MoveWall();
        AnimateFlames();
    }

    void MoveWall() {
        float currentSpeed = GameManager.Instance.deathWallSpeed * Time.deltaTime;
        Vector3 temp = new Vector3(transform.position.x + currentSpeed, transform.position.y, transform.position.z);

        if(transform.position.x < minX) {
            temp.x = minX;
        }

        transform.position = temp;
    }

    void AnimateFlames() {
        for(int i = 0; i < sprites.Count; i++) {
            float offset = Mathf.Sin(Time.time + offsets[i]) *.5f;
            sprites[i].transform.localPosition = new Vector3(sprites[i].transform.localPosition.x, offset, sprites[i].transform.localPosition.z);
        }
    }

 
}

