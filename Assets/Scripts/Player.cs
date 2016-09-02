using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using Prime31;

public class Player : MonoBehaviour {
  // movement config
  public float gravity = -25f;
  public float runSpeed = 8f;
  public float groundDamping = 20f;
  // how fast do we change direction? higher means faster
  public float inAirDamping = 5f;
  public float jumpHeight = 3f;
  public GameObject Arrow;
  public float ArrowForceX = 200f;
  public float ArrowForceY = 12.5f;
  public float WalkAnimationFrameTime = 0.5f;
  public GameObject IdleModel;
  public GameObject WalkingModel;
  public AudioSource JumpSound;
  public AudioSource FireArrowSound;

  [HideInInspector]
  private float normalizedHorizontalSpeed = 0;

  private float _facing = 1f;
  private CharacterController2D _controller;
  private RaycastHit2D _lastControllerColliderHit;
  private Vector3 _velocity;


  void Awake() {
    _controller = GetComponent<CharacterController2D>();

    // listen to some events for illustration purposes
    _controller.onControllerCollidedEvent += onControllerCollider;
    _controller.onTriggerEnterEvent += onTriggerEnterEvent;
    _controller.onTriggerExitEvent += onTriggerExitEvent;

    IdleModel.SetActive(false);
  }

  #region Event Listeners

  void onControllerCollider(RaycastHit2D hit) {
    // bail out on plain old ground hits cause they arent very interesting
    if (hit.normal.y == 1f)
      return;

    // logs any collider hits if uncommented. it gets noisy so it is commented out for the demo
    //Debug.Log( "flags: " + _controller.collisionState + ", hit.normal: " + hit.normal );
  }


  void onTriggerEnterEvent(Collider2D collider) {
    if (collider.gameObject.layer == LayerMask.NameToLayer("Death")) {
      Die();
    }

    if (collider.gameObject.tag == "Enemy") {
      Die();
    }
  }

  void onTriggerExitEvent(Collider2D collider) {
  }

  #endregion

  void Update() {
    if (_controller.isGrounded)
      _velocity.y = 0;

    if (Input.GetKey(KeyCode.RightArrow)) {
      _facing = 1;
      normalizedHorizontalSpeed = 1;
      if (transform.localScale.x < 0f)
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);

      if (_controller.isGrounded) {
        Run();
      }
    } else if (Input.GetKey(KeyCode.LeftArrow)) {
      _facing = -1;
      normalizedHorizontalSpeed = -1;
      if (transform.localScale.x > 0f)
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);

      if (_controller.isGrounded) {
        Run();
      }
    } else {
      normalizedHorizontalSpeed = 0;

      if (_controller.isGrounded) {
        IdleModel.SetActive(true);
        WalkingModel.SetActive(false);
      }
    }

    // Shoot arrow
    if (Input.GetKeyDown(KeyCode.X)) {
      GameObject arrow = Instantiate(Arrow, new Vector2(transform.position.x + (_facing * 1f), transform.position.y + 0.75f), transform.rotation) as GameObject;
      arrow.GetComponent<Rigidbody2D>().AddForce(new Vector2(_facing * ArrowForceX, ArrowForceY));
      FireArrowSound.Play();
    }

    // Jump
    if (_controller.isGrounded && Input.GetKeyDown(KeyCode.Z)) {
      _velocity.y = Mathf.Sqrt(2f * jumpHeight * -gravity);
      IdleModel.SetActive(false);
      WalkingModel.SetActive(true);
      JumpSound.Play();
    }

    // Stop jump if not held
    if (!_controller.isGrounded && !Input.GetKey(KeyCode.Z) && _velocity.y > 0f) {
      _velocity.y = Mathf.Lerp(_velocity.y, 0f, Time.deltaTime * inAirDamping);
    }

    // Apply horizontal speed smoothing
    var smoothedMovementFactor = _controller.isGrounded ? groundDamping : inAirDamping; // how fast do we change direction?
    _velocity.x = Mathf.Lerp(_velocity.x, normalizedHorizontalSpeed * runSpeed, Time.deltaTime * smoothedMovementFactor);

    // Apply gravity
    _velocity.y += gravity * Time.deltaTime;

    // Jump down through one way platforms
    if (_controller.isGrounded && Input.GetKey(KeyCode.DownArrow)) {
      _velocity.y *= 3f;
      _controller.ignoreOneWayPlatformsThisFrame = true;
    }

    _controller.move(_velocity * Time.deltaTime);

    // Get current velocity to use for calculations
    _velocity = _controller.velocity;
  }

  void Die() {
    SceneManager.LoadScene("Level"); 
  }

  void Run() {
    if (Time.timeSinceLevelLoad / WalkAnimationFrameTime % 2 > 1f) {
      IdleModel.SetActive(false);
      WalkingModel.SetActive(true);
    } else {
      IdleModel.SetActive(true);
      WalkingModel.SetActive(false);
    }
  }
}
