using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soldiers : MonoBehaviour
{
    public GameManager gameManager;

    SpriteRenderer spriteRenderer;
    BoxCollider2D boxCollider;
    AudioSource audioSource;
    Vector3 last_pos;
    Vector3 start_pos;

    int last_situation = 1;
    public bool dragging = false;
    
    public bool is_white;


    void Start()
    {
        
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();
        audioSource = GetComponent<AudioSource>();
        Init();
    }

    


    void Update()
    {
        DragNDrop();
        IsClicked();
        DifferentSituation();
        //IsUnderSameStone();
        
    }

    public void Init()
    {
        last_pos = transform.position;
        if (is_white)
        {
            gameManager.coordinates[(int)transform.position.x + 2]
            [(int)transform.position.y + 2].Add("white");

        }

        else
        {
            gameManager.coordinates[(int)transform.position.x + 2]
            [(int)transform.position.y + 2].Add("black");

        }
        gameManager.all_soldier[gameManager.stone_order] = gameObject;
        gameManager.stone_order++;
        Debug.Log(gameManager.stone_order);
    }


    void DifferentSituation()
    {
        if (last_situation != gameManager.remaining_movement)
        {
            if (gameManager.remaining_movement == 2)
            {
                start_pos = transform.position;
            }
            last_pos = transform.position;
            last_situation = gameManager.remaining_movement;
        }

    }

    void DragNDrop()
    {
        
        Vector2 mouse_pos =
            Camera.main.ScreenToWorldPoint(Input.mousePosition);



        if (Input.GetMouseButtonDown(0)
            && boxCollider == Physics2D.OverlapPoint(mouse_pos)
            && gameManager.is_white_turn == is_white)
        {
            dragging = true;
            gameManager.layer_order -= 0.001f;
            //Debug.Log(gameManager.layer_order);
            gameManager.is_stone_clicked = false;
            gameManager.is_stone_clicked = true;
            gameManager.last_pos_of_clicked = last_pos;



        }
        else if (Input.GetMouseButtonDown(0)
            && boxCollider != Physics2D.OverlapPoint(mouse_pos))
        {
            gameManager.is_stone_clicked = false;

        }



        if (dragging)
        {
            transform.position = new Vector3(mouse_pos.x,
                mouse_pos.y, gameManager.layer_order);
        }

        if (Input.GetMouseButtonUp(0)
            && boxCollider == Physics2D.OverlapPoint(mouse_pos))
        {
            dragging = false;
            transform.position = 
                new Vector3((int)Mathf.Round(mouse_pos.x),
                (int)Mathf.Round(mouse_pos.y), gameManager.layer_order);
            
            if (!gameManager.IsNear(last_pos, gameObject)
                || IsSameStoneUnder() || IsGoingBack())
            {
                transform.position = last_pos;
                //Debug.Log(transform.position);
                
            }
            else
            {
                audioSource.Play();
                UpdatePosition();
                IsGameOver();
            }
            
            
        }
    }

    public void UpdatePosition()
    {
        if (is_white)
        {
            gameManager.coordinates[(int)transform.position.x + 2]
            [(int)transform.position.y + 2].Add("white");

        }
        
        else
        {
            gameManager.coordinates[(int)transform.position.x + 2]
            [(int)transform.position.y + 2].Add("black");

        }
        gameManager.remaining_movement--;
        gameManager.coordinates[(int)last_pos.x + 2]
            [(int)last_pos.y + 2].
            RemoveAt(gameManager.coordinates[(int)last_pos.x + 2]
            [(int)last_pos.y + 2].Count - 1);

    }


    bool IsSameStoneUnder()
    {
        int x = (int)transform.position.x + 2;
        int y = (int)transform.position.y + 2;
        int z = gameManager.coordinates[x][y].Count - 1;

        //Debug.Log((x, y, z));
        
        if (gameManager.coordinates[x][y][z] == "")
        {
            return false;
        }
        else if (((gameManager.coordinates[x][y][z] == "white") && is_white)
            || ((gameManager.coordinates[x][y][z] == "black") && !is_white))
        {
            return true;
        }
        else
        {
            return false;
        }
        
        

    }

    void IsClicked()
    {
        Vector2 mouse_pos =
            Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetMouseButtonDown(0)
            && boxCollider == Physics2D.OverlapPoint(mouse_pos)
            && gameManager.is_white_turn == is_white)
        {
            gameManager.is_stone_clicked = false;
            gameManager.is_stone_clicked = true;
            gameManager.last_pos_of_clicked = last_pos;
        }

        else if (Input.GetMouseButtonDown(0)
            && boxCollider != Physics2D.OverlapPoint(mouse_pos))
        {
            gameManager.is_stone_clicked = false;

        }
    }

    bool IsGoingBack()
    {
        if (new Vector2(start_pos.x, start_pos.y)
            == new Vector2(transform.position.x, transform.position.y))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    void IsGameOver()
    {
        if (is_white && transform.position.y == -2)
        {
            gameManager.winner = "white";
        }

        else if (!is_white && transform.position.y == 3)
        {
            gameManager.winner = "black";
        }
    }
}
