using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomPlayerGenerator : MonoBehaviour
{
    public GameObject[] characters;
    void Start()
    {
        int rand = Random.Range(0, characters.Length);
        for (int i = 0; i < characters.Length; i++) {
            characters[i].SetActive(i == rand);
        }
    }

}
