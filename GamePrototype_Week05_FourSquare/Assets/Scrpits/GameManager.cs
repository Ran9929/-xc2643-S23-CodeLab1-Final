using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public int gameLength = 60;
    public float timer = 0;
    
    public TextMeshProUGUI displayText;
    public TextMeshProUGUI endTitile;
    public TextMeshProUGUI restartHint;
    public TextMeshProUGUI highestScore;
    
    
    private bool inGame = true;

    private int score = 0;

    public int Score
    {
        get
        {
            return score;
        }

        set
        {
            score = value;
        }
    }

    public List<int> highScores = new List<int>();

    private string FILE_PATH;
    private const string FILE_DIR = "/Data/";
    private const string FILE_NAME = "highScores.txt";
    
    
    private void Awake()
    {
        if (!Instance)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    
    void Start()
    {
        timer = 0;
        FILE_PATH = Application.dataPath + FILE_DIR + FILE_NAME;
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene("GameScene");
        }
        if (inGame)
        {
            timer += Time.deltaTime;
            endTitile.text = "";
            restartHint.text = "";
            highestScore.text = "";
            displayText.text =
                "Timer: " + (gameLength - (int)timer) + "\n" +
                "Score: " + score;
        }
    
            
        if (timer >= gameLength && inGame)
        {
            SceneManager.LoadScene("EndScene");
            Debug.Log("Game OVA!");
            inGame = false;
            UpdateHighScores();
            endTitile.text = "You Win!";
            restartHint.text = "Press Space to Restart";
            displayText.text = "";
        }
    }

    void UpdateHighScores()
    {
        
        if (highScores.Count == 0) 
        {
            string fileContents = File.ReadAllText(FILE_PATH);
            
            string[] fileSplit = fileContents.Split('\n');
            
            for (int i = 1; i < fileSplit.Length - 1; i++)
            {
                highScores.Add(Int32.Parse(fileSplit[i]));
            }
        }

        for (int i = 0; i < highScores.Count; i++)
        {
            if (highScores[i] < Score)
            {
                highScores.Insert(i, Score);
                break;
            }
        }
        
        highScores.RemoveRange(4, highScores.Count - 5);

        string highScoreStr = "High Scores:\n";
            
        for (int i = 0; i < highScores.Count; i++)
        {
            highScoreStr += highScores[i] + "\n";
        }

        highestScore.text = highScoreStr;
        File.WriteAllText(FILE_PATH, highScoreStr);
    }

    public void LoseGame()
    {
        SceneManager.LoadScene("EndScene");
        Debug.Log("Game OVA!");
        inGame = false;
        UpdateHighScores();
        endTitile.text = "Game OVA!";
        restartHint.text = "Press Space to Restart";
        displayText.text = "";
    }
}
