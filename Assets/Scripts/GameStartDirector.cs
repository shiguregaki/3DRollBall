using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameStartDirector : MonoBehaviour
{
    GameObject latestScoreText;

    void Start()
    {
        this.latestScoreText = GameObject.Find("LatestScore");
        if (GameDirector._score >= 0)
        {
            this.latestScoreText.GetComponent<Text>().text = "Latest Score : " + GameDirector._score.ToString() + " 点";
        }
        else
        {
            this.latestScoreText.GetComponent<Text>().text = "Latest Score : - 点";
        }
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SceneManager.LoadScene("GameScene");
        }
    }
}
