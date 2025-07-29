using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot_Manager : MonoBehaviour
{
    public GameObject HeadShot;
    public GameObject HeadShot_AP;
    public GameObject HeadShot_AP_Half;
    public GameObject HeadShot_HGS;
    
    public GameObject BodyShot;
    public GameObject BodyShot_AP;
    public GameObject BodyShot_AP_Half;

    public GameObject ArmAndLegShot;

    public int HS = 0;
    public int HS_AP = 0;
    public int HS_AP_Hf = 0;
    public int HS_HGS = 0;

    public int BS = 0;
    public int BS_AP = 0;
    public int BS_AP_Hf = 0;

    public int AALS = 0;

    public float Shot_Text_Wait_Time = 2.5f;
    public float Wait_Time_Temp = 0;

    public bool Col;
    
    // Start is called before the first frame update
    void Start()
    {
        All_Off();
        Wait_Time_Temp = Shot_Text_Wait_Time;
    }

    // Update is called once per frame
    void Update()
    {
        Show_Shot_Text();
    }

    void All_Off()
    {
        HeadShot.SetActive(false);
        HeadShot_AP.SetActive(false);
        HeadShot_AP_Half.SetActive(false);
        HeadShot_HGS.SetActive(false);

        BodyShot.SetActive(false);
        BodyShot_AP.SetActive(false);

        ArmAndLegShot.SetActive(false);
    }

    public void All_False()
    {
        HS = 0;
        HS_AP = 0;
        HS_AP_Hf = 0;
        HS_HGS = 0;

        BS = 0;
        BS_AP = 0;
        BS_AP_Hf = 0;

        AALS = 0;
    }

    void Show_Shot_Text()
    {
        if(Col == true)
        {
            if(Shot_Text_Wait_Time > 0)
            {
                Shot_Text_Wait_Time -= Time.deltaTime;
                All_SetActive();
            }
            if(Shot_Text_Wait_Time < 0)
            {
                Col = false;
                All_Off();
                All_False();
                Shot_Text_Wait_Time = Wait_Time_Temp;
            }
        }
    }

    void All_SetActive()
    {
        if(HS == 1)
        {
            HeadShot.SetActive(true);
        }
        if(HS_AP == 1)
        {
            HeadShot_AP.SetActive(true);
        }
        if(HS_AP_Hf == 1)
        {
            HeadShot_AP_Half.SetActive(true);
        }
        if(HS_HGS == 1)
        {
            HeadShot_HGS.SetActive(true);
        }
        if(BS == 1)
        {
            BodyShot.SetActive(true);
        }
        if(BS_AP == 1)
        {
            BodyShot_AP.SetActive(true);
        }
        if(BS_AP_Hf == 1)
        {
            BodyShot_AP_Half.SetActive(true);
        }
        if(AALS == 1)
        {
            ArmAndLegShot.SetActive(true);
        }
        
        if(HS == 0)
        {
            HeadShot.SetActive(false);
        }
        if(HS_AP == 0)
        {
            HeadShot_AP.SetActive(false);
        }
        if(HS_AP_Hf == 0)
        {
            HeadShot_AP_Half.SetActive(false);
        }
        if(HS_HGS == 0)
        {
            HeadShot_HGS.SetActive(false);
        }
        if(BS == 0)
        {
            BodyShot.SetActive(false);
        }
        if(BS_AP == 0)
        {
            BodyShot_AP.SetActive(false);
        }
        if(BS_AP_Hf == 0)
        {
            BodyShot_AP_Half.SetActive(false);
        }
        if(AALS == 0)
        {
            ArmAndLegShot.SetActive(false);
        }

    }
}
