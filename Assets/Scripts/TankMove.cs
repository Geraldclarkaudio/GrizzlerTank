using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankMove : MonoBehaviour
{
    public CharacterController tankController;
    public Vector3 moveDirection = Vector3.zero;
    public float gravity = 100.0f;
    public float speed;
    public float rotSpeed;
    public int surface;
    public int turn;
    public int acceleration;

    [Header("Wwise Variables")]
    [SerializeField]
    private AK.Wwise.Event grizzlerMovement;

    [Header("Wwise RTPCs")]
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
        tankController = GetComponent<CharacterController>();
        grizzlerMovement.Post(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(tankController.velocity);

        playerSpeedRTPC.SetValue(gameObject, speed);
        playerAcceleration.SetValue(gameObject, acceleration);
        playerTurning.SetValue(gameObject, turn);
        wheelSurface.SetValue(gameObject, surface);

        if (tankController.isGrounded == true)
        {
            moveDirection.Set(0.0f, 0.0f, Input.GetAxis("Vertical"));
            moveDirection = transform.TransformDirection(moveDirection);
            
            transform.Rotate(0.0f, Input.GetAxis("Horizontal") * rotSpeed * Time.deltaTime, 0.0f);
        }

        if(Mathf.Abs(Input.GetAxis("Horizontal")) > 0)
        {
            turn = 1;
        }
        else if(Mathf.Abs(Input.GetAxis("Horizontal")) == 0)
        {
            turn = 0;
        }

        moveDirection.y -= gravity * Time.deltaTime;

        if(Input.GetKey(KeyCode.W))
        {
            acceleration = 1;// plays accel sound
            //moving forward
            if(speed >= 25)
            {
                speed = 25;
            }

            speed += 2f * Time.deltaTime;
        }

        if(Input.GetKeyUp(KeyCode.W))
        {
            acceleration = 2; // plays deccel sound
        }

        if(speed <= 0)
        {
            speed = 0;
        }
     
        tankController.Move(moveDirection * speed * Time.deltaTime);
    }
}
