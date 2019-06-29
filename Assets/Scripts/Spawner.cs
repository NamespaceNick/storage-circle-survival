using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    PrefabBank prefabBank;
    GameObject player;
    Vector3 playerOffset;


	void Start ()
    {
        player = GameObject.Find("Player");
        prefabBank = GameObject.Find("_PrefabBank").GetComponent<PrefabBank>();
        playerOffset = transform.position - player.transform.position;
	}

	
	void Update ()
    {
        transform.position = player.transform.position + playerOffset;
	}


    // Game controller will call this function
    public void Spawn(Enemy.EnemyType toSpawn)
    {
        GameObject enemyToSpawn = new GameObject();
        switch (toSpawn)
        {
            case Enemy.EnemyType.StarShip:
                enemyToSpawn = prefabBank.starShip;
                break;
            case Enemy.EnemyType.Kamikaze:
                enemyToSpawn = prefabBank.kamikaze;
                break;
            case Enemy.EnemyType.Battleship:
                enemyToSpawn = prefabBank.battleship;
                break;
            default:
                break;
        }
        GameObject enemy = (GameObject)Instantiate(enemyToSpawn, transform.position, transform.rotation);
    }
}
