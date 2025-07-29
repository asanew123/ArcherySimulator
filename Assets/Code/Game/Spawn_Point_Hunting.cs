using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn_Point_Hunting : MonoBehaviour
{
    public GameObject Rab;      // 1. 토끼
    public GameObject Fo;       // 2. 여우
    public GameObject Wo;       // 3. 늑대
    public GameObject Eag;      // 4. 독수리
    public GameObject Bea;      // 5. 곰

    public GameObject DB;       // 6. 드래곤 보어
    public GameObject DSE;      // 7. 드래곤 소울 이터
    public GameObject Eag_F;    // 8. 독수리(판타지)
    public GameObject Mino;     // 9. 미노타우루스

    public int model = 0;

    public GameObject GM;

    Game_Manager G_M;

    public float Spawn_Next_Time = 9; 
    public float Spawn_Wait_Time = 0;

    bool Spawn_Ready = false;

    public int Enemy_Count = 0;
    public float Time_Count = 0;

    int Mode = 0;       // 1 = 현실   2 = 판타지

    GameObject P;
    Player Play;

    // Start is called before the first frame update
    void Start()
    {
        GM = GameObject.Find("Game_Manager");
        G_M = GM.transform.GetComponent<Game_Manager>();
        Spawn_Wait_Time = Random.Range(1, 3);

        P = GameObject.Find("Player");
        Play = P.transform.GetComponent<Player>();

        Mode = G_M.R_and_F;
    }

    // Update is called once per frame
    void Update()
    {
        if(Play.Time_Stop == false)
        {
            Spawn_Enemy();
        }  
    }

    void Spawn_Enemy()
    {
        if(Time_Count < G_M.Max_Time)   // 시간제
        {
            if(Spawn_Ready == true)
            {
                All_Animal_Spawn();
                Spawn_Wait_Time = Random.Range(Spawn_Next_Time - 3, Spawn_Next_Time + 6);
                Spawn_Ready = false;
                Enemy_Count += 1;
            }
            if(Spawn_Ready == false)
            {
                if(0 < Spawn_Wait_Time)
                {
                    Spawn_Wait_Time -= Time.deltaTime;
                }
                if(0 > Spawn_Wait_Time)
                {
                    Spawn_Wait_Time = 0;
                    Spawn_Ready = true;
                    if(Mode == 1)
                    {
                        model = Random.Range(1, 6);
                    }
                    if(Mode == 2)
                    {
                        model = Random.Range(6, 10);
                    }
                }
            }
            Time_Count += Time.deltaTime;
            G_M.Time_Text = G_M.Max_Time - Time_Count;
        }
    }

    void All_Animal_Spawn()
    {
        if(model == 1)
        {
            Instantiate(Rab, transform.position, transform.rotation);
        }
        if(model == 2)
        {
            Instantiate(Fo, transform.position, transform.rotation);
        }
        if(model == 3)
        {
            Instantiate(Wo, transform.position, transform.rotation);
        }
        if(model == 4)
        {
            Instantiate(Eag, transform.position, transform.rotation);
        }
        if(model == 5)
        {
            Instantiate(Bea, transform.position, transform.rotation);
        }

        if(model == 6)
        {
            Instantiate(DB, transform.position, transform.rotation);
        }
        if(model == 7)
        {
            Instantiate(DSE, transform.position, transform.rotation);
        }
        if(model == 8)
        {
            Instantiate(Eag_F, transform.position, transform.rotation);
        }
        if(model == 9)
        {
            Instantiate(Mino, transform.position, transform.rotation);
        }
    }
}
