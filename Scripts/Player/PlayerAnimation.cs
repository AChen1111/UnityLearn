using System;
using General;
using UnityEngine;

namespace Player
{
   public class PlayerAnimation : MonoBehaviour
   {
      private Animator anim;
      private Rigidbody2D rb;
      private PhysicsCheck physicsCheck;
      private PlayerMove playerMove;
      private void Awake()
      {
         anim = GetComponent<Animator>();
         rb = GetComponent<Rigidbody2D>();
         physicsCheck = GetComponent<PhysicsCheck>();
         playerMove = GetComponent<PlayerMove>();
      }


      private void Update()
      {
         SetAnimation();
      }

      public void SetAnimation()
      {
         anim.SetFloat("velocityX",Math.Abs(rb.velocity.x));
         anim.SetFloat("velocityY", rb.velocity.y);
         anim.SetBool("isGround",physicsCheck.isGround);
         anim.SetBool("isCrouch",playerMove.isCrouch);
      }
   }
}
