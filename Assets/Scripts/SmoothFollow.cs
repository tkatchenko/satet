using UnityEngine;
using System.Collections;
using Prime31;


public class SmoothFollow : MonoBehaviour {
  public Transform target;
  public float smoothDampTime = 0.2f;
  [HideInInspector]
  public new Transform transform;
  public Vector3 cameraOffset;
  public bool useFixedUpdate = false;
	
  private CharacterController2D _playerController;
  private Vector3 _smoothDampVelocity;

	
  void Awake() {
    transform = gameObject.transform;
    _playerController = target.GetComponent<CharacterController2D>();
  }

	
  void LateUpdate() {
    if (!useFixedUpdate)
      updateCameraPosition();
  }


  void FixedUpdate() {
    if (useFixedUpdate)
      updateCameraPosition();
  }


  void updateCameraPosition() {
    float offsetY = 5f;

    if (_playerController == null) {
      transform.position = Vector3.SmoothDamp(transform.position, new Vector3(target.position.x, target.position.y + offsetY, target.position.z) - cameraOffset, ref _smoothDampVelocity, smoothDampTime);
      return;
    }
		
    if (_playerController.velocity.x > 0) {
      transform.position = Vector3.SmoothDamp(transform.position, new Vector3(target.position.x, target.position.y + offsetY, target.position.z) - cameraOffset, ref _smoothDampVelocity, smoothDampTime);
    } else {
      var leftOffset = cameraOffset;
      leftOffset.x *= -1;
      transform.position = Vector3.SmoothDamp(transform.position, new Vector3(target.position.x, target.position.y + offsetY, target.position.z) - leftOffset, ref _smoothDampVelocity, smoothDampTime);
    }
  }
	
}
