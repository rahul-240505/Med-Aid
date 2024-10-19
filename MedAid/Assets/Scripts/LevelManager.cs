using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set; }

    public int noOfTries;
    public int maxTries;

    public int triesUsed;
    public Text triesTxt;
    void Awake()
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
    // Start is called before the first frame update
    void Start()
    {
        noOfTries = maxTries;
    }

    // Update is called once per frame
    void Update()
    {
        triesTxt.text = "Tries Used : " + triesUsed.ToString();
    }
}
