using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextBank : MonoBehaviour
{
    public float currentScore;
    public float kamikazeFontSize, starshipFontSize, battleshipFontSize;
    public float pointTextFadeTime;
    public Text inputTypeText;
    public Text beginText;
    public Text beginTextKB;
    public Text pauseText;
    public Text currentScoreText;
    public Text finalScoreText;
    public Text healthText;
    public Text restartText;
    public Text restartTextKB;
    public Text quitText;
    public Text quitTextKB;
    public GameObject pointTextPrefab;

    void Start()
    {
        // inputTypeText = GameObject.Find("InputText").GetComponent<Text>();
        beginText = GameObject.Find("BeginText").GetComponent<Text>();
        // beginTextKB = GameObject.Find("BeginText (KB)").GetComponent<Text>();
        pauseText = GameObject.Find("PauseText").GetComponent<Text>();
        Debug.Log("Pause text has run");
        currentScoreText = GameObject.Find("ScoreText").GetComponent<Text>();
        finalScoreText = GameObject.Find("FinalScoreText").GetComponent<Text>();
        healthText = GameObject.Find("HealthText").GetComponent<Text>();
        restartText = GameObject.Find("RestartText").GetComponent<Text>();
        // restartTextKB = GameObject.Find("RestartText (KB)").GetComponent<Text>();
        quitText = GameObject.Find("QuitText").GetComponent<Text>();
        // quitTextKB = GameObject.Find("QuitText (KB)").GetComponent<Text>();
    }
}
