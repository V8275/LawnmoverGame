using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float speed = 1.0f;

    private Rigidbody rigidBody;
    private InputAction moveAction;

    private void Start()
    {
        moveAction = InputSystem.actions.FindAction("Move");
        rigidBody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        Vector2 inputVector = moveAction.ReadValue<Vector2>();

        Vector3 moveValue = new Vector3(inputVector.x, 0, inputVector.y).normalized;

        rigidBody.MovePosition(rigidBody.position + moveValue * speed * Time.fixedDeltaTime);
    }
}
