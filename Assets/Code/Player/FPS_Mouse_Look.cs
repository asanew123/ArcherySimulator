using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPS_Mouse_Look : MonoBehaviour {

    public int x_y = 0; // 0 이면 x , 1 이면 y

    public float r_x, r_y;
    float min_x, max_x;
    public float min_y, max_y;
    public float mouse_sence = 1.7f;
    Quaternion currentRotation;

    GameObject P;
    Player Play;

	void Start () 
    {
        currentRotation = transform.rotation; // 현재위치 기억
        P = GameObject.Find("Player");
        Play = P.transform.GetComponent<Player>();
	}
	void Update () 
    {
		if(Play.Option_Use == false && Play.Time_Stop == false)
        {
            Mouse_Ctrl();
        }
	}

    void Mouse_Ctrl()
    {
        if(x_y == 0) {
            r_x += Input.GetAxis("Mouse X") * mouse_sence; // 마우스가 움직이는값을 계속 저장해논다
            Quaternion temp = Quaternion.AngleAxis(r_x, Vector3.up);
            // Quaternion.AngleAxis(각도 , 죽) 죽을 기준으로 각도가 + 오른쪽   , 각도가 - 왼쪽으로 회전한다.
            transform.localRotation = currentRotation * temp;// 위에서 계산한값을 현재각도에 저장
        }
        if(x_y == 1) {
            r_y += Input.GetAxis("Mouse Y") * mouse_sence;
            Quaternion temp = Quaternion.AngleAxis(r_y, Vector3.left);
            transform.localRotation = currentRotation * temp;
        }
    }

    
}