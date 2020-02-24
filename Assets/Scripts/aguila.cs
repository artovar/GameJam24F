using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class aguila : MonoBehaviour
{
    //Variables
    Animator anim;
    bool girar = true;
    SpriteRenderer aguilaRender;
    Rigidbody2D aguilaBody;
    public float vel = 3f;

    //Funciones -----------------------------------------

    private void Voltear()
    {
        girar = !girar;
        aguilaRender.flipX = !aguilaRender.flipX;
    }

    //-----------------------------------------------------

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        aguilaRender = GetComponent<SpriteRenderer>();
        aguilaBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //movimiento
        if (transform.position.x < 0) //rigth
        {
            aguilaBody.velocity = new Vector2(0.7f * vel, 0);
            Voltear();
        }
        else 
        {
            aguilaBody.velocity = new Vector2(0.7f * vel, 0);
            Voltear();
        }
    }
}
