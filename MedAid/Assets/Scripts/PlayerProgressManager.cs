using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;


public class PlayerProgressManager : MonoBehaviour
{
    public static PlayerProgressManager Instance { get; private set; }

    [Header("Star System")]
    public GameObject winPanel;          // The panel to show on level completion
    public GameObject losePanel;
    public GameObject notTreatedPanel;
    public Image[] stars;                // Array to store star UI images
    public Sprite filledStar;            // Sprite for filled star
    public Sprite emptyStar;             // Sprite for empty star
    public int maxStars = 3;             // Max number of stars
    public int experiencePerStar = 10;   // Experience points per star

    [Header("Experience & Rank System")]
    public int currentExperience = 0;    // Player's current experience
    public Slider expSlider;
    public int currentRank = 1;          // Player's current rank
    public int[] rankThresholds;         // Experience points needed to reach each rank
    public Text rankText;                // UI Text to display player's rank
    public string[] rankNames = new string[6];
    public int winMatchNo;

    private string experienceKey = "CurrentExperience";
    private string rankKey = "CurrentRank";
    private string winKey = "CurrentWins";


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);  // Keep this object across scenes
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        if (ES3.KeyExists(experienceKey))
        {
            currentExperience = ES3.Load<int>(experienceKey);
        }
        if (ES3.KeyExists(rankKey))
        {
            currentRank = ES3.Load<int>(rankKey);
        }
        if (ES3.KeyExists(winKey))
        {
            winMatchNo = ES3.Load<int>(winKey);
        }
        rankText.text = rankNames[currentRank - 1];
        expSlider.maxValue = rankThresholds[currentRank];
        expSlider.value = currentExperience;

    }

    public void CheckWinCondition(int woundsTreated, int totalWounds)
    {
        if (woundsTreated >= totalWounds)  // All wounds treated
        {
            WinLevel();
        }
        else
        {
            notTreatedPanel.SetActive(true);
            LevelManager.Instance.triesUsed++;
        }
    }

    private void WinLevel()
    {
        // Show win panel
        winPanel.SetActive(true);

        // Calculate stars based on performance (e.g., tries used)
        int starsEarned = CalculateStars(LevelManager.Instance.triesUsed);

        // Display stars
        for (int i = 0; i < maxStars; i++)
        {
            stars[i].sprite = (i < starsEarned) ? filledStar : emptyStar;
        }

        // Award experience based on stars earned
        int experienceEarned = starsEarned * experiencePerStar;
        ES3.Save(winKey, ++winMatchNo);
        AddExperience(experienceEarned);
    }

    private int CalculateStars(int triesUsed)
    {
        if (triesUsed <= LevelManager.Instance.maxTries) return maxStars;  // Perfect performance
        else if (triesUsed <= LevelManager.Instance.maxTries + 2) return 2;    // Good performance
        else return 1;                        // Acceptable performance
    }

    public void AddExperience(int amount)
    {
        currentExperience += amount;
        ES3.Save(experienceKey, currentExperience);
        expSlider.value = currentExperience;
        CheckRankUp();
    }

    private void CheckRankUp()
    {
        // Check if experience surpasses any rank thresholds
        for (int i = 0; i < rankThresholds.Length; i++)
        {
            if (currentExperience >= rankThresholds[i] && currentRank <= i)
            {
                currentRank = i + 1;
                ES3.Save(rankKey, currentRank);
                rankText.text = rankNames[currentRank - 1];
                expSlider.maxValue = rankThresholds[currentRank];
                UnlockNewLevels();
            }
        }
    }

    public void CPRLevelWin(int airBreathe)
    {
        winPanel.SetActive(true);

        // Calculate stars based on performance (e.g., tries used)
        int starsEarned = CalculateStarsForCPR(airBreathe);

        // Display stars
        for (int i = 0; i < maxStars; i++)
        {
            stars[i].sprite = (i < starsEarned) ? filledStar : emptyStar;
        }

        // Award experience based on stars earned
        int experienceEarned = starsEarned * experiencePerStar;
        ES3.Save(winKey, ++winMatchNo);

        AddExperience(experienceEarned);
    }
    int CalculateStarsForCPR(int airBreathe)
    {
        if (airBreathe == 3) return 3;
        else if (airBreathe == 2) return 2;
        else return 1;
    }

    public void Lose()
    {
        losePanel.SetActive(true);
    }
    private void UnlockNewLevels()
    {
        // Logic to unlock new levels depending on the rank
        Debug.Log("New levels unlocked at Rank: " + currentRank);
    }
}
