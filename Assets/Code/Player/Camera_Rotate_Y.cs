using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Rotate_Y : MonoBehaviour
{

    [SerializeField]
    private float cameraRotationLimit;
    private float currentCameraRotationX = 0;

    [SerializeField]
    private float lookSensitivity;

    [SerializeField]
    private Camera theCamera;

    GameObject P;
    Player Play;

    // Start is called before the first frame update
    void Start()
    {
        theCamera = FindObjectOfType<Camera>();
        P = GameObject.Find("Player");
        Play = P.transform.GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Play.Option_Use == false && Play.Time_Stop == false)
        {
            CameraRotation();
        }    
    }

    public void CameraRotation()
    {
        float _xRotation = Input.GetAxisRaw("Mouse Y");
        float _cameraRotationX = _xRotation * lookSensitivity;
        currentCameraRotationX -= _cameraRotationX;
        currentCameraRotationX = Mathf.Clamp(currentCameraRotationX, -cameraRotationLimit, cameraRotationLimit);

        theCamera.transform.localEulerAngles = new Vector3(currentCameraRotationX, 0f, 0f);
    }
}
