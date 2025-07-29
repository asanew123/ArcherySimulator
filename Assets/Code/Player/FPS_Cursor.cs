using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPS_Cursor : MonoBehaviour {

    GameObject G;
    Game_Manager G_M;

    GameObject SetM;
    Setting_Manager Set_M;

    public bool Game_Over = false;
    public bool Game_Clear = false;

    void Awake()
    {
        if(Game_Over == false && Game_Clear == false)
        {
            G = GameObject.Find("Game_Manager");
            G_M = G.transform.GetComponent<Game_Manager>();
        }
        SetM = GameObject.Find("Setting_Manager");
        Set_M = SetM.transform.GetComponent<Setting_Manager>();
    }
	// Use this for initialization
	void Start () {
        Cursor.lockState = CursorLockMode.Locked;
        if(Set_M.Game_Over == true || Set_M.Game_Clear == true)
        {
            if(Cursor.lockState == CursorLockMode.Locked)
            {
                Cursor.lockState = CursorLockMode.None;
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Tab) || Input.GetKeyDown(KeyCode.Escape))
        {
            if(Cursor.lockState == CursorLockMode.Locked)
            {
                Cursor.lockState = CursorLockMode.None;
            }
            else if(Cursor.lockState == CursorLockMode.None)
            {
                Cursor.lockState = CursorLockMode.Locked;
            }
        }
        if(Set_M.Game_Set == true)
        {
            if(Cursor.lockState == CursorLockMode.Locked)
            {
                Cursor.lockState = CursorLockMode.None;
            }
        }
	}
}
