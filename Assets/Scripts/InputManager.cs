using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem; // InputSystem PROVIDES interfaces dan base classes:

public class InputManager : MonoBehaviour
{
    private PlayerInput playerInput;
    public PlayerInput.OnFootActions onFoot;
    private PlayerMotor motor;
    private PlayerLook look;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        playerInput = new PlayerInput(); // Inisialisasi sistem input
        onFoot = playerInput.OnFoot; // Ambil aksi-aksi untuk mode "on foot"

        // Dapatkan komponen dari GameObject
        motor = GetComponent<PlayerMotor>();
        look = GetComponent<PlayerLook>();

        // Ketika tombol jump (yg diset di onFoot) ditekan, maka panggil function Jump dari 'motor'
        onFoot.Jump.performed += ctx => motor.Jump();
    }

    /// <summary>
    /// This function is called every fixed framerate frame, if the MonoBehaviour is enabled.
    /// </summary>
    void FixedUpdate()
    {
        // menyuruh playermotor untuk bergerak menggunakan value dari movement action
        motor.ProcessMove(onFoot.Movement.ReadValue<Vector2>());
    }

    /// <summary>
    /// LateUpdate is called every frame, if the Behaviour is enabled.
    /// It is called after all Update functions have been called.
    /// </summary>
    void LateUpdate()
    {
        // Read the current Look input (mouse -> vector2) and pass it to the camera look logic.”
        look.ProcessLook(onFoot.Look.ReadValue<Vector2>());
    }

    private void OnEnable()
    {
        onFoot.Enable();
    }

    private void OnDisable()
    {
        onFoot.Disable();
    }
}
