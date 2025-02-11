using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//存放有关物理检查方面的代码
public class PhysicsCheck : MonoBehaviour
{
    public float checkRadius;
    public Vector2 bottomOffset;
    public LayerMask groundLayer;
    
    [Header("状态")]
    public bool isGround;
    
    private void Update()
    {
        Check();
    }

    public void Check()
    {
        //检测地面
       isGround = Physics2D.OverlapCircle((Vector2)transform.position+bottomOffset,checkRadius,groundLayer); 
    }

    private void OnDrawGizmosSelected()
    {
       Gizmos.DrawSphere((Vector2)transform.position+bottomOffset,checkRadius);
    }
}
