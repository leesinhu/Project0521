using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] bool isReverse = false;

    float moveSpeed = 8f;
    float jumpForce = 18f;
    Transform groundCheck;
    public LayerMask groundLayer;
    private Rigidbody rb;
    public bool isGrounded { get; set; }

    [SerializeField] float coyoteTimeDuration = 0.15f;  // 코요테 타임 지속시간
    [SerializeField] float jumpBufferDuration = 0.15f;  // 점프버퍼 지속시간
    float coyoteTimeCounter = 0f;
    float jumpBufferCounter = 0f;

    public InputManager inputManager { get; set; }
    public float movement { get; set; }
    public bool jumpFlag { get; set; }

    private void Awake()
    {
        inputManager = GameManager.Instance.inputManager;
        inputManager.OnInput += HandleInput;

        groundCheck = transform.GetChild(0);
    }
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        /*if(isReverse)
        {
            rb.velocity = new Vector3(moveSpeed * (-1), rb.velocity.y, 0f);
        }*/
    }

    void Update()
    {
 /*       // 점프 입력
        if (jumpFlag && isGrounded)
        {
            jumpBufferCounter = jumpBufferDuration;
        }
        else
        {
            jumpBufferCounter -= Time.deltaTime;
        }

        if (isGrounded)
        {
            coyoteTimeCounter = coyoteTimeDuration; // 착지 시 코요테 타임 초기화
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime;  // 공중에 있으면 카운터 감소
        }*/
    }

    private void FixedUpdate()
    {
        if (jumpFlag && isGrounded)
        {
            rb.velocity = new Vector3(rb.velocity.x, 0f, 0f);
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }

        // 좌우 이동
        if (isGrounded)
        {
            //좌우 이동
            rb.velocity = new Vector3(movement * moveSpeed, rb.velocity.y, 0f);
        }
        else
        {
            float currentX = rb.velocity.x;
            if (movement != 0 && Mathf.Sign(movement) != Mathf.Sign(currentX))
            {
                float deceleration = 12.5f;
                float newX = Mathf.MoveTowards(currentX, 0, deceleration * Time.fixedDeltaTime);
                rb.velocity = new Vector3(newX, rb.velocity.y, 0f);
            }
        }
    }

    private void HandleInput(object sender, InputEventArgs args)
    {
        switch (args.inputType)
        {
            case InputType.MoveLeft:
                movement = isReverse ? 1 : -1;
                break;
            case InputType.MoveRight:
                movement = isReverse ? -1 : 1;
                break;
            case InputType.MoveStop:
                movement = 0;
                break;
            case InputType.Jump:
                jumpFlag = true;
                break;
            case InputType.JumpEnd:
                jumpFlag = false;
                break;
        }
    }
}
