using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameDirector : MonoBehaviour
{
    // ビー玉の比率(赤,青,黒)
    int[] ratios = new int[3] { 2, 4, 1 };
    // ゲームの時間
    float gameTime = 30.0f;

    GameObject timerText, scoreText, GameStartDirector;
    public static int _score = -1;
    int score = 0;

    public void UpdateScore(int score, string marbleTypeTag)
    {
        if(marbleTypeTag == "RedMarble")
        {
            this.score += score * 5;
        }else if(marbleTypeTag == "BlueMarble")
        {
            this.score += score;
        }else if(marbleTypeTag == "BlackMarble" && score != 0)
        {
            // 黒色ビー玉かつどこかの得点スポットに入った場合
            this.score = this.score / 2;
        }
    }

    void Start()
    {
        this.timerText = GameObject.Find("Time");
        this.scoreText = GameObject.Find("Score");
        GameObject.Find("MarbleGenerator").GetComponent<MarbleGenerator>().SetRatios(this.ratios);
    }

    void Update()
    {
        this.gameTime -= Time.deltaTime;
        this.timerText.GetComponent<Text>().text = this.gameTime.ToString("F1");
        this.scoreText.GetComponent<Text>().text = this.score.ToString() + " 点";

        // 制限時間が過ぎたらゲーム開始シーンへ移動
        if (this.gameTime < 0)
        {
            SceneManager.sceneLoaded += GameStartSceneLoaded;
            SceneManager.LoadScene("GameStartScene");
        }
    }
    // 参考： https://note.com/suzukijohnp/n/n050aa20a12f1
    private void GameStartSceneLoaded(Scene next, LoadSceneMode mode)
    {
        // シーン切り替え時に呼ばれる
        GameDirector._score = this.score;
        SceneManager.sceneLoaded -= GameStartSceneLoaded;
    }
}
