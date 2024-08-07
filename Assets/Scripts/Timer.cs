using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] float remainingTime;




    // Update is called once per frame
    void Update()
    {

        if (remainingTime > 0)
        {
            remainingTime -= Time.deltaTime;

        }
        else if (remainingTime < 0)
        {
            remainingTime = 0;
            SceneManager.LoadSceneAsync(0);
            Misc.instance.resetScore();
        }

        int minutes = Mathf.FloorToInt(remainingTime / 60);
        int seconds = Mathf.FloorToInt(remainingTime % 60);


        timerText.text ="Time Left: "  + string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
