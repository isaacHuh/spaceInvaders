using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UfoControl : MonoBehaviour
{
    public GameObject gameManager;
    float spd = 0.05f;
    int enemyScore;

    // Start is called before the first frame update
    void Start()
    {
        enemyScore = Random.Range(50, 100);
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position += new Vector3(-spd, 0, 0);

        if (gameObject.transform.position[0] < -9) {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "bullet") {
            gameManager.GetComponent<GameManager>().enemyDestroyed(enemyScore);
            Destroy(gameObject);
        }
    }


}
