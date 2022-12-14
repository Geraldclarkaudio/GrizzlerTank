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
    [SerializeField] [Range(0, 2)]
    private int acceleration;
    [SerializeField] [Range(0, 1)]
    private int turn; 
    [SerializeField]
    private float movementSpeed;
    [SerializeField]
    private int surface;

    [Header("Wwise Events")]
    [SerializeField]
    private AK.Wwise.Event grizzlerEvent;

    [Header("RTPCs")]
    [SerializeField]
    private AK.Wwise.RTPC playerSpeedRTPC;
    [SerializeField]
    private AK.Wwise.RTPC playerAcceleration;
    [SerializeField]
    private AK.Wwise.RTPC playerTurning;
    [SerializeField]
    private AK.Wwise.RTPC wheelSurface;


    // Start is called before the first frame update
    void Start()
    {
        grizzlerEvent.Post(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("MetalFloor"))
        {
            surface = 0;
        }
        if(other.CompareTag("Concrete Floor"))
        {
            surface = 1;
        }
    }

    private void Update()
    {
        //RTPCs
        playerSpeedRTPC.SetValue(gameObject, _speed);
        playerAcceleration.SetValue(gameObject, acceleration);
        playerTurning.SetValue(gameObject, turn);
        wheelSurface.SetValue(gameObject, surface);

        //Inputs
        float verticalInput = Mathf.Abs(Input.GetAxisRaw("Vertical"));
        float horizontalInput = Mathf.Abs(Input.GetAxisRaw("Horizontal"));
        Vector3 direction = new Vector3(horizontalInput, 0, verticalInput);

        if(verticalInput == 1 || horizontalInput == 1) // if Holding an input
        {
            isMoving += 0.1f * Time.deltaTime; // set load
            acceleration = 1;
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

            if(verticalInput == 1 && horizontalInput ==1)
            {
                turn = 1;
            }
        }

        else if(verticalInput == 0 || horizontalInput == 0) // if not holding input
        {
            
            turn = 0;

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

            if(_speed <= 1.5)
            {
                acceleration = 2;
            }

            transform.Translate(new Vector3(0,0,0.5f) * _speed * Time.deltaTime);
        }
    }


}
