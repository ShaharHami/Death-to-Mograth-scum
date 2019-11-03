using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndGameScoreUI : MonoBehaviour
{
    [SerializeField] GameObject backToStartButton;
    [SerializeField] GameObject saveButton;
    private Text scoreText;
    ScoreBoard scoreBoard;
    private InputField input;
    private HighScoreTable highScoreTable;
    SceneLoader sceneLoader;
    Player player;
    void Awake()
    {
        if (backToStartButton == null)
        {
            backToStartButton = new GameObject();
        }
        player = FindObjectOfType<Player>();
        if (player != null)
        {
            player.OnGameEnd();
        }
        sceneLoader = (SceneLoader)FindObjectOfType(typeof(SceneLoader));
        saveButton.SetActive(true);
        // backToStartButton.SetActive(false);
        scoreBoard = FindObjectOfType<ScoreBoard>();
        scoreText = GameObject.Find("YourScoreNumber").GetComponent<Text>();
        scoreText.text = scoreBoard.score.ToString();
        GetTable();
        input = GameObject.Find("ScoreInputField").GetComponent<InputField>();
    }

    private void GetTable()
    {
        GameObject highScoreTableGo = GameObject.FindGameObjectWithTag("HighScoreTable");
        highScoreTable = highScoreTableGo.GetComponent<HighScoreTable>();
    }

    public void SaveScore()
    {
        if (input.text != "")
        {
            highScoreTable.AddHighScoreEntry(scoreBoard.score, input.text);
            saveButton.SetActive(false);
        }
    }
    public void ViewHighScores()
    {
        highScoreTable.ToggleHighScoreTable();
        gameObject.SetActive(false);
    }
    public void ReturnToStartScreen()
    {
        UnPauseMusic();
        sceneLoader.LoadSceneN(0);
    }
    public void Retry()
    {
        UnPauseMusic();
        sceneLoader.LoadSceneN(1);
    }

    private static void UnPauseMusic()
    {
        MusicPlayer musicPlayer = FindObjectOfType<MusicPlayer>();
        AudioSource music = musicPlayer.GetComponent<AudioSource>();
        music.UnPause();
    }
}
