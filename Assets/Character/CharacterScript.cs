using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class CharacterScript : MonoBehaviour
{
   [SerializeField] private GameObject _target;
   private NavMeshAgent _agent;
   Animator _animator;
   float _speed;
   public float jumpForce = 30f;
   public bool isGrounded = true;
   public LayerMask groundLayer;
   public float groundCheckRadius = 0.1f;
   private float _idleSwitchTime = 5f;
   private float _lastIdleTime;

   private Rigidbody rb;

   private void Start()
   {
      _agent = GetComponent<NavMeshAgent>();
      _agent.SetDestination(_target.transform.position);
      _animator = GetComponent<Animator>();
      rb = GetComponent<Rigidbody>();
      _lastIdleTime = Time.time;
   }

   void Update()
   {

      if (_animator != null)
      {
         _speed = _agent.velocity.magnitude;
         Debug.Log("Speed: " + _speed);
         _animator.SetFloat("Speed", _speed);
      }

      if (_speed < 0.3f)
      {
         _speed = 0f;
      }

      _animator.SetFloat("Speed", _speed);

      isGrounded = Physics.CheckSphere(transform.position - new Vector3(0, 0.5f, 0), groundCheckRadius, groundLayer);
      if (Input.GetKeyDown(KeyCode.Space))
      {
         Jump();
      }

      if (Time.time - _lastIdleTime >= _idleSwitchTime)
      {
         PlayRandomIdleAnimation();
         _lastIdleTime = Time.time;

      }

      void Jump()
      {
         if (_animator != null)
         {
            _animator.SetTrigger("Jump");
         }

         rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
         rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
      }

      void PlayRandomIdleAnimation()
      {

         int randomIdle = Random.Range(0, 2);

         if (randomIdle == 0)
         {
            _animator.SetTrigger("Idle1");
         }
         else
         {
            _animator.SetTrigger("Idle2");
         }
      }

      void OnCollisionEnter(Collision collision)
      {
         if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
         {
            isGrounded = true;
         }

      }
   }
}
