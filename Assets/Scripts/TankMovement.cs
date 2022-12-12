using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankMovement : MonoBehaviour
{
    [Header("Tank Specific")]
    [SerializeField] [Range(0, 25)]
    private float _speed;
    [SerializeField] [Range(0,1)]
    private float isMoving = 0.5f;

    [SerializeField]
    private float movementSpeed;

    [Header("Wwise Events")]
    [SerializeField]
    private AK.Wwise.Event grizzlerEvent;
    [SerializeField]
    private AK.Wwise.Event beginMovementEvent;
    [SerializeField]
    private AK.Wwise.Event stopMovementEvent;
    [Header("RTPCs")]
    [SerializeField]
    private AK.Wwise.RTPC playerSpeedRTPC;
    [SerializeField]
    private AK.Wwise.RTPC playerLoadRTPC;

    // Start is called before the first frame update
    void Start()
    {
        grizzlerEvent.Post(gameObject);
    }

    private void Update()
    {
        playerSpeedRTPC.SetValue(gameObject, _speed);
        playerLoadRTPC.SetValue(gameObject, isMoving);

        float verticalInput = Mathf.Abs(Input.GetAxisRaw("Vertical"));
        float horizontalInput = Mathf.Abs(Input.GetAxisRaw("Horizontal"));
        Vector3 direction = new Vector3(horizontalInput, 0, verticalInput);

        if(verticalInput == 1 || horizontalInput == 1) // if Holding an input
        {
            isMoving += 0.1f * Time.deltaTime; // set load
         
            _speed += movementSpeed * Time.deltaTime; // increase speed over time. 

            if(isMoving >= 1)
            {
                isMoving = 1;
            }

            if(_speed >= 25)
            {
                _speed = 25; //cap speed at 25
            }
        
            transform.Translate(direction * _speed * Time.deltaTime); // move object in whatever direction at speed of _speed;
        }

        else if(verticalInput == 0 || horizontalInput == 0) // if not holding input
        {
            _speed -= movementSpeed * Time.deltaTime; // decrease speed over time

            if (isMoving <= 0)
            {
                isMoving = 0;
            }

            if (_speed > 0)
            {
                isMoving -= 0.1f * Time.deltaTime; // cap at 0
            }

            if(_speed <= 0)
            {
                isMoving = 0.5f; // set load back to 0.5 when reaching 0 _speed;
                _speed = 0;
            }

            transform.Translate(new Vector3(0,0,0.5f) * _speed * Time.deltaTime);
        }
    }


}
