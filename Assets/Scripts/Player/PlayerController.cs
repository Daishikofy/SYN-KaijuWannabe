using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Movement variables")]
    [SerializeField]
    private float velocity = 2.0f;
    [SerializeField]
    private float dashDuration = 1.0f;
    [SerializeField]
    private float dashCooldown = 1.0f;

    [Header("Player Components")]
    [SerializeField]
    private Rigidbody rb;
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private Collider _collider;

    #region Movement variables
    private Vector2 movementInput;
    private Vector3 movementDirection;
    private Vector3 playerMovement;

    private Quaternion targetRotation = new Quaternion(0, 1, 0, 0);
    #endregion

    private bool canDash = true;
    private float dashTimer;
    private float dashCooldownTimer;

    #region GameState variables
    public int playerLevel = 0;
    #endregion

    private void OnValidate()
    {
        if (rb == null)
        {
            rb = GetComponent<Rigidbody>();
        }

        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }

        if (_collider == null)
        {
            _collider = GetComponent<Collider>();
        }
    }

    public void Move(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>();
        if (dashTimer > 0.0)
        {
            return;
        }
        movementDirection.x = movementInput.x;
        movementDirection.z = movementInput.y;
        if (movementInput.x != 0 || movementInput.y != 0)
        {
            animator.SetBool("IsWalking", true);
            targetRotation = Quaternion.LookRotation(movementDirection, Vector3.up);
        }
        else
        {
            animator.SetBool("IsWalking", false);
        }
    }

    public void Attack(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (canDash)
            {
                if (movementInput.x != 0 || movementInput.y != 0)
                {
                    canDash = false;
                    dashTimer = dashDuration;
                    velocity *= 5;
                }
                else
                {
                    animator.SetTrigger("OnAttack");
                }                  
            }
        }
            
    }

    public void Eat()
    {
        transform.localScale += Vector3.one * 0.1f;
        animator.SetTrigger("OnEat");

        GameManager.instance.AddToScore(1);
        int newLevel = (int)(_collider.bounds.size.x * _collider.bounds.size.y * _collider.bounds.size.z);
        if (newLevel != playerLevel)
        {
            GameManager.instance.UpdatePlayerLevel(newLevel);
            playerLevel = newLevel;
        }

    }

    public void Update()
    {
        if (dashTimer > 0.0f)
        {
            dashTimer -= Time.deltaTime;
            if (dashTimer <= 0.0f)
            {
                velocity /= 5;
                dashCooldownTimer = dashCooldown;
            }
        }
        else if (dashCooldownTimer > 0.0f)
        {
            dashCooldownTimer -= Time.deltaTime;
            if (dashCooldownTimer <= 0.0f)
            {
                canDash = true;
            }
        }
    }

    public void FixedUpdate()
    {
        playerMovement = Time.fixedDeltaTime * velocity * movementDirection * 100;
        rb.AddForce(playerMovement, ForceMode.Force);
        rb.rotation = Quaternion.Slerp(rb.rotation, targetRotation.normalized, 0.2f);       
    }


}
