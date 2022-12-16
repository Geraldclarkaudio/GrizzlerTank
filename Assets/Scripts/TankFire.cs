using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankFire : MonoBehaviour
{
    [SerializeField]
    private AK.Wwise.Event fire;
    private float canFire = -1;
    private float fireRate = 2;
    public void Fire()
    {
        canFire = fireRate + Time.time;
        fire.Post(gameObject);
    }

    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && canFire <= Time.time)
        {
            Fire();
        }
    }
}
