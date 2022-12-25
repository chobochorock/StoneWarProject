using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneCreateTiles : MonoBehaviour
{

    SpriteRenderer spriteRenderer;
    BoxCollider2D boxCollider;
    AudioSource audioSource;

    public GameManager gameManager;
    
    
    bool is_pointer_on;
    public bool is_white;


    void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = new Color(1, 1, 1, 0);
        audioSource = GetComponent<AudioSource>();
        

        /**/
    }

    
    void Update()
    {
        IsPointerOn();
        CreateStone();
    }

    void IsPointerOn()
    {
        
        Vector2 mouse_pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (is_white == gameManager.is_white_turn 
            && ((gameManager.limit_of_stone_w != 0 && is_white)
            || (gameManager.limit_of_stone_b != 0 && !is_white)))
        {
            
            if (boxCollider == Physics2D.OverlapPoint(mouse_pos))
            {
                is_pointer_on = true;
                spriteRenderer.color = new Color(1, 1, 1, 0.5f);
                //Debug.Log("포인터 온");
            }
            else
            {
                is_pointer_on = false;
                spriteRenderer.color = new Color(1, 1, 1, 0);
            }
        }
        else
        {
            spriteRenderer.color = new Color(1, 1, 1, 0);
        }
        
    }

    void CreateStone()
    {

        if (Input.GetMouseButtonUp(0) 
            && is_pointer_on 
            && is_white == gameManager.is_white_turn)
        {
            if (gameManager.limit_of_stone_w!=0 && is_white)
            {
                Create();
                gameManager.limit_of_stone_w--;
                gameManager.remaining_movement--;
                audioSource.Play();
                gameManager.is_playing = true;
            }
            else if (gameManager.limit_of_stone_b!=0 && !is_white)
            {
                Create();
                gameManager.limit_of_stone_b--;
                gameManager.remaining_movement--;
                audioSource.Play();
                gameManager.is_playing = true;
            }
            

            

        }

    }

    void Create()
    {
        Vector2 mouse_pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        GameObject soldier =
                Instantiate(gameManager.soldier_obj,
                new Vector3((int)Mathf.Round(mouse_pos.x),
                (int)Mathf.Round(mouse_pos.y), gameManager.layer_order),
                transform.rotation);
    }
}
