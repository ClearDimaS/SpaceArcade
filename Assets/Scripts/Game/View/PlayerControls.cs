using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    [SerializeField]
    protected Joystick joystickMove;

    [SerializeField]
    protected Joystick joystickShoot;

    Vector3 player_Velocity;

    [Tooltip("A number betwenn 0 and 1. Move distance to activate")]
    [SerializeField]
    private float JoyStickMinSensitivity;

    private void OnEnable()
    {
#if UNITY_ANDROID || UNITY_IOS
        joystickMove.gameObject.SetActive(true);
        joystickShoot.gameObject.SetActive(true);
#endif
    }

    private void OnDisable()
    {
        joystickMove.gameObject.SetActive(false);
        joystickShoot.gameObject.SetActive(false);
    }

    public Vector3 InputPlayerMoveSpeed(Transform transform)
    {
        player_Velocity = new Vector3(0, 0, 0);

# if UNITY_STANDALONE
        if (Input.GetKey(KeyCode.W))
        {
            player_Velocity += Vector3.up;
        }
        if (Input.GetKey(KeyCode.S))
        {
            player_Velocity += -Vector3.up;
        }
        if (Input.GetKey(KeyCode.A))
        {
            player_Velocity += -Vector3.right;
        }
        if (Input.GetKey(KeyCode.D))
        {
            player_Velocity += Vector3.right;
        }

        return player_Velocity;
#endif

#if UNITY_ANDROID || UNITY_IOS
        player_Velocity = new Vector3(joystickMove.Horizontal, joystickMove.Vertical, 0);
        return player_Velocity;
#endif
    }

    internal Vector3 GetRotation(Vector3 oldRotation)
    {
#if  UNITY_STANDALONE
        // convert mouse position into world coordinates
        Vector2 mouseScreenPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // get direction you want to point at
        Vector2 direction = (mouseScreenPosition - (Vector2)transform.position).normalized;

        // set vector of transform directly
        return direction;
#endif
#if UNITY_ANDROID || UNITY_IOS
        if (joystickShoot.Direction.magnitude > 0.0f)
            return joystickShoot.Direction;
        else
            return oldRotation;
#endif
    }

    public bool IsFirePressed()
    {
#if  UNITY_STANDALONE
        return Input.GetKey(KeyCode.Mouse0);
#endif

#if UNITY_ANDROID || UNITY_IOS
        return joystickShoot.Direction.magnitude > JoyStickMinSensitivity;
#endif

    }
}
