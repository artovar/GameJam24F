using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arma : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPre;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift)) {
            shoot();
        }
    }
    void shoot() {

        Instantiate(bulletPre, firePoint.position, firePoint.rotation);
    
    }
}
