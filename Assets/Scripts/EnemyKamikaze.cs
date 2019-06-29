using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Dumb enemies which charge directly towards the player at all times
public class EnemyKamikaze : MonoBehaviour
{
    GameObject player;
    Rigidbody rb;
    Enemy enemy;

	void Start ()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody>();
        enemy = GetComponent<Enemy>();
	}
	

	void Update ()
    {
        // Pursue the player at all times
        transform.up = Vector3.Lerp(
            transform.up, 
            (player.transform.position - transform.position).normalized, 
            enemy.lerpDiff);
        rb.velocity = transform.up * enemy.speed;
    }

    void OnDestroy()
    {
        StopAllCoroutines();
    }
}
