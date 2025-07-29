using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UI_Manager : MonoBehaviour
{
    public GameObject SetM;
    Setting_Manager Set_M;

    public GameObject Target_Button;
    public GameObject Hunting_Button;
    public GameObject Siege_Button;
    public GameObject Forge_Button;

    public GameObject Target_Level_Easy;
    public GameObject Target_Level_Normal;
    public GameObject Target_Level_Hard;

    public GameObject Target_Set1;
    public GameObject Target_Set2;
    public GameObject Target_Set3;
    public GameObject Target_Set4;
    public GameObject Target_Set5;
    public GameObject Target_Set6;

    public GameObject Hunting_Level_Easy;
    public GameObject Hunting_Level_Normal;
    public GameObject Hunting_Level_Hard;

    public GameObject Hunting_Mod_R;
    public GameObject Hunting_Mod_F;

    public GameObject Siege_Level_Easy;
    public GameObject Siege_Level_Normal;
    public GameObject Siege_Level_Hard;

    public GameObject Siege_Time;
    public GameObject Siege_Limit;

    public GameObject Choose_Arrow;     // 기본화살 - 선택됨
    public GameObject Choose_Arrow2;    // 관통화살 - 선택됨
    public GameObject Choose_Arrow3;    // 사냥화살 - 선택됨

    public GameObject Arrow_Text;   // 기본화살 - 설명
    public GameObject Arrow2_Text;   // 관통화살 - 설명
    public GameObject Arrow3_Text;   // 사냥화살 - 설명

    public GameObject Arrow2_UnLock_Text; // 관통화살 - 해금 5000
    public GameObject Arrow3_UnLock_Text; // 사냥화살 - 해금 10000

    public GameObject Choose_Button;    // 선택 버튼

    public GameObject Lock_Arrow2;  // 관통화살 잠금
    public GameObject Lock_Arrow3;  // 사냥화살 잠금

    public bool UnLock_Arrow2 = false;  // 관통화살 잠금상태
    public bool UnLock_Arrow3 = false;  // 사냥화살 잠금상태

    public Text Money_Text;

    public AudioSource UnLock;
    public AudioClip Lock_Arr;
    public AudioClip UnLock_Arr;

    public GameObject Arr_Level_0;
    public GameObject Arr_Level_1;
    public GameObject Arr_Level_2;
    public GameObject Arr_Level_3;
    public GameObject Arr_Level_4;
    public GameObject Arr_Level_5;
    public GameObject Arr_Level_6;
    public GameObject Arr_Level_7;
    public GameObject Arr_Level_8;
    public GameObject Arr_Level_9;
    public GameObject Arr_Level_MAX;

    public GameObject Upgrade;
    public Text Upgrade_Arr_Money;

    public GameObject Arr1_MAX_Effect;  // 기본화살 히든효과 : Charge 게이지가 항상 100%가 됨
    public GameObject Arr2_MAX_Effect;  // 관통화살 히든효과 : 중력의 영향을 받지않음
    public GameObject Arr3_MAX_Effect;  // 사냥화살 히든효과 : 화살이 날라가는 동안은 시간이 멈춘다

    public Text Arr_Dmg_Text;       // 기본화살 데미지 설명
    public Text Arr2_Dmg_Text;      // 관통화살 데미지 설명
    public Text Arr3_Dmg_Text;      // 사냥화살 데미지 설명

    float arr_Dmg = 80;
    float arr2_Dmg = 60;
    float arr3_Dmg = 120;

    float arr_AP = 25;
    float arr2_AP = 50;
    float arr3_AP = 10;

    public Text Arr_AP_Text;        // 기본화살 방어관통 확률 설명
    public Text Arr2_AP_Text;        // 관통화살 방어관통 확률 설명
    public Text Arr3_AP_Text;        // 사냥화살 방어관통 확률 설명

    // Start is called before the first frame update
    void Awake()
    {
        SetM = GameObject.Find("Setting_Manager");
    }

    void Start()
    {
        Set_M = SetM.transform.GetComponent<Setting_Manager>();
    }

    // Update is called once per frame
    void Update()
    {
        UI_Set();
    }

    public void Target_Option_On()
    {
        Target_Button.SetActive(true);
    }
    public void Target_Option_Off()
    {
        Target_Button.SetActive(false);
    }
    public void Target_Play()
    {
        Set_M.Time_or_Limited = 0;
        SceneManager.LoadScene(2);
        Set_M.Game_Start = true;
        Set_M.Game_Mode = 1;
    }

    public void Set1_Button()
    {
        Set_M.Set = 1;
    }
    public void Set2_Button()
    {
        Set_M.Set = 2;
    }
    public void Set3_Button()
    {
        Set_M.Set = 3;
    }
    public void Set4_Button()
    {
        Set_M.Set = 4;
    }
    public void Set5_Button()
    {
        Set_M.Set = 5;
    }
    public void Set6_Button()
    {
        Set_M.Set = 6;
    }

    public void Hunting_Option_On()
    {
        Hunting_Button.SetActive(true);
        Set_M.Time_or_Limited = 1;
    }
    public void Hunting_Option_Off()
    {
        Hunting_Button.SetActive(false);
    }
    public void Hunting_Play()
    {
        SceneManager.LoadScene(3);
        Set_M.Game_Start = true;
        Set_M.Game_Mode = 2;
    }

    public void Hunting_Real()
    {
        Set_M.R_and_F = 1;
    }
    public void Hunting_Fan()
    {
        Set_M.R_and_F = 2;
    }

    public void Siege_Option_On()
    {
        Siege_Button.SetActive(true);
    }
    public void Siege_Option_Off()
    {
        Siege_Button.SetActive(false);
    }
    public void Siege_Play()
    {
        SceneManager.LoadScene(4);
        Set_M.Game_Start = true;
        Set_M.Game_Mode = 3;
    }

    public void Siege_Time_Button()
    {
        Set_M.Time_or_Limited = 1;
    }
    public void Siege_Limit_Button()
    {
        Set_M.Time_or_Limited = 2;
    }

    public void Forge_Option_On()
    {
        Forge_Button.SetActive(true);
    }
    public void Forge_Option_Off()
    {
        Set_M.Using_Arrow_Temp = Set_M.Using_Arrow;
        Forge_Button.SetActive(false);
    }
    public void Forge_Arrow()
    {
        Set_M.Using_Arrow_Temp = 1;

        Arrow2_UnLock_Text.SetActive(false);
        Arrow3_UnLock_Text.SetActive(false);

        Choose_Arrow.SetActive(true);
        Choose_Arrow2.SetActive(false);
        Choose_Arrow3.SetActive(false);

        Arrow_Text.SetActive(true);
        Arrow2_Text.SetActive(false);
        Arrow3_Text.SetActive(false);

        Upgrade.SetActive(true);
        Arr_Clear();
        Arr_Level();
    }
    public void Forge_Arrow2()
    {
        if(UnLock_Arrow2 == false)
        {
            Arrow2_UnLock_Text.SetActive(true);
            Arrow3_UnLock_Text.SetActive(false);

            Arrow_Text.SetActive(false);
            Arrow2_Text.SetActive(true);
            Arrow3_Text.SetActive(false);

            Upgrade.SetActive(false);
        }
        if(UnLock_Arrow2 == true)
        {
            Set_M.Using_Arrow_Temp = 2;

            Arrow2_UnLock_Text.SetActive(false);
            Arrow3_UnLock_Text.SetActive(false);

            Arrow_Text.SetActive(false);
            Arrow2_Text.SetActive(true);
            Arrow3_Text.SetActive(false);

            Upgrade.SetActive(true);
            Arr_Clear();
            Arr_Level();
        }
    }
    public void Forge_Arrow3()
    {
        if(UnLock_Arrow3 == false)
        {
            Arrow2_UnLock_Text.SetActive(false);
            Arrow3_UnLock_Text.SetActive(true);

            Arrow_Text.SetActive(false);
            Arrow2_Text.SetActive(false);
            Arrow3_Text.SetActive(true);

            Upgrade.SetActive(false);
        }
        if(UnLock_Arrow3 == true)
        {
            Set_M.Using_Arrow_Temp = 3;

            Arrow2_UnLock_Text.SetActive(false);
            Arrow3_UnLock_Text.SetActive(false);

            Arrow_Text.SetActive(false);
            Arrow2_Text.SetActive(false);
            Arrow3_Text.SetActive(true);

            Upgrade.SetActive(true);
            Arr_Clear();
            Arr_Level();
        }
    }
    public void Forge_Choose()
    {
        Set_M.Using_Arrow = Set_M.Using_Arrow_Temp;
        Forge_Button.SetActive(false);
    }


    public void Level_Easy()
    {
        Set_M.Game_Level = 1;
    }
    public void Level_Normal()
    {
        Set_M.Game_Level = 2;
    }
    public void Level_Hard()
    {
        Set_M.Game_Level = 3;
    }


    void UI_Set()
    {
        if(Set_M.Game_Level == 1)   //난이도 쉬움
        {
            Target_Level_Easy.SetActive(true);
            Hunting_Level_Easy.SetActive(true);
            Siege_Level_Easy.SetActive(true);

            Target_Level_Normal.SetActive(false);
            Hunting_Level_Normal.SetActive(false);
            Siege_Level_Normal.SetActive(false);

            Target_Level_Hard.SetActive(false);
            Hunting_Level_Hard.SetActive(false);
            Siege_Level_Hard.SetActive(false);
        }
        if(Set_M.Game_Level == 2)   //난이도 보통
        {
            Target_Level_Easy.SetActive(false);
            Hunting_Level_Easy.SetActive(false);
            Siege_Level_Easy.SetActive(false);

            Target_Level_Normal.SetActive(true);
            Hunting_Level_Normal.SetActive(true);
            Siege_Level_Normal.SetActive(true);

            Target_Level_Hard.SetActive(false);
            Hunting_Level_Hard.SetActive(false);
            Siege_Level_Hard.SetActive(false);
        }
        if(Set_M.Game_Level == 3)   //난이도 어려움
        {
            Target_Level_Easy.SetActive(false);
            Hunting_Level_Easy.SetActive(false);
            Siege_Level_Easy.SetActive(false);

            Target_Level_Normal.SetActive(false);
            Hunting_Level_Normal.SetActive(false);
            Siege_Level_Normal.SetActive(false);

            Target_Level_Hard.SetActive(true);
            Hunting_Level_Hard.SetActive(true);
            Siege_Level_Hard.SetActive(true);
        }
        if(Set_M.R_and_F == 1)  //사냥모드 - 현실
        {
            Hunting_Mod_R.SetActive(true);
            Hunting_Mod_F.SetActive(false);
        }
        if(Set_M.R_and_F == 2)  //사냥모드 - 판타지
        {
            Hunting_Mod_R.SetActive(false);
            Hunting_Mod_F.SetActive(true);
        }
        if(Set_M.Time_or_Limited == 1)  //공성전 - 시간
        {
            Siege_Time.SetActive(true);
            Siege_Limit.SetActive(false);
        }
        if(Set_M.Time_or_Limited == 2)  //공성전 - 한정된 적
        {
            Siege_Time.SetActive(false);
            Siege_Limit.SetActive(true);
        }
        if(Set_M.Set == 1)  // 타겟모드 - Set1 까지
        {
            Target_Set1.SetActive(true);
            Target_Set2.SetActive(false);
            Target_Set3.SetActive(false);
            Target_Set4.SetActive(false);
            Target_Set5.SetActive(false);
            Target_Set6.SetActive(false);
        }
        if(Set_M.Set == 2)  // 타겟모드 - Set2 까지
        {
            Target_Set1.SetActive(false);
            Target_Set2.SetActive(true);
            Target_Set3.SetActive(false);
            Target_Set4.SetActive(false);
            Target_Set5.SetActive(false);
            Target_Set6.SetActive(false);
        }
        if(Set_M.Set == 3)  // 타겟모드 - Set3 까지
        {
            Target_Set1.SetActive(false);
            Target_Set2.SetActive(false);
            Target_Set3.SetActive(true);
            Target_Set4.SetActive(false);
            Target_Set5.SetActive(false);
            Target_Set6.SetActive(false);
        }
        if(Set_M.Set == 4)  // 타겟모드 - Set4 까지
        {
            Target_Set1.SetActive(false);
            Target_Set2.SetActive(false);
            Target_Set3.SetActive(false);
            Target_Set4.SetActive(true);
            Target_Set5.SetActive(false);
            Target_Set6.SetActive(false);
        }
        if(Set_M.Set == 5)  // 타겟모드 - Set5 까지
        {
            Target_Set1.SetActive(false);
            Target_Set2.SetActive(false);
            Target_Set3.SetActive(false);
            Target_Set4.SetActive(false);
            Target_Set5.SetActive(true);
            Target_Set6.SetActive(false);
        }
        if(Set_M.Set == 6)  // 타겟모드 - Set6 까지
        {
            Target_Set1.SetActive(false);
            Target_Set2.SetActive(false);
            Target_Set3.SetActive(false);
            Target_Set4.SetActive(false);
            Target_Set5.SetActive(false);
            Target_Set6.SetActive(true);
        }

        if(UnLock_Arrow2 == false)  // 관통화살 잠김
        {
            Lock_Arrow2.SetActive(true);
        }
        if(UnLock_Arrow2 == true)  // 관통화살 풀림
        {
            Lock_Arrow2.SetActive(false);
        }
        if(UnLock_Arrow3 == false)  // 사냥화살 잠김
        {
            Lock_Arrow3.SetActive(true);
        }
        if(UnLock_Arrow3 == true)  // 사냥화살 풀림
        {
            Lock_Arrow3.SetActive(false);
        }

        if(Set_M.Using_Arrow_Temp == 1)     // 기본 화살 선택
        {
            Choose_Arrow.SetActive(true);
            Choose_Arrow2.SetActive(false);
            Choose_Arrow3.SetActive(false);
            Upgrade_Arr_Money.text = "1000";
        }
        if(Set_M.Using_Arrow_Temp == 2)     // 관통 화살 선택
        {
            Choose_Arrow.SetActive(false);
            Choose_Arrow2.SetActive(true);
            Choose_Arrow3.SetActive(false);
            Upgrade_Arr_Money.text = "2000";
        }
        if(Set_M.Using_Arrow_Temp == 3)     // 사냥 화살 선택
        {
            Choose_Arrow.SetActive(false);
            Choose_Arrow2.SetActive(false);
            Choose_Arrow3.SetActive(true);
            Upgrade_Arr_Money.text = "3000";
        }
        Money_Text.text = (Set_M.All_Money).ToString(); // 가진 돈 표시
        Unlock_Arrow_Setting(); // 화살 해금 정보 저장 및 연동
        Arr_Text(); // 화살 강화적용 능력치 표시
    }

    public void Arrow2_UnLock_Button()
    {
        if(Set_M.All_Money >= 5000)
        {
            Set_M.All_Money -= 5000;
            UnLock_Arrow2 = true;
            Set_M.UnLock_Arrow2 = true;
            Arrow2_UnLock_Text.SetActive(false);
            Choose_Button.SetActive(true);
            UnLock.PlayOneShot(UnLock_Arr);
            Upgrade.SetActive(true);
            Set_M.Using_Arrow_Temp = 2;
            Set_M.Using_Arrow = Set_M.Using_Arrow_Temp;
        }
        else if(Set_M.All_Money < 5000)
        {
            UnLock.PlayOneShot(Lock_Arr);
        }
    }
    public void Arrow3_UnLock_Button()
    {
        if(Set_M.All_Money >= 10000)
        {
            Set_M.All_Money -= 10000;
            UnLock_Arrow3 = true;
            Set_M.UnLock_Arrow3 = true;
            Arrow3_UnLock_Text.SetActive(false);
            Choose_Button.SetActive(true);
            UnLock.PlayOneShot(UnLock_Arr);
            Upgrade.SetActive(true);
            Set_M.Using_Arrow_Temp = 3;
            Set_M.Using_Arrow = Set_M.Using_Arrow_Temp;
        }
        else if(Set_M.All_Money < 10000)
        {
            UnLock.PlayOneShot(Lock_Arr);
        }
    }

    void Unlock_Arrow_Setting()
    {
        if(Set_M.UnLock_Arrow2 == true)
        {
            UnLock_Arrow2 = true;
        }
        else if(Set_M.UnLock_Arrow2 == false)
        {
            UnLock_Arrow2 = false;
        }

        if(Set_M.UnLock_Arrow3 == true)
        {
            UnLock_Arrow3 = true;
        }
        else if(Set_M.UnLock_Arrow3 == false)
        {
            UnLock_Arrow3 = false;
        }
    }

    public void Level_On_Off()
    {
        if(Set_M.Using_Arrow_Temp == 1)
        {
            Arr_Clear();
            Arr_Level();
        }
        if(Set_M.Using_Arrow_Temp == 2 && Set_M.UnLock_Arrow2 == true)
        {
            Arr_Clear();
            Arr_Level();
        }
        if(Set_M.Using_Arrow_Temp == 3 && Set_M.UnLock_Arrow3 == true)
        {
            Arr_Clear();
            Arr_Level();
        }
    }

    void Arr_Level()
    {
        if(Set_M.Using_Arrow_Temp == 1)
        {
            if(Set_M.Arrow_Level == 0)
            {
                Arr_Clear();
                Arr_Level_0.SetActive(true);
            }
            if(Set_M.Arrow_Level == 1)
            {
                Arr_Clear();
                Arr_Level_1.SetActive(true);
            }
            if(Set_M.Arrow_Level == 2)
            {
                Arr_Clear();
                Arr_Level_2.SetActive(true);
            }
            if(Set_M.Arrow_Level == 3)
            {
                Arr_Clear();
                Arr_Level_3.SetActive(true);
            }
            if(Set_M.Arrow_Level == 4)
            {
                Arr_Clear();
                Arr_Level_4.SetActive(true);
            }
            if(Set_M.Arrow_Level == 5)
            {
                Arr_Clear();
                Arr_Level_5.SetActive(true);
            }
            if(Set_M.Arrow_Level == 6)
            {
                Arr_Clear();
                Arr_Level_6.SetActive(true);
            }
            if(Set_M.Arrow_Level == 7)
            {
                Arr_Clear();
                Arr_Level_7.SetActive(true);
            }
            if(Set_M.Arrow_Level == 8)
            {
                Arr_Clear();
                Arr_Level_8.SetActive(true);
            }
            if(Set_M.Arrow_Level == 9)
            {
                Arr_Clear();
                Arr_Level_9.SetActive(true);
            }
            if(Set_M.Arrow_Level == 10)
            {
                Arr_Clear();
                Arr_Level_MAX.SetActive(true);
                Arr1_MAX_Effect.SetActive(true);
                Upgrade.SetActive(false);
            }
        }
        

        if(Set_M.Using_Arrow_Temp == 2)
        {
            if(Set_M.Arrow2_Level == 0)
            {
                Arr_Clear();
                Arr_Level_0.SetActive(true);
            }
            if(Set_M.Arrow2_Level == 1)
            {
                Arr_Clear();
                Arr_Level_1.SetActive(true);
            }
            if(Set_M.Arrow2_Level == 2)
            {
                Arr_Clear();
                Arr_Level_2.SetActive(true);
            }
            if(Set_M.Arrow2_Level == 3)
            {
                Arr_Clear();
                Arr_Level_3.SetActive(true);
            }
            if(Set_M.Arrow2_Level == 4)
            {
                Arr_Clear();
                Arr_Level_4.SetActive(true);
            }
            if(Set_M.Arrow2_Level == 5)
            {
                Arr_Clear();
                Arr_Level_5.SetActive(true);
            }
            if(Set_M.Arrow2_Level == 6)
            {
                Arr_Clear();
                Arr_Level_6.SetActive(true);
            }
            if(Set_M.Arrow2_Level == 7)
            {
                Arr_Clear();
                Arr_Level_7.SetActive(true);
            }
            if(Set_M.Arrow2_Level == 8)
            {
                Arr_Clear();
                Arr_Level_8.SetActive(true);
            }
            if(Set_M.Arrow2_Level == 9)
            {
                Arr_Clear();
                Arr_Level_9.SetActive(true);
            }
            if(Set_M.Arrow2_Level == 10)
            {
                Arr_Clear();
                Arr_Level_MAX.SetActive(true);
                Arr2_MAX_Effect.SetActive(true);
                Upgrade.SetActive(false);
            }
        }
        

        if(Set_M.Using_Arrow_Temp == 3)
        {
            if(Set_M.Arrow3_Level == 0)
            {
                Arr_Clear();
                Arr_Level_0.SetActive(true);
            }
            if(Set_M.Arrow3_Level == 1)
            {
                Arr_Clear();
                Arr_Level_1.SetActive(true);
            }
            if(Set_M.Arrow3_Level == 2)
            {
                Arr_Clear();
                Arr_Level_2.SetActive(true);
            }
            if(Set_M.Arrow3_Level == 3)
            {
                Arr_Clear();
                Arr_Level_3.SetActive(true);
            }
            if(Set_M.Arrow3_Level == 4)
            {
                Arr_Clear();
                Arr_Level_4.SetActive(true);
            }
            if(Set_M.Arrow3_Level == 5)
            {
                Arr_Clear();
                Arr_Level_5.SetActive(true);
            }
            if(Set_M.Arrow3_Level == 6)
            {
                Arr_Clear();
                Arr_Level_6.SetActive(true);
            }
            if(Set_M.Arrow3_Level == 7)
            {
                Arr_Clear();
                Arr_Level_7.SetActive(true);
            }
            if(Set_M.Arrow3_Level == 8)
            {
                Arr_Clear();
                Arr_Level_8.SetActive(true);
            }
            if(Set_M.Arrow3_Level == 9)
            {
                Arr_Clear();
                Arr_Level_9.SetActive(true);
            }
            if(Set_M.Arrow3_Level == 10)
            {
                Arr_Clear();
                Arr_Level_MAX.SetActive(true);
                Arr3_MAX_Effect.SetActive(true);
                Upgrade.SetActive(false);
            }
        }

    }

    void Arr_Clear()
    {
        Arr_Level_0.SetActive(false);
        Arr_Level_1.SetActive(false);
        Arr_Level_2.SetActive(false);
        Arr_Level_3.SetActive(false);
        Arr_Level_4.SetActive(false);
        Arr_Level_5.SetActive(false);
        Arr_Level_6.SetActive(false);
        Arr_Level_7.SetActive(false);
        Arr_Level_8.SetActive(false);
        Arr_Level_9.SetActive(false);
        Arr_Level_MAX.SetActive(false);

        Arr1_MAX_Effect.SetActive(false);
        Arr2_MAX_Effect.SetActive(false);
        Arr3_MAX_Effect.SetActive(false);
    }

    public void Upgrade_Arr_Button()
    {
        if(Set_M.Using_Arrow_Temp == 1)
        {
            if(Set_M.All_Money >= 1000 && Set_M.Arrow_Level < 10)
            {
                Set_M.All_Money -= 1000;
                Set_M.Arrow_Level += 1;
                UnLock.PlayOneShot(UnLock_Arr);
                Arr_Clear();
                Arr_Level();
                Set_M.Using_Arrow = Set_M.Using_Arrow_Temp;
            }
            else
            {
                UnLock.PlayOneShot(Lock_Arr);
            }
        }
        if(Set_M.Using_Arrow_Temp == 2)
        {
            if(Set_M.All_Money >= 2000 && Set_M.Arrow2_Level < 10)
            {
                Set_M.All_Money -= 2000;
                Set_M.Arrow2_Level += 1;
                UnLock.PlayOneShot(UnLock_Arr);
                Arr_Clear();
                Arr_Level();
                Set_M.Using_Arrow = Set_M.Using_Arrow_Temp;
            }
            else
            {
                UnLock.PlayOneShot(Lock_Arr);
            }
        }
        if(Set_M.Using_Arrow_Temp == 3)
        {
            if(Set_M.All_Money >= 3000 && Set_M.Arrow3_Level < 10)
            {
                Set_M.All_Money -= 3000;
                Set_M.Arrow3_Level += 1;
                UnLock.PlayOneShot(UnLock_Arr);
                Arr_Clear();
                Arr_Level();
                Set_M.Using_Arrow = Set_M.Using_Arrow_Temp;
            }
            else
            {
                UnLock.PlayOneShot(Lock_Arr);
            }
        }
    }

    void Arr_Text()
    {
        Arr_Dmg_Text.text = "데미지 : " + arr_Dmg.ToString();
        Arr2_Dmg_Text.text = "데미지 : " + arr2_Dmg.ToString();
        Arr3_Dmg_Text.text = "데미지 : " + arr3_Dmg.ToString();

        Arr_AP_Text.text = "(방어관통확률 : "+arr_AP.ToString()+" %)";
        Arr2_AP_Text.text = "(방어관통확률 : "+arr2_AP.ToString()+" %)";
        Arr3_AP_Text.text = "(방어관통확률 : "+arr3_AP.ToString()+" %)";

        arr_Dmg = 80 * (1+((float)Set_M.Arrow_Level/10));
        arr2_Dmg = 60 * (1+((float)Set_M.Arrow2_Level/10));
        arr3_Dmg = 120 * (1+((float)Set_M.Arrow3_Level/10));

        arr_AP = 25 * (1+((float)Set_M.Arrow_Level/10));
        arr2_AP = 50 * (1+((float)Set_M.Arrow2_Level/10));
        arr3_AP = 10 + (3 * (float)Set_M.Arrow3_Level);
    }
}
