using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
//存放与玩家按键有关的操作

public class PlayerMove : MonoBehaviour
{
    public PlayerControl PlayerInput;
    public Vector2 InputVector2;
    
    
    private Rigidbody2D rb;
    private PhysicsCheck physicsCheck;
    private CapsuleCollider2D coll;
    //问题 连按跳跃会起飞
    //解决方法限制跳跃次数
    private bool isJumping = false;
    //额.....虽然减少了触发频率但依然在特定的地点偶尔的起飞 .......无语了
    
    
    
    [Header("基本参数")]
    public float speed;
    public float jumpForce;
    public bool isCrouch;
    
    private Vector2 originalOffset;//原始位移
    private Vector2 originalSize;//原始尺寸
   
    private float runSpeed;
    
    //1.踩坑不能直接定义 runSpeed => speed
    //因为在按下shit后会直接把speed改成walkSpeed
    
    private float walkSpeed => speed/2.5f;

    private void Awake()
    {
        PlayerInput = new PlayerControl();
        rb = GetComponent<Rigidbody2D>();
        physicsCheck = GetComponent<PhysicsCheck>();   
        coll = GetComponent<CapsuleCollider2D>();   
        
        originalOffset = coll.offset;
        originalSize = coll.size;
        
        PlayerInput.Player.Jump.started += Jump;
        runSpeed = speed;
        //1.正确写法

        #region 走动与跑动
        PlayerInput.Player.WalkButton.performed += ctx =>
        {
            if (physicsCheck.isGround)
            {
                speed = walkSpeed;
            }
        };
        PlayerInput.Player.WalkButton.canceled += ctx =>
        {
            speed = runSpeed;
        };
        
        #endregion
    }


    private void OnEnable()
    {
        PlayerInput.Enable();
    }

    private void OnDisable()
    {
        PlayerInput.Disable();
    }

    private void Update()
    {
        if (physicsCheck.isGround)
        {
            isJumping = false;
        }
        
        InputVector2 = PlayerInput.Player.Move.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        Move(); 
    }

    public void Move()
    {
        //主要的移动方法
        if (!isCrouch)
        {
        
            rb.velocity = new Vector2(InputVector2.x * speed * Time.deltaTime, rb.velocity.y);
        }

        #region  人物反转

        int faceDir = (int)transform.localScale.x;
        if(InputVector2.x > 0) faceDir = 1;
        else if(InputVector2.x < 0) faceDir = -1;
        transform.localScale = new Vector3(faceDir,1,1);

        #endregion

        #region 下蹲处理
        
        isCrouch = InputVector2.y < -0.05f && physicsCheck.isGround;
        if (isCrouch)
        {
            coll.offset = new Vector2(-0.05f,0.85f);
            coll.size = new Vector2(0.7f,1.7f);
           //修改碰撞体大小和位移
        }
        else
        {
            coll.offset = originalOffset;
            coll.size = originalSize;   
            //还原
        }
        #endregion

    }
    
    private void Jump(InputAction.CallbackContext obj)
    {
        //Debug.Log("Jump");
        if(physicsCheck.isGround && !isJumping)
        rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
    }
    
    

}
