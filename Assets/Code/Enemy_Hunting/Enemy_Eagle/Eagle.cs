using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class Eagle : MonoBehaviour
{
    public float Walk_Speed = 4f;
    public float Run_Speed = 10f;

    public Transform target;
    UnityEngine.AI.NavMeshAgent nav;

    public Animator Eagle_anim;
    public bool Run_Use = false;
    public bool Attack_Use = false;
    Rigidbody rb;

    public GameObject SPM;
    Spawn_Point_Manager S_P_M;

    bool Count_Cheak = false;

    public float HP = 60;              // 기본 체력   Easy : 40       Normal : 50        Hard : 60
    public float Max_HP;
    public int Damage = 15;                  // 기본 데미지      Easy : 5    Normal : 10     Hard : 15;     
    public float Damage_Time = 0.5f;    // 데미지 딜레이;

    public int Game_Level = 2;  // 1 = Easy     2 = Normal      3 = Hard

    public GameObject P;

    Player Play;

    public bool Death = false;
    float death_time = 0.2f;

    public Image HP_Bar;
    public Text HP_Text; 

    public float Attack_Delay = 1.5f;
    public float Attack_Wait_Time = 0;
    public bool Attack_Ready = false;

    public GameObject GM;
    Game_Manager G_M;

    bool Kill_Cheak = false;
    public bool Damage_Cheak = false;
    public bool OneHitShot_Kill_Cheak = false;

    public AudioSource Animal;
    public AudioClip Attack_Audio;
    public AudioClip Die_Audio;

    void Awake()
    {
        nav = GetComponent<UnityEngine.AI.NavMeshAgent>();
    }
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

        target = P.transform;

        Damage = (Game_Level * 5);

        HP = 30 + (10 * Game_Level);   
        Max_HP = HP;

        

        Attack_Wait_Time = Attack_Delay;
    }

    // Update is called once per frame
    void Update()
    {
        if(Play.Time_Stop == false)
        {
            Eagle_anim.SetBool("Time_Stop", false);
            Enemy_Ctrl();
        }
        else if(Play.Time_Stop == true)
        {
            Eagle_anim.SetBool("Time_Stop", true);
        }
    }

    void Enemy_Ctrl()
    {
        Enemy_UI();
        if(Death == false)
        {
            if(Attack_Use == true)
            {
                Eagle_anim.SetBool("Attack", true);
                rb.constraints = RigidbodyConstraints.FreezeAll;
                if(Attack_Wait_Time > 0)
                {
                    Attack_Wait_Time -= Time.deltaTime;
                }
                if(Attack_Wait_Time < 0)
                {
                    Play.HP -= Damage;
                    Animal.PlayOneShot(Attack_Audio);
                    Attack_Wait_Time = Attack_Delay;
                }
            }
            else if(Attack_Use == false)
            {
                if(Damage_Cheak == true)
                {
                    transform.Translate(Vector3.forward * Run_Speed * Time.deltaTime);
                    Eagle_anim.SetBool("Fast_Fly", true);
                    nav.SetDestination(target.position);
                }
                else if(Damage_Cheak == false)
                {
                    transform.Translate(Vector3.forward * Walk_Speed * Time.deltaTime);
                }
            }
        }
        Die();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Walk Zone")
        {
            Eagle_anim.SetBool("Walk", true);
        }
        if(other.tag == "Attack Zone")
        {
            Eagle_anim.SetBool("Attack", true);
            Attack_Use = true;
            nav.enabled = false;
        }
        if(other.tag == "Delete Zone")
        {
            Destroy(this.gameObject, 2f);
        }
    }


    void Die()
    {
        if(HP <= 0)
        {
            HP = 0;
            Death = true;
            Eagle_anim.SetBool("Die", true);
            nav.enabled = false;
        }
        if(Death == true && death_time > 0)
        {
            death_time -= Time.deltaTime;
        }
        if(death_time < 0)
        {
            death_time = 0;
            Eagle_anim.SetBool("Die_Cheak", true);
            rb.isKinematic = true;
            Destroy(this.gameObject, 2f);
            G_M.Kill_Count += 1;
            G_M.Kill_Cheak = true;
            G_M.Money_Wait_Time = G_M.Wait_Time_Temp;
            G_M.Hunting_Clear();
            G_M.Eagle_Kill = true;
            Animal.PlayOneShot(Die_Audio);
            if(Game_Level == 1)
            {
                if(G_M.HeadShot_Kill == true && G_M.OneHitShot_Kill == true)    // 헤드샷 + 즉사 보너스 x4
                {
                    G_M.Money += 160;
                    G_M.Add_Money = 160;
                    G_M.Animal_Kill_Money = 40;
                }
                if(G_M.HeadShot_Kill == true && G_M.OneHitShot_Kill == false) // 헤드샷만 맞춘경우 x2
                {
                    G_M.Money += 80;
                    G_M.Add_Money = 80;
                    G_M.Animal_Kill_Money = 40;
                }
                if(G_M.HeadShot_Kill == false && G_M.OneHitShot_Kill == true) // 즉사만 시킨경우 x2
                {
                    G_M.Money += 80;
                    G_M.Add_Money = 80;
                    G_M.Animal_Kill_Money = 40;
                }
                else if(G_M.HeadShot_Kill == false && G_M.OneHitShot_Kill == false)  // 그냥 죽였을때
                {
                    G_M.Money += 40;
                    G_M.Add_Money = 40;
                    G_M.Animal_Kill_Money = 40;
                }
            }
            if(Game_Level == 2)
            {
                if(G_M.HeadShot_Kill == true && G_M.OneHitShot_Kill == true)    // 헤드샷 + 즉사 보너스 x4
                {
                    G_M.Money += 320;
                    G_M.Add_Money = 320;
                    G_M.Animal_Kill_Money = 80;
                }
                if(G_M.HeadShot_Kill == true && G_M.OneHitShot_Kill == false) // 헤드샷만 맞춘경우 x2
                {
                    G_M.Money += 160;
                    G_M.Add_Money = 160;
                    G_M.Animal_Kill_Money = 80;
                }
                if(G_M.HeadShot_Kill == false && G_M.OneHitShot_Kill == true) // 즉사만 시킨경우 x2
                {
                    G_M.Money += 160;
                    G_M.Add_Money = 160;
                    G_M.Animal_Kill_Money = 80;
                }
                else if(G_M.HeadShot_Kill == false && G_M.OneHitShot_Kill == false)  // 그냥 죽였을때
                {
                    G_M.Money += 80;
                    G_M.Add_Money = 80;
                    G_M.Animal_Kill_Money = 80;
                }
            }
            if(Game_Level == 3)
            {
                if(G_M.HeadShot_Kill == true && G_M.OneHitShot_Kill == true)    // 헤드샷 + 즉사 보너스 x4
                {
                    G_M.Money += 480;
                    G_M.Add_Money = 480;
                    G_M.Animal_Kill_Money = 120;
                }
                if(G_M.HeadShot_Kill == true && G_M.OneHitShot_Kill == false) // 헤드샷만 맞춘경우 x2
                {
                    G_M.Money += 240;
                    G_M.Add_Money = 240;
                    G_M.Animal_Kill_Money = 120;
                }
                if(G_M.HeadShot_Kill == false && G_M.OneHitShot_Kill == true) // 즉사만 시킨경우 x2
                {
                    G_M.Money += 240;
                    G_M.Add_Money = 240;
                    G_M.Animal_Kill_Money = 120;
                }
                else if(G_M.HeadShot_Kill == false && G_M.OneHitShot_Kill == false)  // 그냥 죽였을때
                {
                    G_M.Money += 120;
                    G_M.Add_Money = 120;
                    G_M.Animal_Kill_Money = 120;
                }
            }
        }
        if(OneHitShot_Kill_Cheak == false)
        {
            if(Max_HP != HP)
            {
                if(HP > 0)
                {
                    G_M.OneHitShot_Kill = false;
                    OneHitShot_Kill_Cheak = true;
                }
                else if(HP <= 0)
                {
                    G_M.OneHitShot_Kill = true;
                    OneHitShot_Kill_Cheak = true;
                } 
            }
        }
    }

    void Enemy_UI()
    {
        HP_Bar.fillAmount = HP / (30 + (10 * Game_Level)); 
        HP_Text.text = Mathf.Round(HP).ToString() + " / " + Max_HP.ToString();
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
