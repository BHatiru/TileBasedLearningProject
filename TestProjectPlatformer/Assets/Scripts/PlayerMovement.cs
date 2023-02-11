using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour
{
    Vector2 moveInput;
    Rigidbody2D myrigidbody;
    CapsuleCollider2D myCapsuleCollider;
    BoxCollider2D myBoxCollider;

    
    public GameObject deathPanel;
    
    [SerializeField] float VelocityCoefficient = 5;
    [SerializeField] float JumpSpeed = 5;

   

    float defaultGravity;
    bool isAlive = true;

    Animator myAnimator;
    void Start()
    {   
        myrigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myCapsuleCollider = GetComponent<CapsuleCollider2D>();
        myBoxCollider = GetComponent<BoxCollider2D>();
        defaultGravity = myrigidbody.gravityScale;
        
    }

    public bool GetLiveState(){
        return isAlive;
    }

    void Update()
    {
        if (!isAlive) {return;}
            Run();
            FlipSprite();
            Die();
        
        
    }

    
    void OnMove(InputValue value)
    {
        if (!isAlive) {return;}
            moveInput = value.Get<Vector2>();
    }

    void OnJump(InputValue value)
    {
        if (!isAlive) {return;}
        
            if (value.isPressed && myBoxCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
            {
                myrigidbody.velocity += new Vector2(0f, JumpSpeed);
            }
        
    }

    void OnQuit(InputValue value)
    {
        Debug.Log("Game closed");
        Application.Quit();
    }

    void Run()
    {
        Vector2 playerVelocity = new Vector2(moveInput.x * VelocityCoefficient, myrigidbody.velocity.y);
        myrigidbody.velocity = playerVelocity;
        bool playerHasHorizontalSpeed = Mathf.Abs(myrigidbody.velocity.x) > Mathf.Epsilon;
        myAnimator.SetBool("IsRunning", playerHasHorizontalSpeed);
    }


    void FlipSprite()
    {
        bool playerHasHorizontalSpeed = Mathf.Abs(myrigidbody.velocity.x) > Mathf.Epsilon;
        if (playerHasHorizontalSpeed)
            transform.localScale = new Vector2(Mathf.Sign(myrigidbody.velocity.x), 1f);
    }

    void Die(){
        if(myCapsuleCollider.IsTouchingLayers(LayerMask.GetMask("Enemies", "Hazards"))){
            isAlive = false;
            myAnimator.SetTrigger("IsDying");
            myrigidbody.velocity = new Vector2(-myrigidbody.velocity.x , 10f);
            StartCoroutine(CanvasDelayedShow());
            
        }
    }

    IEnumerator CanvasDelayedShow(){
        yield return new WaitForSeconds(0.6f);
        deathPanel.SetActive(true);
    }


}
