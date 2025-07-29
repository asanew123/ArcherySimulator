using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy_Siege_UI : MonoBehaviour
{
    public GameObject enemy;

    public GameObject Helm_Image;
    public GameObject Armor_Image;
    public GameObject Sword_Image;

    //public RectTransform rectTransform;

    public int Buff = 0;    // n번 버프  버프 종류구별
    public int Item = 0;    // 활성화 아이템의 종류     1 = 검  2 = 갑옷    3 = 투구

    Enemy_Siege Ene;
    
    // Start is called before the first frame update
    void Start()
    {
        Ene = enemy.transform.GetComponent<Enemy_Siege>();
        //rectTransform = this.GetComponent<RectTransform>();
        All_UI_Off();
        Buff_On_and_Off();
    }

    // Update is called once per frame
    void Update()
    {
        if(Ene.Equipment_Change != Ene.Using_Equipment)
        {
            Buff_On_and_Off();
            Ene.Equipment_Change = Ene.Using_Equipment;
        }   
    }

    void All_UI_Off()
    {
        Helm_Image.SetActive(false);
        Armor_Image.SetActive(false);
        Sword_Image.SetActive(false);
    }

    void Buff_On_and_Off()
    {
        if(Ene.weapon_B == true)
        {
            Sword_Image.SetActive(true);
        }
        else if(Ene.weapon_B == false)
        {
            Sword_Image.SetActive(false);
        }
        if(Ene.armor_B == true)
        {
            Armor_Image.SetActive(true);
        }
        else if(Ene.armor_B == false)
        {
            Armor_Image.SetActive(false);
        }
        if(Ene.helm_B == true)
        {
            Helm_Image.SetActive(true);
        }
        else if(Ene.helm_B == false)
        {
            Helm_Image.SetActive(false);
        }
    }
}
