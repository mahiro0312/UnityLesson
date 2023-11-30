using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Core : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.name == "Tank_Shell")
        {
            Debug.Log(">>>>>>>>>>>>>>> Hit!");
            transform.localScale += new Vector3(1.0f, 1.0f, 1.0f);
            GetComponent<Rigidbody2D>().AddForce(new Vector2(1000.0f, -1000.0f));
        }
    }
}
