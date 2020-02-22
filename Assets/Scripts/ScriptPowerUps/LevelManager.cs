using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{

    private PlayerController myPlayerController;

    // Start is called before the first frame update
    void Start()
    {
        myPlayerController = FindObjectOfType<PlayerController>();
    }

    public void SpeedUpPlayer(float speedIncrease, float time)
    {
        myPlayerController.SpeedUp(speedIncrease, time);
    }

    public void MakeInvincibleplayer(float time)
    {
        myPlayerController.MakeInvincible(time);
    }
}
