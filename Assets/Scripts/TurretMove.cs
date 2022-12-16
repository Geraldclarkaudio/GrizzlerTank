using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretMove : MonoBehaviour
{
    public float rotSpeed;

    public float rtpcIncrement;
    
    [Header("Wwise Variables")]
    [SerializeField]
    AK.Wwise.Event turretSounds;

    [Header("RTPCs")]
    [SerializeField]
    private AK.Wwise.RTPC turret;


    private void Start()
    {
        turretSounds.Post(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        turret.SetValue(gameObject, rtpcIncrement);

        if(Input.GetKey(KeyCode.G))
        {
            rtpcIncrement += 1 * Time.deltaTime;
            if(rtpcIncrement >=2)
            {
                rtpcIncrement = 2;
            }
            transform.Rotate(0.0f, -1 * rotSpeed * Time.deltaTime, 0.0f);
        }      

        if(Input.GetKey(KeyCode.H))
        {
            rtpcIncrement += 1 * Time.deltaTime;
            if (rtpcIncrement >= 2)
            {
                rtpcIncrement = 2;
            }
            transform.Rotate(0.0f, 1 * rotSpeed * Time.deltaTime, 0.0f);
        }

        if(Input.GetKeyUp(KeyCode.G) || Input.GetKeyUp(KeyCode.H))
        {
            rtpcIncrement = 3;
            StartCoroutine(STOPROTATING());
        }

        if(rtpcIncrement >3)
        {
            rtpcIncrement = 3;
        }
    }

    IEnumerator STOPROTATING()
    {
        yield return new WaitForSeconds(2.25f);
        rtpcIncrement = 0;
    }
}
