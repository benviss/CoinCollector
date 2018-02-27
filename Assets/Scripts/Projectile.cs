using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {
    private float speed = 0;

    //Don't judge me
    float xDirection;
    float yDirection;

    public Vector2 targetPosition;

    // Update is called once per frame
    void Update() {
        Vector2 newPosition = new Vector2((transform.position.x - xDirection), transform.position.y - yDirection);
        transform.position = Vector2.MoveTowards(transform.position, newPosition, speed * Time.deltaTime);
    }

    public void SetTarget(Vector2 _targetPosition)
    {
        xDirection = transform.position.x - _targetPosition.x;
        yDirection = transform.position.y - _targetPosition.y;
    }

    public void SetSpeed(float _speed)
    {
        speed = _speed;
    }
}
