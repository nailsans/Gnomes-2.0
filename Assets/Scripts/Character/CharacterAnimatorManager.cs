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
    public void UpdateAnimatorMovementParameter(string parameterName, bool value)
    {
        character.animator.SetBool(parameterName, value);
    }
}
