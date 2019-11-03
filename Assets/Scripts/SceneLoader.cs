using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField]
    GameObject highScoreTable;
    // private HighScoreTable highScoreTable;
    void Awake()
    {
        int sceneLoaders = FindObjectsOfType<SceneLoader>().Length;
        if (sceneLoaders > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    void LoadFirstScene()
    {
        SceneManager.LoadScene(1);
    }
    public void LoadSceneN(int n)
    {
        SceneManager.LoadScene(n);
    }
    public void Quit()
    {
        Application.Quit();
    }
    public void ViewHighScores()
    {
        Instantiate(highScoreTable);
    }
}
