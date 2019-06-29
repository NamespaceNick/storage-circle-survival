using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public bool isEnemy = true;
    public float speed;
    public float damageDealt;
    public float deleteDistance;
    GameObject player;
    Rigidbody rb;


	// Use this for initialization
	void Start ()
    {
        player = GameObject.Find("Player");
        rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        rb.velocity = transform.up * speed;
        if (Vector3.Distance(transform.position, player.transform.position) > deleteDistance)
        {
            Destroy(gameObject);
        }
	}


    void OnTriggerEnter(Collider other)
    {
        if (isEnemy)
        { // Bullet that was shot by an enemy and has not been deflected
            switch (other.gameObject.tag)
            {
                case "Shield":
                    isEnemy = false;
                    transform.up = Vector3.Reflect(transform.up, other.transform.up);
                    break;
                case "Player":
                    Destroy(gameObject);
                    break;
                case "Enemy":
                    break;
                case "Bullet":
                    break;
                default:
                    break;
            }
        }
        else
        { // Bullet from the player or that was deflected from shield
            if (other.CompareTag("Enemy"))
            {
                // TODO: Particle effect on destruction
                Destroy(gameObject);
            }
        }
        
    }


}
