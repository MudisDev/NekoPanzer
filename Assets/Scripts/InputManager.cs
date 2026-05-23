using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static InputManager sharedInstance;

    [Header("Touch Controls")]
    [SerializeField] private FixedJoystick movementJoystick;
    [SerializeField] private FixedJoystick turretJoystick;

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
        // Input del mando/teclado
        Vector2 gamepadInput = input.Gameplay.Move.ReadValue<Vector2>();

        // Si hay input del mando, usarlo
        if (gamepadInput.magnitude > 0.1f)
            return gamepadInput;

        // Si no, usar joystick táctil
        if (movementJoystick != null)
        {
            Vector2 touchInput = new Vector2(
                movementJoystick.Horizontal,
                movementJoystick.Vertical
            );

            return touchInput;
        }

        return Vector2.zero;
    }
    public Vector2 GetTurretMovement()
    {
        // Input del mando/teclado
        Vector2 gamepadInput = input.Gameplay.TurretMove.ReadValue<Vector2>();

                if (gamepadInput.magnitude > 0.1f)
                    return gamepadInput;

                if (turretJoystick != null)
                {
                    Vector2 touchInput = new Vector2(
                        turretJoystick.Horizontal,
                        turretJoystick.Vertical
                    );

                    return touchInput;
                }

                return Vector2.zero;

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