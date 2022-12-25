using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePoint : MonoBehaviour
{

    SpriteRenderer spriteRenderer;

    public GameManager gameManager;

    public bool cant_go;


    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = new Color(1, 1, 1, 0);
    }

    // Update is called once per frame
    void Update()
    {
        //display_move();
    }

    void display_move()
    {
        if (gameManager.is_stone_clicked
            && gameManager.IsNear(
                gameManager.last_pos_of_clicked, gameObject))
        {
            Debug.Log("on");
            spriteRenderer.color = new Color(1, 1, 1, 0.5f);
        }
        else
        {
            spriteRenderer.color = new Color(1, 1, 1, 0);
        }
    }
    
}
