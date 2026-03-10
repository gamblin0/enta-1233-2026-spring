using System.Collections;
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

    [SerializeField] private float jumpPower;

    private int _numberOfjumps;
    [SerializeField] private int maxNumberOfJumps = 2;

    [SerializeField] private Health _health;


    [SerializeField] private Animator _animator;
    private static readonly int Speed = Animator.StringToHash("Speed");

   // private static readonly int VerticalVelocity = Animator.StringToHash("VerticalVelocity");
    private static readonly int IsGroundedAnim = Animator.StringToHash("IsGrounded");
    private static readonly int JumpReq = Animator.StringToHash("JumpReq");
    private bool _isJumping;


    private void Awake()
    {
       if (_characterController == null) _characterController = GetComponent<CharacterController>(); //gets ChracterController Componnent
       if(_health == null) _health = GetComponent<Health>();

    }

    private void OnEnable()
    {
        if (_health != null)
        {
            _health.OnDamaged += HandleDamaged;
            _health.OnDied += HandleDied;
        }
        
    }

    private void OnDisable()
    {
        if (_health != null)
        {
            _health.OnDamaged -= HandleDamaged;
            _health.OnDied -= HandleDied;
        }
    }

   

    private void Update()
    {
        ApplyGravity();
        ApplyRotation();
        ApplyMovement();
        AnimationParameters();
        
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
        if (IsGrounded() && _velocity < 0.0f)
        {
            _velocity = -1.0f;
        }
        else
        {
            _velocity += _gravity * gravityMultiplier * Time.deltaTime;
        }
           
        _direction.y = _velocity;
    }

    private void AnimationParameters()
    {
        _animator?.SetFloat(Speed, _input.sqrMagnitude);
        //_animator?.SetFloat(VerticalVelocity, _velocity);
        _animator?.SetBool(IsGroundedAnim, IsGrounded());
        if (_isJumping) _animator?.SetTrigger(JumpReq);
        _isJumping = false;
    }


    public void Move(InputAction.CallbackContext context)
    {
       _input = context.ReadValue<Vector2>();
        _direction = new Vector3(_input.x, 0.0f, _input.y);
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (!context.started) return; //when pressed space then jump if not don't
        if (!IsGrounded() && _numberOfjumps >= maxNumberOfJumps) return; //if in the air and already max jumped don't jump
        if (_numberOfjumps == 0) StartCoroutine(WaitForLanding());

        _isJumping = true;
        _numberOfjumps++;
        _velocity = jumpPower; //makes character jump
    }

    public void Attack(InputAction.CallbackContext context)
    {
        if (!context.started) return;
        _animator?.SetTrigger("Attack");
        
    }

    private IEnumerator WaitForLanding() //after landing, reset the jump amount so player can jump again
    {
        yield return new WaitUntil(() => !IsGrounded()); //wait until chracter is in the air
        yield return new WaitUntil(IsGrounded);  //wait until character is on the ground

        _numberOfjumps = 0;
    }

    private bool IsGrounded() => _characterController.isGrounded; //we can call IsGrounded instead of writing _characterController.isGrounded


    private void HandleDamaged(DamageInfo info)
    {
        Debug.Log(
           $"[Dummy] Hit by" +
           $"{info.Source?.name ?? "Unknown"} " +
           $"for {info.Amount} damage" +
           $"HP: {_health.CurrentHealth}/{_health.MaxHealth}");
        if (_health.CurrentHealth > 0)
            _animator?.SetTrigger("Hit");
    }
    private void HandleDied()
    {
        Debug.Log("[Dummy] Died! Resetting..");
        _animator?.SetTrigger("Die");
        _characterController = null;
        enabled = false;
        StartCoroutine(GameOverTransition());
    }

    private IEnumerator GameOverTransition()
    {
        yield return new WaitForSeconds(1f);

        GameMgr.Instance.GameOver();
    }
}
