using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAController1 : MonoBehaviour
{

    public bool isTouchingGround;
    public float speed = 5f;
    public float jumpSpeed = 5f;
    private Rigidbody2D rigidBody;
    public Transform enemyGroundCheckPoint;
    public LayerMask groundLayer;
    public LayerMask playerLayer;

    private Vector2 groundCheckPointA;
    private Vector2 groundCheckPointB;

    private Animator myAnimator;

    //GJ
    public CharacterMovement theboss;
    public AtaqueMelee thebossAttack;
    public jumppointController myjumppoint;
    public bool salto;
    public bool isEnemyNear;
    public bool descansando;
    public float enemyAttackRange = 1f;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        //GJ
        theboss = FindObjectOfType<CharacterMovement>();
        thebossAttack = FindObjectOfType<AtaqueMelee>();
        myjumppoint = FindObjectOfType<jumppointController>();
    }

    // Update is called once per frame
    void Update()
    {
        groundCheckPointA = new Vector2(enemyGroundCheckPoint.position.x - 0.63f, enemyGroundCheckPoint.position.y);
        groundCheckPointB = new Vector2(enemyGroundCheckPoint.position.x + 0.44f, enemyGroundCheckPoint.position.y + 0.35f);
        isTouchingGround = Physics2D.OverlapArea(groundCheckPointA, groundCheckPointB, groundLayer);

        if (!descansando)
        {
            //movement
            //GJ
            if (transform.position.x < theboss.transform.position.x) //right
            {
                rigidBody.velocity = new Vector2(0.7f * speed, rigidBody.velocity.y);
                transform.localScale = new Vector2(1f, 1f);
            }
            else if (transform.position.x > theboss.transform.position.x) //left
            {
                rigidBody.velocity = new Vector2(-0.7f * speed, rigidBody.velocity.y);
                transform.localScale = new Vector2(-1f, 1f);
            }
            else //idle
            {
                rigidBody.velocity = new Vector2(0, rigidBody.velocity.y);
            }

            //jump
            //GJ
            salto = (int)transform.position.x == (int)myjumppoint.transform.position.x;
            if (salto && isTouchingGround)
            {
                rigidBody.velocity = new Vector2(rigidBody.velocity.x, jumpSpeed);
            }
            else if (((transform.position.x < -0.98f) || (transform.position.x > 8.05f)) && isTouchingGround && (Random.Range(0.0f, 1.0f) < 0.008f))
            {
                rigidBody.velocity = new Vector2(rigidBody.velocity.x, jumpSpeed);
            }
            else if ((transform.position.y < theboss.transform.position.y) && isTouchingGround && ((int)transform.position.x == (int)theboss.transform.position.x))
            {
                rigidBody.velocity = new Vector2(rigidBody.velocity.x, jumpSpeed);
            }
        }

        //attack
        //GJ
        isEnemyNear = Physics2D.OverlapCircle(enemyGroundCheckPoint.position, enemyAttackRange, playerLayer);

        //descansa
        //GJ
        if ((Random.Range(0.0f, 1.0f) < 0.0008f) && !descansando && isTouchingGround)
        {
            descansando = true;
            StartCoroutine("DescansandoCoroutine");
        }

        //animator conditions
        myAnimator.SetFloat("Speed", Mathf.Abs(rigidBody.velocity.x));
        myAnimator.SetBool("OnGround", isTouchingGround);
        //GJ
        myAnimator.SetBool("OnAttack", isEnemyNear);
    }

    //descansa
    //GJ
    public IEnumerator DescansandoCoroutine()
    {
        rigidBody.velocity = new Vector2(0, rigidBody.velocity.y);
        yield return new WaitForSeconds(0.8f);
        descansando = false;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if ((other.tag == "Player") && thebossAttack.enemyHitted)
        {
            Destroy(gameObject);
        } else
        {
            thebossAttack.enemyHitted = false;
        }
    }
}
