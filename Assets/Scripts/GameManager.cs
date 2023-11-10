using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Text = TMPro.TextMeshProUGUI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Text timerText;
    public int initialSeconds = 10; // 初始分钟数
    public int coinTimeBonus = 2; // 每个金币的秒数奖励
    public int totalCoins = 0; // 总金币数量
    public int collectedCoins = 0; // 已收集金币数量
    public Text RestartText; // 重新开始按钮游戏对象
    public Text ExitText; // 退出按钮游戏对象

    private float currentTime;
    private bool isGameOver = false;
    private AudioSource audioSource;

    void Start()
    {
        RestartText.gameObject.SetActive(false);
        ExitText.gameObject.SetActive(false);
        // 初始化audioSource
        audioSource = GetComponent<AudioSource>();
        PlayMusic();
        currentTime = initialSeconds; // 将初始分钟数转化为秒数
        UpdateTimerText();
    }

    void Update()
    {
        if (!isGameOver)
        {
            if (currentTime > 0)
            {
                currentTime -= Time.deltaTime;
            }
            else
            {
                currentTime = 0;
                if (totalCoins > 0)
                {
                    GameOver(); // 游戏结束
                }
            }

            UpdateTimerText();
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                Restart();
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                Exit();
            }
        }

    }

    // 播放音乐
    public void PlayMusic()
    {
        if (audioSource != null)
        {
            audioSource.Play();
        }
    }

    // 暂停音乐
    public void PauseMusic()
    {
        if (audioSource != null)
        {
            audioSource.Pause();
        }
    }


    void UpdateTimerText()
    {
        int minutes = Mathf.FloorToInt(currentTime / 60);
        int seconds = Mathf.FloorToInt(currentTime % 60);
        timerText.text = "Time: " + minutes.ToString("00") + ":" + seconds.ToString("00");
    }

    public void CollectCoin()
    {
        totalCoins--;
        collectedCoins++;
        currentTime += coinTimeBonus;

        if (totalCoins == 0)
        {
            // 所有金币已收集，游戏胜利
            GameOver();
        }
    }

    public void Restart()
    {
        Time.timeScale = 1; // 開始游戏
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void GameOver()
    {
        isGameOver = true;
        // 显示游戏结束状态
        if (totalCoins == 0)
        {
            timerText.text = "You Win!";
        }
        else
        {
            timerText.text = "Game Over";
        }

        PauseMusic();
        // 启用 "RestartButton" 和 "ExitButton" 游戏对象
        RestartText.gameObject.SetActive(true);
        ExitText.gameObject.SetActive(true);

        Time.timeScale = 0; // 暂停游戏
    }
}
