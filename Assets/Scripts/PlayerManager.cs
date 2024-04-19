using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    //запус всех скриптов игрока
    private InputManager inputManager;
    private PlayerMotion playerMotion;


    private void Awake()
    {
        inputManager = GetComponent<InputManager>();
        playerMotion = GetComponent<PlayerMotion>();
    }

    private void Update()
    {
        inputManager.HandleAllInputs();   
    }

    private void FixedUpdate()
    {
        playerMotion.HandleAllMovement();
    }
}
