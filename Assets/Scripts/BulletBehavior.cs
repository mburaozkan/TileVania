using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    [SerializeField] float bulletSpeed = 20f;
    float xSpeed;
    PlayerMovement player;

    Rigidbody2D myRigidbody;
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        player = FindObjectOfType<PlayerMovement>();
        xSpeed = player.transform.localScale.x * bulletSpeed;
    }

    void Update()
    {
        myRigidbody.velocity = new Vector2(xSpeed, 0f);
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Enemy")
        {
            FindObjectOfType<GameSession>().killScore();
            Destroy(other.gameObject);
        }

        Destroy(gameObject);
    }
    void OnCollisionEnter2D(Collision2D other) {
        Destroy(gameObject);
    }
}
