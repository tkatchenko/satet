using UnityEngine;
using System.Collections;

public class Screenshot : MonoBehaviour {

  // Update is called once per frame
  void Update() {
    if (Input.GetKey(KeyCode.Space)) {
      Application.CaptureScreenshot(System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss") + ".png");
    }
  }
}
