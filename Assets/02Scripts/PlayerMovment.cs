using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovment : MonoBehaviour
{
    #region Vars

    [Header ("Movment Vars")]

    public float normalSpeed;
    public float jumpPower = 1f;
    private Rigidbody2D rb;
    public SpriteRenderer sprite;
    private Vector2 velocity;
    public LayerMask ground;
    private float speed;




    #endregion


    void Awake() 
    {
        rb = GetComponent<Rigidbody2D>();
        speed = normalSpeed;
        sprite = GetComponentInChildren<SpriteRenderer>();
    }


    void Update()
    {
        ProcInput();
    }

    void FixedUpdate()
    {
        ProcMovment();
    }

    void ProcInput()
    {
        float inputX = Input.GetAxis("Horizontal");

        if (inputX < 0){
            sprite.flipX = true;
        }
        else if (inputX >0){
            sprite.flipX = false;
        }




        if (Physics2D.CircleCast(new Vector2(transform.position.x,transform.position.y),1f,Vector2.down,1f, ground)){
            Debug.Log("Hit");
        }


        velocity = new Vector2(inputX,0f);
    }

    void ProcMovment()
    {
        if (Input.GetButton("Jump"))
        {
            Debug.Log("Jumping");
            rb.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
        }
        rb.velocity = new Vector2(velocity.x * speed,rb.velocity.y);
    }
}
