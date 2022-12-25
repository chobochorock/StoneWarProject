using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public GameObject soldier_obj_w;
    public GameObject soldier_obj_b;
    public GameObject soldier_obj;
    public GameObject[] all_soldier = new GameObject[30];
    public GameObject movePointObject;
    public GameObject illegalMovePointObject;
    public GameObject win_panel_w;
    public GameObject win_panel_b;
    public GameObject ui_w;
    public GameObject ui_b;
    public GameObject reset_button;
    public StoneCreateTiles white_stone_create_tile;
    public StoneCreateTiles black_stone_create_tile;
    AudioSource audioSource;

    public bool is_stone_clicked;
    public bool is_white_turn;
    public bool reset;
    public bool is_playing;
    public int remaining_movement;
    public int limit_of_stone_w;
    public int limit_of_stone_b;
    public int stone_order;
    public int limit_time;
    public float layer_order;
    public string winner;
    public Vector2 last_pos_of_clicked;
    public List<List<List<string>>> coordinates = new List<List<List<string>>>();

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        CreateStoneCreateTile();
        Init();
        //CreatMovePoint();
        

    }

    void Awake()
    {
        CreateCoordinates();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (remaining_movement == 0)
        {
            NextTurn();
        }
        Winner();
    }

    public void Init()
    {
        
        is_white_turn = false;
        win_panel_w.SetActive(false);
        win_panel_b.SetActive(false);
        soldier_obj = soldier_obj_b;
        limit_of_stone_w = 12;
        limit_of_stone_b = 12;
        limit_time = 30;
        remaining_movement = 1;
        layer_order = -1;
        stone_order = 0;
        winner = "";
        for (int i = 0; i < 6; i++)
        {
            for (int j = 0; j < 6; j++)
            {
                for (int k = 0; k < coordinates[i][j].Count; k++)
                {
                    coordinates[i][j][k] = "";
                    
                }
            }
        }
        ui_b.SetActive(true);
        ui_w.SetActive(true);
        soldier_obj_w.SetActive(true);
        soldier_obj_b.SetActive(true);
    }

    public void ResetGame()
    {
        Init();
        reset_button.SetActive(false);
    }

    void CreateCoordinates()
    {
        for (int i = 0; i < 6; i++)
        {
            coordinates.Add(new List<List<string>>());
            for (int j = 0; j < 6; j++)
            {
                coordinates[i].Add(new List<string>());
                coordinates[i][j].Add("");
                
            }
        }

    }

    void CreateStoneCreateTile()
    {
        for (int x = -2; x <= 3; x++)
        {
            Instantiate(white_stone_create_tile, new Vector3(x, 3, -1), transform.rotation);
        }
        for (int x = -2; x <= 3; x++)
        {
            Instantiate(black_stone_create_tile, new Vector3(x, -2, -1), transform.rotation);
        }
    }

    void CleanField()
    {
        for (int a = 0; a < stone_order; a++)
        {
            
            all_soldier[a].SetActive(false);
            all_soldier[a] = null;

        }
    }

    void NextTurn()
    {
        remaining_movement = 2;
        if (is_white_turn)
        {
            soldier_obj = soldier_obj_b;
            is_white_turn = false;
        }
        else
        {
            soldier_obj = soldier_obj_w;
            is_white_turn = true;
        }

    }

    public bool IsNear(Vector2 a, GameObject b)
    {
        if (Mathf.Abs((int)a.x
                - b.transform.position.x) +
            Mathf.Abs((int)a.y
                - b.transform.position.y) == 1)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    

    void CreateMovePoint()
    {
        for (int x = -2; x <= 3; x++)
        {
            for (int y = -2; y <= 3; y++)
            {
                GameObject move_point =
                    Instantiate(movePointObject,
                    new Vector3(x, y, 0.5f), transform.rotation);
                GameObject cant_go_point =
                    Instantiate(illegalMovePointObject,
                    new Vector3(x, y, 0.5f), transform.rotation);
            }
        }
        
    }

    void Winner()
    {
        if (winner == "white")
        {
            win_panel_w.SetActive(true);
            audioSource.Play();
            reset_button.SetActive(true);
            CleanField();
            is_playing = false;


        }
        else if (winner == "black")
        {
            win_panel_b.SetActive(true);
            audioSource.Play();
            reset_button.SetActive(true);
            CleanField();
            is_playing = false;
        }
        winner = "";
        
    }

    
}
