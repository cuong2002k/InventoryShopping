using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerMovement : MonoBehaviour
{
    private CharacterController controller;
    public InputAction playerControl;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    [SerializeField]private float playerSpeed = 2.0f;
    private float gravityValue = -9.81f;
    public Vector2 Move => playerControl.ReadValue<Vector2>();
    private void Start()
    {
        controller = gameObject.AddComponent<CharacterController>();
    }

    private void OnEnable() {
        playerControl?.Enable();
    }
    private void OnDisable() {
        playerControl?.Disable();
    }

    void Update()
    {
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        Vector3 move = new Vector3(Move.x, 0, Move.y);
        controller.Move(move * Time.deltaTime * playerSpeed);

        if (move != Vector3.zero)
        {
            gameObject.transform.forward = move;
        }
        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }
}
