using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    float moveSpd = 0.04f;
    float offset = 0.5f;
    public GameObject bulletPrefab;
    //public GameObject gameManager;
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        Move(Input.GetAxis("Horizontal"), moveSpd);

        /*
        if (Input.GetKey(KeyCode.RightArrow)) {
            Move(1,moveSpd);
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            Move(-1, moveSpd);
        }*/

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
        } 
    }

    void Move(float dir, float spd)
    {
        Vector3 posAdd = new Vector3(dir * spd, 0, 0);
        if (Mathf.Abs(gameObject.transform.position[0] + posAdd[0]) > 8) {
            return;
        }

        gameObject.transform.position += posAdd;
    }

    void Shoot()
    {
        animator.SetTrigger("shoot");
        GameObject bullet = Instantiate(bulletPrefab, new Vector2(transform.position[0], transform.position[1] + offset), Quaternion.identity);
        bullet.GetComponent<BulletControl>().dir = 1;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "bullet") {
            Destroy(collision.gameObject);
            animator.SetTrigger("die");
            Destroy(gameObject, 0.75f);
        }
    }
}
