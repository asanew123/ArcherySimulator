using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public Animator anim;
    public float Charge = 0;
    public int Charge_Speed = 100;
    public float Charge_Save = 0;
    public bool Charging = false;
    float Charge_CoolTime = 0.5f;
    public bool Shoot_Ready = true;
    public float Reload_Time = 0.5f;
    public bool Reload = false;
    float Wait_Time = 0;
    float Wait_Time2 = 0;
    float Wait_Time3 = 0;

    public bool Zoom = false;
    float zoom = 40;
    float normal = 60;
    float smooth = 5;

    int Game_Level = 0;

    public float damage = 0;
    public float Head_M = 0;
    public float HeadShot_damage = 0;

    public float HP = 100;

    public int Using_Arrow = 1;  // 사용하는 화살 종류 1 = 기본,  2 = 관통,   3 = 사냥
    public int Arrow = 24;  // 가지고 있는 화살수 1.표적모드에서는 갯수제한되고 그외 사냥모드와 공성전 모드에서는 화살 제한없음
    public int Ready_Arrow = 1; // 장전된 화살수
    public int Game_Mode = 1; // 1 = 표적모드   2 = 사냥모드    3 = 공성모드
    public GameObject Arrow1;
    public GameObject Arrow2;
    public GameObject Arrow3;
    public GameObject camera;
    public GameObject Arrow1_SP;
    public GameObject Arrow2_SP;
    public GameObject Arrow3_SP;


    public Image HP_Bar;
    public Image Charging_Bar;

    public Text HP_Text;
    public Text Charging_Text;
    public Text Ammo_Text;

    public bool Armor_Col = false;

    public GameObject GM;
    Game_Manager G_M;

    public bool Armor_Piercing = false;

    public bool Shoot_Mod = false;

    GameObject T1;
    GameObject T2;
    GameObject T3;

    public GameObject Player_UI;

    public GameObject Q_Key;

    public bool Last_Shooting = false;

    public bool Time_Stop = false;
    public bool Option_Use = false;

    public GameObject SetM;
    Setting_Manager Set_M;

    public AudioSource Player_Audio;
    public AudioClip Charge_Audio;
    public AudioClip Shoot_Audio;

    bool Charge_Audio_Cheak = false;

    public GameObject Target_Camera1;
    public GameObject Target_Camera2;
    public GameObject Target_Camera3;

    void Awake()
    {
        
        SetM = GameObject.Find("Setting_Manager");
        Set_M = SetM.transform.GetComponent<Setting_Manager>();
        
    }
    
    // Start is called before the first frame update
    void Start()
    {
        GM = GameObject.Find("Game_Manager");
        G_M = GM.transform.GetComponent<Game_Manager>();
        Game_Mode = G_M.Game_Mode;
        Game_Level = G_M.Game_Level;

        
        Game_Mode = Set_M.Game_Mode;
        Game_Level = Set_M.Game_Level;
        Using_Arrow = Set_M.Using_Arrow;
        

        Last_Shooting = false;

        if(Using_Arrow == 1)    // 장착한 화살이 기본화살 일때
        {
            All_Arrow_Object_Off();
            Arrow1.SetActive(true);
        }
        if(Using_Arrow == 2)    // 장착한 화살이 관통화살 일때
        {
            All_Arrow_Object_Off();
            Arrow2.SetActive(true);
        }
        if(Using_Arrow == 3)    // 장착한 화살이 사냥화살 일때
        {
            All_Arrow_Object_Off();
            Arrow3.SetActive(true);
        }
        if(Game_Mode == 1)  // 표적모드 일때
        {
            T1 = GameObject.Find("양궁 타겟(완성)(쉬움)(30m)");
            T2 = GameObject.Find("양궁 타겟(완성)(보통)(50m)");
            T3 = GameObject.Find("양궁 타겟(완성)(어려움)(100m)");
            if(Game_Level == 1)
            {
                T2.SetActive(false);
                T3.SetActive(false);
            }
            if(Game_Level == 2)
            {
                T1.SetActive(false);
                T3.SetActive(false);
            }
            if(Game_Level == 3)
            {
                T1.SetActive(false);
                T2.SetActive(false);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Option_Use == false && G_M.Set_End_Effect == false && G_M.Game_Set_Effect == false && G_M.Game_Set == false)
        {
            Player_Ctrl();
        }
        if(G_M.Game_Set_Effect == false && G_M.Set_End_Effect == false)  // 옵션 관련 설정
        {
            if(Option_Use == false && Time_Stop == false)
            {
                if(Input.GetKeyDown(KeyCode.Escape))
                {
                    Time_Stop = true;
                    Option_Use = true;
                }
            }
            else if(Option_Use == true && Time_Stop == true)
            {
                if(Input.GetKeyDown(KeyCode.Escape))
                {
                    Time_Stop = false;
                    Option_Use = false;
                }
            }
        }
        Game_End_Camera();
        Target_Camera_Mod();
        Die();
    }

    void All_Arrow_Object_Off()
    {
        Arrow1.SetActive(false);
        Arrow2.SetActive(false);
        Arrow3.SetActive(false);
    }
    
    void HP_and_Charge_UI()
    {
        HP_Bar.fillAmount = HP * 0.01f;
        Charging_Bar.fillAmount = Charge * 0.01f;

        HP_Text.text = Mathf.Round(HP).ToString() + " / 100";
        if(Charge <= 100)
        {
            Charging_Text.text = Mathf.Round(Charge).ToString() + "%";
        }
        else if(Charge > 100)
        {
            Charging_Text.text = "100%";
        }
        if(Game_Mode == 2 || Game_Mode == 3)    // 사냥 또는 공성모드
        {
            Ammo_Text.text = Ready_Arrow.ToString() + " / ∞";
        }
        else if(Game_Mode == 1) // 타겟 모드
        {
            Ammo_Text.text = (Arrow+Ready_Arrow).ToString() + "발";
            if((Arrow+Ready_Arrow) == 0)
            {
                G_M.Arrow_Empty = true;
            }
        }
    }

    void Player_Ctrl()
    {
        HP_and_Charge_UI();
        if(Shoot_Mod == false)
        {
            if(Charge <= 100 && Charging == false)
            {
                if(Input.GetKey(KeyCode.Mouse0) && Shoot_Ready == true)
                {
                    if(Set_M.Using_Arrow == 1 && G_M.Arr1_Hiden_Skill == true)
                    {
                        Charge = 100;
                    }
                    else
                    {
                        Charge += Time.deltaTime * Charge_Speed;
                    }

                    if(Charge_Audio_Cheak == false)
                    {
                        Player_Audio.PlayOneShot(Charge_Audio);
                        Charge_Audio_Cheak = true;
                    }
                    if(Game_Mode == 2)
                    {
                        G_M.Arrow_Delete = true;
                    }
                }
            }
            if(Input.GetKeyUp(KeyCode.Mouse0) && Shoot_Ready == true)
            {
                if(Game_Mode == 2)
                {
                    G_M.Arrow_Delete = false;
                }
                if(Charge > 100)
                {
                    Charge = 100;
                }
                if(Charge < 100)
                {
                    Charge = Mathf.Round(Charge);
                }
                Charge_Save = Charge;
                Charge = 0;
                Charging = true;
                Reload = true;
                Wait_Time = Charge_CoolTime;
                Wait_Time3 = Reload_Time;
                anim.SetBool("Shoot", true);
                if(Ready_Arrow == 1) // 장전된 화살이 있을때 발사
                {
                    HeadShot_damage = damage * Head_M;
                    Charge_Audio_Cheak = false;
                    Player_Audio.Stop();
                    Player_Audio.PlayOneShot(Shoot_Audio);
                    if(Using_Arrow == 1)
                    {
                        Instantiate(Arrow1, Arrow1_SP.transform.position, Arrow1_SP.transform.rotation);
                    }
                    else if(Using_Arrow == 2)
                    {
                        Instantiate(Arrow2, Arrow2_SP.transform.position, Arrow2_SP.transform.rotation);
                    }
                    else if(Using_Arrow == 3)
                    {
                        Instantiate(Arrow3, Arrow3_SP.transform.position, Arrow3_SP.transform.rotation);
                    }   
                    Ready_Arrow = 0;
                    if(Game_Mode == 1 || Last_Shooting == true)
                    {
                        Shoot_Mod = true;
                    }
                }
            }
        }
        if(Input.GetKeyDown(KeyCode.Q)) // 발사 취소
        {
            Charge = 0;
            Charge_Save = 0;
            Wait_Time = Charge_CoolTime;
            Shoot_Ready = false;
            Wait_Time2 = Charge_CoolTime; 

        }
        if(Input.GetKey(KeyCode.Mouse1))
        {
            Zoom = true;
        }
        if(Input.GetKeyUp(KeyCode.Mouse1))
        {
            Zoom = false;
        }
        if(Wait_Time > 0)
        {
            Charging = true;
            Wait_Time -= Time.deltaTime;
        }
        if(Wait_Time < 0)
        {
            Charging = false;
            Wait_Time = 0;
            anim.SetBool("Shoot", false);
        }
        if(Charge > 0)
        {
            anim.SetBool("Charging", true);
            Q_Key.SetActive(true);
        }
        if(Charge == 0)
        {
            anim.SetBool("Charging", false);
            Q_Key.SetActive(false);
        }
        if(Shoot_Ready == false)
        {     
            if(Wait_Time2 > 0)
            {
                Wait_Time2 -= Time.deltaTime;
            }
            if(Wait_Time2<0)
            {
                Shoot_Ready = true;
                Wait_Time2 = 0;
            }
        }
        if(Reload == true && Arrow >= 1) // 자동 재장전
        {
            if(Wait_Time3 > 0)
            {
                Wait_Time3 -= Time.deltaTime;
            }
            if(Wait_Time3 > Reload_Time/2)
            {
                All_Arrow_Object_Off();
            }
            if(Wait_Time3<0)
            {
                if(Game_Mode == 1)
                {
                    Arrow -= 1;
                }
                Ready_Arrow = 1;
                Reload = false;
                Wait_Time3 = 0;
                anim.SetBool("Reload", false);
            }
            
        }
        if(Ready_Arrow == 0 && Reload == true) // 장전된 화살이 없을때
        {
            All_Arrow_Object_Off();
        }
        if(Ready_Arrow == 1)
        {
            if(Using_Arrow == 1)
            {
                Arrow1.SetActive(true);        
            }
            if(Using_Arrow == 2)
            {
                Arrow2.SetActive(true);
            }
            if(Using_Arrow == 3)
            {
                Arrow3.SetActive(true);
            }
        }
        if(Zoom == true)
        {
            camera.GetComponent<Camera>().fieldOfView = Mathf.Lerp(camera.GetComponent<Camera>().fieldOfView, zoom, Time.deltaTime * smooth);
        }
        if(Zoom == false)
        {
            camera.GetComponent<Camera>().fieldOfView = Mathf.Lerp(camera.GetComponent<Camera>().fieldOfView, normal, Time.deltaTime * smooth);
        }
        if(Shoot_Mod == true)
        {
            camera.SetActive(false);
            Player_UI.SetActive(false);
            if(G_M.Arr3_Hiden_Skill == true && Set_M.Using_Arrow == 3)
            {
                Time_Stop = true;
            }
        }
        if(Shoot_Mod == false && G_M.Game_Set == false)
        {
            camera.SetActive(true);
            Player_UI.SetActive(true);
            if(G_M.Arr3_Hiden_Skill == true && Set_M.Using_Arrow == 3 && G_M.Game_Mode != 3)
            {
                Time_Stop = false;
            }
        }
        if(G_M.Game_Set == true)
        {
            Player_UI.SetActive(false);
        }
    }

    void Die()
    {
        if(HP <= 0)
        {
            G_M.Game_Over = true;
        }
        if(HP < 0)
        {
            HP = 0;
        }
    }

    void Target_Camera_Mod()
    {
        if(Game_Mode == 1)
        {
            if(Shoot_Mod == false && Time_Stop == false)
            {
                if(Game_Level == 1)
                {
                    if(Input.GetKey(KeyCode.Space))
                    {
                        Target_Camera1.SetActive(true);
                        camera.SetActive(false);
                        Player_UI.SetActive(false);
                    }
                    if(Input.GetKeyUp(KeyCode.Space))
                    {
                        Target_Camera1.SetActive(false);
                        camera.SetActive(true);
                        Player_UI.SetActive(true);
                    }
                }
                if(Game_Level == 2)
                {
                    if(Input.GetKey(KeyCode.Space))
                    {
                        Target_Camera2.SetActive(true);
                        camera.SetActive(false);
                        Player_UI.SetActive(false);
                    }
                    if(Input.GetKeyUp(KeyCode.Space))
                    {
                        Target_Camera2.SetActive(false);
                        camera.SetActive(true);
                        Player_UI.SetActive(true);
                    }
                }
                if(Game_Level == 3)
                {
                    if(Input.GetKey(KeyCode.Space))
                    {
                        Target_Camera3.SetActive(true);
                        camera.SetActive(false);
                        Player_UI.SetActive(false);
                    }
                    if(Input.GetKeyUp(KeyCode.Space))
                    {
                        Target_Camera3.SetActive(false);
                        camera.SetActive(true);
                        Player_UI.SetActive(true);
                    }
                }
            }
            if(G_M.Game_Set_Effect == true || G_M.Game_Set == true)
            {
                Player_UI.SetActive(false);
            }
        }
    }

    void Game_End_Camera()
    {
        if(G_M.Set_End_Effect == true)
        {
            camera.SetActive(true);
        }
        if(G_M.Game_Set_Effect == true)
        {
            camera.SetActive(true);
        }
    }
}
