using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Title_Arrow : MonoBehaviour
{
    public int speed = 20;

    public float Wait_Time = 3f; 

    public bool Play_Button_Use = false;

    public bool Trigger_Arrow = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Click_Button()
    {
        Play_Button_Use = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(Play_Button_Use == true)
        {
            transform.Translate(Vector3.down * speed * Time.deltaTime);
        }
        if(Trigger_Arrow == true)
        {
            if(Wait_Time > 0)
            {
                Wait_Time -= Time.deltaTime;
            }
            if(Wait_Time < 0)
            {
                SceneManager.LoadScene(1);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        speed = 0;
        Trigger_Arrow = true;
    }

}