using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
   private CharacterController _controller;
   private Vector3 _move;

   
   public static float forwardSpeed;
   [SerializeField]
   private float _maxSpeed;

   private int _desiredLane = 1;//0:left, 1:middle, 2:right

   [SerializeField]
   private float _laneDistance = 0.9f;//The distance between two lanes

   public bool isGrounded;
   public LayerMask groundLayer;
   public Transform groundCheck;
  
  

   [SerializeField]
   private float _gravity = -12f;
   
   public static float jumpHeight = 1;
   private Vector3 _velocity;

   [SerializeField]
   private Animator _animator;
   private bool _isSliding = false;

   public float slideDuration = 1.5f;

   bool toggle = false;

  

   void Start()
   {
      _controller = GetComponent<CharacterController>();
      Time.timeScale = 1.2f;
   }

   private void FixedUpdate()
   {
      if (!PlayerManager.isGameStarted || PlayerManager.gameOver)
         return;

      //Increase Speed
      if (toggle)
      {
         toggle = false;
         if (forwardSpeed < _maxSpeed)
            forwardSpeed += 0.1f * Time.fixedDeltaTime;
      }
      else
      {
         toggle = true;
         if (Time.timeScale < 2f)
            Time.timeScale += 0.005f * Time.fixedDeltaTime;
      }
   }

   void Update()
   {

      if (!PlayerManager.isGameStarted || PlayerManager.gameOver)
         return;
      //animator.SetBool("isGameStarted", true);
      _move.z = forwardSpeed;


      isGrounded = Physics.CheckSphere(groundCheck.position, 0.17f, groundLayer);
      _animator.SetBool("isGrounded", isGrounded);
      if (isGrounded && _velocity.y < 0)
         _velocity.y = -1f;

      if (isGrounded)
      {
         if (SwipeManager.swipeUp)
            Jump();

         if (SwipeManager.swipeDown && !_isSliding)
            StartCoroutine(Slide());
      }
      else
      {
         _velocity.y += _gravity * Time.deltaTime;
         if (SwipeManager.swipeDown && !_isSliding)
         {
            StartCoroutine(Slide());
            _velocity.y = -10;
         }

      }
      _controller.Move(_velocity * Time.deltaTime);

      //Check the inputs on which lane we should be
      if (SwipeManager.swipeRight)
      {
         _desiredLane++;
         if (_desiredLane == 3)
            _desiredLane = 2;
      }
      if (SwipeManager.swipeLeft)
      {
         _desiredLane--;
         if (_desiredLane == -1)
            _desiredLane = 0;
      }

      //Calculate where we should be in the future
      Vector3 targetPosition = transform.position.z * transform.forward + transform.position.y * transform.up;
      if (_desiredLane == 0)
         targetPosition += Vector3.left * _laneDistance;
      else if (_desiredLane == 2)
         targetPosition += Vector3.right * _laneDistance;

      //transform.position = targetPosition;
      if (transform.position != targetPosition)
      {
         Vector3 diff = targetPosition - transform.position;
         Vector3 moveDir = diff.normalized * 20 * Time.deltaTime;
         if (moveDir.sqrMagnitude < diff.magnitude)
            _controller.Move(moveDir);
         else
            _controller.Move(diff);
      }

     

      _controller.Move(_move * Time.deltaTime);
   }

   private void Jump()
   {
      StopCoroutine(Slide());
      _animator.SetBool("isSliding", false);
      _animator.SetTrigger("JumpOver");
      _controller.center = new Vector3(0, 0.48f, 0);
      _controller.height = 0.92f;
      _isSliding = false;

      _velocity.y = Mathf.Sqrt(jumpHeight * 1 * -_gravity);
   }

   private void OnControllerColliderHit(ControllerColliderHit hit)
   {
      if (hit.transform.tag == "Obstacle")
      {
         _animator.SetBool("Stumble", true);

         //StartCoroutine(ShieldOff());
         //if (PlayerManager.shieldOn == false)
         {
            StartCoroutine(Death());
            PlayerManager.gameOver = true;
            //_speed = 0;
         }
      }

      
   }

   private IEnumerator Slide()
   {
      _isSliding = true;
      _animator.SetBool("isSliding", true);
      yield return new WaitForSeconds(0.25f / Time.timeScale);
      _controller.center = new Vector3(0, -0.1f, 0);
      _controller.height = 0.3f;

      yield return new WaitForSeconds((slideDuration - 0.25f) / Time.timeScale);

      _animator.SetBool("isSliding", false);

      _controller.center = new Vector3(0,0.48f,0);
      _controller.height = 0.92f;

      _isSliding = false;
   }
   IEnumerator Death()
   {
      yield return new WaitForSeconds(2.0f);
      forwardSpeed = 0;
      Destroy(gameObject);
   }
   IEnumerator ShieldOff()
   {
      yield return new WaitForSeconds(0.3f);
      PlayerManager.shieldOn = false;
      PlayerManager.shieldCount--;
   }
}
