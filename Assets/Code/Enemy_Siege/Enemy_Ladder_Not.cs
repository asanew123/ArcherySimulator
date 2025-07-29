using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Ladder_Not : MonoBehaviour
{
    public GameObject E;
    Enemy_Siege Ene;
    // Start is called before the first frame update
    void Start()
    {
        Ene = E.transform.GetComponent<Enemy_Siege>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Ene.Death == true)
        {
            transform.position = new Vector3(0,0,0);
        }
    }
}
