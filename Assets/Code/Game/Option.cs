using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Option : MonoBehaviour
{
    public GameObject P;
    Player Play;

    public GameObject G;
    Game_Manager G_M;

    GameObject SetM;
    Setting_Manager Set_M;

    GameObject ScM;
    Score_Manager Sc_M;

    public GameObject Op;

    void Awake()
    {
        P = GameObject.Find("Player");
        Play = P.transform.GetComponent<Player>();

        G = GameObject.Find("Game_Manager");
        G_M = G.transform.GetComponent<Game_Manager>();

        SetM = GameObject.Find("Setting_Manager");
        Set_M = SetM.transform.GetComponent<Setting_Manager>();

        if(Set_M.Game_Mode == 1)
        {
            ScM = GameObject.Find("Score_Manager");
            Sc_M = ScM.transform.GetComponent<Score_Manager>();
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(G_M.Set_End_Effect == false || G_M.Game_Set_Effect == false)
        {
            if(G_M.Game_Set == false && Play.Option_Use == true)
            {
                Op.SetActive(true);
            }
            else if(Play.Option_Use == false)
            {
                Op.SetActive(false);
            }
            if(G_M.Game_Set_Effect == true || G_M.Game_Set == true)
            {
                Op.SetActive(false);
            }
            if(G_M.Set_End_Effect == true)
            {
                Op.SetActive(false);
            }
        } 
    }

    public void Re_Target_Button()
    {
        All_Clear();
        
        SceneManager.LoadScene(2);
    }

    public void Re_Hunting_Button()
    {
        SceneManager.LoadScene(3);
    }

    public void Re_Siege_Button()
    {
        SceneManager.LoadScene(4);
    }

    public void Main_Button()
    {
        if(Set_M.Game_Mode == 1)
        {
            All_Clear();   
        }
        Set_M.Time_or_Limited = 1;
        SceneManager.LoadScene(1);
    }

    public void Clear_Re_Button()
    {
        Set_M.All_Money += Sc_M.Total_Add_Money;
        All_Clear();
        SceneManager.LoadScene(2);
    }

    public void Clear_Main_Button()
    {
        Set_M.All_Money += Sc_M.Total_Add_Money;
        All_Clear();
        SceneManager.LoadScene(1);
    }

    void All_Clear()
    {
        Sc_M.Save_Set1_Score = 0;
        Sc_M.Save_Set2_Score = 0;
        Sc_M.Save_Set3_Score = 0;
        Sc_M.Save_Set4_Score = 0;
        Sc_M.Save_Set5_Score = 0;
        Sc_M.Save_Set6_Score = 0;

        Sc_M.Set1_Add_Money = 0;
        Sc_M.Set2_Add_Money = 0;
        Sc_M.Set3_Add_Money = 0;
        Sc_M.Set4_Add_Money = 0;
        Sc_M.Set5_Add_Money = 0;
        Sc_M.Set6_Add_Money = 0;

        Sc_M.Total_Add_Money = 0;
        Sc_M.Total_Score = 0;

        Set_M.Time_or_Limited = 1;
        Set_M.Game_Set = false;
    }
}
