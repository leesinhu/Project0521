using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum InputType
{
    MoveLeft,
    MoveRight,
    MoveStop,
    Jump,
    JumpEnd
}

public class InputEventArgs : EventArgs
{
    public InputType inputType;

    public InputEventArgs(InputType type)
    {
        inputType = type;
    }
}

public class InputManager : MonoBehaviour
{
    public event EventHandler<InputEventArgs> OnInput;

    private void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");

        if (horizontal == -1)
        {
            OnInput?.Invoke(this, new InputEventArgs(InputType.MoveLeft));
        }
        else if (horizontal == 1)
        {
            OnInput?.Invoke(this, new InputEventArgs(InputType.MoveRight));
        }
        else
        {
            OnInput?.Invoke(this, new InputEventArgs(InputType.MoveStop));
        }

        if (Input.GetKey(KeyCode.LeftAlt) || Input.GetKey(KeyCode.RightAlt))
        {
            OnInput?.Invoke(this, new InputEventArgs(InputType.Jump));
        }
        else
        {
            OnInput?.Invoke(this, new InputEventArgs(InputType.JumpEnd));
        }
    }
}
