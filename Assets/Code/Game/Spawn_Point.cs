using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn_Point : MonoBehaviour
{
    public GameObject Enemy_Siege;
    public GameObject GM;

    Game_Manager G_M;

    GameObject P;
    Player Play;

    public float Spawn_Next_Time = 9; 
    public float Spawn_Wait_Time = 0;

    bool Spawn_Ready = false;

    public int Enemy_Count = 0;
    public float Time_Count = 0;

    // Start is called before the first frame update
    void Start()
    {
        G_M = GM.transform.GetComponent<Game_Manager>();
        Spawn_Wait_Time = Random.Range(1, 3);

        P = GameObject.Find("Player");
        Play = P.transform.GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Play.Time_Stop == false)
        {
            Spawn_Siege();
        }
    }

    void Spawn_Siege()
    {
        if(G_M.All_Enemy_Count < G_M.Max_Enemy && G_M.Time_or_Limited == 2) // 한정된 적의수
        {
            if(Spawn_Ready == true)
            {
                Instantiate(Enemy_Siege, transform.position, transform.rotation);
                G_M.All_Enemy_Count += 1;
                Spawn_Wait_Time = Random.Range(Spawn_Next_Time - 3, Spawn_Next_Time + 3);
                Spawn_Ready = false;
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
                }
            }
        }
        if(Time_Count < G_M.Max_Time && G_M.Time_or_Limited == 1)   // 시간제
        {
            if(Spawn_Ready == true)
            {
                Instantiate(Enemy_Siege, transform.position, transform.rotation);
                Spawn_Wait_Time = Random.Range(Spawn_Next_Time - 3, Spawn_Next_Time + 3);
                Spawn_Ready = false;
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
                }
            }
            Time_Count += Time.deltaTime;
            G_M.Time_Text = G_M.Max_Time - Time_Count;
        }
    }
}
