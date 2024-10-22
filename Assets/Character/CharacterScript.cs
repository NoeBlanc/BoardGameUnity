using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CharacterScript : MonoBehaviour
{
   [SerializeField] private GameObject _target;
   private NavMeshAgent _agent;
   Animator _animator;
   float _speed;

   private void Start()
   {
      _agent = GetComponent<NavMeshAgent>();
      _agent.SetDestination(_target.transform.position);
      _animator = GetComponent<Animator>();
   }

   void Update()
   {
      if (_animator != null)
      {
         _speed = _agent.velocity.magnitude;
         _animator.SetFloat("speed", _speed);  
      }
   }
}
