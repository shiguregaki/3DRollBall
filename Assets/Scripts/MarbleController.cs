using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MarbleController : MonoBehaviour
{
    // 削除するときのspeed
    float destroySpeed = 1.0f;
    // 削除するまでの時間
    float destroyTime = 1.0f;

    GameObject GameDirector, MarbleGenerator;
    Collider lastDetectCollider;
    bool detectFlg = false;
    bool isGenerated = false;

    void Start()
    {
        this.GameDirector = GameObject.Find("GameDirector");
        this.MarbleGenerator = GameObject.Find("MarbleGenerator");

        // タグにより色の判定
        if (this.tag == "RedMarble")
        {
            GetComponent<Renderer>().material.SetColor("_Color", new Color(1, 0.0f, 0.0f, 1));
        }
        else if (this.tag == "BlueMarble")
        {
            GetComponent<Renderer>().material.SetColor("_Color", new Color(0.227f, 0.855f, 1, 1));
        }
        else if (this.tag == "BlackMarble")
        {
            GetComponent<Renderer>().material.SetColor("_Color", new Color(0.605f, 0.590f, 0.622f, 1));
        }
        GetComponent<Renderer>().material.SetFloat("_DistortionE", 1.0f);
    }
    void Update()
    {
        int score = 0;
        // 速度を監視して、ビー玉が停止したら一番最後に触れたコライダに応じて得点をカウント
        if (this.GetComponent<Rigidbody>().velocity.magnitude <= this.destroySpeed)
        {
            if (this.lastDetectCollider != null && this.detectFlg == false)
            {
                this.detectFlg = true;

                // タグごとに得点を算出
                if (this.lastDetectCollider.tag == "100point")
                {
                    score = 100;
                }
                else if (this.lastDetectCollider.tag == "200point")
                {
                    score = 200;
                }
                else if (this.lastDetectCollider.tag == "500point")
                {
                    score = 500;
                }
                else
                {
                    score = 0;
                }
                // 得点をGameDirectoreに伝える
                this.GameDirector.GetComponent<GameDirector>().UpdateScore(score, this.tag);
                // 1秒後にビー玉を削除する
                StartCoroutine(DelayMethod(this.destroyTime, () =>
                {
                    Destroy(this.gameObject);
                }));
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        // MarbleDetectorであれば、ビー玉を生成
        if (other.name == "MarbleDetector" && !isGenerated)
        {
            // MarbleGeneratorでビー玉生成する
            this.MarbleGenerator.GetComponent<MarbleGenerator>().GenerateMarble();
            isGenerated = true;
        }
        // ScoreSpotであれば、lastDetectColliderに格納
        if (other.name == "ScoreSpot")
        {
            this.lastDetectCollider = other;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        // BottomWallであれば、lastDetectColliderに格納
        if (collision.collider.name == "BottomWall")
        {
            this.lastDetectCollider = collision.collider;
        }
    }
    /// <summary>
    /// 渡された処理を指定時間後に実行する
    /// </summary>
    /// <param name="waitTime">遅延時間[ミリ秒]</param>
    /// <param name="action">実行したい処理</param>
    /// <returns></returns>
    /// <remarks>https://qiita.com/toRisouP/items/e402b15b36a8f9097ee9</remarks>
    private IEnumerator DelayMethod(float waitTime, Action action)
    {
        yield return new WaitForSeconds(waitTime);
        action();
    }
}
