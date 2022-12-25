using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RemainStoneNum : MonoBehaviour
{

    Text RSNText;
    public GameManager gameManager;
    public bool is_white = true;

    void Start()
    {
        RSNText = GetComponent<Text>();
    }

    void Update()
    {
        DisplayRemainNumber();
    }

    void DisplayRemainNumber()
    {
        if (is_white)
            RSNText.text = " X " + Convert.ToString(gameManager.limit_of_stone_w); 
        else
            RSNText.text = " X " + Convert.ToString(gameManager.limit_of_stone_b);
    }

}
