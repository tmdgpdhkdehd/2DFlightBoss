using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class gameMgr : MonoBehaviour
{
    public static gameMgr instance;

    public GameObject player;

    public GameObject[] enemies;
    public Transform[] spawnPlaces;

    Rigidbody2D rigid ;

    public float maxSpawnDelay = 1;
    public float curSpawnDelay = 0;

    public float curScore = 0;

    int randEnemy ;
    int randomPoint;

    public Text scoreText;
    public Canvas Score;

    bool end = false;


    private void Awake()
    {
        instance = this;

        curSpawnDelay += Time.deltaTime;
        curSpawnDelay = 0;

        randomPoint = 1;
        randEnemy = Random.Range(0, 1);
    }
    void Start()
    {
        GameObject enemy = Instantiate(enemies[randEnemy],
            spawnPlaces[randomPoint].position,
            spawnPlaces[randomPoint].rotation);

        rigid = enemy.GetComponent<Rigidbody2D>();

        Score.gameObject.SetActive(false);

        Time.timeScale = 1;

        Invoke("endGame", 60.0f);
    }

    void Update()
    {
        if (end)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneChange.instance.IntroScene();
            }
        }
    }

    public void incScore(float amount)
    {
        curScore += amount;
        scoreText.text = curScore.ToString();
    }

    public void restorePlayer()
    {
        Invoke("respawnPlayerLater", 2f);
    }

    public void respawnPlayerLater()
    {
        player.transform.position = Vector3.down * 0.6f;
        player.SetActive(true);
        player.transform.GetComponent<playerCtrl>().Initialize(); // 초기화 
    }

    public void endGame()
    {
        Score.gameObject.SetActive(true);
        end = true;
        Time.timeScale = 0;
    }
}
