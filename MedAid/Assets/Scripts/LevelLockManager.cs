using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelLockManager : MonoBehaviour
{
    private Button btn;
    public bool isLocked;
    public Image lockImg;

    public int rankToUnlock;

    // Start is called before the first frame update
    void Start()
    {
        btn = GetComponent<Button>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isLocked)
        {
            btn.interactable = false;
            lockImg.gameObject.SetActive(true);
        }
        else
        {
            btn.interactable = true;
            lockImg.gameObject.SetActive(false);
        }
        if (rankToUnlock <= PlayerProgressManager.Instance.currentRank) isLocked = false;
    }
}
