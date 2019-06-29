using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStarship : MonoBehaviour {
    public float fireInterval;
    public float firingDistance;
    public float stoppingDistance;


    AudioBank audioBank;
    PrefabBank prefabBank;
    Enemy enemy;
    GameObject player;
    Rigidbody rb;

	void Start ()
    {
        audioBank = GameObject.Find("_AudioBank").GetComponent<AudioBank>();
        prefabBank = GameObject.Find("_PrefabBank").GetComponent<PrefabBank>();
        player = GameObject.FindGameObjectWithTag("Player");
        enemy = GetComponent<Enemy>();
        rb = GetComponent<Rigidbody>();
        StartCoroutine(BeginFiring());
	}
	

	void Update ()
    {

        transform.up = Vector3.Lerp(
            transform.up, 
            (player.transform.position - transform.position).normalized, enemy.lerpDiff);
        if (Vector3.Distance(transform.position, player.transform.position) <= stoppingDistance)
        {
            rb.velocity = Vector3.zero;
        }
        else
        {
            rb.velocity = transform.up * enemy.speed;
        }
    }


    IEnumerator BeginFiring()
    {
        while(true)
        {
            if (Vector3.Distance(transform.position, player.transform.position) <= firingDistance)
            {
                Debug.Log("less than stopping distance");
                yield return new WaitForSeconds(fireInterval);
                GameObject enemyBullet = (GameObject)Instantiate(
                    prefabBank.enemyBullet, 
                    transform.position + transform.up * enemy.bulletBufferDist,
                    transform.rotation);
                AudioSource.PlayClipAtPoint(audioBank.starshipFire, Camera.main.transform.position, audioBank.starshipFireVol);
            }
            yield return null;
        }

    }

    void OnDestroy()
    {
        StopAllCoroutines();
    }
}
