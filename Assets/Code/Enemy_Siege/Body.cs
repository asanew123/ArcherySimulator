using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Body : MonoBehaviour
{
    public GameObject E;

    GameObject P;

    Player Play;

    Enemy_Siege Ene;

    GameObject shootM;

    Shot_Manager Shot_M;

    GameObject G;
    Game_Manager G_M;

    float Audio_Wait_Time = 0.25f; 
    bool Audio_Cool_Time = false;
    public AudioSource Siege_Body;
    public AudioClip Siege_ArmorPiecing_Hit;
    public AudioClip Siege_Hit;


    // Start is called before the first frame update
    void Start()
    {
        P = GameObject.Find("Player");
        Play = P.transform.GetComponent<Player>();
        Ene = E.transform.GetComponent<Enemy_Siege>();

        Shot_M = P.transform.GetComponent<Shot_Manager>();

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
            if(Ene.armor_B == true && Play.Armor_Piercing == false)
            {
                Debug.Log("갑옷 효과 : 화살 방어됨");
                Play.damage = 0;
                Destroy(other.gameObject);
            }
            if(Ene.armor_B == false || Play.Armor_Piercing == true)
            {
                Ene.HP -= Play.damage;
                if(Play.Armor_Piercing == false)
                {
                    Debug.Log("몸 피격");

                    Shot_M.All_False();
                    Shot_M.Shot_Text_Wait_Time = Shot_M.Wait_Time_Temp;
                    Shot_M.BS = 1;
                    Shot_M.Col = true;
                    if(Audio_Cool_Time == false)
                    {
                        Siege_Body.PlayOneShot(Siege_Hit);
                        Audio_Cool_Time = true;
                    }
                }
                if(Play.Armor_Piercing == true)
                {
                    Debug.Log("몸 피격(방어관통)");

                    Shot_M.All_False();
                    Shot_M.Shot_Text_Wait_Time = Shot_M.Wait_Time_Temp;
                    Shot_M.BS_AP = 1;
                    Shot_M.Col = true;
                    if(Audio_Cool_Time == false)
                    {
                        Siege_Body.PlayOneShot(Siege_ArmorPiecing_Hit);
                        Audio_Cool_Time = true;
                    }
                }
                Play.damage = 0;
                Play.Armor_Piercing = false;
            }
            
        }
        if(other.tag == "Arrow2" && Play.damage > 0)
        {
            G_M.HeadShot_Kill = false;
            if(Ene.armor_B == true && Play.Armor_Piercing == false)
            {
                Debug.Log("갑옷 효과 : 화살 방어됨");
                Play.damage = 0;
                Destroy(other.gameObject);
            }
            if(Ene.armor_B == false || Play.Armor_Piercing == true)
            {
                Ene.HP -= Play.damage;
                if(Play.Armor_Piercing == false)
                {
                    Debug.Log("몸 피격");
                    Shot_M.All_False();
                    Shot_M.Shot_Text_Wait_Time = Shot_M.Wait_Time_Temp;
                    Shot_M.BS = 1;
                    Shot_M.Col = true;
                    if(Audio_Cool_Time == false)
                    {
                        Siege_Body.PlayOneShot(Siege_Hit);
                        Audio_Cool_Time = true;
                    }
                }
                if(Play.Armor_Piercing == true)
                {
                    Debug.Log("몸 피격(방어관통)");

                    Shot_M.All_False();
                    Shot_M.Shot_Text_Wait_Time = Shot_M.Wait_Time_Temp;
                    Shot_M.BS_AP = 1;
                    Shot_M.Col = true;
                    if(Audio_Cool_Time == false)
                    {
                        Siege_Body.PlayOneShot(Siege_ArmorPiecing_Hit);
                        Audio_Cool_Time = true;
                    }
                }
                Play.damage = 0;
                Play.Armor_Piercing = false;
            }
            
        }
        if(other.tag == "Arrow3" && Play.damage > 0)
        {
            G_M.HeadShot_Kill = false;
            if(Ene.armor_B == true)
            {
                if(Play.Armor_Piercing == true)
                {
                    Ene.HP -= (Play.damage/2);
                    Debug.Log("몸 피격(방어관통 50%)");

                    Shot_M.All_False();
                    Shot_M.Shot_Text_Wait_Time = Shot_M.Wait_Time_Temp;
                    Shot_M.BS_AP_Hf = 1;
                    Shot_M.Col = true;

                    Play.damage = 0;
                    Play.Armor_Piercing = false;
                    if(Audio_Cool_Time == false)
                    {
                        Siege_Body.PlayOneShot(Siege_ArmorPiecing_Hit);
                        Audio_Cool_Time = true;
                    }
                }
                if(Play.Armor_Piercing == false)
                {
                    Debug.Log("갑옷 효과 : 화살 방어됨");
                    Play.damage = 0;
                    Destroy(other.gameObject);
                }
            }
            if(Ene.armor_B == false)
            {
                Ene.HP -= Play.damage;
                if(Play.Armor_Piercing == false)
                {
                    Debug.Log("몸 피격");

                    Shot_M.All_False();
                    Shot_M.Shot_Text_Wait_Time = Shot_M.Wait_Time_Temp;
                    Shot_M.BS = 1;
                    Shot_M.Col = true;
                    if(Audio_Cool_Time == false)
                    {
                        Siege_Body.PlayOneShot(Siege_Hit);
                        Audio_Cool_Time = true;
                    }
                }
                Play.damage = 0;
                Play.Armor_Piercing = false;
            }            
        }
    }
}
