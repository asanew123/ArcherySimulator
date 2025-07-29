using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow2 : MonoBehaviour
{
    public float damage = 60;   // 기본 데미지
    float Head_M = 3;   // 헤드샷 배율
    float AP_P = 50;    // 방어 관통 확률(최대치);

    public float speed = 70f;

    public float Rotate = 0;
    public float RotationRate = 25;

    public bool Maximum_Rotate = false;

    GameObject P;
    Player Play;

    public bool Player_Arrow = true;

    Rigidbody rb;

    public int Game_Mode = 0; // 1 = 표적 모드  2 = 사냥 모드  3 공성 모드

    public GameObject Hit_Box;

    public float Armor_Piercing_Cheaking = 0;

    public GameObject Target_Mod_Camera;

    bool Shoot_Mod = false;

    public GameObject GM;
    Game_Manager G_M;

    public float Shoot_Next_Time = 2.5f;
    float Shoot_Wait_Time = 0;
    bool Time_Wait = false;

    public float Cheak_Score = 0;
    public float Cheak_Score_Temp = 0;
    bool Score_Cheaking = false;
    bool Score_Decide = false;

    bool Robin_Arrow_Cheak = false;
    float Robin_Arrow_Score;

    float Cheak_Next_Time = 0;
    float Cheak_Wait_Time = 0.2f;

    bool Total_Score_Cheaking = false;

    Rigidbody Wind_Rigid;
    float x_wind = 0;
    float y_wind = 0;

    GameObject SM;
    Score_Manager Sc_M;

    public GameObject Game_UI;

    bool Slow_Cheak = false;

    
    public AudioSource Arrow_Audio;
    public AudioClip Arrow_Target;
    public AudioClip Arrow_Ground;
    public AudioClip Arrow_Fly;

    public AudioClip Scroe_1;
    public AudioClip Scroe_2;
    public AudioClip Scroe_3;
    public AudioClip Scroe_4;
    public AudioClip Scroe_5;
    public AudioClip Scroe_6;
    public AudioClip Scroe_7;
    public AudioClip Scroe_8;
    public AudioClip Scroe_9;
    public AudioClip Scroe_10;
    public AudioClip Scroe_x10;

    bool Fly_Sound_Cheak = false;

    public AudioClip Weapon_Break;
    float Audio_Wait_Time = 0.25f; 
    bool Audio_Cool_Time = false;

    GameObject SetM;
    Setting_Manager Set_M;

    // Start is called before the first frame update
    void Start()
    {
        Slow_Cheak = false;
        P = GameObject.Find("Player");
        Play = P.transform.GetComponent<Player>();

        if(Game_Mode == 1)
        {
            SM = GameObject.Find("Score_Manager");
            Sc_M = SM.transform.GetComponent<Score_Manager>();
        }

        SetM = GameObject.Find("Setting_Manager");
        Set_M = SetM.transform.GetComponent<Setting_Manager>();

        rb = GetComponent<Rigidbody>();
        
        GM = GameObject.Find("Game_Manager");
        G_M = GM.GetComponent<Game_Manager>();

        Game_Mode = G_M.Game_Mode;

        Wind_Rigid = transform.GetComponent<Rigidbody>();

        Rotate = 0;
        Maximum_Rotate = false;

        speed = 7 * (0.1f * Play.Charge_Save);
        if(G_M.Arr2_Hiden_Skill == true)
        {
            RotationRate = 0;
        }
        else if(G_M.Arr2_Hiden_Skill == false)
        {
            RotationRate = 25 - (Play.Charge_Save/5);
        }

        damage = (6*(1+((float)Set_M.Arrow2_Level/10))) * (0.1f * Play.Charge_Save);
        AP_P = (5*(1+((float)Set_M.Arrow2_Level/10))) * (0.1f * Play.Charge_Save);

        Play.damage = damage;
        Play.Head_M = Head_M;

        Play.Armor_Col = false;
        
        if(speed > 0)
        {
            Player_Arrow = false;
        }

        if(Player_Arrow == true)
        {
            damage = 0;
            Hit_Box.SetActive(false);
        }
        if(Player_Arrow == false)
        {
            Hit_Box.SetActive(true);
        }

        if(Game_Mode == 2 && Game_Mode == 3)
        {
            Destroy(gameObject, 3f);
        }

        Armor_Piercing_Cheaking = Random.Range(0, 100);
        if(Armor_Piercing_Cheaking <= AP_P)
        {
            Play.Armor_Piercing = true;
        }
        else
        {
            Play.Armor_Piercing = false;
        }
        Wind_Start();
    }

    // Update is called once per frame
    void Update()
    {
        if(Play.Time_Stop == false)
        {
            Arrow2_Ctrl();
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
        Arrow_Del();
    }

    void Arrow2_Ctrl()
    {
        transform.Translate(Vector3.down * speed * Time.deltaTime);
        if(Game_Mode == 1 || Play.Last_Shooting == true)
        {
            Shoot_Mod_Camera();
        }

        if(G_M.Arr2_Hiden_Skill == false)
        {
            if (Rotate < 180 && Maximum_Rotate == false)
            {
                transform.Rotate(Vector3.right * RotationRate * Time.deltaTime);
                Rotate += RotationRate * Time.deltaTime;
            }
            if(Rotate > 180)
            {
                Rotate = 0;
                Maximum_Rotate = true;
            }
        }
        if(G_M.Arr2_Hiden_Skill == true)
        {
            Maximum_Rotate = true;
        }
        Shoot_Wait_Time2();
        Total_Score_Cheak();
        Wind_Effect();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Armor" && damage > 0)
        {
            speed /= 1.2f;
            RotationRate /= 1.2f;
            if(Player_Arrow == false)
            {
                if(Play.Armor_Piercing == true)
                {
                    Destroy(other.gameObject);
                }
                else if(Play.Armor_Piercing == false)
                {
                    Destroy(other.gameObject);
                    Play.damage = 0;
                    Destroy(this.gameObject);
                }
            }
            Debug.Log("갑옷 파괴");
            Play.Armor_Col = true;
        }
        if(other.tag == "Weapon" && damage > 0)
        {
            if(Player_Arrow == false)
            {
                Destroy(other.gameObject);
                Destroy(gameObject,0.1f);
                if(Audio_Cool_Time == false)
                {
                    Arrow_Audio.PlayOneShot(Weapon_Break);
                    Audio_Cool_Time = true;
                }
            }    
        }
        if(other.tag == "Ground" || other.tag == "Object")
        {
            damage = 0;
            Play.damage = 0;
            speed = 0;
            Arrow_Audio.Pause();
            Arrow_Audio.PlayOneShot(Arrow_Ground);
            Maximum_Rotate = true;
            rb.constraints = RigidbodyConstraints.FreezeAll;
            Play.Armor_Piercing = false;
            if(Player_Arrow == false && Game_Mode != 1)
            {
                Destroy(this.gameObject,5f);
            }
            if(Game_Mode == 1)
            {
                Time_Wait = true;
                Sc_M.Show = true;
                Sc_M.miss = true;
            }
            if(Play.Last_Shooting == true && Game_Mode == 3)
            {
                Time_Wait = true;
            }
            if(Play.Last_Shooting == true && Game_Mode == 2)
            {
                Play.Shoot_Mod = false;
            }
        }
        if(other.tag == "Slow Zone" && Slow_Cheak == false)
        {
            speed /= 3f;
            RotationRate /= 3f;

            x_wind /= 3f;
            y_wind /= 3f;

            Slow_Cheak = true;
        }
        if(other.tag == "Enemy" && Slow_Cheak == false)
        {
            speed /= 3f;
            RotationRate /= 3f;

            x_wind /= 3f;
            y_wind /= 3f;

            Slow_Cheak = true;
        }
        if(other.tag == "Target")
        {
            damage = 0;
            Play.damage = 0;
            speed = 0;
            Arrow_Audio.Pause();
            Arrow_Audio.PlayOneShot(Arrow_Target);
            Maximum_Rotate = true;
            rb.constraints = RigidbodyConstraints.FreezeAll;
            Play.Armor_Piercing = false;
            if(Game_Mode == 1)
            {
                Time_Wait = true;
                Sc_M.Show = true;
            }
        }
        if(other.tag == "Arrow2" && Game_Mode == 1)
        {
            damage = 0;
            Play.damage = 0;
            Maximum_Rotate = true;
            rb.constraints = RigidbodyConstraints.FreezeAll;
            Debug.Log("Robin Arrow!");

            Sc_M.Robin = true;

            if(Game_Mode == 1)
            {
                Time_Wait = true;
            }
        }
        if(other.tag == "Head" || other.tag == "Body")  // 화살이 적에게 박히는거 구현
        {
            if(Player_Arrow == false)
            {
                speed = 0;
                Maximum_Rotate = true;
                rb.constraints = RigidbodyConstraints.FreezeAll;
                transform.parent = other.transform;
                if(Play.Last_Shooting == true)
                {
                    Play.Shoot_Mod = false;
                }
            }
        }
        if(other.tag == "Arm" || other.tag=="Leg")
        {
            if(Player_Arrow == false)
            {
                speed = 0;
                Maximum_Rotate = true;
                rb.constraints = RigidbodyConstraints.FreezeAll;
                transform.parent = other.transform;
                if(Play.Last_Shooting == true)
                {
                    Play.Shoot_Mod = false;
                }
            }
        }

        
        
    }
    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "X10" && Score_Decide == false)
        {
            Score_Cheaking = true;
            Cheak_Score = 11;
            if(Cheak_Score_Temp < Cheak_Score)
            {
                Cheak_Score_Temp = Cheak_Score;
            }
        }
        if(other.tag == "10" && Score_Decide == false)
        {
            Score_Cheaking = true;
            Cheak_Score = 10;
            if(Cheak_Score_Temp < Cheak_Score)
            {
                Cheak_Score_Temp = Cheak_Score;
            }
        }
        if(other.tag == "9" && Score_Decide == false)
        {
            Score_Cheaking = true;
            Cheak_Score = 9;
            if(Cheak_Score_Temp < Cheak_Score)
            {
                Cheak_Score_Temp = Cheak_Score;
            }
        }
        if(other.tag == "8" && Score_Decide == false)
        {
            Score_Cheaking = true;
            Cheak_Score = 8;
            if(Cheak_Score_Temp < Cheak_Score)
            {
                Cheak_Score_Temp = Cheak_Score;
            }
        }
        if(other.tag == "7" && Score_Decide == false)
        {
            Score_Cheaking = true;
            Cheak_Score = 7;
            if(Cheak_Score_Temp < Cheak_Score)
            {
                Cheak_Score_Temp = Cheak_Score;
            }
        }
        if(other.tag == "6" && Score_Decide == false)
        {
            Score_Cheaking = true;
            Cheak_Score = 6;
            if(Cheak_Score_Temp < Cheak_Score)
            {
                Cheak_Score_Temp = Cheak_Score;
            }
        }
        if(other.tag == "5" && Score_Decide == false)
        {
            Score_Cheaking = true;
            Cheak_Score = 5;
            if(Cheak_Score_Temp < Cheak_Score)
            {
                Cheak_Score_Temp = Cheak_Score;
            }
        }
        if(other.tag == "4" && Score_Decide == false)
        {
            Score_Cheaking = true;
            Cheak_Score = 4;
            if(Cheak_Score_Temp < Cheak_Score)
            {
                Cheak_Score_Temp = Cheak_Score;
            }
        }
        if(other.tag == "3" && Score_Decide == false)
        {
            Score_Cheaking = true;
            Cheak_Score = 3;
            if(Cheak_Score_Temp < Cheak_Score)
            {
                Cheak_Score_Temp = Cheak_Score;
            }
        }
        if(other.tag == "2" && Score_Decide == false)
        {
            Score_Cheaking = true;
            Cheak_Score = 2;
            if(Cheak_Score_Temp < Cheak_Score)
            {
                Cheak_Score_Temp = Cheak_Score;
            }
        }
        if(other.tag == "1" && Score_Decide == false)
        {
            Score_Cheaking = true;
            Cheak_Score = 1;
            if(Cheak_Score_Temp < Cheak_Score)
            {
                Cheak_Score_Temp = Cheak_Score;
            }
        }
    }
    

    void Shoot_Mod_Camera()
    {
        if(Player_Arrow == false)
        {
            if(Play.Shoot_Mod == true)
            {
                Target_Mod_Camera.SetActive(true);
                if(Fly_Sound_Cheak == false)
                {  
                    Arrow_Audio.PlayOneShot(Arrow_Fly);
                    Fly_Sound_Cheak = true;
                }
            }
            if(Play.Shoot_Mod == false)
            {
                Target_Mod_Camera.SetActive(false);
                Fly_Sound_Cheak = false;
                Arrow_Audio.Pause();
            }
        }
    }

    void Shoot_Wait_Time2()
    {
        if(Time_Wait == true)
        {
            if(Shoot_Wait_Time < Shoot_Next_Time)
            {
                Shoot_Wait_Time += Time.deltaTime;
            }
            if(Shoot_Wait_Time > Shoot_Next_Time)
            {
                Shoot_Wait_Time = 0;
                Time_Wait = false;
                Play.Shoot_Mod = false;
                if(G_M.Arrow_Empty == true)
                {
                    G_M.Set_Count += 1;
                    Play.Arrow = 6;
                    G_M.Set_End = true;
                    G_M.Arrow_Empty = false;
                    if(G_M.Set == G_M.Set_Count)
                    {
                        G_M.Game_Set_Effect = true;
                    }
                    if(G_M.Set > G_M.Set_Count)
                    {
                        G_M.Set_End_Effect = true;
                    }
                }
            }
        }
    }

    void Total_Score_Cheak()
    {
        if(Score_Cheaking == true && Robin_Arrow_Cheak == false)
        {
            if(Cheak_Next_Time < Cheak_Wait_Time)
            {
                Cheak_Next_Time += Time.deltaTime;
            }
            if(Cheak_Next_Time > Cheak_Wait_Time)
            {
                if(Cheak_Score_Temp == 11)
                {
                    G_M.Total_Score += (10);
                    G_M.Now_Score = Cheak_Score_Temp;
                    Robin_Arrow_Score = Cheak_Score_Temp;

                    Score_Sound();

                    Sc_M.Score = Cheak_Score_Temp;
                    Sc_M.Show = true;

                    Cheak_Score_Temp = 0;
                    Score_Cheaking = false;
                    
                    Cheak_Next_Time = 0;
                    Score_Decide = true;
                    Cheak_Score = 0;
                }
                else if(Cheak_Score_Temp != 11)
                {
                    G_M.Total_Score += (Cheak_Score_Temp);
                    G_M.Now_Score = Cheak_Score_Temp;
                    Robin_Arrow_Score = Cheak_Score_Temp;

                    Score_Sound();

                    Sc_M.Score = Cheak_Score_Temp;
                    Sc_M.Show = true;
                    if(Cheak_Score_Temp == 0)
                    {
                        Sc_M.miss = true;
                    }
                    
                    Cheak_Score_Temp = 0;
                    Score_Cheaking = false;
                        
                    Cheak_Next_Time = 0;
                    Score_Decide = true;
                    Cheak_Score = 0;
                }
                Total_Score_Cheaking = false;
            }
        }
    }

    void Wind_Effect()
    {
        if(speed > 0)
        {
            if(x_wind > 0)  // 오른쪽 바람
            {
                transform.Translate(Vector3.right * Time.deltaTime * x_wind);
            }
            if(x_wind < 0)  // 왼쪽 바람
            {
                transform.Translate(Vector3.right * Time.deltaTime * x_wind);
            }
            if(y_wind > 0)  // 위쪽 바람
            {
                transform.Translate(Vector3.forward * Time.deltaTime * y_wind);
            }
            if(y_wind < 0)  // 아래쪽 바람
            {
                transform.Translate(Vector3.forward * Time.deltaTime * y_wind);
            }
        }
    }

    void Wind_Start()
    {
        x_wind = (G_M.x_wind/16);
        y_wind = (G_M.y_wind/16);
    }

    void Arrow_Del()
    {
        if(Player_Arrow == false && G_M.Arrow_Delete == true)
        {
            Destroy(this.gameObject);
        }
    }

    void Score_Sound()
    {
        if(Cheak_Score_Temp == 11)
        {
            Arrow_Audio.PlayOneShot(Scroe_10);
            Arrow_Audio.PlayOneShot(Scroe_x10);
        }
        if(Cheak_Score_Temp == 10)
        {
            Arrow_Audio.PlayOneShot(Scroe_10);
        }
        if(Cheak_Score_Temp == 9)
        {
            Arrow_Audio.PlayOneShot(Scroe_9);
        }
        if(Cheak_Score_Temp == 8)
        {
            Arrow_Audio.PlayOneShot(Scroe_8);
        }
        if(Cheak_Score_Temp == 7)
        {
            Arrow_Audio.PlayOneShot(Scroe_7);
        }
        if(Cheak_Score_Temp == 6)
        {
            Arrow_Audio.PlayOneShot(Scroe_6);
        }
        if(Cheak_Score_Temp == 5)
        {
            Arrow_Audio.PlayOneShot(Scroe_5);
        }
        if(Cheak_Score_Temp == 4)
        {
            Arrow_Audio.PlayOneShot(Scroe_4);
        }
        if(Cheak_Score_Temp == 3)
        {
            Arrow_Audio.PlayOneShot(Scroe_3);
        }
        if(Cheak_Score_Temp == 2)
        {
            Arrow_Audio.PlayOneShot(Scroe_2);
        }
        if(Cheak_Score_Temp == 1)
        {
            Arrow_Audio.PlayOneShot(Scroe_1);
        }
    }
}
