using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCtrl : MonoBehaviour
{
    public float speed = 1;
    Rigidbody2D rigid;

    public GameObject player;

    public GameObject bulletA;

    public float maxShootingDelay = 1.0f;
    public float curShootingDelay = 0.0f;

    public float health = 100;
    SpriteRenderer spriteRender;

    public GameObject manager;

    public GameObject itemToDrop; // drop item 

    public int patternIndex;
    public int curPatternCount;
    public int[] maxPatternCount;

    public AudioClip audioE_Attack;
    public AudioClip audioItem;
    AudioSource audioSource;

    void Awake()
    {
        this.audioSource = this.gameObject.AddComponent<AudioSource>();
        this.audioSource.loop = false;
    }

    void Start()
    {
        player = GameObject.Find("Player");
        spriteRender = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        curShootingDelay += Time.deltaTime;
        if (curShootingDelay < maxShootingDelay)
            return;

        curShootingDelay = 0;

        Think();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "borderBullet") // 자신 소멸
            Destroy(this.gameObject);

        if (collision.gameObject.tag == "playerBullet") // 플레이어 총알
        {
            bullet bullet = collision.gameObject.GetComponent<bullet>();

            Debug.Log(health);
            health -= bullet.damage;


            spriteRender.color = new Color(0.46f, 0.87f, 0.95f, 1);
            Invoke("restoreColor", 0.1f);

            Destroy(collision.gameObject); // 총알 소멸 

            gameMgr.instance.incScore(10);

            if (health == 16000 || health == 13000 || health == 10000 || health == 7000 || health == 4000)
            {
                GameObject item01
                    = Instantiate(itemToDrop,
                                  transform.position + Vector3.down * 0.2f,
                                  transform.rotation); // 아이템 드롭

                this.audioSource.volume = 1;
                this.audioSource.PlayOneShot(audioItem);

                // 아래로 느리게 이동
                item01.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, -1) * 30);
            }
            else if (health <= 0)
            {
                Destroy(gameObject);
            }
        }
    }

    void restoreColor()
    {
        spriteRender.color = new Color(1, 1, 1, 1);
    }

    void Think()
    {
        patternIndex = patternIndex == 3 ? 0 : patternIndex + 1;
        curPatternCount = 0;

        switch (patternIndex)
        {
            case 0:
                FireFoward();
                break;
            case 1:
                FireShot();
                break;
            case 2:
                FireArc();
                break;
            case 3:
                FireAround();
                break;
        }
        this.audioSource.volume = 0.5f;
        this.audioSource.PlayOneShot(audioE_Attack);
    }

    void FireFoward()
    {
        GameObject bullet01 = Instantiate(bulletA, transform.position, transform.rotation);
        GameObject bullet02 = Instantiate(bulletA, transform.position, transform.rotation);
        GameObject bullet03 = Instantiate(bulletA, transform.position, transform.rotation);
        GameObject bullet04 = Instantiate(bulletA, transform.position, transform.rotation);
        GameObject bullet05 = Instantiate(bulletA, transform.position, transform.rotation);
        GameObject bullet07 = Instantiate(bulletA, transform.position, transform.rotation);

        Rigidbody2D rigid01 = bullet01.GetComponent<Rigidbody2D>();
        Rigidbody2D rigid02 = bullet02.GetComponent<Rigidbody2D>();
        Rigidbody2D rigid03 = bullet03.GetComponent<Rigidbody2D>();
        Rigidbody2D rigid04 = bullet04.GetComponent<Rigidbody2D>();
        Rigidbody2D rigid05 = bullet05.GetComponent<Rigidbody2D>();
        Rigidbody2D rigid07 = bullet07.GetComponent<Rigidbody2D>();

        bullet01.transform.position = transform.position + Vector3.right * 0.5f;
        bullet02.transform.position = transform.position + Vector3.right * 0.7f;
        bullet03.transform.position = transform.position + Vector3.left * 0.5f;
        bullet04.transform.position = transform.position + Vector3.left * 0.7f;
        bullet05.transform.position = transform.position + Vector3.right * 0.9f;
        bullet07.transform.position = transform.position + Vector3.left * 0.9f;

        rigid01.AddForce(Vector2.down * 1.3f, ForceMode2D.Impulse);
        rigid02.AddForce(Vector2.down * 1.3f, ForceMode2D.Impulse);
        rigid03.AddForce(Vector2.down * 1.3f, ForceMode2D.Impulse);
        rigid04.AddForce(Vector2.down * 1.3f, ForceMode2D.Impulse);
        rigid05.AddForce(Vector2.down * 1.3f, ForceMode2D.Impulse);
        rigid07.AddForce(Vector2.down * 1.3f, ForceMode2D.Impulse);

        curPatternCount++;

        if (curPatternCount >= maxPatternCount[patternIndex])
            Invoke("Think", 4);
    }

    void FireShot()
    {
        for (int index = 0; index < 5; index++)
        {
            GameObject bullet01 = Instantiate(bulletA, transform.position, transform.rotation);
            bullet01.transform.position = transform.position;

            Rigidbody2D rigid01 = bullet01.GetComponent<Rigidbody2D>();
            Vector2 dirVec = player.transform.position - transform.position;
            Vector2 ranVec = new Vector2(Random.Range(-0.5f, 0.5f), Random.Range(0f, 2f));
            dirVec += ranVec;
            rigid01.AddForce(dirVec.normalized * 1.3f, ForceMode2D.Impulse);
        }

        curPatternCount++;

        if (curPatternCount >= maxPatternCount[patternIndex])
            Invoke("Think", 4);
    }

    void FireArc()
    {
        GameObject bullet01 = Instantiate(bulletA, transform.position, transform.rotation);
        bullet01.transform.position = transform.position;
        bullet01.transform.rotation = Quaternion.identity;

        Rigidbody2D rigid01 = bullet01.GetComponent<Rigidbody2D>();
        Vector2 dirVec = new Vector2(Mathf.Sin(curPatternCount), -1);
        rigid01.AddForce(dirVec.normalized * 1.5f, ForceMode2D.Impulse);

        curPatternCount++;

        if (curPatternCount < maxPatternCount[patternIndex])
            Invoke("FireArc", 0.15f);
        else
            Invoke("Think", 4);
    }

    void FireAround()
    {
        int roundNumA = 20;
        for (int index = 0; index < roundNumA; index++)
        {
            GameObject bullet01 = Instantiate(bulletA, transform.position, transform.rotation);
            bullet01.transform.position = transform.position;
            bullet01.transform.rotation = Quaternion.identity;

            Rigidbody2D rigid01 = bullet01.GetComponent<Rigidbody2D>();
            Vector2 dirVec = new Vector2(Mathf.Cos(Mathf.PI * 2 * index / roundNumA)
                , Mathf.Sin(Mathf.PI * 2 * index / roundNumA));
            rigid01.AddForce(dirVec.normalized * 0.7f, ForceMode2D.Impulse);

            Vector3 rotVec = Vector3.forward * 360 * index / roundNumA + Vector3.forward * 90;
            bullet01.transform.Rotate(rotVec);
        }

        curPatternCount++;

        if (curPatternCount >= maxPatternCount[patternIndex])
            Invoke("Think", 4);
    }
}
