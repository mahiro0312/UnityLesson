using System.Collections;
using UnityEngine;

public class Tank : MonoBehaviour
{
    GameObject goShell = null;
        bool action = false;
    private Rigidbody2D rb;
    
    // Start is called before the first frame update
    void Start()
    {   
        //砲弾のゲームオブジェクトの取得と非表示の設定
        goShell = transform.Find("Tank_Shell").gameObject;
        goShell.SetActive(false);  
        rb = GetComponent<Rigidbody2D>(); 
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButton(0)){ //ボタンが押されたら
            //タンクがクリックされたら
            Vector2 tapPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Collider2D collition2d = Physics2D.OverlapPoint(tapPoint);

            if(collition2d){
                if(collition2d.gameObject == gameObject){
                    action = true; //アクションを有効にする
                }
            }
            if(action){
                //タンクの移動
                rb.AddForce(new Vector2(+30.0f, 0.0f));
            }
        }
        else
        {
            if(Input.GetMouseButtonUp (0) && action) // ボタンが離れたら      
            {
                //砲弾発射
                if (goShell)
                {
                    goShell.SetActive(true);
                    rb.AddForce(new Vector2(+300.0f, 500.0f));
                    Destroy(goShell.gameObject, 3.0f);
                }
                action = false;

            }
        }
    }
}

void OnGUI()
{
    GUI.TextField(new Ract(10, 10, 300, 60)) ,
        "[Unity 2D でゲームを作る本　sample 2-1]\n戦車をクリックすると加速\n離すと発射！");
    if(GUI.Button(new Rect(10, 80, 100, 20), "リセット")) {
    Application.LoadLevel(Application.loadedLevelName);
    Use SceneManager.LoadScene;
}

         
}