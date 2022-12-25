using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public GameManager gameManager;
    public Text TimerIndicator;
    public Image TimerBar;
    AudioSource audioSource;
    Color default_color;

    public bool is_white;
    
    bool is_audio_played;
    float currentValue;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        Init();
    }

    void Update()
    {
        if (gameManager.is_playing)
        {
            TimerProgress();
        }
        else
        {
            Init();
        }
        
    }

    void Init()
    {
        currentValue = gameManager.limit_time;
        default_color = TimerBar.color;
    }

    void TimerProgress()
    {
        
        if (gameManager.is_white_turn != is_white) // 턴 종료 또는 상대 턴
        {
            currentValue = gameManager.limit_time;
            Indicating();
            is_audio_played = false;
        }
        else if (currentValue <= 0) // 시간 종료
        {
            if (is_white)
            {
                gameManager.winner = "black";
            }
            else
            {
                gameManager.winner = "white";
            }
            
        }
        else 
        {
            currentValue -= Time.deltaTime;
            TimeAlarm();
        }
        Indicating();
    }

    void Indicating()
    {
        TimerIndicator.text = ((int)(currentValue + 0.99f)).ToString();
        TimerBar.fillAmount = currentValue / gameManager.limit_time;
    }

    void TimeAlarm()
    {
        
        if (currentValue <= (gameManager.limit_time / 3)
            && !is_audio_played)
        {
            audioSource.Play();
            is_audio_played = true;
        }

        //float color_time = 205 / (gameManager.limit_time / 3);

        /*
        if (is_white)
        {
            TimerBar.color -= new Color(0, color_time * Time.deltaTime,
                color_time * Time.deltaTime, 0);
        }
        else
        {
            TimerBar.color += new Color(color_time * Time.deltaTime, 0,0,0);
        }
        if (gameManager.is_white_turn != is_white)
        {
            TimerBar.color = default_color;
        }*/
    }
}
