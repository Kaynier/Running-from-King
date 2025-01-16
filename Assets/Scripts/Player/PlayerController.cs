
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float playerSpeed = 10f;
    [SerializeField] float xClamp = 3f;
    [SerializeField] float zClamp = 3f;
    Vector2 movement;
    Rigidbody rb;

    void Awake(){
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate(){
        HandleMovement();
    }
    public void Move(InputAction.CallbackContext context){
        movement = context.ReadValue<Vector2>();
    }
    void HandleMovement(){
        Vector3 currentPosition = rb.position;
        Vector3 moveDirection = new Vector3(movement.x, 0f, movement.y);
        Vector3 newPos = currentPosition + moveDirection * (playerSpeed * Time.fixedDeltaTime);

        newPos.x = Mathf.Clamp(newPos.x, -xClamp, xClamp);
        newPos.z = Mathf.Clamp(newPos.z, -zClamp, zClamp);
        rb.MovePosition(newPos);
    }
}
