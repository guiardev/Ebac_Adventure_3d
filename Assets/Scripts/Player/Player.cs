using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour{

    private float _vSpeed = 0f;
    public Animator animator;
    public KeyCode jumpKeyCode = KeyCode.Space;
    public CharacterController characterController;
    public float speed = 1f, turnSpeed = 1f, jumpSpeed = 15f, gravity = -9.8f;

    [Header("Run Setup")]
    public KeyCode keyRun = KeyCode.LeftShift;
    public float speedRun = 1.5f;

    // Update is called once per frame
    void Update(){
        
        transform.Rotate(0, Input.GetAxis("Horizontal") * turnSpeed * Time.deltaTime, 0);

        var inputAxisVertical = Input.GetAxis("Vertical");
        var speedVector = transform.forward * inputAxisVertical * speed;


        if(characterController.isGrounded){

            _vSpeed = 0;

            if(Input.GetKeyDown(jumpKeyCode)){
                _vSpeed = jumpSpeed;
            }
        }

        _vSpeed -= gravity * Time.deltaTime; // fazendo gravidade funcionar
        speedVector.y = _vSpeed;

        var isWalking = inputAxisVertical != 0;
        if(isWalking){
            
            if(Input.GetKey(keyRun)){

                speedVector *= speedRun;
                animator.speed = speedRun;
            }else{
                animator.speed = 1;
            }
        }

        characterController.Move(speedVector * Time.deltaTime);

        animator.SetBool("Run", inputAxisVertical != 0);  //verificar input Vertical

        /*
        //verificar input Vertical
        if(inputAxisVertical != 0){
            animator.SetBool("Run", true);
        }else{
            animator.SetBool("Run", false);
        }
        */

    }
}