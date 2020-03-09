using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControl : MonoBehaviour
{
    float angCount;
    float angSpd = 0.03f;
    float offset = -0.5f;
    public GameObject gameManager;
    public GameObject bulletPrefab;
    public bool canShoot = false;
    public bool dying = false;

    public int enemyScore = 10;

    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        angCount = Random.Range(0f, 360f);
        StartCoroutine("ShootEvent");
    }

    private IEnumerator ShootEvent()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(1f,5f)); // wait half a second

            if (canShoot)
            {
                GameObject bullet = Instantiate(bulletPrefab, new Vector2(transform.position[0], transform.position[1] + offset), Quaternion.identity);
                bullet.GetComponent<BulletControl>().dir = -1;
                animator.SetTrigger("shoot");

            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position[1] <= -4) {
            gameManager.GetComponent<GameManager>().gameOver();
            Destroy(gameObject);
        }
        angCount += angSpd;
        transform.rotation = Quaternion.Euler(0.0f, 0.0f, 10*Mathf.Sin(angCount));
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log("Collide");
        if (collision.gameObject.tag == "bullet")
        {
            Debug.Log("HIT");
            dying = true;
            gameManager.GetComponent<GameManager>().enemyDestroyed(enemyScore);
            Destroy(collision.gameObject);
            animator.SetTrigger("die");
            Destroy(gameObject, 0.75f);
       
            //Destroy(gameObject);
        }

        if (collision.gameObject.tag == "breakable")
        {
            Destroy(collision.gameObject);
        }

    }
}
