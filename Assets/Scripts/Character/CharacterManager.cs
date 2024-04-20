using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Unity.Netcode;

public class CharacterManager : NetworkBehaviour
{   
    public CharacterController characterController;
    public Animator animator;

    public CharacterNetworkManager characterNetworkManager;


    /// <summary>
    /// Is character perfoming action like attacking etc
    /// </summary>
    public bool isPerfomingAction = false;
    protected virtual void Awake()
    {
        DontDestroyOnLoad(this);

        characterController = GetComponent<CharacterController>();
        characterNetworkManager = GetComponent<CharacterNetworkManager>();
        animator = GetComponentInChildren<Animator>();
    }

    protected virtual void Update()
    {
        if (IsOwner)
        {
            ///If we own the character, then put some values into network variables
            characterNetworkManager.networkPosition.Value = transform.position;
            characterNetworkManager.networkRotation.Value = transform.rotation;
        }
        else
        {
            ///If we dont own the character, then take variables from network


            ///Position
            transform.position = Vector3.SmoothDamp(
                transform.position,
                characterNetworkManager.networkPosition.Value,
                ref characterNetworkManager.networkPositionVelocity,
                characterNetworkManager.networkPositionSmoothTime);
            ///Rotation
            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                characterNetworkManager.networkRotation.Value,
                characterNetworkManager.networkRotationSmoothTime);
        }
    }

    protected virtual void LateUpdate()
    {

    }

}
