using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    MovementInfo info;

    [SerializeField]
    private float linearDrag = 0.95f, 
                  angularDrag = 0.70f;
  
    [SerializeField]
    private float maxVelocity = 20f;

    [SerializeField]
    private float mouseSensitivity = 2.5f;

    [SerializeField]
    private float walkVelocity = 8f;

    public MovementInfo GetInfo => info;
    public Vector3 lastFrameForward;

    [SerializeField]
    private GameObject playerCamera, flashlight;
    private Vector3 offset;

    void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;

        // Checks for camera 
        if (playerCamera != null) playerCamera = GameObject.Find("Main Camera");

        // Initializes 
        info.position = transform.position;
        Vector3 forward = transform.forward;
        lastFrameForward = playerCamera.transform.forward;

        //info.orientationV2.x = Mathf.Atan2(forward.x, forward.z);
        //info.orientationV2.y = Mathf.Atan2(forward.y, forward.x);

        info.orientationV2 = Vector2.zero;
        //offset = transform.position - flashlight.transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        Steering steering = new Steering();
        PlayerMovementUpdate(steering);
        PlayerLookUpdate(steering,this.transform,playerCamera.transform);
       

    }

    private void PlayerMovementUpdate(Steering nextFrameSteering)
    {

        // Update our position according to current velocity vector 
        info.position += info.velocity * Time.deltaTime;

        // Apply drag
        info.velocity *= linearDrag;

        // Read Horizontal and Vertical Axes, and update velocity/rotation
        float vertical = Input.GetAxis("Vertical");
        float horizontal = Input.GetAxis("Horizontal");

        // Next frame movement
        nextFrameSteering.linear = (transform.forward * vertical + transform.right * horizontal) * Time.deltaTime * walkVelocity;

        // Updates vectors for next frame calculus
        info.velocity += nextFrameSteering.linear;

        // Do not exceed our max velocity
        info.velocity = Vector3.ClampMagnitude(info.velocity, maxVelocity);

        // Update Unity Information
        transform.position = info.position;
    }

    private void PlayerLookUpdate(Steering nextFrameSteering, Transform playerTransform, Transform cameraTransform)
    {
        // Update our position according to current rotation vector 
        info.orientationV2 += info.rotationV2 * Time.deltaTime * mouseSensitivity;

        // Add drag
        info.rotationV2 *= angularDrag;


        // Read Mouse Movement
        Vector2 mouseXY = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));


        // Gets mouse inputs
        if (Mathf.Abs(mouseXY.x) != 0 || Mathf.Abs(mouseXY.y) != 0)
        {
            // Adds angular X
            nextFrameSteering.angular = mouseXY.x * Time.deltaTime * mouseSensitivity;
            info.rotationV2.x += nextFrameSteering.angular;

            //Adds angular Y
            nextFrameSteering.angular = mouseXY.y * Time.deltaTime * mouseSensitivity;
            info.rotationV2.y -= nextFrameSteering.angular;
        }
        
        //Normalize orientation
        info.orientationV2 = AuxMethods.NormAngle(info.orientationV2);

        //Y axis clamp
        info.orientationV2.y = Mathf.Clamp(info.orientationV2.y, -Mathf.PI / 2, Mathf.PI / 2);

        // Resets rotations for next frame(values are additive and odnt reset after update)
        playerCamera.transform.localRotation = Quaternion.identity;
        transform.rotation = Quaternion.identity;

        lastFrameForward = cameraTransform.forward;
        // Rotates all player right-left
        playerTransform.Rotate(playerTransform.up, info.orientationV2.x * Mathf.Rad2Deg);

        // Onbly rotates camera up-down
        cameraTransform.Rotate(cameraTransform.right, info.orientationV2.y * Mathf.Rad2Deg, Space.World);
    }

    private void PlayerFollowFlashlight()
    {

        //this.transform.rotation = Quaternion.identity;
        //this.transform.rotation = Quaternion.Slerp(this.transform.rotation, flashlight.transform.rotation, 3.0f * Time.deltaTime);
        //this.transform.Rotate(this.transform.up, )

    }

}
