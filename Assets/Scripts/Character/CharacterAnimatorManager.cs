using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimatorManager : MonoBehaviour
{
    CharacterManager character;

    protected virtual void Awake()
    {
        character = GetComponent<CharacterManager>();
    }
    public void UpdateAnimatorParameter(string parameterName, bool value)
    {

        if (character.characterNetworkManager.isRunning.Value)
        {
            character.animator.SetBool("isRunning", true);
        }
        else
        {
            character.animator.SetBool(parameterName, value);
            character.animator.SetBool("isRunning", false);
        }
    }


}
