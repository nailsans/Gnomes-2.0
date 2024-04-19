using Mono.Cecil.Cil;
using UnityEngine;

public class PlayerLocomotionManager : CharacterLocomotionManager
{
    PlayerManager player;

    public float verticalMovement;
    public float horizontalMovement;
    public bool isShiftPressed;
    public bool isWalking;

    private Vector3 moveDirection;
    private Vector3 targetRotationDirection;
    [SerializeField] private float walkingSpeed = 2;
    [SerializeField] private float runningSpeed = 5;
    [SerializeField] private float rotationSpeed = 15;

    protected override void Awake()
    {
        base.Awake();

        player = GetComponent<PlayerManager>();
    }
    protected override void Update()
    {
        base.Update();  

        if (player.IsOwner)
        {
            player.PlayerNetworkManager.isWalking.Value = isWalking;
        }
        else
        {
            isWalking = player.PlayerNetworkManager.isWalking.Value;

            player.PlayerAnimatorManager.UpdateAnimatorParameter("isWalking", isWalking);
        }
    }
    public void HandleAllMovement()
    {
        HandleMovement();
        HandleRotation();
    }


    private void GetMotionInputs()
    {
        verticalMovement = PlayerInputManager.instance.verticalInput;
        horizontalMovement = PlayerInputManager.instance.horizontalInput;
        isWalking = PlayerInputManager.instance.isWalking;
    }
    private void HandleMovement()
    {

        GetMotionInputs();

        // Movement direction based on camera perspective
        moveDirection = PlayerCamera.instance.transform.forward * verticalMovement;
        moveDirection = moveDirection + PlayerCamera.instance.transform.right * horizontalMovement;
        moveDirection.Normalize();
        moveDirection.y = 0;


        if (player.PlayerNetworkManager.isRunning.Value)
        {
            player.characterController.Move(moveDirection * runningSpeed * Time.deltaTime);
        }
        else if (PlayerInputManager.instance.isWalking)
        {
            player.characterController.Move(moveDirection * walkingSpeed * Time.deltaTime);
        }
    }

    private Vector3 getMousePos()
    {
        Vector3 direction = Vector3.zero;
        Vector3 objectPosition = transform.position;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
        float rayDistance;

        if (groundPlane.Raycast(ray, out rayDistance))
        {
            Vector3 point = ray.GetPoint(rayDistance);
            direction = point - objectPosition;
            direction.y = 0;
        }

        return direction;
    }

    private void HandleRotation()
    {
        Vector3 mp = Input.mousePosition;
        if (0 > mp.x || 0 > mp.y || Screen.width < mp.x || Screen.height < mp.y)
        {
            return;
        };
        
        if (!player.characterNetworkManager.isRunning.Value)
        {
            Vector3 direction = getMousePos();
            if (direction != Vector3.zero)
            {
                Quaternion toRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, float.MaxValue);
            }
        }

        else if (PlayerInputManager.instance.isWalking)
        {
            targetRotationDirection = Vector3.zero;
            targetRotationDirection = PlayerCamera.instance.cameraObject.transform.forward * verticalMovement;
            targetRotationDirection = targetRotationDirection + PlayerCamera.instance.cameraObject.transform.right * horizontalMovement;
            targetRotationDirection.Normalize();
            targetRotationDirection.y = 0;

            if (targetRotationDirection == Vector3.zero)
            {
                targetRotationDirection = transform.forward;
            }

            Quaternion newRotation = Quaternion.LookRotation(targetRotationDirection);
            Quaternion targetRotation = Quaternion.Slerp(transform.rotation, newRotation, rotationSpeed * Time.deltaTime);
            transform.rotation = targetRotation;
        }
    }

    public void HandleRunning()
    {
        if (player.isPerfomingAction)
        {
            player.PlayerNetworkManager.isRunning.Value = false;
        }

        if (PlayerInputManager.instance.isWalking)
        {
            player.PlayerNetworkManager.isRunning.Value = true;
        }
        else
        {
            player.PlayerNetworkManager.isRunning.Value = false;
        }
    }
}
    
