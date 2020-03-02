using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletControl : MonoBehaviour
{
    float spd = 0.06f;
    public int dir = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(0,dir*spd,0);

        if (Mathf.Abs(transform.position[1]) > 6) {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "breakable") {
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }
}
