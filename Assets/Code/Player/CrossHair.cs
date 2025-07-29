using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossHair : MonoBehaviour
{
    public Animator animator;
    public GameObject player;

    Player pl;
    float C_S;


    // Start is called before the first frame update
    void Start()
    {
        pl = player.GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if(pl.Charge <= 100)
        {
            animator.SetLayerWeight(1, (pl.Charge/100));
        }
        
    }
}
