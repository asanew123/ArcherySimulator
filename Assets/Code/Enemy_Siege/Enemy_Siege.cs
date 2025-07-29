using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy_Siege : MonoBehaviour
{
    public Animator Enemy_anim;
    public bool Ladder_Use = false;
    public bool Attack_Use = false;
    public bool Ladder_Not = false;
    public float Ladder_Speed = 1f;
    Rigidbody rb;

    public GameObject SPM;
    Spawn_Point_Manager S_P_M;

    bool Count_Cheak = false;

    public GameObject L_N;      // 사다리 오르는걸 막는 오브젝트    같은 라인에 사다리에 중복되서 오르는걸 막아준다.

    public float HP = 50;              // 기본 체력   Easy : 50       Normal : 100        Hard : 150
    float Max_HP;
    public int Damage = 10;                  // 기본 데미지      맨손 데미지 Easy : 5    Normal : 10     Hard : 15;      무기 장착시 데미지 2배
    public float Damage_Time = 0.5f;    // 데미지 딜레이;

    public int Game_Level = 2;  // 1 = Easy     2 = Normal      3 = Hard

    public GameObject Weapon;
    public GameObject Armor;
    public GameObject Helm;

    public GameObject P;

    Player Play;

    public bool weapon_B = false;   //무기  적의 데미지를 증가 시켜준다.    화살로 맞춰서 무장해제 시킬수 있다.
    public bool helm_B = false;     //투구  머리에 날라오는 화살을 1회 막아준다.    단 화살이 방어관통(확률성)에 성공하거나 투구로 보호 안된부분을 맞출경우 투구의 효과를 무효화 한다.
    public bool armor_B = false;    //갑옷  몸통에 날라오는 화살을 1회 막아준다.    단 팔/다리 부분은 보호하지 못한다.
    int weapon_E = 0;
    int helm_E = 0;
    int armor_E = 0;

    public bool Death = false;
    float death_time = 0.2f;

    public int Using_Equipment = 0; // 착용한 장비 갯수
    public int Equipment_Change = 0;  // 변화 감지

    float Helm_P = 25;  // 투구 장착 확률    쉬움 : 25%  보통 : 50%  어려움 : 75%
    float Armor_P = 25; // 갑옷 장착 확률    쉬움 : 25%  보통 : 50%  어려움 : 75%
    float Sword_P = 25; // 검 장착 확률    쉬움 : 25%  보통 : 50%  어려움 : 75%

    float Helm_Per;
    float Armor_Per;
    float Sword_Per;

    public Image HP_Bar;
    public Text HP_Text; 

    public float Attack_Delay = 1.5f;
    public float Attack_Wait_Time = 0;
    public bool Attack_Ready = false;

    public GameObject GM;
    Game_Manager G_M;

    bool Kill_Cheak = false;

    float Audio_Wait_Time = 0.25f; 
    bool Audio_Cool_Time = false;
    public AudioSource Siege_Enemy;
    public AudioClip Armor_Break;
    public AudioClip Siege_Enemy_Attack_Punch;
    public AudioClip Siege_Enemy_Attack_Weapon;

    // Start is called before the first frame update
    void Start()
    {
        GM = GameObject.Find("Game_Manager");
        G_M = GM.transform.GetComponent<Game_Manager>();
        Game_Level = G_M.Game_Level;

        if(G_M.Game_Mode == 3)
        {
            SPM = GameObject.Find("Spawn_Point_Manager");
            S_P_M = SPM.transform.GetComponent<Spawn_Point_Manager>();
        }

        P = GameObject.Find("Player");
        Play = P.transform.GetComponent<Player>();
        rb = GetComponent<Rigidbody>();

        Helm_P *= Game_Level;
        Helm_Per = Random.Range(0,100);

        if(Helm_P >= Helm_Per)
        {
            helm_B = true;
        }
        else if(Helm_P < Helm_Per)
        {
            helm_B = false;
        }
        
        Armor_P *= Game_Level;
        Armor_Per = Random.Range(0,100);

        if(Armor_P >= Armor_Per)
        {
            armor_B = true;
        }
        else if(Armor_P < Armor_Per)
        {
            armor_B = false;
        }
        
        Sword_P *= Game_Level;
        Sword_Per = Random.Range(0,100);

        if(Sword_P >= Sword_Per)
        {
            weapon_B = true;
        }
        else if(Sword_P < Sword_Per)
        {
            weapon_B = false;
        }

        Weapon_Damage();
        L_N.SetActive(false);
        HP = (50 * Game_Level);
        Max_HP = HP;
        Equipment();
        Using_Equipment = weapon_E + helm_E + armor_E;
        Equipment_Change = Using_Equipment;

        Attack_Wait_Time = Attack_Delay;


    }

    // Update is called once per frame
    void Update()
    {
        if(Play.Time_Stop == false)
        {
            Enemy_anim.SetBool("Time_Stop", false);
            Enemy_Ctrl();
        }
        else if(Play.Time_Stop == true)
        {
            Enemy_anim.SetBool("Time_Stop", true);
        }
    }

    void Enemy_Ctrl()
    {
        Enemy_UI();
        if(Death == false)
        {
            if(Ladder_Not == true)
            {
                Enemy_anim.SetBool("Ladder_Not", true);
            }
            if(Ladder_Use == true && Ladder_Not == false)
            {
                Enemy_anim.SetBool("Ladder_Not", false);
                transform.Translate(Vector3.up * Ladder_Speed * Time.deltaTime);
                transform.Translate(Vector3.forward * (Ladder_Speed/2) * Time.deltaTime);
                L_N.SetActive(true);
            }
            if(Attack_Use == true)
            {
                Ladder_Speed = 0;
                rb.constraints = RigidbodyConstraints.FreezeAll;
                if(Attack_Wait_Time > 0)
                {
                    Attack_Wait_Time -= Time.deltaTime;
                }
                if(Attack_Wait_Time < 0)
                {
                    Play.HP -= Damage;
                    Attack_Wait_Time = Attack_Delay;
                    if(weapon_B == true)
                    {
                        Siege_Enemy.PlayOneShot(Siege_Enemy_Attack_Weapon);
                    }
                    if(weapon_B == false)
                    {
                        Siege_Enemy.PlayOneShot(Siege_Enemy_Attack_Punch);
                    }
                }
            }
            Weapon_and_Armor();
            Weapon_and_Armor2();
            Weapon_Damage();
        }
        Die();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Walk Zone")
        {
            Enemy_anim.SetBool("Walk", true);
        }
        if(other.tag == "Ladder Zone")
        {
            Enemy_anim.SetBool("Ladder", true);
            Ladder_Use = true;
            rb.useGravity = false;
        }
        if(other.tag == "Attack Zone")
        {
            Enemy_anim.SetBool("Attack", true);
            Attack_Use = true;
        }
        if(other.tag == "Ladder_Not")
        {
            Ladder_Not = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Ladder_Not")
        {
            Ladder_Not = false;
        }
    }

    void Weapon_and_Armor()
    {
        if(Weapon != null)
        {
            if(weapon_B == true)
            {
                Weapon.SetActive(true);
            }
            else if(weapon_B == false)
            {
                Weapon.SetActive(false);
            }
        }
        if(Helm != null)
        {
            if(helm_B == true)
            {
                Helm.SetActive(true);
            }
            else if(helm_B == false)
            {
                Helm.SetActive(false);
            }
        }
        if(Armor != null)
        {
            if(armor_B == true)
            {
                Armor.SetActive(true);
            }
            else if(armor_B == false)
            {
                Armor.SetActive(false);
            }
        }
    }

    void Weapon_and_Armor2()
    {
        if(Weapon == null)
        {
            weapon_B = false;
            Using_Equipment -= 1;
        }
        if(Helm == null)
        {
            helm_B = false;
            Using_Equipment -= 1;
            if(Audio_Cool_Time == false)
            {
                Siege_Enemy.PlayOneShot(Armor_Break);
                Audio_Cool_Time = true;
            }
        }
        if(Armor == null)
        {
            armor_B = false;
            Using_Equipment -= 1;
            if(Audio_Cool_Time == false)
            {
                Siege_Enemy.PlayOneShot(Armor_Break);
                Audio_Cool_Time = true;
            }
        }

    }

    void Weapon_Damage()
    {
        if(weapon_B == true)
        {
            Damage = 5 * Game_Level * 2;
        }
        else if(weapon_B == false)
        {
            Damage = 5 * Game_Level;
        }
    }


    void Die()
    {
        if(HP <= 0)
        {
            HP = 0;
            Death = true;
            Enemy_anim.SetBool("Die", true);
            if(G_M.Time_or_Limited == 2 && Count_Cheak == false)  // 한정된 적 모드 일때
            {
                G_M.Enemy_Text -= 1;
                Count_Cheak = true;
            }
            
        }
        if(Death == true && death_time > 0)
        {
            death_time -= Time.deltaTime;
        }
        if(death_time < 0)
        {
            death_time = 0;
            Enemy_anim.SetBool("Die_Cheak", true);
            rb.isKinematic = true;
            Destroy(this.gameObject, 2f);
            G_M.Kill_Count += 1;
            G_M.Kill_Cheak = true;
            G_M.Money_Wait_Time = G_M.Wait_Time_Temp;
            if(Game_Level == 1)
            {
                if(G_M.HeadShot_Kill == true)
                {
                    G_M.Money += 20;
                    G_M.Add_Money = 20;
                }
                else if(G_M.HeadShot_Kill == false)
                {
                    G_M.Money += 10;
                    G_M.Add_Money = 10;
                }
            }
            if(Game_Level == 2)
            {
                if(G_M.HeadShot_Kill == true)
                {
                    G_M.Money += 60;
                    G_M.Add_Money = 60;
                }
                else if(G_M.HeadShot_Kill == false)
                {
                    G_M.Money += 30;
                    G_M.Add_Money = 30;
                }
            }
            if(Game_Level == 3)
            {
                if(G_M.HeadShot_Kill == true)
                {
                    G_M.Money += 100;
                    G_M.Add_Money = 100;
                }
                else if(G_M.HeadShot_Kill == false)
                {
                    G_M.Money += 50;
                    G_M.Add_Money = 50;
                }
            }
        }
    }

    void Enemy_UI()
    {
        HP_Bar.fillAmount = HP * 0.02f / Game_Level;
        HP_Text.text = Mathf.Round(HP).ToString() + " / " + Max_HP.ToString();
    }

    void Equipment()
    {
        if(weapon_B == true)
        {
            weapon_E = 1;
        }
        if(armor_B == true)
        {
            armor_E = 1;
        }
        if(helm_B == true)
        {
            helm_E = 1;
        }
    }

    void Enemy_Attack()
    {
        if(Attack_Ready == true)
        {
            Play.HP -= Damage;
            Attack_Wait_Time = Attack_Delay;
            Attack_Ready = false;
        }
        if(Attack_Wait_Time > 0)
        {
            Attack_Wait_Time -= Time.deltaTime;
        }
        if(Attack_Wait_Time < 0)
        {
            Attack_Wait_Time = 0;
            Attack_Ready = true;
        }
    }
    
}
