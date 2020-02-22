using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Subject
{
    [SerializeField]
    private float maxHealth = 100f;
    public float MaxHealth { get { return maxHealth; } }
    private float currentHealth;
    public float CurrentHealth { get { return currentHealth; } }

    [SerializeField]
    private float speed = 5f;
    private float currentSpeed;

    private bool isInvincible;

    private void Awake()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        currentSpeed = speed;
        isInvincible = false;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
        float inputZ = Input.GetAxis("Vertical");
        float inputX = Input.GetAxis("Horizontal");
        Vector3 direction = new Vector3(inputX, 0, inputZ) * currentSpeed;

        this.transform.Translate(direction * Time.deltaTime) ;
    }

    public void SpeedUp(float speedIncrease, float time)
    {
        currentSpeed *= speedIncrease;
        StartCoroutine(speedCoroutine(time));
    }

    public void MakeInvincible(float time)
    {
        isInvincible = true;
        StartCoroutine(invincibleCoroutine(time));
    }

    private void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case "PowerUp":
                other.gameObject.GetComponent<IPowerUp>().Execute();
                break;
            case "Damage":
                if(!isInvincible)
                {
                    float damage = other.GetComponent<DamageController>().Damage;
                    currentHealth -= damage;
                    Notify(new MyEvent(this, MyEvent.EventType.damageDone));
                }
                break;
        }
    }

    private IEnumerator speedCoroutine(float time)
    {
        yield return new WaitForSeconds(time);
        currentSpeed = speed;
    }

    private IEnumerator invincibleCoroutine(float time)
    {
        yield return new WaitForSeconds(time);
        isInvincible = false;
    }
}
