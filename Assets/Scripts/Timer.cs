using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [Header("Playing UI")]
    public TextMeshProUGUI timerText;

    [Header("Floats")]
    [SerializeField] float startTime = 100f;

    int sec;
    int min;

    bool gameOver = false;

    private void Update()
    {
        if (gameOver)
            return;

        startTime -= Time.deltaTime;
        sec = (int)(startTime % 60);
        min = (int)(startTime / 60 % 60);

        if(timerText != null)
            timerText.text = "Timer:" + string.Format("{0:00}:{1:00}", min, sec);


        if(startTime <= 0)
        {
            gameOver = true;
            UIManager.instance.LevelFailed();
        }
    }
}
