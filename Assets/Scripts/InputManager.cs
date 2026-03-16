using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static InputManager sharedInstance;

    private GameInput input;

    private void Awake()
    {
        if (sharedInstance == null)
            sharedInstance = this;
        else
            Destroy(gameObject);

        input = new GameInput();
    }

    private void Update()
    {
        //Debug.Log("Stick Input: " + input.Gameplay.Move.ReadValue<Vector2>());
        //Debug.Log("Turret Stick Input: " + input.Gameplay.TurretMove.ReadValue<Vector2>());
    }

    private void OnEnable()
    {
        input.Enable();
    }

    private void OnDisable()
    {
        input.Disable();
    }

    public Vector2 GetMovement()
    {
        return input.Gameplay.Move.ReadValue<Vector2>();
    }
    public Vector2 GetTurretMovement()
    {
        return input.Gameplay.TurretMove.ReadValue<Vector2>();
    }
    /*
        public bool GetJumpButton()
        {
            return input.Gameplay.Jump.triggered;
        } */

    public bool GetAttackButton()
    {
        return input.Gameplay.Attack.triggered;
    }

    public bool GetPauseButton()
    {
        return input.Gameplay.Pause.triggered;
    }

    /* public bool GetAnimationTestButton(){
        return input.Gameplay.AnimationTest.triggered;
    }  */

    public bool GetActionButton()
    {
        return input.Gameplay.Action.triggered;
    }

}


/* public bool GetAnimationTestButton(){
        return input.Gameplay.AnimationTest.triggered;
    } */