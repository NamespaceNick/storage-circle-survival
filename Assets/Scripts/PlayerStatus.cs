using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerStatus : MonoBehaviour
{
    public bool isDead = false;
    public bool invulnerable = false;
    public float maxHealth;
    public float flashTime;
    public float damageCooldownTime;
    public float deathWaitTime;
    public float lowHealthBoundary;
    public int numShieldsActive = 0;
    public SpriteRenderer rend;
    public GameObject healthBar;


    public float wordPopWait;
    bool onDamageCooldown;
    bool isPopping = false;
    bool lowStarted = false;
    float currHealth;
    float initialHealthFrac;
    GameController gameController;
    AudioBank audioBank;
    TextBank textBank;
    Coroutine dying, lowHealth, iFrames;
    PlayerController controller;


	void Start ()
    {
        gameController = GameObject.Find("_GameController").GetComponent<GameController>();
        audioBank = GameObject.Find("_AudioBank").GetComponent<AudioBank>();
        textBank = GameObject.Find("_TextBank").GetComponent<TextBank>();
        controller = GetComponent<PlayerController>();
        currHealth = maxHealth;
        textBank.healthText.text = "Health: " + currHealth;
        initialHealthFrac = healthBar.transform.localScale.x;
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet") && other.GetComponent<Bullet>().isEnemy)
        {
            PlayerDamaged(other.GetComponent<Bullet>().damageDealt);
        }
        else if (other.CompareTag("Enemy"))
        { // Directly collides with enemy
            PlayerDamaged(other.GetComponent<Enemy>().collisionDmgDealt);
        }
        
    }

    public void PlayerDamaged(float damageValue)
    {
        if (invulnerable || onDamageCooldown || isDead)
            return;
        onDamageCooldown = true;
        StartCoroutine(DamageCooldown());
        currHealth -= damageValue;
        if (currHealth < 0)
            currHealth = 0;
        healthBar.transform.localScale = new Vector3((currHealth / maxHealth) * initialHealthFrac, healthBar.transform.localScale.y, healthBar.transform.localScale.z);
        textBank.healthText.text = "Health: " + currHealth;
        if (currHealth <= 0)
        {
            if (isDead)
                return;
            isDead = true;
            AudioSource.PlayClipAtPoint(audioBank.playerDeath, Camera.main.transform.position);
            dying = StartCoroutine(PlayerDeath());
            return;
        }
        else
        {
            AudioSource.PlayClipAtPoint(audioBank.playerHurt, Camera.main.transform.position);
        }
        if (currHealth <= lowHealthBoundary)
        {
            if (!lowStarted)
            { // Critical health levels will activate alarm sound
                lowStarted = true;
                lowHealth = StartCoroutine(LowHealth());
            }
        }
    }


    IEnumerator DamageCooldown()
    {
        iFrames = StartCoroutine(InvulnerabilityFrames());
        yield return new WaitForSeconds(damageCooldownTime);
        onDamageCooldown = false;
        StopCoroutine(iFrames);
        rend.material.color = Color.white;
        healthBar.GetComponent<Renderer>().material.color = Color.red;
    }

    IEnumerator InvulnerabilityFrames()
    {
        while (true)
        {
            rend.material.color = Color.red;
            healthBar.GetComponent<Renderer>().material.color = Color.white;
            yield return new WaitForSeconds(flashTime);
            rend.material.color = Color.white;
            healthBar.GetComponent<Renderer>().material.color = Color.red;
            yield return new WaitForSeconds(flashTime);
        }
    }
    

    IEnumerator PlayerDeath()
    {
        AudioSource.PlayClipAtPoint(audioBank.playerDeath, Camera.main.transform.position);
        foreach (SpriteRenderer r in gameObject.GetComponentsInChildren<SpriteRenderer>())
        {
            r.enabled = false;
        }
        yield return new WaitForSeconds(deathWaitTime);
        gameController.GameOver();
    }


    IEnumerator LowHealth()
    {
        while(currHealth <= lowHealthBoundary)
        {
            AudioSource.PlayClipAtPoint(audioBank.playerLowHealth, Camera.main.transform.position);
            yield return new WaitForSeconds(2f);
        }
    }


    private void OnDestroy()
    {
        StopAllCoroutines();
    }
}
