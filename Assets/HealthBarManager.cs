using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarManager : MonoBehaviour
{


    public CharacterMovement player;
    public Image bar;
    void Start()
    {
        
    }


    // Update is called once per frame
    void Update()
    {
        bar.fillAmount = player.vida;

    }



}
