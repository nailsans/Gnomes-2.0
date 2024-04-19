using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : CharacterManager
{

    public PlayerLocomotionManager PlayerLocomotionManager;
    public PlayerAnimatorManager PlayerAnimatorManager;
    public PlayerNetworkManager PlayerNetworkManager;
    protected override void Awake()
    {
        base.Awake();

        PlayerLocomotionManager = GetComponent<PlayerLocomotionManager>();
        PlayerAnimatorManager = GetComponent<PlayerAnimatorManager>();
        PlayerNetworkManager = GetComponent<PlayerNetworkManager>();
    }

    protected override void Update()
    {
        base.Update();

        //If we dont own this game object, we dont control or edit it
        if (!IsOwner)
        {
            return;
        }

        PlayerLocomotionManager.HandleAllMovement();
    }

    protected override void LateUpdate()
    {
        if (!IsOwner) {
            return;
        }
        base.LateUpdate();

        PlayerCamera.instance.HandleAllCameraActions();
    }

    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();

        if (IsOwner) {
            PlayerCamera.instance.player = this;
            PlayerInputManager.instance.player = this;
        }
    }
}
