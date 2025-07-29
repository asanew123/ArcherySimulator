using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Game_Manager : MonoBehaviour
{
    public int Game_Level = 0;
    public int Game_Mode = 0;

    public float Total_Score = 0;
    public float Now_Score = 0;

    public GameObject time_bar;
    public Image time_bar_image;

    public Text time_bar_text1;
    public Text time_bar_text2;
    public Text time_bar_text3;

    public GameObject enemy_bar;
    public Image enemy_bar_image;

    public Text enemy_bar_text1;
    public Text enemy_bar_text2;
    public Text enemy_bar_text3;

    public GameObject Kill_Bar;
    public Text Kill_Bar_Text;

    public bool Game_Over = false;
    public bool Game_Clear = false;

    public int All_Money = 0;  // 메인 메뉴 돈
    public int Money = 0;  // 게임중 모은돈
    public int Add_Money = 0;  // 게임중 얻은돈

    public Text Money_Text; // 표기 되는돈
    public Text Add_Money_Text;

    public bool HeadShot_Kill = false;  //  헤드샷 킬 여부
    public bool OneHitShot_Kill = false; // 사냥모드 - 한방에 잡았는가?

    public int Time_or_Limited = 0; // 1 = 시간제  2 = 한정된 적의 수

    public int R_and_F = 0; // 1 = 현실     2 = 판타지

    public float Max_Time = 30;     // 쉬움 30초    보통 60초   어려움 = 90초
    public float Time_Text = 0;         // 시간 텍스트

    public float Max_Enemy = 30;    // 쉬움 = 30마리    보통 = 65마리   어려움  = 100마리
    public float Enemy_Text;        // 남은 적 텍스트

    public float All_Enemy_Count = 0;   // 소환된 모든 적

    public int Kill_Count = 0;

    public float x_wind = 0;
    public float y_wind = 0;

    public GameObject wind_Game;

    public Text x_wind_text;
    public Text y_wind_text;

    public GameObject Left_Wind;
    public GameObject Right_Wind;
    public GameObject Up_Wind;
    public GameObject Down_Wind;

    public GameObject Left_and_Right_Wind;
    public GameObject Up_and_Down_Wind;

    public GameObject No_Wind;
    float Wait_Time = 0;

    public GameObject Money_Object;
    public GameObject Difficulty_Easy_HeadShot;

    public GameObject Difficulty_Normal;
    public GameObject Difficulty_Hard;

    public GameObject Difficulty_HeadShot;

    public bool Kill_Cheak = false;      // 적 사망시 체크

    public float Money_Wait_Time = 2.5f;
    public float Wait_Time_Temp = 0;

    public GameObject All_Money_Text;

    public GameObject P;
    Player Play;

    public int Set = 0;
    public int Set_Count = 0;
    public bool Arrow_Empty = false;

    public bool Arrow_Delete = false;  // 화살 삭제  // 세트마다 화살 제거함

    public bool Set_End = false;
    public bool Game_Set = false;

    public bool Set_End_Effect = false;
    public bool Game_Set_Effect = false;

    public float Effect_Wait_Time = 5f;
    float Effect_Wait_Time2 = 3f;

    public GameObject Set_End_Object;
    public GameObject Set_Start_Object;
    public GameObject Canvas_SetEnd;

    public GameObject Canvas_GameSet;
    public GameObject GameSet_Objcet;
    public GameObject GameEnd_Object;

    public GameObject Player_Canvas;

    public Text Set_End_Text;
    public Text Set_Start_Text;

    GameObject SetM;
    Setting_Manager Set_M;

    GameObject Sc;
    Score_Manager Sc_M;

    float Siege_Ending_Time = 2.5f;
    public int Set_Score_Count_Text = 1;

    public GameObject Hunting_All_Money_Text;

    public GameObject Hunting_Add_Money;
    public Text Hunting_Add_Money_Text;

    public GameObject Hunting_Animal_Kill;
    public Text Hunting_Animal_Kill_Text;

    public GameObject Hunting_HeadShot;
    public GameObject Hunting_OneHitKill;
    public GameObject Hunting_OneHitKill_Solo;

    public bool Rabbit_Kill = false;
    public bool Fox_Kill = false;
    public bool Wolf_Kill = false;
    public bool Eagle_Kill = false;
    public bool Bear_Kill = false;
    
    public bool DB_Kill = false;
    public bool Eagle_F_Kill = false;
    public bool DSE_Kill = false;
    public bool Minota_Kill = false;

    public int Animal_Kill_Money = 0;

    public AudioSource End;
    public AudioClip End_W;
    bool End_W_C1 = false;
    bool End_W_C2 = false;

    public bool Arr1_Hiden_Skill = false;
    public bool Arr2_Hiden_Skill = false;
    public bool Arr3_Hiden_Skill = false;

    public GameObject Time_Stop_Effect;

    void Awake()
    {
        P = GameObject.Find("Player");
        Play = P.transform.GetComponent<Player>();
        
        SetM = GameObject.Find("Setting_Manager");
        Set_M = SetM.transform.GetComponent<Setting_Manager>();
        
        Game_Mode = Set_M.Game_Mode;
        Time_or_Limited = Set_M.Time_or_Limited;
        R_and_F = Set_M.R_and_F;
        Game_Level = Set_M.Game_Level;
        Set = Set_M.Set;
        if(Set_M.Arrow_Level == 10)
        {
            Arr1_Hiden_Skill = true;
        }
        if(Set_M.Arrow2_Level == 10)
        {
            Arr2_Hiden_Skill = true;
        }
        if(Set_M.Arrow3_Level == 10)
        {
            Arr3_Hiden_Skill = true;
        }
    }
    // Start is called before the first frame update
    
    void Start()
    {
        if(Game_Mode == 1)
        {
            All_Wind_Object();
            if(Game_Level == 1)
            {
                No_Wind.SetActive(true);
            }
            Sc = GameObject.Find("Score_Manager");
            Sc_M = Sc.transform.GetComponent<Score_Manager>();
        }
        if(Game_Mode == 3)
        {
            Game_Level_and_Mode();
            Wait_Time_Temp = Money_Wait_Time;
            All_Add_Money_Effect_Off();
            if(Time_or_Limited == 1)
            {
                Siege_Ending_Time = 0.25f; 
            }
        }
        if(Game_Level >= 2 && Game_Mode == 1)
        {
            x_wind = Random.Range(-5.0f,5.0f);
            y_wind = Random.Range(-5.0f,5.0f);
            Wait_Time = Random.Range(5f, 15f);
        }
        if(Game_Mode == 2)
        {
            Game_Level_and_Mode();
            Wait_Time_Temp = Money_Wait_Time;
            Hunting_All_Add_Money_Effect_Off();
        }
    }

    // Update is called once per frame
    void Update()
    {
        Game_End();
        Time_Effect();
        if(Game_Mode == 1)  // 타겟 모드
        {
            Wind_Object();
            Random_Wind();
        }
        if(Game_Mode == 2)  // 사냥 모드
        {
            Play.Last_Shooting = true;
            All_UI_Bar();
            deltaTime_Mod2();
            Money_Add();
            if(Kill_Cheak == true)
            {
                Hunting_Add_Money_Effect();
            }
            Hunting_Show_Money_Text();
            Hutning_Text_Change();
            Game_Ending();
        }
        if(Game_Mode == 3)  // 공성 모드
        {
            All_UI_Bar();
            Money_Add();
            if(Kill_Cheak == true)
            {
                Add_Money_Effect();
            }
            Show_Money_Text();
            Last_Shooting_Mode();
            Game_Ending();
        }
    }

    

    void Game_End()
    {
        if(Game_Mode == 1 && Set_End_Effect == true)
        {
            Set_End_Text.text = "Set " + (Set_Count).ToString() + " - End";
            Set_Start_Text.text = "  Set " + (Set_Count+1).ToString();
            Effect_Wait_Time -= Time.deltaTime;
            if(Effect_Wait_Time > 0)
            {
                Play.Time_Stop = true;
                Arrow_Delete = true;
            }
            if(Effect_Wait_Time > 3)
            {
                Player_Canvas.SetActive(true);
                Canvas_SetEnd.SetActive(true);
                Set_End_Object.SetActive(true);
                if(End_W_C1 == false)
                {
                    End.PlayOneShot(End_W);
                    End_W_C1 = true;
                }
            }
            if(Effect_Wait_Time <= 3)
            {
                Set_End_Object.SetActive(false);
                Set_Start_Object.SetActive(true);
                if(End_W_C2 == false)
                {
                    End.PlayOneShot(End_W);
                    End_W_C2 = true;
                }
            }
            if(Effect_Wait_Time < 0)
            {
                Player_Canvas.SetActive(false);
                Canvas_SetEnd.SetActive(false);
                Set_Start_Object.SetActive(false);
                Effect_Wait_Time = 5f;
                Arrow_Delete = false;
                Set_End_Effect = false;
                Play.Time_Stop = false;
                Set_Score_Count_Text += 1;
                Sc_M.Score_Save();
                End_W_C1 = false;
                End_W_C2 = false;
            }
        }
        if(Game_Mode == 1 && Game_Set_Effect == true)
        {
            Effect_Wait_Time2 -= Time.deltaTime;
            Canvas_GameSet.SetActive(true);
            if(Effect_Wait_Time2 > 0)
            {
                Play.Time_Stop = true;
            }
            if(Effect_Wait_Time2 > 2)
            {
                Player_Canvas.SetActive(true);
                GameSet_Objcet.SetActive(true);
                if(End_W_C1 == false)
                {
                    End.PlayOneShot(End_W);
                    End_W_C1 = true;
                }
            }
            if(Effect_Wait_Time2 < 0)
            {
                Effect_Wait_Time2 = 0;
                GameSet_Objcet.SetActive(false);
                Sc_M.Score_Save();
                Game_Set_Effect = false;
                Game_Set = true;
                Set_M.Game_Set = true;
                if(End_W_C2 == false)
                {
                    End.PlayOneShot(End_W);
                    End_W_C2 = true;
                }
            }
        }
        if(Game_Mode == 1 && Game_Set == true)
        {
            Player_Canvas.SetActive(false);
            GameEnd_Object.SetActive(true);
            Play.Time_Stop = true;
        }
        if(Game_Clear == true && Game_Mode == 2)    // 사냥모드 클리어
        {
            Debug.Log("게임 클리어!");
            Set_M.Kill_Count = Kill_Count;
            Set_M.Money = Money;
            Set_M.Game_Clear = true;
            SceneManager.LoadScene(8);
        }
        if(Game_Over == true && Game_Mode == 2)     // 사냥모드 게임오버
        {
            Debug.Log("게임 오버!");
            Set_M.Kill_Count = Kill_Count;
            Set_M.Money = Money;
            Set_M.Game_Over = true;
            SceneManager.LoadScene(7);
        }

        if(Game_Clear == true && Game_Mode == 3)    // 공성모드 클리어
        {
            Set_M.Kill_Count = Kill_Count;
            Set_M.Money = Money;
            if(Siege_Ending_Time > 0)
            {
                Siege_Ending_Time -= Time.deltaTime;
            }
            if(Siege_Ending_Time < 0)
            {
                Debug.Log("게임 클리어!");
                Set_M.Game_Clear = true;
                SceneManager.LoadScene(6);
            }
        }
        if(Game_Over == true && Game_Mode == 3)     // 공성모드 게임오버
        {
            Debug.Log("게임 오버!");
            Set_M.Kill_Count = Kill_Count;
            Set_M.Money = Money;
            Set_M.Game_Over = true;
            SceneManager.LoadScene(5);
        }
    }



    void Game_Level_and_Mode()
    {
        if(Time_or_Limited == 1)    // 시간제
        {
            if(Game_Mode == 2)
            {
                Max_Time = 60 * Game_Level;
            }
            if(Game_Mode == 3)
            {
                Max_Time = 30 * Game_Level;
            }
            Time_Text = Max_Time;

            time_bar.SetActive(true);
            enemy_bar.SetActive(false);
        }
        if(Time_or_Limited == 2)    // 한정된 적
        {
            Max_Enemy = 30 * Game_Level + (Game_Level-1) * 5;
            Enemy_Text = Max_Enemy;

            time_bar.SetActive(false);
            enemy_bar.SetActive(true);
        }
        Kill_Bar.SetActive(true);
    }

    void All_UI_Bar()
    {
        time_bar_text1.text = Mathf.Round(Time_Text).ToString();
        time_bar_text2.text = Mathf.Round(Max_Time/2).ToString();
        time_bar_text3.text = Max_Time.ToString();
        time_bar_image.fillAmount = (1/Max_Time) * Time_Text;
        
        enemy_bar_text1.text = Enemy_Text.ToString();
        enemy_bar_text2.text = Mathf.Round(Max_Enemy/2).ToString();
        enemy_bar_text3.text = Max_Enemy.ToString();
        enemy_bar_image.fillAmount = (1/Max_Enemy) * Enemy_Text;

        Kill_Bar_Text.text = Kill_Count.ToString();
    }

    void Game_Ending()
    {
        if(Time_or_Limited == 1 && Mathf.Round(Time_Text) <= 0)
        { 
            Game_Clear = true;
        }
        if(Time_or_Limited == 2 && Enemy_Text == 0)
        {
            Game_Clear = true;
        }
    }

    void Wind_Object()
    {
        if(x_wind > 0)
        {
            Left_Wind.SetActive(false);
            Right_Wind.SetActive(true);
            x_wind_text.text = string.Format("{0:0.#}", x_wind).ToString() + "m/s";
        }
        if(x_wind < 0)
        {
            Left_Wind.SetActive(true);
            Right_Wind.SetActive(false);
            x_wind_text.text = string.Format("{0:0.#}", -(x_wind)).ToString() + "m/s";
        }
        if(y_wind > 0)
        {
            Up_Wind.SetActive(true);
            Down_Wind.SetActive(false);
            y_wind_text.text = string.Format("{0:0.#}", y_wind).ToString() + "m/s";
        }
        if(y_wind < 0)
        {
            Up_Wind.SetActive(false);
            Down_Wind.SetActive(true);
            y_wind_text.text = string.Format("{0:0.#}", -(y_wind)).ToString() + "m/s";
        }
    }

    void All_Wind_Object()
    {
        Left_Wind.SetActive(false);
        Right_Wind.SetActive(false);
        Up_Wind.SetActive(false);
        Down_Wind.SetActive(false);
        if(Game_Level > 1)
        {
            No_Wind.SetActive(false);
            Left_and_Right_Wind.SetActive(true);
            Up_and_Down_Wind.SetActive(true);
        }
    }

    void Random_Wind()
    {
        if(Wait_Time > 0)
        {
            Wait_Time -= Time.deltaTime;
        }
        if(Wait_Time < 0)
        {
            x_wind = Random.Range(-5.0f,5.0f);
            y_wind = Random.Range(-5.0f,5.0f);
            Wait_Time = Random.Range(5f, 15f);
        }
    }

    void Money_Add()
    {
        Money_Text.text = Money.ToString();
        Add_Money_Text.text = "Money + " + Add_Money.ToString();
    }

    void Add_Money_Effect()
    {
        if(Game_Level == 1)
        {
            if(HeadShot_Kill == true)
            {
                Money_Object.SetActive(true);
                Difficulty_Easy_HeadShot.SetActive(true);
            }
            else if(HeadShot_Kill == false)
            {
                Money_Object.SetActive(true);
            }
        }
        if(Game_Level == 2)
        {
            if(HeadShot_Kill == true)
            {
                Money_Object.SetActive(true);
                Difficulty_Normal.SetActive(true);
                Difficulty_HeadShot.SetActive(true);
            }
            else if(HeadShot_Kill == false)
            {
                Money_Object.SetActive(true);
                Difficulty_Normal.SetActive(true);
                Difficulty_HeadShot.SetActive(false);
            }
        }
        if(Game_Level == 3)
        {
            if(HeadShot_Kill == true)
            {
                Money_Object.SetActive(true);
                Difficulty_Hard.SetActive(true);
                Difficulty_HeadShot.SetActive(true);
            }
            else if(HeadShot_Kill == false)
            {
                Money_Object.SetActive(true);
                Difficulty_Hard.SetActive(true);
            }
        }
    }

    void All_Add_Money_Effect_Off()
    {
        Money_Object.SetActive(false);
        Difficulty_Easy_HeadShot.SetActive(false);
        Difficulty_Normal.SetActive(false);
        Difficulty_HeadShot.SetActive(false);
        Difficulty_Hard.SetActive(false);
    }

    void Show_Money_Text()
    {
        if(Kill_Cheak == true)
        {
            if(Money_Wait_Time > 0)
            {
                Money_Wait_Time -= Time.deltaTime;
                All_Money_Text.SetActive(true);
            }
            if(Money_Wait_Time < 0)
            {
                Kill_Cheak = false;
                All_Money_Text.SetActive(false);
                All_Add_Money_Effect_Off();
                Money_Wait_Time = Wait_Time_Temp;
            }
        }
    }

    void Hunting_Add_Money_Effect()
    {
        if(HeadShot_Kill == true && OneHitShot_Kill == true)
        {
            Hunting_All_Money_Text.SetActive(true);
            Hunting_Animal_Kill.SetActive(true);
            Hunting_HeadShot.SetActive(true);
            Hunting_OneHitKill.SetActive(true);
        }
        if(HeadShot_Kill == false && OneHitShot_Kill == true)
        {
            Hunting_All_Money_Text.SetActive(true);
            Hunting_Animal_Kill.SetActive(true);
            Hunting_OneHitKill_Solo.SetActive(true);
        }
        if(HeadShot_Kill == true && OneHitShot_Kill == false)
        {
            Hunting_All_Money_Text.SetActive(true);
            Hunting_Animal_Kill.SetActive(true);
            Hunting_HeadShot.SetActive(true);
        }
        if(HeadShot_Kill == false && OneHitShot_Kill == false)
        {
            Hunting_All_Money_Text.SetActive(true);
            Hunting_Animal_Kill.SetActive(true);
        }
    }

    void Hunting_All_Add_Money_Effect_Off()
    {
        Hunting_All_Money_Text.SetActive(false);
        Hunting_Add_Money.SetActive(false);
        Hunting_Animal_Kill.SetActive(false);
        Hunting_HeadShot.SetActive(false);
        Hunting_OneHitKill.SetActive(false);
        Hunting_OneHitKill_Solo.SetActive(false);
    }

    void Hunting_Show_Money_Text()
    {
        if(Kill_Cheak == true)
        {
            if(Money_Wait_Time > 0)
            {
                Money_Wait_Time -= Time.deltaTime;
                Hunting_Add_Money.SetActive(true);
            }
            if(Money_Wait_Time < 0)
            {
                Kill_Cheak = false;
                Hunting_Add_Money.SetActive(false);
                Hunting_All_Add_Money_Effect_Off();
                Money_Wait_Time = Wait_Time_Temp;
            }
        }
    }

    void Last_Shooting_Mode()
    {
        if(Time_or_Limited == 1 && Time_Text <= 3)
        {
            Play.Last_Shooting = true;
        }
        if(Time_or_Limited == 2 && Enemy_Text == 1)
        {
            Play.Last_Shooting = true;
        }
    }

    void deltaTime_Mod2()
    {
        if(Play.Time_Stop == false)
        {
            if(Time_Text > 0)
            {
                Time_Text -= Time.deltaTime;
            }
            if(Time_Text < 0)
            {
                Time_Text = 0;
            }
        }
    }

    public void Hunting_Clear()
    {
        Rabbit_Kill = false;
        Fox_Kill = false;
        Wolf_Kill = false;
        Eagle_Kill = false;
        Bear_Kill = false;
        
        DB_Kill = false;
        Eagle_F_Kill = false;
        DSE_Kill = false;
        Minota_Kill = false;
    }

    void Hutning_Text_Change()
    {
        if(Rabbit_Kill == true)
        {
            Hunting_Animal_Kill_Text.text = "Rabbit Kill + " + Animal_Kill_Money.ToString();
        }
        if(Fox_Kill == true)
        {
            Hunting_Animal_Kill_Text.text = "Fox Kill + " + Animal_Kill_Money.ToString();
        }
        if(Wolf_Kill == true)
        {
            Hunting_Animal_Kill_Text.text = "Wolf Kill + " + Animal_Kill_Money.ToString();
        }
        if(Eagle_Kill == true)
        {
            Hunting_Animal_Kill_Text.text = "Eagle Kill + " + Animal_Kill_Money.ToString();
        }
        if(Bear_Kill == true)
        {
            Hunting_Animal_Kill_Text.text = "Bear Kill + " + Animal_Kill_Money.ToString();
        }

        if(DB_Kill == true)
        {
            Hunting_Animal_Kill_Text.text = "Dragon Boar Kill + " + Animal_Kill_Money.ToString();
        }
        if(Eagle_F_Kill == true)
        {
            Hunting_Animal_Kill_Text.text = "Eagle(Fantasy) Kill + " + Animal_Kill_Money.ToString();
        }
        if(DSE_Kill == true)
        {
            Hunting_Animal_Kill_Text.text = "Dragon Soul Eater Kill + " + Animal_Kill_Money.ToString();
        }
        if(Minota_Kill == true)
        {
            Hunting_Animal_Kill_Text.text = "Minotaur Kill + " + Animal_Kill_Money.ToString();
        }
    }

    void Time_Effect()
    {
        if(Play.Time_Stop == true && Arr3_Hiden_Skill == true && Play.Option_Use == false && Game_Set == false)
        {
            Time_Stop_Effect.SetActive(true);

        }
        else
        {
            Time_Stop_Effect.SetActive(false);
        }
    }
}
