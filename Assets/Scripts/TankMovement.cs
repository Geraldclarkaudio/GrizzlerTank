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
    private float turn; 
    [SerializeField]
    private float movementSpeed;
    [SerializeField]
    private int surface;
    public float rotSpeed;

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
            Debug.Log("colliding with metal");

            surface = 0;
            wheelSurface.SetValue(gameObject, surface);
        }
        if(other.CompareTag("ConcreteFloor"))
        {
            Debug.Log("Colliding with Concrete");
            surface = 1;
            wheelSurface.SetValue(gameObject, surface);
        }
    }

    private void Update()
    {
        //RTPCs
        playerSpeedRTPC.SetValue(gameObject, _speed);
        playerAcceleration.SetValue(gameObject, acceleration);
        playerTurning.SetValue(gameObject, turn);
  

        //Inputs
        float verticalInput = Mathf.Abs(Input.GetAxisRaw("Vertical"));
        float horizontalInput = Mathf.Abs(Input.GetAxisRaw("Horizontal"));
        Vector3 direction = new Vector3(0, 0, verticalInput);

        turn = horizontalInput;

        if(verticalInput == 1) // if Holding an input
        {
            acceleration = 1;
            _speed += movementSpeed * Time.deltaTime; // increase speed over time. 

            if(_speed >= 25)
            {
                _speed = 25; //cap speed at 25
            }

            transform.Translate(direction * _speed * Time.deltaTime); // move object in whatever direction at speed of _speed;

        }

        else if(verticalInput == 0 || horizontalInput == 0) // if not holding input
        {
            
            turn = 0;

            _speed -= movementSpeed * Time.deltaTime; // decrease speed over time        

            if (_speed > 0 && _speed <= 4) // kinda works
            {
                acceleration = 0;
                isMoving -= 0.1f * Time.deltaTime; // cap at 0
            }

            if(_speed <= 0)
            {
                isMoving = 0.5f; // set load back to 0.5 when reaching 0 _speed;
                _speed = 0;
            }

            transform.Translate(new Vector3(0,0,0.5f) * _speed * Time.deltaTime); // move object
        }

        if(horizontalInput == 1)
        {
            turn = 1;
            transform.Rotate(0.0f, Input.GetAxis("Horizontal") * rotSpeed * Time.deltaTime, 0.0f);
        }

        if(Input.GetKeyUp(KeyCode.W))
        {
            acceleration = 2;
        }
    }


}
