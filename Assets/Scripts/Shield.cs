using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    public float bulletForce, kamikazeForce; // Amnt of force that deflecting a bullet or kamikaze provides


    AudioBank audioBank;
    TextBank textBank;
    PlayerStatus status;
    Rigidbody playerRB;


    void Awake()
    {
        playerRB = GameObject.Find("Player").GetComponent<Rigidbody>();
        audioBank = GameObject.Find("_AudioBank").GetComponent<AudioBank>();
        textBank = GameObject.Find("_TextBank").GetComponent<TextBank>();
        status = GameObject.Find("Player").GetComponent<PlayerStatus>();
    }


    public void Equip()
    {
        gameObject.SetActive(true);
    }


    public void Unequip()
    {
        gameObject.SetActive(false);
    }


    public void DamageShield(float damageValue)
    {
        if (status.isDead)
        { return; }
        AudioSource.PlayClipAtPoint(audioBank.shieldDeflect, Camera.main.transform.position);
        textBank.currentScore += 2f;
        textBank.currentScoreText.text = "Score: " + textBank.currentScore;
    }


    void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Bullet") && other.GetComponent<Bullet>().isEnemy)
        {
            Debug.Log("An enemy bullet hit the shield");
            DamageShield(other.GetComponent<Bullet>().damageDealt);
            playerRB.AddForce(other.transform.up * bulletForce);
        }
        else if (other.CompareTag("Enemy") && (other.GetComponent<Enemy>().type == Enemy.EnemyType.Kamikaze))
        {
            DamageShield(other.GetComponent<Enemy>().collisionDmgDealt);
            playerRB.AddForce(other.transform.up * kamikazeForce);
        }
    }
}
