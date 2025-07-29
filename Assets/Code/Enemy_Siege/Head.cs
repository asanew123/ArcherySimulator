using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Head : MonoBehaviour
{
    public GameObject E;

    GameObject P;

    Player Play;

    Enemy_Siege Ene;

    bool col = false;

    float wait_time = 0;
    float next_time = 0.1f;

    bool Damage_Cheaking = false;

    float wait_time2 = 0;
    float next_time2 = 0.1f;

    GameObject shootM;

    Shot_Manager Shot_M;

    GameObject G;
    Game_Manager G_M;

    float Audio_Wait_Time = 0.25f; 
    bool Audio_Cool_Time = false;
    public AudioSource Siege_Head;
    public AudioClip Siege_Head_Hit;
    public AudioClip Siege_Helm_Hit;
    public AudioClip Siege_ArmorPiecing_Hit;

    // Start is called before the first frame update
    void Start()
    {
        P = GameObject.Find("Player");
        Play = P.transform.GetComponent<Player>();
        Ene = E.transform.GetComponent<Enemy_Siege>();

        Shot_M = P.transform.GetComponent<Shot_Manager>();

        G = GameObject.Find("Game_Manager");
        G_M = G.transform.GetComponent<Game_Manager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(col == true)
        {
            if(wait_time > 0)
            {
                wait_time -= Time.deltaTime;
            }
            if(wait_time < 0)
            {
                wait_time = 0;
                col = false;
                Ene.helm_B = false;
                Play.Armor_Col = false;
            }
        }
        if(Damage_Cheaking == true)
        {
            if(wait_time2 > 0)
            {
                wait_time2 -= Time.deltaTime;
            }
            if(wait_time2 < 0)
            {
                wait_time2 = 0;
                Damage_Cheaking = false;
            }
        }
        if(Audio_Cool_Time == true)
        {
            Audio_Wait_Time -= Time.deltaTime;
        }
        if(Audio_Wait_Time < 0)
        {
            Audio_Wait_Time = 0.25f;
            Audio_Cool_Time = false;
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Arrow" && Play.damage > 0)
        {
            if(Ene.helm_B == true)  //  투구 착용
            {
                if(Play.Armor_Col == true && Play.Armor_Piercing == true) // 투구에 닿았는가?      // 방어관통
                {   
                    if(Damage_Cheaking == false)
                    {
                        Ene.HP -= (Play.damage * Play.Head_M);
                        Play.damage = 0;
                        Damage_Cheaking = true;
                        wait_time2 = next_time2;
                    }
                    Debug.Log("헤드샷!(방어관통)");
                    if((Ene.HP -= (Play.damage * Play.Head_M)) <= 0)
                    {
                        G_M.HeadShot_Kill = true;
                    }
                    if((Ene.HP -= (Play.damage * Play.Head_M)) > 0)
                    {
                        G_M.HeadShot_Kill = false;
                    }
                    Shot_M.All_False();
                    Shot_M.Shot_Text_Wait_Time = Shot_M.Wait_Time_Temp;
                    Shot_M.HS_AP = 1;
                    Shot_M.Col = true;
                    Play.damage = 0;
                    Play.Armor_Piercing = false;
                    if(Audio_Cool_Time == false)
                    {
                        Siege_Head.PlayOneShot(Siege_ArmorPiecing_Hit);
                        Audio_Cool_Time = true;
                    }
                }
                if(Play.Armor_Col == true && Play.Armor_Piercing == false)   // 방어관통 판정 실패
                {
                    Debug.Log("투구 효과 : 화살 방어됨");
                    Play.damage = 0;
                    Destroy(other.gameObject);
                    col = true;
                    wait_time = next_time;
                    if(Audio_Cool_Time == false)
                    {
                        Siege_Head.PlayOneShot(Siege_Helm_Hit);
                        Audio_Cool_Time = true;
                    }
                }
                if(Play.Armor_Col == false)    // 투구 미보호 부위 적중
                {
                    if(Damage_Cheaking == false)
                    {
                        Ene.HP -= (Play.damage * Play.Head_M);
                        Play.damage = 0;
                        Damage_Cheaking = true;
                        wait_time2 = next_time2;
                    }
                    Debug.Log("헤드샷!(보호 안되는 부분 피격)");
                    if((Ene.HP -= (Play.damage * Play.Head_M)) <= 0)
                    {
                        G_M.HeadShot_Kill = true;
                    }
                    if((Ene.HP -= (Play.damage * Play.Head_M)) > 0)
                    {
                        G_M.HeadShot_Kill = false;
                    }
                    Shot_M.All_False();
                    Shot_M.Shot_Text_Wait_Time = Shot_M.Wait_Time_Temp;
                    Shot_M.HS_HGS = 1;
                    Shot_M.Col = true;
                    Play.damage = 0;
                    Play.Armor_Piercing = false;
                    if(Audio_Cool_Time == false)
                    {
                        Siege_Head.PlayOneShot(Siege_Head_Hit);
                        Audio_Cool_Time = true;
                    }
                }
            }
            if(Ene.helm_B == false) // 투구 미착용
            {
                Debug.Log("헤드샷!");
                if(Damage_Cheaking == false)
                {
                    Ene.HP -= (Play.damage * Play.Head_M);
                    Play.damage = 0;
                    Damage_Cheaking = true;
                    wait_time2 = next_time2;
                }
                if((Ene.HP -= (Play.damage * Play.Head_M)) <= 0)
                {
                    G_M.HeadShot_Kill = true;
                }
                if((Ene.HP -= (Play.damage * Play.Head_M)) > 0)
                {
                    G_M.HeadShot_Kill = false;
                }
                if(Play.Game_Mode == 3)
                {
                    Shot_M.All_False();
                    Shot_M.Shot_Text_Wait_Time = Shot_M.Wait_Time_Temp;
                    if(Shot_M.HS_AP != 1)
                    {
                        Shot_M.HS = 1;
                    }
                    Shot_M.Col = true;
                }
                Play.damage = 0;
                Play.Armor_Piercing = false;
                if(Audio_Cool_Time == false)
                {
                    Siege_Head.PlayOneShot(Siege_Head_Hit);
                    Audio_Cool_Time = true;
                }
            }
            
        }
        if(other.tag == "Arrow2" && Play.damage > 0)
        {
            if(Ene.helm_B == true)  //  투구 착용
            {
                if(Play.Armor_Col == true && Play.Armor_Piercing == true) // 투구에 닿았는가?      // 방어관통
                {   
                    if(Damage_Cheaking == false)
                    {
                        Ene.HP -= (Play.damage * Play.Head_M);
                        Play.damage = 0;
                        Damage_Cheaking = true;
                        wait_time2 = next_time2;
                    }
                    Debug.Log("헤드샷!(방어관통)");
                    if((Ene.HP -= (Play.damage * Play.Head_M)) <= 0)
                    {
                        G_M.HeadShot_Kill = true;
                    }
                    if((Ene.HP -= (Play.damage * Play.Head_M)) > 0)
                    {
                        G_M.HeadShot_Kill = false;
                    }
                    Shot_M.All_False();
                    Shot_M.Shot_Text_Wait_Time = Shot_M.Wait_Time_Temp;
                    Shot_M.HS_AP = 1;
                    Shot_M.Col = true;
                    Play.damage = 0;
                    Play.Armor_Piercing = false;
                    if(Audio_Cool_Time == false)
                    {
                        Siege_Head.PlayOneShot(Siege_ArmorPiecing_Hit);
                        Audio_Cool_Time = true;
                    }
                }
                if(Play.Armor_Col == true && Play.Armor_Piercing == false)   // 방어관통 판정 실패
                {
                    Debug.Log("투구 효과 : 화살 방어됨");
                    Play.damage = 0;
                    Destroy(other.gameObject);
                    col = true;
                    wait_time = next_time;
                    if(Audio_Cool_Time == false)
                    {
                        Siege_Head.PlayOneShot(Siege_Helm_Hit);
                        Audio_Cool_Time = true;
                    }
                }
                if(Play.Armor_Col == false)    // 투구 미보호 부위 적중
                {
                    if(Damage_Cheaking == false)
                    {
                        Ene.HP -= (Play.damage * Play.Head_M);
                        Play.damage = 0;
                        Damage_Cheaking = true;
                        wait_time2 = next_time2;
                    }
                    Debug.Log("헤드샷!(보호 안되는 부분 피격)");
                    if((Ene.HP -= (Play.damage * Play.Head_M)) <= 0)
                    {
                        G_M.HeadShot_Kill = true;
                    }
                    if((Ene.HP -= (Play.damage * Play.Head_M)) > 0)
                    {
                        G_M.HeadShot_Kill = false;
                    }
                    Shot_M.All_False();
                    Shot_M.Shot_Text_Wait_Time = Shot_M.Wait_Time_Temp;
                    Shot_M.HS_HGS = 1;
                    Shot_M.Col = true;
                    Play.damage = 0;
                    Play.Armor_Piercing = false;
                    if(Audio_Cool_Time == false)
                    {
                        Siege_Head.PlayOneShot(Siege_Head_Hit);
                        Audio_Cool_Time = true;
                    }
                }
            }
            if(Ene.helm_B == false) // 투구 미착용
            {
                Debug.Log("헤드샷!");
                if(Damage_Cheaking == false)
                {
                    Ene.HP -= (Play.damage * Play.Head_M);
                    Play.damage = 0;
                    Damage_Cheaking = true;
                    wait_time2 = next_time2;
                }
                if((Ene.HP -= (Play.damage * Play.Head_M)) <= 0)
                {
                    G_M.HeadShot_Kill = true;
                }
                if((Ene.HP -= (Play.damage * Play.Head_M)) > 0)
                {
                    G_M.HeadShot_Kill = false;
                }
                if(Play.Game_Mode == 3)
                {
                    Shot_M.All_False();
                    Shot_M.Shot_Text_Wait_Time = Shot_M.Wait_Time_Temp;
                    if(Shot_M.HS_AP != 1)
                    {
                        Shot_M.HS = 1;
                    }
                    Shot_M.Col = true;
                }
                Play.damage = 0;
                Play.Armor_Piercing = false;
                if(Audio_Cool_Time == false)
                {
                    Siege_Head.PlayOneShot(Siege_Head_Hit);
                    Audio_Cool_Time = true;
                }
            }
            
        }
        if(other.tag == "Arrow3" && Play.damage > 0)
        {
            if(Ene.helm_B == true)  //  투구 착용
            {
                if(Play.Armor_Col == true && Play.Armor_Piercing == true) // 투구에 닿았는가?      // 방어관통
                {   
                    if(Damage_Cheaking == false)
                    {
                        Ene.HP -= ((Play.damage * Play.Head_M)/2);
                        Play.damage = 0;
                        Damage_Cheaking = true;
                        wait_time2 = next_time2;
                    }
                    Debug.Log("헤드샷!(방어관통 50%)");
                    if((Ene.HP -= ((Play.damage * Play.Head_M)/2)) <= 0)
                    {
                        G_M.HeadShot_Kill = true;
                    }
                    if((Ene.HP -= (Play.damage * Play.Head_M)) > 0)
                    {
                        G_M.HeadShot_Kill = false;
                    }
                    Shot_M.All_False();
                    Shot_M.Shot_Text_Wait_Time = Shot_M.Wait_Time_Temp;
                    Shot_M.HS_AP_Hf = 1;
                    Shot_M.Col = true;
                    Play.damage = 0;
                    Play.Armor_Piercing = false;
                    if(Audio_Cool_Time == false)
                    {
                        Siege_Head.PlayOneShot(Siege_ArmorPiecing_Hit);
                        Audio_Cool_Time = true;
                    }
                }
                if(Play.Armor_Col == true && Play.Armor_Piercing == false)   // 방어관통 판정 실패
                {
                    Debug.Log("투구 효과 : 화살 방어됨");
                    Play.damage = 0;
                    Destroy(other.gameObject);
                    col = true;
                    wait_time = next_time;
                    if(Audio_Cool_Time == false)
                    {
                        Siege_Head.PlayOneShot(Siege_Helm_Hit);
                        Audio_Cool_Time = true;
                    }
                }
                if(Play.Armor_Col == false)    // 투구 미보호 부위 적중
                {
                    if(Damage_Cheaking == false)
                    {
                        Ene.HP -= (Play.damage * Play.Head_M);
                        Play.damage = 0;
                        Damage_Cheaking = true;
                        wait_time2 = next_time2;
                    }
                    Debug.Log("헤드샷!(보호 안되는 부분 피격)");
                    if((Ene.HP -= (Play.damage * Play.Head_M)) <= 0)
                    {
                        G_M.HeadShot_Kill = true;
                    }
                    if((Ene.HP -= (Play.damage * Play.Head_M)) > 0)
                    {
                        G_M.HeadShot_Kill = false;
                    }
                    Shot_M.All_False();
                    Shot_M.Shot_Text_Wait_Time = Shot_M.Wait_Time_Temp;
                    Shot_M.HS_HGS = 1;
                    Shot_M.Col = true;
                    Play.damage = 0;
                    Play.Armor_Piercing = false;
                    if(Audio_Cool_Time == false)
                    {
                        Siege_Head.PlayOneShot(Siege_Head_Hit);
                        Audio_Cool_Time = true;
                    }
                }
            }
            if(Ene.helm_B == false) // 투구 미착용
            {
                Debug.Log("헤드샷!");
                if(Damage_Cheaking == false)
                {
                    Ene.HP -= (Play.damage * Play.Head_M);
                    Play.damage = 0;
                    Damage_Cheaking = true;
                    wait_time2 = next_time2;
                }
                if((Ene.HP -= (Play.damage * Play.Head_M)) <= 0)
                {
                    G_M.HeadShot_Kill = true;
                }
                if((Ene.HP -= (Play.damage * Play.Head_M)) > 0)
                {
                    G_M.HeadShot_Kill = false;
                }
                if(Play.Game_Mode == 3)
                {
                    Shot_M.All_False();
                    Shot_M.Shot_Text_Wait_Time = Shot_M.Wait_Time_Temp;
                    if(Shot_M.HS_AP_Hf != 1)
                    {
                        Shot_M.HS = 1;
                    }
                    Shot_M.Col = true;
                }
                Play.damage = 0;
                Play.Armor_Piercing = false;
                if(Audio_Cool_Time == false)
                {
                    Siege_Head.PlayOneShot(Siege_Head_Hit);
                    Audio_Cool_Time = true;
                }
            }
            
        }
    }
}
