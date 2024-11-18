using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using System.Runtime.CompilerServices;
using UnityEditor.UIElements;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class Moviment : MonoBehaviour
{
    public Camera playerCamera; 
    public float spd = 6f;
    public float runSpd = 8f;
    public float sensitivity = 2f;

    public float lookXLimit = 45f;

    public float jumpPower = 40f;

    public float gravity = 10f;
    float rotX = 0;
    UnityEngine.Vector3 moveDir = UnityEngine.Vector3.zero;

    public bool isMove = true;
    public bool isAiming = false;
    CharacterController characterController;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;

    }


    private void aim(){
        
    }


    void Update(){ 


        UnityEngine.Vector3 forward = transform.TransformDirection(UnityEngine.Vector3.forward);
        UnityEngine.Vector3 right = transform.TransformDirection(UnityEngine.Vector3.right);



        bool isRunning = Input.GetKey(KeyCode.LeftShift);

        float curSpdX = isMove ? (isRunning ? runSpd : spd) * Input.GetAxis("Vertical") : 0;
        float curSpdY = isMove ? (isRunning ? runSpd : spd) * Input.GetAxis("Horizontal") : 0;
        float moveDirY = moveDir.y;
        moveDir = (forward * curSpdX) + (right * curSpdY);        



        if(Input.GetButton("Jump") && isMove && characterController.isGrounded ){
            moveDir.y = jumpPower;
        } else {
            moveDir.y = moveDirY;
        }

        if (!characterController.isGrounded){
            moveDir.y -= gravity * Time.deltaTime;
        }

        characterController.Move(moveDir * Time.deltaTime);

        if(isMove){
            rotX -= Input.GetAxis("Mouse Y") * sensitivity;
            rotX = Mathf.Clamp(rotX, -lookXLimit, lookXLimit);
            playerCamera.transform.localRotation = UnityEngine.Quaternion.Euler(rotX, 0 , 0);
            transform.rotation *= UnityEngine.Quaternion.Euler(0, Input.GetAxis("Mouse X") * sensitivity, 0);
        }



        if(Input.GetButtonDown("Fire2")){
            isAiming = true;
            Debug.Log("aiming");
            aim();
        }




    }

}