using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private CharacterController characterController;
    [SerializeField] private Transform cameraTransform;
    [SerializeField] [Range(1, 10)] private float movementSpeed = 1f;
    [SerializeField] [Range(1, 10)] private float cameraSpeed = 1f;

    private const string HorizontalAxis = "Horizontal";
    private const string VerticalAxis = "Vertical";
    private const string MouseAxisX = "Mouse X";
    private const string MouseAxisY = "Mouse Y";
    private const float Gravity = -9.81f;

    private void Update()
    {
        var xMove = Input.GetAxis(HorizontalAxis);
        var zMove = Input.GetAxis(VerticalAxis);

        var forward = cameraTransform.forward;
        forward.y = 0f;
        forward *= zMove;

        var right = cameraTransform.right;
        right.y = 0f;
        right *= xMove;

        var playerMove = forward.normalized + right.normalized;
        playerMove.y += Gravity;
        characterController.Move(playerMove * Time.deltaTime * movementSpeed);

        var mouseX = Input.GetAxis(MouseAxisX);
        var mouseY = Input.GetAxis(MouseAxisY);

        var rotation = cameraTransform.rotation.eulerAngles;
        rotation.x -= mouseY * cameraSpeed;
        rotation.y += mouseX * cameraSpeed;
        cameraTransform.rotation = Quaternion.Euler(rotation);
    }
}
