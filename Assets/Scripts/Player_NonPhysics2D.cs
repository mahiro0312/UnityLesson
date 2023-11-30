using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player_NonPhysics2D : MonoBehaviour
{

    //フィールド 
    //Inspectorでの調整
    public float speed = 15.0f; //Playerの速度
    public Sprite[] run; //走りスプライト
    public Sprite[] jump;　//ジャンプスプライト

    //ローカル変数   
    float jumpVy;//プライヤーの上昇速度
    int animIndex; //アニメ再生インデックス
    bool goalCheck; //ゴール判定

    // Start is called before the first frame update
    void Start()
    {
        //初期化
        jumpVy = 0.0f;
        animIndex = 0;
        goalCheck = false;
    }

    //Playerに他のオブジェクトのコリジョンが入った
    private void OnCollisionEnter2D(Collision2D collision)
    {   
        //ゴールチェック
        if (collision.gameObject.name == "Stage_Gate")
        {
            goalCheck = true;
            return;
        }
        //ゴールでなければステージを再読み込みしてリセット
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // Update is called once per frame
    void Update()
    {
        if (goalCheck)//ゴールなら停止
        {
            return;
        }

        //現在のプレイヤーキャラの高さを計算
        float height = transform.position.y + jumpVy;

        //接地チェック
        if(height <= 0.0f)
        {
            //ジャンプ初期化
            height = 0.0f;
            jumpVy = 0.0f;

            //ジャンプチェック
            if (Input.GetButtonDown("Fire1"))
            {
                //ジャンプ処理
                jumpVy = +1.3f;

                //ジャンプスプライトの画像に切り替え
                GetComponent<SpriteRenderer>().sprite = jump[0];
            }
            else
            {
                //走り処理
                animIndex++;
                if(animIndex >= run.Length)
                {
                    animIndex = 0;
                }

                //走りスプライト画像に切り替え
                GetComponent<SpriteRenderer>().sprite = run[animIndex];
            }
        }
        else
        {
            //ジャンプ後の降下中
            //jumpVy -= 0.2f;
            jumpVy -= 6.0f * Time.deltaTime;
        }


        //プレイヤーのキャラ移動(座標設定)
        transform.position = new Vector3(
            transform.position.x + speed * Time.deltaTime, height, 0.0f);

        //カメラの移動(座標を相対移動)
        GameObject goCam = GameObject.Find("Main Camera");
        goCam.transform.Translate(speed * Time.deltaTime, 0.0f, 0.0f);
    }

    //GUIの表示
    void OnGUI()
    {
        //デバックテキスト
        GUI.TextField(new Rect(10, 10, 300, 60),
            "[Unity 2D Sample 3-1 A]\nマウスの左ボタンをマウスの左ボタンを押すと加速\nはなすとジャンプ");

        //リセットボタン
        if(GUI.Button(new Rect(10, 80, 100, 20), "リセット"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
