using UnityEngine;
using System.Collections;

public class GoDiscrete : MonoBehaviour {
  public float Delay = 3f;

  float _creationTime = 0f;

  void Start () {
    _creationTime = Time.timeSinceLevelLoad;
  }

  void Update () {
    if (_creationTime + Delay < Time.timeSinceLevelLoad) {
      GetComponent<Rigidbody2D>().collisionDetectionMode = CollisionDetectionMode2D.Discrete;
      Destroy(this);
    }
  }
}
