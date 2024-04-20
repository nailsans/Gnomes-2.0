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
    public bool isWalking;

    [Header("PLAYER ACTION INPUT")]
    [SerializeField] bool sprintInput = false;
    [SerializeField] bool attackInput = false;

    private void OnEnable()
    {
        if (playerInputActions == null)
        {
            playerInputActions = new PlayerInputActions();
            playerInputActions.Player.Move.performed += i => movementInput = i.ReadValue<Vector2>();

            ///Holding the run button, sets bool to true
            playerInputActions.Player.Run.performed += i => sprintInput = true;

            ///Realising the run button, sets bool to false
            playerInputActions.Player.Run.canceled += i => sprintInput = false;

            ///attack button
            playerInputActions.Player.Attack.performed += i => attackInput = true;
            playerInputActions.Player.Attack.canceled += i => attackInput = false;
        }

        playerInputActions.Enable();
        
    }

    private void OnDestroy()
    {
        ///Unsubscribe from the event
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

        ///When the scene changes, this logic running
        SceneManager.activeSceneChanged += OnSceneChange;

        instance.enabled = false;
        
    }

    private void Update()
    {
        HandleMovementInput();
        HandleRunning();
        HandleAttack();
    }

    private void OnSceneChange(Scene oldScene, Scene newScene)
    {
        ///If we are loading into world scene, enable player controls
        if (newScene.buildIndex == WorldSaveGameManager.instance.GetWorldSceneIndex())
        {
            instance.enabled = true;
        }
        ///Otherwise we must be at the main menu, disable player conrols
        else
        {
            instance.enabled = false;
        }
    }

    private void HandleMovementInput()
    {
        verticalInput = movementInput.y;
        horizontalInput = movementInput.x;

        if (verticalInput != 0 | horizontalInput != 0)
        {
            isWalking = true;
        }

        else
        {
            isWalking = false;
        }

        

        if (player == null)
            return;

        if (isWalking)
        {
            player.PlayerAnimatorManager.UpdateAnimatorParameter("isWalking", true);
        }

        else
        {
            player.PlayerAnimatorManager.UpdateAnimatorParameter("isWalking", false);
        }
    }

    private void HandleRunning()
    {
        if (sprintInput)
        {
            player.PlayerLocomotionManager.HandleRunning();
        }

        else
        {
            player.PlayerNetworkManager.isRunning.Value = false;
        }
    }

    private void HandleAttack()
    {
        if (attackInput)
        {
            player.PlayerAttack.Attack();
        }
    }

    /// <summary>
    /// If we minimize or lower the window, stop adjusting inputs
    /// </summary>
    /// <param name="focus"></param>
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
