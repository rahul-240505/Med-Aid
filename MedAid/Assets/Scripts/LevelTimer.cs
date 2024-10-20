using UnityEngine;
using UnityEngine.UI;

public class LevelTimer : MonoBehaviour
{
    public bool isTimerLevel;    // Flag to check if it's a timed level
    public float timer;          // Set the countdown timer duration (in seconds)

    public Text timerTxt;        // Reference to the Text component

    private float currentTime;   // Internal variable to track the time remaining

    // Start is called before the first frame update
    void Start()
    {
        if(LevelManager.Instance != null && LevelManager.Instance.isTestLevel) isTimerLevel = true;
        currentTime = timer;
    }

    // Update is called once per frame
    void Update()
    {
        if (isTimerLevel && currentTime > 0)
        {
            // Decrease the current time by the amount of time passed since the last frame
            currentTime -= Time.deltaTime;

            // Ensure the timer doesn't go below zero
            if (currentTime < 0)
            {
                currentTime = 0;
                PlayerProgressManager.Instance.Lose();
            }

            // Convert time to minutes and seconds
            int minutes = Mathf.FloorToInt(currentTime / 60);
            int seconds = Mathf.FloorToInt(currentTime % 60);

            // Display the time in the Text UI element
            timerTxt.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
    }
}
