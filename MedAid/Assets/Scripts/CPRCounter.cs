using UnityEngine;
using UnityEngine.UI;

public class CPRCounter : MonoBehaviour
{
    public int cprCounter = 0;         // Tracks the number of successful CPR compressions
    public int counterGoal;            // The goal number of compressions to reach

    int airProvided;

    public Slider airSlider;           // Slider to track air given during CPR
    public Button airBtn;              // Button to activate air delivery when slider is full
    public Button cprBtn;              // Button to activate air delivery when slider is full

    public LevelTimer timer;           // Reference to the LevelTimer script to handle timing

    // Start is called before the first frame update
    void Start()
    {
        // Initialize slider to zero
        if (airSlider != null)
        {
            airSlider.value = 0;
        }

        // Ensure the air button is initially disabled
        if (airBtn != null)
        {
            airBtn.enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Ensure the timer is active
        if (timer != null)
        {
            // Check if the CPR counter has reached the goal
            if (cprCounter >= counterGoal)
            {
                WinCondition();
            }
        }

        // Enable the air button when the slider is full, otherwise keep it disabled
        if (airSlider != null && airBtn != null)
        {
            if (airSlider.value == airSlider.maxValue)
            {
                airBtn.enabled = true;   // Activate the air button when the slider is full
            }
            else
            {
                airBtn.enabled = false;  // Disable the air button when the slider is not full
            }
        }
    }

    // Method to increase the CPR counter and increment the air slider
    public void IncreaseCounter()
    {
        cprCounter++;                  // Increment the CPR counter
        if (airSlider != null)
        {
            airSlider.value++;          // Increment the air slider
        }
    }

    // Method to reset the air slider back to zero (e.g., after air is delivered)
    public void ResetSliderValue()
    {
        if (airSlider != null)
        {
            airSlider.value = 0;        // Reset slider value to zero
        }
        airProvided++;
        cprBtn.enabled = false;
        Invoke("EnableCPRBtn", 3);
    }

    // Handle the win condition when the CPR goal is reached
    private void WinCondition()
    {
        PlayerProgressManager.Instance.CPRLevelWin(airProvided);
    }
    void EnableCPRBtn()
    {
        cprBtn.enabled = true;
    }
}
