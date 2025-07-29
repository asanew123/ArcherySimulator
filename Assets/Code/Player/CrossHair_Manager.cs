
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossHair_Manager : MonoBehaviour
{
    public GameObject a1;
    public GameObject a2;
    public GameObject a3;

    GameObject P;
    Player Play;

    GameObject GM;
    Game_Manager G_M;

    int Game_Mode;

    float smooth = 5f;

    public RectTransform Rect;

    float CrossHair_Zoom = 0;
    float min = 1f;
    float max = 1.5f;
    public float zoom_speed = 1.01f;

    void Awake()
    {
        Rect = GetComponent<RectTransform>();
    }

    // Start is called before the first frame update
    void Start()
    {
        P = GameObject.Find("Player");
        Play = P.transform.GetComponent<Player>();

        GM = GameObject.Find("Game_Manager");
        G_M = GM.transform.GetComponent<Game_Manager>();

        Game_Mode = G_M.Game_Mode;

        CrossHair_Zoom = min;


        All_Off();
    }

    // Update is called once per frame
    void Update()
    {
        if(Game_Mode == 1 || Game_Mode == 2)
        {
            a1_a2_a3();
            Zoom_UI();
        }
    }

    void All_Off()
    {
        a1.SetActive(false);
        a2.SetActive(false);
        a3.SetActive(false);
    }

    void a1_a2_a3()
    {
        if(Play.Using_Arrow == 1)
        {
            a1.SetActive(true);
        }
        if(Play.Using_Arrow == 2)
        {
            a2.SetActive(true);
        }
        if(Play.Using_Arrow == 3)
        {
            a3.SetActive(true);
        }
    }

    void Zoom_UI()
    {
        if(Play.Zoom == true)
        {
            if(CrossHair_Zoom < 1.57f)
            {
                Rect.transform.localScale *= zoom_speed;
                CrossHair_Zoom *= zoom_speed;
            }
        }
        if(Play.Zoom == false)
        {
            if(CrossHair_Zoom > 1f)
            {
                Rect.transform.localScale /= zoom_speed;
                CrossHair_Zoom /= zoom_speed;
            }
        }
    }
}
