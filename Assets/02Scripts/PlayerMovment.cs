using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    private bool isGrounded = false;




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


        RaycastHit2D ray = Physics2D.CircleCast(new Vector2(transform.position.x, transform.position.y), 0.5f, Vector2.down, 0.5f, ground);

        if (ray){
            Debug.Log("Grounded");
            isGrounded = true;
        }
        else{
            Debug.Log("not Grounded");
            isGrounded = false;
        }


        velocity = new Vector2(inputX,0f);
    }

    void ProcMovment()
    {
        if (Input.GetButton("Jump") && isGrounded)
        {
            Debug.Log("Jumping");
            rb.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
        }
        rb.velocity = new Vector2(velocity.x * speed,rb.velocity.y);
    }

    void OnCollisionEnter2D(Collision2D other) 
    {
        if(other.gameObject.tag =="Food"){
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);

        }
        if (other.gameObject.tag == "Enemy")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        }
        
    }
}
