using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneChange : MonoBehaviour
{

    public static SceneChange instance;

    public GameObject gameMenu;
    public GameObject guide;
    public Text keyText;
    public Text gameName;

    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        guide.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            switch (gameName.text)
            {
                case "슈퍼 집지키기":
                    SceneManager.LoadScene("Defense");
                    break;
                case "슈퍼 슈팅":
                    SceneManager.LoadScene("Shooting");
                    break;
                case "슈퍼 마리오":
                    SceneManager.LoadScene("Mario");
                    break;
                case "슈퍼 브레인":
                    SceneManager.LoadScene("Brain");
                    break;
                case "슈퍼 은행원":
                    SceneManager.LoadScene("Intro_coin");
                    break;
                default:
                    break;
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            gameMenu.SetActive(true);
            guide.SetActive(false);
        }
    }

    public void DefenseScene()
    {
        gameMenu.SetActive(false);
        guide.SetActive(true);
        gameName.text = "슈퍼 집지키기";
        keyText.text = "A,S,D : 장애물 움직이기\nL : 건물 업그레이드\nK : 회전 속도 증가";
    }

    public void ShootingScene()
    {
        gameMenu.SetActive(false);
        guide.SetActive(true);
        gameName.text = "슈퍼 슈팅";
        keyText.text = "방향키 : 이동\nCtrl : 발사";
    }

    public void MarioScene()
    {
        gameMenu.SetActive(false);
        guide.SetActive(true);
        gameName.text = "슈퍼 마리오";
        keyText.text = "W : 점프";
    }

    public void BrainScene()
    {
        gameMenu.SetActive(false);
        guide.SetActive(true);
        gameName.text = "슈퍼 브레인";
        keyText.text = "SPACE : 게이지 조정\nA : 정답 제출";
    }

    public void CasherScene()
    {
        gameMenu.SetActive(false);
        guide.SetActive(true);
        gameName.text = "슈퍼 은행원";
        keyText.text = "A,S,D : 동전 넣기";
    }

    public void IntroScene()
    {
        SceneManager.LoadScene("Intro");
    }
}
