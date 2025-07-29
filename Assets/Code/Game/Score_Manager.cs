using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score_Manager : MonoBehaviour
{
    public GameObject Score_1;
    public GameObject Score_2;
    public GameObject Score_3;
    public GameObject Score_4;
    public GameObject Score_5;
    public GameObject Score_6;
    public GameObject Score_7;
    public GameObject Score_8;
    public GameObject Score_9;
    public GameObject Score_10;
    public GameObject Score_X10;

    public GameObject RobinArrow;

    public GameObject Miss;

    public GameObject GM;
    Game_Manager G_M;

    public Text Set_Score_Text;
    public Text Set_Text;
    int Set = 0;

    public float Score_Show_Time = 0.1f;
    float Score_Show_Time_Temp = 0;
    public float Score_Wait_Time = 2.5f;
    float Wait_Time_Temp = 0;

    public bool Show = false;

    public float Score = 0;
    
    public float Robin_Score_Temp = 0;

    public bool Robin = false;         

    public bool miss = false;

    public GameObject Score_Easy;
    public GameObject Score_Normal;
    public GameObject Score_Hard;

    public float Save_Set1_Score = 0;
    public float Save_Set2_Score = 0;
    public float Save_Set3_Score = 0;
    public float Save_Set4_Score = 0;
    public float Save_Set5_Score = 0;
    public float Save_Set6_Score = 0;

    float Score_Mult = 5;

    public GameObject Set1_Score_Easy;
    public GameObject Set2_Score_Easy;
    public GameObject Set3_Score_Easy;
    public GameObject Set4_Score_Easy;
    public GameObject Set5_Score_Easy;
    public GameObject Set6_Score_Easy;

    public Text Set1_Score_Easy_Text;
    public Text Set2_Score_Easy_Text;
    public Text Set3_Score_Easy_Text;
    public Text Set4_Score_Easy_Text;
    public Text Set5_Score_Easy_Text;
    public Text Set6_Score_Easy_Text;

    public GameObject Set2_Score_Easy_NoShoot;
    public GameObject Set3_Score_Easy_NoShoot;
    public GameObject Set4_Score_Easy_NoShoot;
    public GameObject Set5_Score_Easy_NoShoot;
    public GameObject Set6_Score_Easy_NoShoot;


    public GameObject Set1_Score_Normal;
    public GameObject Set2_Score_Normal;
    public GameObject Set3_Score_Normal;
    public GameObject Set4_Score_Normal;
    public GameObject Set5_Score_Normal;
    public GameObject Set6_Score_Normal;

    public Text Set1_Score_Normal_Text;
    public Text Set2_Score_Normal_Text;
    public Text Set3_Score_Normal_Text;
    public Text Set4_Score_Normal_Text;
    public Text Set5_Score_Normal_Text;
    public Text Set6_Score_Normal_Text;

    public GameObject Set2_Score_Normal_NoShoot;
    public GameObject Set3_Score_Normal_NoShoot;
    public GameObject Set4_Score_Normal_NoShoot;
    public GameObject Set5_Score_Normal_NoShoot;
    public GameObject Set6_Score_Normal_NoShoot;


    public GameObject Set1_Score_Hard;
    public GameObject Set2_Score_Hard;
    public GameObject Set3_Score_Hard;
    public GameObject Set4_Score_Hard;
    public GameObject Set5_Score_Hard;
    public GameObject Set6_Score_Hard;

    public Text Set1_Score_Hard_Text;
    public Text Set2_Score_Hard_Text;
    public Text Set3_Score_Hard_Text;
    public Text Set4_Score_Hard_Text;
    public Text Set5_Score_Hard_Text;
    public Text Set6_Score_Hard_Text;

    public GameObject Set2_Score_Hard_NoShoot;
    public GameObject Set3_Score_Hard_NoShoot;
    public GameObject Set4_Score_Hard_NoShoot;
    public GameObject Set5_Score_Hard_NoShoot;
    public GameObject Set6_Score_Hard_NoShoot;

    public int Set1_Add_Money = 0;
    public int Set2_Add_Money = 0;
    public int Set3_Add_Money = 0;
    public int Set4_Add_Money = 0;
    public int Set5_Add_Money = 0;
    public int Set6_Add_Money = 0;

    public int Total_Add_Money = 0;
    public Text Total_Add_Money_Text;

    public float Total_Score = 0;
    public Text Total_Score_Text;

    // Start is called before the first frame update
    void Start()
    {
        All_Score_Off();
        Wait_Time_Temp = Score_Wait_Time;
        Score_Show_Time_Temp = Score_Show_Time;
        GM = GameObject.Find("Game_Manager");
        G_M = GM.transform.GetComponent<Game_Manager>();
        Set = G_M.Set;
        if(G_M.Game_Level == 1)
        {
            Score_Mult = 5;
            Score_Easy.SetActive(true);
        }
        if(G_M.Game_Level == 2)
        {
            Score_Mult = 10;
            Score_Normal.SetActive(true);
        }
        if(G_M.Game_Level == 3)
        {
            Score_Mult = 20;
            Score_Hard.SetActive(true);
        }
        All_Set_Score_Off();
        All_Set_Score_On();
    }

    // Update is called once per frame
    void Update()
    {
        Score_Show();
        Set_Score();
        Total();
    }

    void All_Score_Off()
    {
        Score_1.SetActive(false);
        Score_2.SetActive(false);
        Score_3.SetActive(false);
        Score_4.SetActive(false);
        Score_5.SetActive(false);
        Score_6.SetActive(false);
        Score_7.SetActive(false);
        Score_8.SetActive(false);
        Score_9.SetActive(false);
        Score_10.SetActive(false);
        Score_X10.SetActive(false);
        RobinArrow.SetActive(false);
        Miss.SetActive(false);
    }

    void Score_Show()
    {
        if(Show == true)
        {
            if(Score_Show_Time > 0)
            {
                Score_Show_Time -= Time.deltaTime;
            }
            if(Score_Show_Time < 0)
            {
                if(Score_Wait_Time > 0)
                {
                    Score_Wait_Time -= Time.deltaTime;
                    if(Score == 1)
                    {
                        Score_1.SetActive(true);
                    }
                    if(Score == 2)
                    {
                        Score_2.SetActive(true);
                    }
                    if(Score == 3)
                    {
                        Score_3.SetActive(true);
                    }
                    if(Score == 4)
                    {
                        Score_4.SetActive(true);
                    }
                    if(Score == 5)
                    {
                        Score_5.SetActive(true);
                    }
                    if(Score == 6)
                    {
                        Score_6.SetActive(true);
                    }
                    if(Score == 7)
                    {
                        Score_7.SetActive(true);
                    }
                    if(Score == 8)
                    {
                        Score_8.SetActive(true);
                    }
                    if(Score == 9)
                    {
                        Score_9.SetActive(true);
                    }
                    if(Score == 10)
                    {
                        Score_10.SetActive(true);
                    }
                    if(Score == 11)
                    {
                        Score_X10.SetActive(true);
                    }
                    if(Robin == true)
                    {
                        RobinArrow.SetActive(true);
                    }
                    if(Score == 0 || miss == true)
                    {
                        Miss.SetActive(true);
                    }
                    if(Score > 0)
                    {
                        Miss.SetActive(false);
                    }
                }
                if(Score_Wait_Time < 0)
                {
                    All_Score_Off();
                    Show = false;
                    Robin = false;
                    Score_Wait_Time = 0;
                    Score_Show_Time = 0;
                    Score = 0;
                    miss = false;
                }
            }
        }
        if(Show == false)
        {
            Score_Wait_Time = Wait_Time_Temp;
            Score_Show_Time = Score_Show_Time_Temp;
        }
    }

    void Set_Score()
    {
        Set_Score_Text.text = "Set" + (G_M.Set_Score_Count_Text).ToString() + "   Score : " + G_M.Total_Score.ToString();   
        Set_Text.text = "Set " + (G_M.Set_Count+1).ToString();
    }

    public void Score_Save()
    {
        if(G_M.Set_Count == 1)
        {
            Save_Set1_Score = G_M.Total_Score; 
            if(Save_Set1_Score == 60)
            {
                Set1_Add_Money = (int)Save_Set1_Score * (int)(Score_Mult*2);

                Set1_Score_Easy_Text.text = "Set1 Score(Easy) " + Save_Set1_Score.ToString() + " * " + (Score_Mult*2).ToString() + " = " + Set1_Add_Money.ToString();
                Set1_Score_Normal_Text.text = "Set1 Score(Normal) " + Save_Set1_Score.ToString() + " * " + (Score_Mult*2).ToString() + " = " + Set1_Add_Money.ToString();
                Set1_Score_Hard_Text.text = "Set1 Score(Hard) " + Save_Set1_Score.ToString() + " * " + (Score_Mult*2).ToString() + " = " + Set1_Add_Money.ToString();
            }
            else if(Save_Set1_Score != 60)
            {
                Set1_Add_Money = (int)Save_Set1_Score * (int)Score_Mult;
                
                Set1_Score_Easy_Text.text = "Set1 Score(Easy) " + Save_Set1_Score.ToString() + " * " + Score_Mult.ToString() + " = " + Set1_Add_Money.ToString();
                Set1_Score_Normal_Text.text = "Set1 Score(Normal) " + Save_Set1_Score.ToString() + " * " + Score_Mult.ToString() + " = " + Set1_Add_Money.ToString();
                Set1_Score_Hard_Text.text = "Set1 Score(Hard) " + Save_Set1_Score.ToString() + " * " + Score_Mult.ToString() + " = " + Set1_Add_Money.ToString();
            }
            G_M.Total_Score = 0;
        }
        if(G_M.Set_Count == 2)
        {
            Save_Set2_Score = G_M.Total_Score;
            if(Save_Set2_Score == 60)
            {
                Set2_Add_Money = (int)Save_Set2_Score * (int)(Score_Mult*2);

                Set2_Score_Easy_Text.text = "Set2 Score(Easy) " + Save_Set2_Score.ToString() + " * " + (Score_Mult*2).ToString() + " = " + Set2_Add_Money.ToString();
                Set2_Score_Normal_Text.text = "Set2 Score(Normal) " + Save_Set2_Score.ToString() + " * " + (Score_Mult*2).ToString() + " = " + Set2_Add_Money.ToString();
                Set2_Score_Hard_Text.text = "Set2 Score(Hard) " + Save_Set2_Score.ToString() + " * " + (Score_Mult*2).ToString() + " = " + Set2_Add_Money.ToString();
            }
            else if(Save_Set2_Score != 60)
            {
                Set2_Add_Money = (int)Save_Set2_Score * (int)Score_Mult;
                
                Set2_Score_Easy_Text.text = "Set2 Score(Easy) " + Save_Set2_Score.ToString() + " * " + Score_Mult.ToString() + " = " + Set2_Add_Money.ToString();
                Set2_Score_Normal_Text.text = "Set2 Score(Normal) " + Save_Set2_Score.ToString() + " * " + Score_Mult.ToString() + " = " + Set2_Add_Money.ToString();
                Set2_Score_Hard_Text.text = "Set2 Score(Hard) " + Save_Set2_Score.ToString() + " * " + Score_Mult.ToString() + " = " + Set2_Add_Money.ToString();
            }
            G_M.Total_Score = 0;
        }
        if(G_M.Set_Count == 3)
        {
            Save_Set3_Score = G_M.Total_Score; 
            if(Save_Set3_Score == 60)
            {
                Set3_Add_Money = (int)Save_Set3_Score * (int)(Score_Mult*2);

                Set3_Score_Easy_Text.text = "Set3 Score(Easy) " + Save_Set3_Score.ToString() + " * " + (Score_Mult*2).ToString() + " = " + Set3_Add_Money.ToString();
                Set3_Score_Normal_Text.text = "Set3 Score(Normal) " + Save_Set3_Score.ToString() + " * " + (Score_Mult*2).ToString() + " = " + Set3_Add_Money.ToString();
                Set3_Score_Hard_Text.text = "Set3 Score(Hard) " + Save_Set3_Score.ToString() + " * " + (Score_Mult*2).ToString() + " = " + Set3_Add_Money.ToString();
            }
            else if(Save_Set3_Score != 60)
            {
                Set3_Add_Money = (int)Save_Set3_Score * (int)Score_Mult;
                
                Set3_Score_Easy_Text.text = "Set3 Score(Easy) " + Save_Set3_Score.ToString() + " * " + Score_Mult.ToString() + " = " + Set3_Add_Money.ToString();
                Set3_Score_Normal_Text.text = "Set3 Score(Normal) " + Save_Set3_Score.ToString() + " * " + Score_Mult.ToString() + " = " + Set3_Add_Money.ToString();
                Set3_Score_Hard_Text.text = "Set3 Score(Hard) " + Save_Set3_Score.ToString() + " * " + Score_Mult.ToString() + " = " + Set3_Add_Money.ToString();
            }
            G_M.Total_Score = 0;
        }
        if(G_M.Set_Count == 4)
        {
            Save_Set4_Score = G_M.Total_Score; 
            if(Save_Set4_Score == 60)
            {
                Set4_Add_Money = (int)Save_Set4_Score * (int)(Score_Mult*2);

                Set4_Score_Easy_Text.text = "Set4 Score(Easy) " + Save_Set4_Score.ToString() + " * " + (Score_Mult*2).ToString() + " = " + Set4_Add_Money.ToString();
                Set4_Score_Normal_Text.text = "Set4 Score(Normal) " + Save_Set4_Score.ToString() + " * " + (Score_Mult*2).ToString() + " = " + Set4_Add_Money.ToString();
                Set4_Score_Hard_Text.text = "Set4 Score(Hard) " + Save_Set4_Score.ToString() + " * " + (Score_Mult*2).ToString() + " = " + Set4_Add_Money.ToString();
            }
            else if(Save_Set4_Score != 60)
            {
                Set4_Add_Money = (int)Save_Set4_Score * (int)Score_Mult;
                
                Set4_Score_Easy_Text.text = "Set4 Score(Easy) " + Save_Set4_Score.ToString() + " * " + Score_Mult.ToString() + " = " + Set4_Add_Money.ToString();
                Set4_Score_Normal_Text.text = "Set4 Score(Normal) " + Save_Set4_Score.ToString() + " * " + Score_Mult.ToString() + " = " + Set4_Add_Money.ToString();
                Set4_Score_Hard_Text.text = "Set4 Score(Hard) " + Save_Set4_Score.ToString() + " * " + Score_Mult.ToString() + " = " + Set4_Add_Money.ToString();
            }
            G_M.Total_Score = 0;
        }
        if(G_M.Set_Count == 5)
        {
            Save_Set5_Score = G_M.Total_Score; 
            if(Save_Set5_Score == 60)
            {
                Set5_Add_Money = (int)Save_Set5_Score * (int)(Score_Mult*2);

                Set5_Score_Easy_Text.text = "Set5 Score(Easy) " + Save_Set5_Score.ToString() + " * " + (Score_Mult*2).ToString() + " = " + Set5_Add_Money.ToString();
                Set5_Score_Normal_Text.text = "Set5 Score(Normal) " + Save_Set5_Score.ToString() + " * " + (Score_Mult*2).ToString() + " = " + Set5_Add_Money.ToString();
                Set5_Score_Hard_Text.text = "Set5 Score(Hard) " + Save_Set5_Score.ToString() + " * " + (Score_Mult*2).ToString() + " = " + Set5_Add_Money.ToString();
            }
            else if(Save_Set5_Score != 60)
            {
                Set5_Add_Money = (int)Save_Set5_Score * (int)Score_Mult;
                
                Set5_Score_Easy_Text.text = "Set5 Score(Easy) " + Save_Set5_Score.ToString() + " * " + Score_Mult.ToString() + " = " + Set5_Add_Money.ToString();
                Set5_Score_Normal_Text.text = "Set5 Score(Normal) " + Save_Set5_Score.ToString() + " * " + Score_Mult.ToString() + " = " + Set5_Add_Money.ToString();
                Set5_Score_Hard_Text.text = "Set5 Score(Hard) " + Save_Set5_Score.ToString() + " * " + Score_Mult.ToString() + " = " + Set5_Add_Money.ToString();
            }
            G_M.Total_Score = 0;
        }
        if(G_M.Set_Count == 6)
        {
            Save_Set6_Score = G_M.Total_Score; 
            if(Save_Set6_Score == 60)
            {
                Set6_Add_Money = (int)Save_Set6_Score * (int)(Score_Mult*2);

                Set6_Score_Easy_Text.text = "Set6 Score(Easy) " + Save_Set6_Score.ToString() + " * " + (Score_Mult*2).ToString() + " = " + Set6_Add_Money.ToString();
                Set6_Score_Normal_Text.text = "Set6 Score(Normal) " + Save_Set6_Score.ToString() + " * " + (Score_Mult*2).ToString() + " = " + Set6_Add_Money.ToString();
                Set6_Score_Hard_Text.text = "Set6 Score(Hard) " + Save_Set6_Score.ToString() + " * " + (Score_Mult*2).ToString() + " = " + Set6_Add_Money.ToString();
            }
            else if(Save_Set6_Score != 60)
            {
                Set6_Add_Money = (int)Save_Set6_Score * (int)Score_Mult;
                
                Set6_Score_Easy_Text.text = "Set6 Score(Easy) " + Save_Set6_Score.ToString() + " * " + Score_Mult.ToString() + " = " + Set6_Add_Money.ToString();
                Set6_Score_Normal_Text.text = "Set6 Score(Normal) " + Save_Set6_Score.ToString() + " * " + Score_Mult.ToString() + " = " + Set6_Add_Money.ToString();
                Set6_Score_Hard_Text.text = "Set6 Score(Hard) " + Save_Set6_Score.ToString() + " * " + Score_Mult.ToString() + " = " + Set6_Add_Money.ToString();
            }
            G_M.Total_Score = 0;
        }
    }

    void All_Set_Score_Off()
    {
        Set1_Score_Easy.SetActive(false);
        Set2_Score_Easy.SetActive(false);
        Set3_Score_Easy.SetActive(false);
        Set4_Score_Easy.SetActive(false);
        Set5_Score_Easy.SetActive(false);
        Set6_Score_Easy.SetActive(false);

        Set2_Score_Easy_NoShoot.SetActive(false);
        Set3_Score_Easy_NoShoot.SetActive(false);
        Set4_Score_Easy_NoShoot.SetActive(false);
        Set5_Score_Easy_NoShoot.SetActive(false);
        Set6_Score_Easy_NoShoot.SetActive(false);


        Set1_Score_Normal.SetActive(false);
        Set2_Score_Normal.SetActive(false);
        Set3_Score_Normal.SetActive(false);
        Set4_Score_Normal.SetActive(false);
        Set5_Score_Normal.SetActive(false);
        Set6_Score_Normal.SetActive(false);

        Set2_Score_Normal_NoShoot.SetActive(false);
        Set3_Score_Normal_NoShoot.SetActive(false);
        Set4_Score_Normal_NoShoot.SetActive(false);
        Set5_Score_Normal_NoShoot.SetActive(false);
        Set6_Score_Normal_NoShoot.SetActive(false);


        Set1_Score_Hard.SetActive(false);
        Set2_Score_Hard.SetActive(false);
        Set3_Score_Hard.SetActive(false);
        Set4_Score_Hard.SetActive(false);
        Set5_Score_Hard.SetActive(false);
        Set6_Score_Hard.SetActive(false);

        Set2_Score_Hard_NoShoot.SetActive(false);
        Set3_Score_Hard_NoShoot.SetActive(false);
        Set4_Score_Hard_NoShoot.SetActive(false);
        Set5_Score_Hard_NoShoot.SetActive(false);
        Set6_Score_Hard_NoShoot.SetActive(false);
    }

    void All_Set_Score_On()
    {
        if(Set == 1)
        {
            Set1_Score_Easy.SetActive(true);

            Set2_Score_Easy_NoShoot.SetActive(true);
            Set3_Score_Easy_NoShoot.SetActive(true);
            Set4_Score_Easy_NoShoot.SetActive(true);
            Set5_Score_Easy_NoShoot.SetActive(true);
            Set6_Score_Easy_NoShoot.SetActive(true);

            
            Set1_Score_Normal.SetActive(true);

            Set2_Score_Normal_NoShoot.SetActive(true);
            Set3_Score_Normal_NoShoot.SetActive(true);
            Set4_Score_Normal_NoShoot.SetActive(true);
            Set5_Score_Normal_NoShoot.SetActive(true);
            Set6_Score_Normal_NoShoot.SetActive(true);


            Set1_Score_Hard.SetActive(true);

            Set2_Score_Hard_NoShoot.SetActive(true);
            Set3_Score_Hard_NoShoot.SetActive(true);
            Set4_Score_Hard_NoShoot.SetActive(true);
            Set5_Score_Hard_NoShoot.SetActive(true);
            Set6_Score_Hard_NoShoot.SetActive(true);
        }
        if(Set == 2)
        {
            Set1_Score_Easy.SetActive(true);
            Set2_Score_Easy.SetActive(true);

            Set3_Score_Easy_NoShoot.SetActive(true);
            Set4_Score_Easy_NoShoot.SetActive(true);
            Set5_Score_Easy_NoShoot.SetActive(true);
            Set6_Score_Easy_NoShoot.SetActive(true);

            
            Set1_Score_Normal.SetActive(true);
            Set2_Score_Normal.SetActive(true);

            Set3_Score_Normal_NoShoot.SetActive(true);
            Set4_Score_Normal_NoShoot.SetActive(true);
            Set5_Score_Normal_NoShoot.SetActive(true);
            Set6_Score_Normal_NoShoot.SetActive(true);


            Set1_Score_Hard.SetActive(true);
            Set2_Score_Hard.SetActive(true);

            Set3_Score_Hard_NoShoot.SetActive(true);
            Set4_Score_Hard_NoShoot.SetActive(true);
            Set5_Score_Hard_NoShoot.SetActive(true);
            Set6_Score_Hard_NoShoot.SetActive(true);
        }
        if(Set == 3)
        {
            Set1_Score_Easy.SetActive(true);
            Set2_Score_Easy.SetActive(true);
            Set3_Score_Easy.SetActive(true);

            Set4_Score_Easy_NoShoot.SetActive(true);
            Set5_Score_Easy_NoShoot.SetActive(true);
            Set6_Score_Easy_NoShoot.SetActive(true);

            
            Set1_Score_Normal.SetActive(true);
            Set2_Score_Normal.SetActive(true);
            Set3_Score_Normal.SetActive(true);

            Set4_Score_Normal_NoShoot.SetActive(true);
            Set5_Score_Normal_NoShoot.SetActive(true);
            Set6_Score_Normal_NoShoot.SetActive(true);


            Set1_Score_Hard.SetActive(true);
            Set2_Score_Hard.SetActive(true);
            Set3_Score_Hard.SetActive(true);

            Set4_Score_Hard_NoShoot.SetActive(true);
            Set5_Score_Hard_NoShoot.SetActive(true);
            Set6_Score_Hard_NoShoot.SetActive(true);
        }
        if(Set == 4)
        {
            Set1_Score_Easy.SetActive(true);
            Set2_Score_Easy.SetActive(true);
            Set3_Score_Easy.SetActive(true);
            Set4_Score_Easy.SetActive(true);

            Set5_Score_Easy_NoShoot.SetActive(true);
            Set6_Score_Easy_NoShoot.SetActive(true);

            
            Set1_Score_Normal.SetActive(true);
            Set2_Score_Normal.SetActive(true);
            Set3_Score_Normal.SetActive(true);
            Set4_Score_Normal.SetActive(true);

            Set5_Score_Normal_NoShoot.SetActive(true);
            Set6_Score_Normal_NoShoot.SetActive(true);


            Set1_Score_Hard.SetActive(true);
            Set2_Score_Hard.SetActive(true);
            Set3_Score_Hard.SetActive(true);
            Set4_Score_Hard.SetActive(true);

            Set5_Score_Hard_NoShoot.SetActive(true);
            Set6_Score_Hard_NoShoot.SetActive(true);
        }
        if(Set == 5)
        {
            Set1_Score_Easy.SetActive(true);
            Set2_Score_Easy.SetActive(true);
            Set3_Score_Easy.SetActive(true);
            Set4_Score_Easy.SetActive(true);
            Set5_Score_Easy.SetActive(true);

            Set6_Score_Easy_NoShoot.SetActive(true);

            
            Set1_Score_Normal.SetActive(true);
            Set2_Score_Normal.SetActive(true);
            Set3_Score_Normal.SetActive(true);
            Set4_Score_Normal.SetActive(true);
            Set5_Score_Normal.SetActive(true);

            Set6_Score_Normal_NoShoot.SetActive(true);


            Set1_Score_Hard.SetActive(true);
            Set2_Score_Hard.SetActive(true);
            Set3_Score_Hard.SetActive(true);
            Set4_Score_Hard.SetActive(true);
            Set5_Score_Hard.SetActive(true);

            Set6_Score_Hard_NoShoot.SetActive(true);
        }
        if(Set == 6)
        {
            Set1_Score_Easy.SetActive(true);
            Set2_Score_Easy.SetActive(true);
            Set3_Score_Easy.SetActive(true);
            Set4_Score_Easy.SetActive(true);
            Set5_Score_Easy.SetActive(true);
            Set6_Score_Easy.SetActive(true);

            
            Set1_Score_Normal.SetActive(true);
            Set2_Score_Normal.SetActive(true);
            Set3_Score_Normal.SetActive(true);
            Set4_Score_Normal.SetActive(true);
            Set5_Score_Normal.SetActive(true);
            Set6_Score_Normal.SetActive(true);


            Set1_Score_Hard.SetActive(true);
            Set2_Score_Hard.SetActive(true);
            Set3_Score_Hard.SetActive(true);
            Set4_Score_Hard.SetActive(true);
            Set5_Score_Hard.SetActive(true);
            Set6_Score_Hard.SetActive(true);
        }
    }

    void Total()
    {
        Total_Add_Money = Set1_Add_Money + Set2_Add_Money + Set3_Add_Money + Set4_Add_Money + Set5_Add_Money + Set6_Add_Money;
        Total_Score = Save_Set1_Score + Save_Set2_Score + Save_Set3_Score + Save_Set4_Score + Save_Set5_Score + Save_Set6_Score;

        Total_Add_Money_Text.text = Total_Add_Money.ToString();
        Total_Score_Text.text = "Total : " + Total_Score.ToString();
    }
}
