using UnityEngine;
using System.Collections;

public class Walker : MonoBehaviour {
  public int Facing = -1;
  public float Speed = 50f;
  public float WalkAnimationFrameTime = 0.5f;
  public GameObject IdleModel;
  public GameObject WalkingModel;

  private Rigidbody2D _rigidbody;
  private bool _dead = false;

  void Start() {
    _rigidbody = GetComponent<Rigidbody2D>();
  }

  void FixedUpdate() {
    if (!_dead && (transform.rotation.eulerAngles.z < 45f || transform.rotation.eulerAngles.z > 315f)) {
      Facing = (_rigidbody.velocity.x > 0) ? 1 : -1;
      _rigidbody.velocity = new Vector2(Facing * Speed * Time.fixedDeltaTime, _rigidbody.velocity.y);

      if (Facing == 1) {
        transform.eulerAngles = new Vector3(0f, 0f, 0f);
      } else {
        transform.eulerAngles = new Vector3(0f, 180f, 0f);
      }
    }
  }

  void Update() {
    if (!_dead) {
      Run();
    } else {
      transform.position += new Vector3(0f, 0f, 10f * Time.deltaTime);
    }
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

  public void Die() {
    _dead = true;
    //_rigidbody.Sleep();
    Destroy(GetComponent<BoxCollider2D>());
    Destroy(gameObject, 30f);
  }
}
