using UnityEngine;
using System.Collections;

public class SelfHide : MonoBehaviour {
  public float HideDelay = 3f;

  float _creationTime = 0f;

  void Start() {
    _creationTime = Time.timeSinceLevelLoad;
  }

  void OnEnable() {
    _creationTime = Time.timeSinceLevelLoad;
  }
	
  // Update is called once per frame
  void Update() {
    if (_creationTime + HideDelay < Time.timeSinceLevelLoad) {
      gameObject.SetActive(false);
    }
  }
}
