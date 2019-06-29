using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using InControl;

public class GameController : MonoBehaviour
{
    public Spawner[] spawners;
    public GameObject healthBar;
    public int minStarships = 5;
    public int minKamikazes = 1;
    public int currStarships = 0;
    public int currKamikazes = 0;
    public Text disclaimerText;
    public Text controlsText;

    public float starshipIncreaseRate;
    public float kamikazeIncreaseRate;

    public bool started = false;
    public bool pauseOnStart = true;
    public bool usingController = true;
    bool isRunning = false;
    bool isPaused = false;
    bool isOver = false;
    bool acquiringInput = true;
    bool disclaimerRunning = true;
    InputDevice device;
    MyCharacterActions playerActions;
    PlayerController playerController;
    Text beginText, restartText, quitText;

    AudioBank audioBank;
    TextBank textBank;

	void Start ()
    {
        playerActions = new MyCharacterActions();
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        healthBar.SetActive(false);
        Time.timeScale = 1f;
        audioBank = GameObject.Find("_AudioBank").GetComponent<AudioBank>();
        textBank = GameObject.Find("_TextBank").GetComponent<TextBank>();
        beginText = textBank.beginText;
        quitText = textBank.quitText;
        restartText = textBank.restartText;
        textBank.healthText.enabled = true;
        textBank.currentScoreText.enabled = true;
        StartCoroutine(KamikazeSpawnIncreases());
        StartCoroutine(StarshipSpawnIncreases());
        StartCoroutine(DisclaimerTimer());
        BeginGame();
        // StartCoroutine(AcquireInputType());
	}
	
	void Update ()
    {
        device = InputManager.ActiveDevice;

        /*
        if (acquiringInput)
        {
            return;
        }
        */

        if (Input.GetKeyDown(KeyCode.Escape))
        {

            if (isPaused)
            {
                HidePauseScreen();
            }
            else if (!isPaused && isRunning)
            {
                ShowPauseScreen();
            }
            else if (isOver)
            {
                RestartGame();
            }
        }

        /*
        if (playerActions.quit.WasPressed)
        {
            if (!started || isPaused || isOver)
            {
                QuitGame();
            }
        }
        */

        if (disclaimerRunning)
        {
            return;
        }

        if (currStarships < minStarships)
        {
            currStarships++;
            spawners[Random.Range(0, spawners.Length)].Spawn(Enemy.EnemyType.StarShip);
        }
        if (currKamikazes < minKamikazes)
        {
            currKamikazes++;
            spawners[Random.Range(0, spawners.Length)].Spawn(Enemy.EnemyType.Kamikaze);
        }




        // TODO: Integrate pause menu with final controls
        /*
        if (playerActions.pressStart.WasPressed)
        {
            if (isPaused)
            {
                HidePauseScreen();
            }
            else if (!isPaused && isRunning)
            {
                ShowPauseScreen();
            }
            else if (isOver)
            {
                RestartGame();
            }
        }
        */
	}


    void BeforeGame()
    {
        Time.timeScale = 0f;
        started = false;
        beginText.enabled = true;
        quitText.enabled = true;
    }


    void BeginGame()
    {
        healthBar.SetActive(true);
        StartCoroutine(audioBank.BeginBattleMusic());
        Time.timeScale = 1f;
        started = true;
        isRunning = true;
        beginText.enabled = false;
        textBank.pauseText.enabled = false;
        quitText.enabled = false;
    }


    void ShowPauseScreen()
    {
        Time.timeScale = 0f;
        isRunning = false;
        isPaused = true;
        textBank.pauseText.enabled = true;
        quitText.enabled = true;
    }


    void HidePauseScreen()
    {
        isRunning = true;
        isPaused = false;
        textBank.pauseText.enabled = false;
        quitText.enabled = false;
        Time.timeScale = 1f;
    }


    public void GameOver()
    {
        Time.timeScale = 0f;
        isRunning = false;
        isPaused = false;
        isOver = true;
        textBank.finalScoreText.text = "Final Score: " + textBank.currentScore;
        textBank.finalScoreText.enabled = true;
        restartText.enabled = true;
        quitText.enabled = true;
    }


    void RestartGame()
    {
        SceneManager.LoadScene("_Main");
    }

    public void ShipDestroyed(Enemy enemyDestroyed)
    {
        StartCoroutine(ShipDestroyedCoroutine(enemyDestroyed));

        Debug.Log("ShipDestroyed() called");
    }


    public IEnumerator ShipDestroyedCoroutine(Enemy enemyDestroyed)
    {
        switch (enemyDestroyed.type)
        {
            case Enemy.EnemyType.Kamikaze:
                currKamikazes--;
                break;
            case Enemy.EnemyType.StarShip:
                currStarships--;
                break;
            default:
                break;
        }
        /*
        GameObject pointTextPrefab = (GameObject)Instantiate(textBank.pointTextPrefab, enemyDestroyed.transform.position, Quaternion.identity);
        pointTextPrefab.GetComponent<Canvas>().transform.position = enemyDestroyed.transform.position;
        Text pointText = pointTextPrefab.GetComponent<Text>();
        pointText.fontSize = enemyDestroyed.pointTextSize;
        pointText.text = "+" + enemyDestroyed.enemyScore;
        pointText.CrossFadeAlpha(0, textBank.pointTextFadeTime, false);
        yield return new WaitForSeconds(textBank.pointTextFadeTime + 1f);
        Destroy(pointText);
        */
        yield break;
    }

    IEnumerator StarshipSpawnIncreases()
    {
        while (true)
        {
            yield return new WaitForSeconds(starshipIncreaseRate);
            minStarships++;
        }
    }

    IEnumerator KamikazeSpawnIncreases()
    {
        while (true)
        {
            yield return new WaitForSeconds(kamikazeIncreaseRate);
            minKamikazes++;
        }

    }


    // Determines if the player is using a controller or a keyboard
    IEnumerator AcquireInputType()
    {
        started = false;
        Time.timeScale = 0f;
        textBank.inputTypeText.enabled = true;
        while (true)
        {
            device = InputManager.ActiveDevice;
            if (Input.GetKeyDown(KeyCode.Return))
            {
                beginText = textBank.beginTextKB;
                quitText = textBank.quitTextKB;
                restartText = textBank.restartTextKB;
                playerController.usingController = false;
                GameObject.Find("Equipped 2/Shield").GetComponent<BoxCollider>().enabled = false;
                GameObject.Find("Equipped 2/Shield").GetComponent<SpriteRenderer>().enabled = false;
                break;
            }
            if (device.Command.WasPressed)
            {
                beginText = textBank.beginText;
                quitText = textBank.quitText;
                restartText = textBank.restartText;
                playerController.usingController = true;
                break;
            }
            yield return null;
        }
        StartCoroutine(BeforeGameCoroutine());
    }

    IEnumerator BeforeGameCoroutine()
    {
        acquiringInput = false;
        textBank.inputTypeText.enabled = false;
        started = false;
        BeforeGame();
        while (true)
        {
            yield return null;
            if (playerActions.pressStart.WasPressed)
            {
                BeginGame();
                yield break;
            }
        }

    }


    IEnumerator DisclaimerTimer()
    {
        yield return new WaitForSeconds(15f);
        if (disclaimerText)
        {
            disclaimerText.enabled = false;
        }
        else
        {
            Debug.LogWarning("No disclaimer text found");
        }
        if (controlsText)
        {
            controlsText.enabled = false;
        }
        else
        {
            Debug.LogWarning("no controls text found");
        }
        disclaimerRunning = false;
    }

    void QuitGame()
    {
        Application.Quit();
    }
}
