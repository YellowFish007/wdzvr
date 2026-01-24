using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Engine;

public class VRInputManager : SingletonGameObject<VRInputManager>
{
    private InputAction m_LeftGripAction;
    private InputAction m_RightGripAction;

    public void Init()
    {
    }

    private void Awake()
    {
        // Initialize Actions for Pico 4 (and other XR controllers)
        // Using "gripPressed" binding which is standard for XR controllers in Unity Input System
        m_LeftGripAction = new InputAction(name: "LeftGrip", type: InputActionType.Button);
        m_LeftGripAction.AddBinding("<XRController>{LeftHand}/gripPressed");
        
        m_RightGripAction = new InputAction(name: "RightGrip", type: InputActionType.Button);
        m_RightGripAction.AddBinding("<XRController>{RightHand}/gripPressed");

        // Subscribe to events
        m_LeftGripAction.performed += OnGripPerformed;
        m_RightGripAction.performed += OnGripPerformed;
    }

    private void OnEnable()
    {
        if (m_LeftGripAction != null) m_LeftGripAction.Enable();
        if (m_RightGripAction != null) m_RightGripAction.Enable();
    }

    private void OnDisable()
    {
        if (m_LeftGripAction != null) m_LeftGripAction.Disable();
        if (m_RightGripAction != null) m_RightGripAction.Disable();
    }

    private void OnGripPerformed(InputAction.CallbackContext context)
    {
        Debug.Log($"[VRInputManager] Grip Pressed! Device: {context.control.device.name}");
        // Send global event
        GameEvent.Send(EventConfig.PICO_GRIP_PRESS);
    }
}
