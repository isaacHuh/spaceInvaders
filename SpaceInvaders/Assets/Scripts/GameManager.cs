using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //[SerializeField] GameObject enemyPrefab;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Text gameText;
    [SerializeField] Text restartText;

    [SerializeField] GameObject playerPrefab;

    [SerializeField] GameObject smallEnemy;
    [SerializeField] GameObject midEnemy;
    [SerializeField] GameObject largeEnemy;

    [SerializeField] GameObject ufoEnemy;

    GameObject player;

    public static int score;
    public static int highScore;
    int lives = 3;

    int gridSizeRow = 5;
    int gridSizeColumn = 12;

    int numEnemies;

    int anchorX = -8;
    int anchorY = 0;

    int moveDir = 1;
    int movesMax = 5;
    int movesLeft;

    float moveTime = 2f;
    bool displayGameOver = false;

    List<List<GameObject>> enemyGrid = new List<List<GameObject>>();

    // Start is called before the first frame update
    void Start()
    {
        GameManager.score = 0;
        movesLeft = movesMax;
        numEnemies = gridSizeRow * gridSizeColumn;

        for (int y = 0; y < gridSizeRow; y++)
        {
            //add row to grid
            enemyGrid.Add(new List<GameObject>());
            for (int x = 0; x < gridSizeColumn; x++)
            {
                GameObject enemyPrefab = midEnemy;
                int enemyScore = 10;
                switch (y)
                {
                    case 0:
                        enemyPrefab = largeEnemy;
                        enemyScore = 10;
                        break;
                    case 1:
                        enemyPrefab = largeEnemy;
                        enemyScore = 10;
                        break;
                    case 2:
                        enemyPrefab = midEnemy;
                        enemyScore = 20;
                        break;
                    case 3:
                        enemyPrefab = midEnemy;
                        enemyScore = 20;
                        break;
                    case 4:
                        enemyPrefab = smallEnemy;
                        enemyScore = 30;
                        break;
                }

                //add enemy to grid
                GameObject enemy = Instantiate(enemyPrefab, new Vector2(anchorX + x, anchorY + (2.0f * y / 3.0f)), Quaternion.identity);
                enemy.GetComponent<EnemyControl>().gameManager = gameObject;
                enemy.GetComponent<EnemyControl>().bulletPrefab = bulletPrefab;
                enemy.GetComponent<EnemyControl>().enemyScore = enemyScore;

                enemyGrid[y].Add(enemy);

            }
        }
        assignShoot();

        player = Instantiate(playerPrefab, new Vector2(0, -4), Quaternion.identity);

        StartCoroutine("MoveEvent");
        StartCoroutine("UfoEvent");
    }

    // Update is called once per frame
    void Update()
    {
        if(score > highScore)
        {
            highScore = score;
        }

        gameText.text = "Lives: " + lives.ToString()+ " | Score: " + score.ToString("0000") + " | High Score: " + highScore.ToString("0000");

        if (displayGameOver) {
            SceneTraversal.LoadCredits();
            restartText.text = "Game Over\nPress R to Restart";
        }

        // check if player died and respawn
        if (player == null && lives > 0) {
            player = Instantiate(playerPrefab, new Vector2(0, -4), Quaternion.identity);
            lives--;
        }

        if ((player == null && lives == 0) || numEnemies == 0 && !displayGameOver) {
            gameOver();
        }

        if (Input.GetKeyDown(KeyCode.R)) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    private IEnumerator MoveEvent()
    {
        while (true)
        {
            yield return new WaitForSeconds(moveTime); // wait half a second
            if (movesLeft <= 0)
            {
                moveEnemiesDown();
                movesLeft = movesMax;
                moveDir *= -1;
            }
            else
            {
                moveEnemies();
                movesLeft--;
            }
        }
    }

    private IEnumerator UfoEvent()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(10f,20f)); // wait half a second
            GameObject enemy = Instantiate(ufoEnemy, new Vector2(9f, 4f), Quaternion.identity);
            enemy.GetComponent<UfoControl>().gameManager = gameObject;
        }
    }

    void moveEnemies() {
        for (int y = 0; y < gridSizeRow; y++)
        {
            for (int x = 0; x < gridSizeColumn; x++)
            {
                if (enemyGrid[y][x] == null) { continue; }
                Vector2 pos = enemyGrid[y][x].transform.position;
                pos[0] += moveDir;

                enemyGrid[y][x].transform.position = pos;
            }
        }
    }

    void moveEnemiesDown()
    {
        for (int y = 0; y < gridSizeRow; y++)
        {
            for (int x = 0; x < gridSizeColumn; x++)
            {
                if (enemyGrid[y][x] == null) { continue; }
                Vector2 pos = enemyGrid[y][x].transform.position;
                pos[1]--;

                enemyGrid[y][x].transform.position = pos;
            }
        }
    }

    public void enemyDestroyed(int score) {
        numEnemies--;
        GameManager.score += score;
        moveTime -= 0.035f;
        assignShoot();
    }

    public void assignShoot() {
        for (int x = 0; x < gridSizeColumn; x++)
        {
            for (int y = 0; y < gridSizeRow; y++)
            {
                if (enemyGrid[y][x] != null) {
                    if (!enemyGrid[y][x].GetComponent<EnemyControl>().dying)
                    {
                        enemyGrid[y][x].GetComponent<EnemyControl>().canShoot = true;
                        break;
                    }
                }
            }
        }
    }

    public void gameOver() {
        if (player != null) {
            Destroy(player);
            lives = 0;
        }

        displayGameOver = true;
        //SceneTraversal.LoadStart();
    }
}
