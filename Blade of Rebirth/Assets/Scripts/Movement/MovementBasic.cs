using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]

public class MovementBasic : MonoBehaviour
{
    [SerializeField]
    private InputActionReference movementControl;
    [SerializeField]
    private InputActionReference jumpControl;
    [SerializeField]
    private float playerSpeed = 2.0f;
    [SerializeField]
    private float jumpHeight = 1.0f;
    [SerializeField]
    private float gracePeriodSet = 0.5f;
    [SerializeField]
    private float gravityValue = -9.81f;
    [SerializeField]
    private float rotationSpeed = 4f;

    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    private bool groundedGracePeriod;
    private bool graceJump;
    private float gracePeriodPriv;
    private Transform cameraMainTransform;

    private void OnEnable()
    {
        movementControl.action.Enable();
        jumpControl.action.Enable();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void OnDisable()
    {
        movementControl.action.Disable();
        jumpControl.action.Disable();
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }


    private void Start()
    {
        controller = gameObject.GetComponent<CharacterController>();
        cameraMainTransform = Camera.main.transform;
        groundedGracePeriod = false;
        gracePeriodPriv = gracePeriodSet;
    }

    void Update()
    {
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
            graceJump = true;
            gracePeriodPriv = gracePeriodSet;
        }
        if (!groundedPlayer && graceJump)
        {
            groundedGracePeriod = true;
            gracePeriodPriv -= Time.deltaTime;
            if(gracePeriodPriv == 0)
            {
                groundedGracePeriod = false;
                graceJump = false;
            }
        }

        Vector2 movement = movementControl.action.ReadValue<Vector2>();
        //Debug.Log(movement);
        Vector3 move = new Vector3(movement.x, 0, movement.y);
        Vector3 projectedForward = new Vector3(cameraMainTransform.forward.x, 0, cameraMainTransform.forward.z).normalized;
        move = projectedForward * move.z + cameraMainTransform.right * move.x;
        move.y = 0f;
        controller.Move(move * Time.deltaTime * playerSpeed);

        // Changes the height position of the player..
        if ((jumpControl.action.triggered && groundedPlayer) || (jumpControl.action.triggered && groundedGracePeriod))
        {
            playerVelocity.y = 0f;
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
            graceJump = false;
            groundedGracePeriod = false;
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);

        if (movement != Vector2.zero)
        {
            float targetAngle = Mathf.Atan2(movement.x, movement.y) * Mathf.Rad2Deg + cameraMainTransform.eulerAngles.y;
            Quaternion rotation = Quaternion.Euler(0f, targetAngle, 0f);
            //transform.rotation = rotation;
            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
        }
    }
}
