using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn_Point_Manager_Hunting : MonoBehaviour
{
    public GameObject SetM;
    Setting_Manager Set_M;


    public GameObject Right_SpawnPoint_10m;
    public GameObject Left_SpawnPoint_10m;

    public GameObject Right_SpawnPoint_30m;
    public GameObject Left_SpawnPoint_30m;

    public GameObject Right_SpawnPoint_50m;
    public GameObject Left_SpawnPoint_50m;

    public int Enemy_Count = 0;
    public int Game_Level = 2;

    void Awake()
    {
        SetM = GameObject.Find("Setting_Manager");
        Set_M = SetM.transform.GetComponent<Setting_Manager>();
    }
    // Start is called before the first frame update
    void Start()
    {
        Game_Level = Set_M.Game_Level;
        if(Game_Level == 1)
        {
            All_Off();
            Right_SpawnPoint_10m.SetActive(true);
            Left_SpawnPoint_10m.SetActive(true);
        }
        if(Game_Level == 2)
        {
            All_Off();
            Right_SpawnPoint_30m.SetActive(true);
            Left_SpawnPoint_30m.SetActive(true);
        }
        if(Game_Level == 3)
        {
            All_Off();
            Right_SpawnPoint_50m.SetActive(true);
            Left_SpawnPoint_50m.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    void All_Off()
    {
        Right_SpawnPoint_10m.SetActive(false);
        Left_SpawnPoint_10m.SetActive(false);

        Right_SpawnPoint_30m.SetActive(false);
        Left_SpawnPoint_30m.SetActive(false);

        Right_SpawnPoint_50m.SetActive(false);
        Left_SpawnPoint_50m.SetActive(false);
    }
}
