using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonSoulEater_Leg : MonoBehaviour
{
    public GameObject DSE;

    GameObject P;

    Player Play;

    DragonSoulEater DragonSE;

    GameObject shootM;

    Shot_Manager Shot_M;

    GameObject G;
    Game_Manager G_M;

    float Audio_Wait_Time = 0.25f; 
    bool Audio_Cool_Time = false;
    public AudioSource Animal_Arm_and_Leg;
    public AudioClip Hit_Audio;

    // Start is called before the first frame update
    void Start()
    {
        P = GameObject.Find("Player");
        Play = P.transform.GetComponent<Player>();

        Shot_M = P.transform.GetComponent<Shot_Manager>();

        DragonSE = DSE.transform.GetComponent<DragonSoulEater>();

        G = GameObject.Find("Game_Manager");
        G_M = G.transform.GetComponent<Game_Manager>();

    }

    // Update is called once per frame
    void Update()
    {
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
            G_M.HeadShot_Kill = false;
            DragonSE.HP -= (Play.damage/2);
            DragonSE.Damage_Cheak = true;
            Shot_M.All_False();
            Shot_M.Shot_Text_Wait_Time = Shot_M.Wait_Time_Temp;
            Shot_M.AALS = 1;
            Shot_M.Col = true;
            Play.damage = 0;
            Debug.Log("드래곤 소울 이터 다리 피격");
            if(Audio_Cool_Time == false)
            {
                Animal_Arm_and_Leg.PlayOneShot(Hit_Audio);
                Audio_Cool_Time = true;
            }
            
        }
        if(other.tag == "Arrow2" && Play.damage > 0)
        {
            G_M.HeadShot_Kill = false;
            DragonSE.HP -= (Play.damage/2);
            DragonSE.Damage_Cheak = true;
            Shot_M.All_False();
            Shot_M.Shot_Text_Wait_Time = Shot_M.Wait_Time_Temp;
            Shot_M.AALS = 1;
            Shot_M.Col = true;
            Play.damage = 0;
            Debug.Log("드래곤 소울 이터 다리 피격");  
            if(Audio_Cool_Time == false)
            {
                Animal_Arm_and_Leg.PlayOneShot(Hit_Audio);
                Audio_Cool_Time = true;
            }
        }
        if(other.tag == "Arrow3" && Play.damage > 0)
        {
            G_M.HeadShot_Kill = false;
            DragonSE.HP -= (Play.damage/2);
            DragonSE.Damage_Cheak = true;
            Shot_M.All_False();
            Shot_M.Shot_Text_Wait_Time = Shot_M.Wait_Time_Temp;
            Shot_M.AALS = 1;
            Shot_M.Col = true;
            Play.damage = 0;
            Debug.Log("드래곤 소울 이터 다리 피격");  
            if(Audio_Cool_Time == false)
            {
                Animal_Arm_and_Leg.PlayOneShot(Hit_Audio);
                Audio_Cool_Time = true;
            }
        }
    }
}
