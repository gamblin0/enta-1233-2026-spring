using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]

public class PlayerController : MonoBehaviour
{
    private Vector2 _input;
    private CharacterController _characterController;
    private Vector3 _direction;

    [SerializeField] private float smoothTime = 0.05f;
    private float _currentVelocity;

    private float _gravity = -9.81f;
    [SerializeField] private float gravityMultiplier = 3.0f;
    private float _velocity;

    [SerializeField] private float speed; //allows us to control speed

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>(); //gets ChracterController Componnent
    }

    private void Update()
    {
        ApplyGravity();
        ApplyRotation();
        ApplyMovement();
        
    }

    private void ApplyRotation()
    {
        if (_input.sqrMagnitude == 0) return; //no keys are beinng touched so we don't move or rotate the character

        var targetAngle = Mathf.Atan2(_direction.x, _direction.z) * Mathf.Rad2Deg; //gets the angle of the player moving. Atan2 gets the direction in radians, so we use Rad2Deg to turn radians into degrees
        var angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref _currentVelocity, smoothTime); //makes the player rotation smooth
        transform.rotation = Quaternion.Euler(0.0f, angle, 0.0f); //
    }


    private void ApplyMovement()
    {
        _characterController.Move(_direction * speed * Time.deltaTime); // allows us to move and makes it depend on frame rate
    }

    private void ApplyGravity()
    {
        if (_characterController.isGrounded && _velocity < 0.0f)
        {
            _velocity = -1.0f;
        }
        else
        {
            _velocity += _gravity * gravityMultiplier * Time.deltaTime;
        }
           
        _direction.y = _velocity;
    }


    public void Move(InputAction.CallbackContext context)
    {
       _input = context.ReadValue<Vector2>();
        _direction = new Vector3(_input.x, 0.0f, _input.y);
    }


}
