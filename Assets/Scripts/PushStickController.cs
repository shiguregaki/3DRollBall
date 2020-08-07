using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushStickController : MonoBehaviour
{
    // 参考
    // [2点間に引く線をLine Rendererで描画する[Unity5][2D]](https://qiita.com/otuki0191/items/0be096c527d542d796cc)
    // [LineRenderer](https://docs.unity3d.com/ja/current/ScriptReference/LineRenderer.html)

    Vector3 startPos;
    Rigidbody rb;
    float forceRate = 4.0f;
    LineRenderer line;
    GameObject PushStick, BottomWall;

    void Start()
    {
        rb = this.GetComponent<Rigidbody>();

        this.PushStick = GameObject.Find("PushStick");
        this.BottomWall = GameObject.Find("BottomWall");

        //コンポーネントを取得する
        this.line = GetComponent<LineRenderer>();

        //線の色と幅を決める
        this.line.startColor = new Color(0.821f, 0.750f, 0.097f, 1.0f);
        this.line.endColor = new Color(0.821f, 0.750f, 0.097f, 1.0f);
        this.line.startWidth = 0.3f;
        this.line.endWidth = 0.3f;

        //頂点の数を決める
        this.line.positionCount = 2;
    }

    void Update()
    {
        // ドラッグの長さを求める
        if (Input.GetMouseButtonDown(0))
        {
            this.startPos = Input.mousePosition;
        }
        else if (Input.GetMouseButton(0))
        {
            Vector3 currentPos = Input.mousePosition;
            float force_y = this.forceRate * (currentPos.y - this.startPos.y);
            Vector3 force = new Vector3(0.0f, 0.0f, force_y);
            rb.AddForce(force);
        }
        // バネのデザイン用の線を引く
        line.SetPosition(0, PushStick.transform.position);
        line.SetPosition(1, new Vector3(
            PushStick.transform.position.x, 
            PushStick.transform.position.y, 
            BottomWall.transform.position.z));
    }
}
