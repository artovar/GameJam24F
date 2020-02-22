using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageController : MonoBehaviour
{
    [SerializeField]
    private float damage = 5f;
    public float Damage { get { return damage; } }
}
