using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public float forceCoef;
    public float bulletBufferDistance;
    public float shootCooldownTime;
    public float recoilVelocity;

    bool onShootCooldown = false;
    AudioBank audioBank;
    PrefabBank prefabBank;
    Rigidbody playerRB;


	void Start ()
    {
        prefabBank = GameObject.Find("_PrefabBank").GetComponent<PrefabBank>();
        audioBank = GameObject.Find("_AudioBank").GetComponent<AudioBank>();
        playerRB = GameObject.Find("Player").GetComponent<Rigidbody>();
	}
	

    public void Equip()
    {
        gameObject.SetActive(true);
        onShootCooldown = false;
    }


    public void Unequip()
    {
        gameObject.SetActive(false);
    }


    public void Shoot()
    {
        if (onShootCooldown)
        { return; }
        onShootCooldown = true;
        StartCoroutine(ShootCooldown());
        GameObject newBullet = (GameObject)Instantiate(
            prefabBank.playerBullet,
            transform.position + (transform.up * bulletBufferDistance),
            transform.rotation);
        playerRB.AddForce(-transform.up * forceCoef);
        AudioSource.PlayClipAtPoint(audioBank.turretFire, Camera.main.transform.position);
    }


    IEnumerator ShootCooldown()
    {
        yield return new WaitForSeconds(shootCooldownTime);
        onShootCooldown = false;
    }

}
