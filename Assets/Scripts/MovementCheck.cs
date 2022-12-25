using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MovementCheck : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    Image image;

    public GameManager gameManager;
    

    public int movement_num;
    public bool is_white;


    void Start()
    {
        image = GetComponent<Image>();
        //spriteRenderer = GetComponent<SpriteRenderer>();
        image.color = new Color(1, 1, 1, 0.5f);
        //spriteRenderer.color = new Color(1, 1, 1, 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        OnNOff();
    }

    void OnNOff()
    {
        if (movement_num <= gameManager.remaining_movement 
            && is_white == gameManager.is_white_turn)
        {
            image.color = new Color(1, 1, 1, 1);
            //spriteRenderer.color = new Color(1, 1, 1, 1);
        }
        else
        {
            image.color = new Color(1, 1, 1, 0.5f);
            //spriteRenderer.color = new Color(1, 1, 1, 0.5f);
        }
    }
}
