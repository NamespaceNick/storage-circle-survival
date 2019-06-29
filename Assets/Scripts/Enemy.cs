using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Contains information that all enemy objects possess
public class Enemy : MonoBehaviour
{
    public enum EnemyType { StarShip, Kamikaze, Battleship }

    // public float shakeStart, shakeLerp, shakeWait;
    public EnemyType type;
    public bool isHostile = true;
    public float deleteDistance;
    public float speed;
    public float lerpDiff;
    public float bulletBufferDist;
    public float maxHealth;
    public float damageDealt;
    // public float damageCooldownTime;
    public float enemyScore;
    public float collisionDmgDealt;

    public AudioClip enemyDamagedAC, enemyDestroyedAC;
    public int pointTextSize;
    public float enemyDamagedVol, enemyDestroyedVol;

    float currHealth;
    // AudioBank audioBank;
    // PrefabBank prefabBank;
    TextBank textBank;
    GameObject player;
    GameController gameController;
    Coroutine shakeCR;
	

	void Start ()
    {
        gameController = GameObject.Find("_GameController").GetComponent<GameController>();
        textBank = GameObject.Find("_TextBank").GetComponent<TextBank>();
        player = GameObject.Find("Player");
        currHealth = maxHealth;
	}

    void Update()
    {
        if (Vector3.Distance(transform.position, player.transform.position) > deleteDistance)
        {
            if (type != EnemyType.Battleship)
                Destroy(gameObject);
        }
    }


    public void EnemyDamaged(float damageValue)
    {
        Debug.Log("Enemy received damage of amount " + damageValue);
        currHealth -= damageValue;
        Debug.Log("Enemy health now: " + currHealth);
        if (currHealth > 0)
        {
            AudioSource.PlayClipAtPoint(enemyDamagedAC, Camera.main.transform.position, enemyDamagedVol);
            // Consider shaking the enemy slightly
        }
        else
        {
            textBank.currentScore += enemyScore;
            textBank.currentScoreText.text = "Score: " + textBank.currentScore;
            EnemyDestroyed();
        }

    }


    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Shield") && (type == EnemyType.Kamikaze))
        {
            EnemyDestroyed();
        }
        if (other.CompareTag("Player"))
        {
            EnemyDestroyed();
        }
        else if (other.CompareTag("Bullet") && !other.GetComponent<Bullet>().isEnemy)
        {
            EnemyDamaged(other.GetComponent<Bullet>().damageDealt);
        }
    }


    void EnemyDestroyed()
    {
        AudioSource.PlayClipAtPoint(enemyDestroyedAC, Camera.main.transform.position, enemyDestroyedVol);
        gameController.ShipDestroyed(this);
        Destroy(gameObject);
    }


    void OnDestroy()
    {
        // Inform gameController of ship death
        StopAllCoroutines();
    }
}
