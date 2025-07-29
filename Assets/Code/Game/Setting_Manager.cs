using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Setting_Manager : MonoBehaviour
{
    public int Using_Arrow = 1;
    public int Using_Arrow_Temp;

    public int Game_Level = 2;
    public int Game_Mode = 0;

    public bool Game_Set = false;

    public int Time_or_Limited = 1;
    public int R_and_F = 1;

    public int All_Money = 0;
    public int Money = 0;

    public bool Game_Start = false;
    public bool Game_Over = false;
    public bool Game_Clear = false;

    public int Set = 3;

    public bool UnLock_Arrow2 = false;
    public bool UnLock_Arrow3 = false;

    public int Kill_Count = 0;

    public int Arrow_Level = 0;     // 기본 화살 강화 단계
    public int Arrow2_Level = 0;    // 관통 화살 강화 단계
    public int Arrow3_Level = 0;    // 사냥 화살 강화 단계

    public static Setting_Manager instance = null;

    public static Setting_Manager Instance 
    {
        get 
        {
            return instance;
        }
    }

    private void Awake()
    {
        if(instance) 
        {
            DestroyImmediate (gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
