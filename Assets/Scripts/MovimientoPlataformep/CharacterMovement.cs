
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterMovement : MonoBehaviour {

    [Header("Movement")]
    public float walkSpeed = 1f;

    [Header("Jumping")]
    public float jumpForce = 5f;
    public bool isGrounded = true;
    public Vector2 movimiento;
    public Rigidbody2D rb;

    //Raycasts
    public GameObject projectile;
    private Vector3 rightOrigin;
    private Vector3 leftOrigin;
    public float width;
    public float heigth;
    LayerMask Ground;
    public float RayLength = 0.1f;
    private bool IsDying;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        IsDying = false;
        //Ground = 1 << LayerMask.NameToLayer("Ground");
    }


    // Update is called once per frame
    void Update()
    {
        //movimiento = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        if (Input.GetKeyDown(KeyCode.UpArrow)) { Jump(); }
        
        if (IsDying) return;
        IsGrounded();
    }

    /*
     *  LateUpdate is called at the end of Update(). 
     *  Useful for camera scripts, animations, etc, which need to keep tracks of other elements.
    */
    private void LateUpdate()
    {

    }

    private void FixedUpdate()
    {
        if (IsDying) return;

        Move();


    }

    void Move()
    {
        float movement = Input.GetAxisRaw("Horizontal");
        if (movement > 0)
        {
            transform.Translate(new Vector2(walkSpeed * Time.deltaTime * movement, 0));
            transform.rotation = Quaternion.Euler(0, 0, 0);
            //transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, 1);
        }
        else if (movement < 0)
        {
            transform.Translate(new Vector2(walkSpeed * Time.deltaTime * movement * -1, 0));
            //transform.eulerAngles.y = 180;
            transform.rotation = Quaternion.Euler(0, 180, 0);
            //transform.localScale = new Vector3(-1 * Mathf.Abs(transform.localScale.x), transform.localScale.y, 1);
        }

    }

    void MoveForces(/*Vector2 direction*/)
    {
        //rb.velocity = walkSpeed * direction;
        //rb.MovePosition((Vector2)transform.position + (direction * walkSpeed * Time.deltaTime));
        float movement = Input.GetAxisRaw("Horizontal");
        if (movement > 0)
        {
            rb.velocity = new Vector2(walkSpeed * movement, rb.velocity.y);
            
            //transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, 1);
        }
        else if (movement < 0)
        {
           
            rb.velocity = new Vector2(walkSpeed * movement, rb.velocity.y);
            //transform.localScale = new Vector3(-1 * Mathf.Abs(transform.localScale.x), transform.localScale.y, 1);
        }

    }
   
    void Jump()
    {   
            rb.velocity = new Vector2(rb.velocity.x, 0);
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }

    /*
     * Is the character grounded?
     * Uses raycasts at each side of the character width.
     */
    void IsGrounded()
    {
        rightOrigin = transform.position + new Vector3(width, -heigth / 2, 0);
        leftOrigin = transform.position + new Vector3(-width, -heigth / 2, 0);

        RaycastHit2D rightRay = Physics2D.Raycast(rightOrigin, -Vector3.up, RayLength, Ground);
        RaycastHit2D leftRay = Physics2D.Raycast(leftOrigin, -Vector3.up, RayLength, Ground);

        Debug.DrawLine(rightOrigin, rightOrigin + -Vector3.up * RayLength, Color.red);
        Debug.DrawLine(leftOrigin, leftOrigin + -Vector3.up * RayLength, Color.red);


        isGrounded = rightRay.collider != null || leftRay.collider != null;
    }

    void KillPlayer()
    {
        IsDying = true;
        Invoke("Respawn", 1f);
    }

    void Respawn()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Auch!!");
            KillPlayer();
        } 
    }
}
