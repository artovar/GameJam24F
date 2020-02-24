
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterMovement : MonoBehaviour
{

    [Header("Movement")]
    public float walkSpeed = 1f;

    [Header("Jumping")]
    public float jumpForce = 5f;
    public bool isGrounded = true;
    public int vida = 6;

    public Rigidbody2D rb;
    Animator anim;
    //Raycasts
    private Vector3 rightOrigin;
    private Vector3 leftOrigin;
    public float width;
    public float heigth;
    LayerMask Ground;
    public float RayLength = 2f;
    private bool IsDying;



    bool enSuelo = false;
    float ComprobarRadioSuelo = 0.2f;
    public LayerMask CapaSuelo; //elegimos la capa donde pisa
    public Transform comprobarSuelo; //Mira si esta en el suelo


    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        IsDying = false;
        Ground = 1 << LayerMask.NameToLayer("Ground");
    }


    // Update is called once per frame
    void Update()
    {
        if (IsDying) return;
        IsGrounded();

        //Saltar 

        enSuelo = Physics2D.OverlapCircle(comprobarSuelo.position, ComprobarRadioSuelo, CapaSuelo);
        anim.SetBool("estaSuelo", enSuelo); //cambia la variable estaSuelo
    }

    /*
     *  LateUpdate is called at the end of Update(). 
     *  Useful for camera scripts, animations, etc, which need to keep tracks of other elements.
    */
   

    private void FixedUpdate()
    {
        if (IsDying) return;
        Move();
        if (Input.GetButton("Jump")) Jump();
    }

    void Move()
    {
        float movement = Input.GetAxisRaw("Horizontal");
        anim.SetFloat("velX", Mathf.Abs(movement));

        if (movement > 0)
        {
            transform.Translate(new Vector3(walkSpeed * Time.deltaTime * movement, 0, 0));
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, 1);
        }
        else if (movement < 0)
        {
            transform.Translate(new Vector3(walkSpeed * Time.deltaTime * movement, 0, 0));
            transform.localScale = new Vector3(-1 * Mathf.Abs(transform.localScale.x), transform.localScale.y, 1);
        }

    }

    void MoveForces()
    {
        float movement = Input.GetAxisRaw("Horizontal");
        if (movement > 0)
        {
            rb.velocity = new Vector2(walkSpeed * movement, rb.velocity.y);
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, 1);
        }
        else if (movement < 0)
        {
            rb.velocity = new Vector2(walkSpeed * movement, rb.velocity.y);
            transform.localScale = new Vector3(-1 * Mathf.Abs(transform.localScale.x), transform.localScale.y, 1);
        }
        anim.SetFloat("velY", Mathf.Abs(movement));

    }

    void Jump()
    {
        if (isGrounded)
        {
            anim.SetBool("estaSuelo", false);
            rb.velocity = new Vector2(rb.velocity.x, 0);
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            isGrounded = false;
            enSuelo = false;

        }
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

        isGrounded = rightRay.collider != null || leftRay.collider != null;
    }

    void KillPlayer()
    {
        IsDying = true;
        vida = vida - (1 / 6);

        if (vida==0) Invoke("Respawn", 1f);
        else IsDying = false;
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
