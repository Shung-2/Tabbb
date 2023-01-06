using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    private Rigidbody myRigid; // 리지드바디
    private float currentCameraRoationX = 0f; // 카메라의 현재 회전 각도

    [SerializeField] private float lookSensitivity; // 마우스 감도
    [SerializeField] private float cameraRotationLimit; // 카메라 회전 제한 각도
    [SerializeField] private Camera theCamera; // 카메라 

    // Start is called before the first frame update
    void Start()
    {
        myRigid = GetComponent<Rigidbody>(); // 리지드바디 컴포넌트를 가져온다.
    }

    // Update is called once per frame
    void Update()
    {
        CameraRotation(); // 상하 카메라 회전
        CharacterRoation(); // 좌우 캐릭터 회전
    }

    private void CameraRotation()
    {
        // 상하 카메라 회전
        float _xRotation = Input.GetAxisRaw("Mouse Y"); // 마우스의 좌우 이동
        float _cameraRotationX = _xRotation * lookSensitivity; // 카메라의 회전 각도
        currentCameraRoationX -= _cameraRotationX; // 카메라의 현재 회전 각도에 카메라의 회전 각도를 더한다.
        currentCameraRoationX = Mathf.Clamp(currentCameraRoationX, -cameraRotationLimit, cameraRotationLimit); // 카메라의 현재 회전 각도를 제한한다.

        theCamera.transform.localEulerAngles = new Vector3(currentCameraRoationX, 0f, 0f); // 카메라의 회전 각도를 설정한다.
    }

    private void CharacterRoation()
    {
        float _yRotation = Input.GetAxisRaw("Mouse X"); // 마우스의 상하 이동
        Vector3 _characterRoationY = new Vector3(0f, _yRotation, 0f) * lookSensitivity; // 캐릭터의 회전 각도

        myRigid.MoveRotation(myRigid.rotation * Quaternion.Euler(_characterRoationY)); // 리지드바디의 회전 각도를 설정한다.
    }
}
