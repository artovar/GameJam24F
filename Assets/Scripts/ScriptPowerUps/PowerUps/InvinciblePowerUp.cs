using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvinciblePowerUp : Subject, IPowerUp
{
    private LevelManager myLevelManager;

    [SerializeField]
    private float time = 5f;

    private void Awake()
    {
        myLevelManager = FindObjectOfType<LevelManager>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public float GetTime() { return time; }

    public void Execute()
    {
        myLevelManager.MakeInvincibleplayer(time);
        Notify(new MyEvent(this, MyEvent.EventType.invincibleConsumed));
        Destroy(this.gameObject);
    }
}