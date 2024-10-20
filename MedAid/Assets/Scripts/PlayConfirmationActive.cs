using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayConfirmationActive : MonoBehaviour
{
    private Button btn;
    public GameObject playConfirmation;
    public Image playConfirmationBackground;
    public Sprite background;

    public Button playBtn;
    public int levelIndex = 1;

    void Start()
    {
        btn = GetComponent<Button>();
        btn.onClick.AddListener(ActivatePlayConfirmation);
    }

    void ActivatePlayConfirmation()
    {
        playConfirmationBackground.sprite = background;
        playConfirmation.SetActive(true);
        // Using a lambda expression to pass the levelIndex when clicked
        playBtn.onClick.AddListener(() => PlayLoadLevel(levelIndex));
    }

    void PlayLoadLevel(int index)
    {
        Debug.Log("Loading level: " + index);
        
    }
}
