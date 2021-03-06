using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CountDownTImer : MonoBehaviour
{
    public Text timer;
    public float time;
    public int loadlevel;

    private void Start()
    {
        //StartCountDownTimer();
    }

     /*void StartCountDownTimer()
    {
        timer.text = "TimeLeft: ";
        InvokeRepeating("UpdateTimer", 0.0f, 0.01667f);
    }
     */
    private void FixedUpdate()
    {
        if (timer != null)
        {
            time -= Time.deltaTime;
            string minutes = Mathf.Floor(time / 60).ToString("00");
            string seconds = Mathf.Floor(time % 60).ToString("00");
            string fraction = Mathf.Floor(time * 100).ToString("000");
            timer.text = "Time Left: " + minutes + ":" + seconds + ":" + fraction;
        }

        if(time <= 0)
        {
            SceneManager.LoadScene(loadlevel);
        }
    }
}
