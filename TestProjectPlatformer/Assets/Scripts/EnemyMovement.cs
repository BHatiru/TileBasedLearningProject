using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 1f;
    Rigidbody2D enemyRbody;
    void Start()
    {
        enemyRbody = GetComponent<Rigidbody2D>();  
    }

    
    void Update()
    {
        enemyRbody.velocity = new Vector2(moveSpeed, 0f);
    }

    void Flip(){
        transform.localScale = new Vector2(-(Mathf.Sign(enemyRbody.velocity.x)), 1f);
    }

    void OnTriggerExit2D(Collider2D other) {
        moveSpeed = -moveSpeed;
        Flip();
    }
    

}
