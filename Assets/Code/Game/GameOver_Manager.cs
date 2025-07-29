using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOver_Manager : MonoBehaviour
{
    public Text Money_Text;
    public Text Kill_Text;

    public int Money = 0;
    public int Kill = 0;

    GameObject SetM;
    Setting_Manager Set_M;

    void Awake()
    {
        SetM = GameObject.Find("Setting_Manager");
        Set_M = SetM.transform.GetComponent<Setting_Manager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        Money = Set_M.Money;
        Kill = Set_M.Kill_Count;

        Money_Text.text = (Money/2).ToString();
        Kill_Text.text = Kill.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GameOver_Siege_Re_Button()
    {
        Set_M.All_Money += (Set_M.Money/2);
        Set_M.Money = 0;
        Set_M.Kill_Count = 0;
        Set_M.Game_Over = false;
        Set_M.Time_or_Limited = 1;
        SceneManager.LoadScene(4);
    }

    public void GameOver_Hunting_Re_Button()
    {
        Set_M.All_Money += (Set_M.Money/2);
        Set_M.Money = 0;
        Set_M.Kill_Count = 0;
        Set_M.Game_Over = false;
        Set_M.Time_or_Limited = 1;
        SceneManager.LoadScene(3);
    }

    public void GameOver_Main_Button()
    {
        Set_M.All_Money += (Set_M.Money/2);
        Set_M.Money = 0;
        Set_M.Kill_Count = 0;
        Set_M.Game_Over = false;
        Set_M.Time_or_Limited = 1;
        SceneManager.LoadScene(1);
    }
}
