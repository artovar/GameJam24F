using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedPowerUp : Subject, IPowerUp
{

    private LevelManager myLevelManager;

    [SerializeField]
    private float speedIncrease = 1f;

    [SerializeField]
    private float time = 1f;

    private void Start()
    {
        myLevelManager = FindObjectOfType<LevelManager>();
    }

    public float GetTime() { return time; }

    public void Execute()
    {
        myLevelManager.SpeedUpPlayer(speedIncrease, time);
        Notify(new MyEvent(this, MyEvent.EventType.speedConsumed));
        Destroy(this.gameObject);
    }
}
