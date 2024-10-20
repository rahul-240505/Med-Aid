using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SituationManager : MonoBehaviour
{
    public Sprite[] situations;

    public GameObject situationUI;
    public Image situationImg;

    void Start()
    {
        if (LevelManager.Instance.isTestLevel)
        {
            situationUI.SetActive(true);
            situationImg.sprite = situations[Random.Range(0, situations.Length)];
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
