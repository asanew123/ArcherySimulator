using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Spawn_Point_Manager : MonoBehaviour
{
    public GameObject G;
    Game_Manager G_M;

    public GameObject S_P1;
    public GameObject S_P2;
    public GameObject S_P3;

    public GameObject Lad1;
    public GameObject Lad2;
    public GameObject Lad3;

    int Game_Level = 0;


    // Start is called before the first frame update
    void Start()
    {
        G_M = G.transform.GetComponent<Game_Manager>();
    
        Game_Level_SP_and_Lad();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void Game_Level_SP_and_Lad()
    {
        Game_Level = G_M.Game_Level;
        if(Game_Level == 1)
        {
            All_GameObject_Off();

            S_P2.SetActive(true);
            Lad2.SetActive(true);
        }
        if(Game_Level == 2)
        {
            All_GameObject_Off();

            S_P1.SetActive(true);
            S_P3.SetActive(true);
            Lad1.SetActive(true);
            Lad3.SetActive(true);
        }
        if(Game_Level == 3)
        {
            All_GameObject_Off();

            All_GameObject_On();
        }
    }

    void All_GameObject_Off()
    {
        S_P1.SetActive(false);
        S_P2.SetActive(false);
        S_P3.SetActive(false);

        Lad1.SetActive(false);
        Lad2.SetActive(false);
        Lad3.SetActive(false);
    }

    void All_GameObject_On()
    {
        S_P1.SetActive(true);
        S_P2.SetActive(true);
        S_P3.SetActive(true);

        Lad1.SetActive(true);
        Lad2.SetActive(true);
        Lad3.SetActive(true);
    }

    

}
