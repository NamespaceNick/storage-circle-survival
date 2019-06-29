using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using InControl;


public class PlayerController : MonoBehaviour
{
    public bool usingController = true;
    public float coreOffset = 0.7f;
    public float slowCoef;
    public EquipmentSlot leftConfig, rightConfig;
    public Rigidbody rb;


    MyCharacterActions playerActions;
    GameObject player;
    InputDevice device;
    PlayerStatus status;
    Vector3 leftConfigDir, rightConfigDir;

    void Start ()
    {

        playerActions = new MyCharacterActions();
        rb = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
        status = GetComponent<PlayerStatus>();
	}
	
	void Update ()
    {
        // device = InputManager.ActiveDevice;
        /*
        switch (device.DeviceClass)
        {
            case InputDeviceClass.Controller:
                Debug.Log("Using a controller");
                break;
            case InputDeviceClass.Keyboard:
                Debug.Log("Using a keyboard");
                break;
            case InputDeviceClass.Mouse:
                Debug.Log("Using a mouse");
                break;
            default:
                Debug.Log("Using device other than controller, kb, or mouse");
                break;
        }
        */
        /*
        if (usingController)
        {
            leftConfigDir = device.LeftStick.Value.normalized;
            rightConfigDir = device.RightStick.Value.normalized;
        }
        
        else
        {
        leftConfigDir = Input.mousePosition;
        rightConfigDir = Vector3.zero;
        }
        */

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
        leftConfigDir = (mousePos - player.transform.position).normalized;
        if (!status.isDead)
        {
            HandleOrientation();
            HandleCycling();
            HandleShooting();
            rb.velocity = Vector3.Lerp(rb.velocity, Vector3.zero, slowCoef);
        }
	}


    void HandleCycling()
    {
        // if (device.LeftBumper.WasPressed)
        /*
        if (playerActions.leftCycle.WasPressed)
        {
        */
        if (Input.GetMouseButtonDown(1))
        {
            leftConfig.CycleEquipment();
        }
        /*
    }
    if (playerActions.rightCycle)
    {
        rightConfig.CycleEquipment();
    }
    */
    }


    void HandleOrientation()
    {
        if (leftConfigDir != Vector3.zero)
        {
            leftConfig.transform.position = player.transform.position + leftConfigDir * coreOffset;
            leftConfig.transform.up = leftConfig.transform.position - transform.position;
        }
        /*
        if(rightConfigDir != Vector3.zero)
        {
            rightConfig.transform.position = player.transform.position + rightConfigDir * coreOffset;
            rightConfig.transform.up = rightConfig.transform.position - transform.position;
        }
        */
    }


    void HandleShooting()
    {
        /*
        if (playerActions.leftAbility.WasPressed)
        {
            leftConfig.TriggerPressed();
        }
        if (playerActions.rightAbility.WasPressed)
        {
            rightConfig.TriggerPressed();
        }
        */
        if (Input.GetMouseButtonDown(0))
        {
            leftConfig.TriggerPressed();
        }
    }
}

