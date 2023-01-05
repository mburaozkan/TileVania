using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinBehavior : MonoBehaviour
{
    [SerializeField] AudioClip coinPickup;
    // Start is called before the first frame update
    void OnTriggerEnter2D(Collider2D other) 
    {
        AudioSource.PlayClipAtPoint(coinPickup, Camera.main.transform.position);
        FindObjectOfType<GameSession>().pickUpCoin();
        Destroy(gameObject);
    }
}
