using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerInputManager : MonoBehaviour
{
    public static PlayerInputManager instance;
    PlayerInputActions playerInputActions;
    public PlayerManager player;

    [Header("PLAYER MOVEMENT INPUT")]
    [SerializeField] Vector2 movementInput;
    public float horizontalInput;
    public float verticalInput;
    public float moveAmount;

    [Header("PLAYER ACTION INPUT")]
    [SerializeField] bool sprintInput = false;

    private void OnEnable()
    {
        if (playerInputActions == null)
        {
            playerInputActions = new PlayerInputActions();
            playerInputActions.Player.Move.performed += i => movementInput = i.ReadValue<Vector2>();

            //Holding the run button, sets bool to true
            playerInputActions.Player.Run.performed += i => sprintInput = true;

            //Realising the run button, sets bool to false
            playerInputActions.Player.Run.canceled += i => sprintInput = false;
        }

        playerInputActions.Enable();
        
    }

    private void OnDestroy()
    {
        //Unsubscribe from the event
        SceneManager.activeSceneChanged -= OnSceneChange;
    }
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        
    }

    private void Start()
    {
        DontDestroyOnLoad(gameObject);

        //When the scene changes, this logic running
        SceneManager.activeSceneChanged += OnSceneChange;

        instance.enabled = false;
        
    }

    private void Update()
    {
        HandleMovementInput();
        HandleRunning();
    }

    private void OnSceneChange(Scene oldScene, Scene newScene)
    {
        //If we are loading into world scene, enable player controls
        if (newScene.buildIndex == WorldSaveGameManager.instance.GetWorldSceneIndex())
        {
            instance.enabled = true;
        }
        //Otherwise we must be at the main menu, disable player conrols
        else
        {
            instance.enabled = false;
        }
    }

    private void HandleMovementInput()
    {
        verticalInput = movementInput.y;
        horizontalInput = movementInput.x;


        ////Returns always the positive number
        moveAmount = Mathf.Clamp01(Mathf.Abs(verticalInput) + Mathf.Abs(horizontalInput));

        if (moveAmount <= 0.5f && moveAmount > 0)
        {
            moveAmount = 0.5f;
        }
        else if (moveAmount > 0.5f && moveAmount < 1)
        {
            moveAmount = 1;
        }

        if (player == null)
            return;

        if (moveAmount > 0)
        {
            player.PlayerAnimatorManager.UpdateAnimatorMovementParameter("isWalking", true);
        }

        else
        {
            player.PlayerAnimatorManager.UpdateAnimatorMovementParameter("isWalking", false);
        }
    }

    private void HandleRunning()
    {
        if (sprintInput)
        {
            player.PlayerLocomotionManager.HandleRunning();
        }
    }
    //If we minimize or lower the window, stop adjusting inputs
    private void OnApplicationFocus(bool focus)
    {
        if(enabled) {
            
            if (focus)
            {
                playerInputActions.Enable();
            }
            else
            {
                playerInputActions.Disable(); 
            }
        }
    }

}
