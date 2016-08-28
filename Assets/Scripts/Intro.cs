using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Intro : MonoBehaviour {
  //int _state = 0;
  float _creationTime = 0f;

  void Start () {
    _creationTime = Time.timeSinceLevelLoad;
  }

	// Update is called once per frame
	void Update () {
    /*if (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.X)) { 	
      switch (_state) {
        case 0:
          transform.Find("Keys").gameObject.SetActive(false);
          transform.Find("Intro Text").gameObject.SetActive(true);
          break;
        case 1:
          transform.Find("Intro Text").gameObject.SetActive(false);
          transform.Find("Title").gameObject.SetActive(true);
          break;
        case 2:
          SceneManager.LoadScene("Level");
          break;
      }
      _state++;
    }*/
    if (_creationTime + 3f < Time.timeSinceLevelLoad) {
      transform.Find("Keys").gameObject.SetActive(false);
      transform.Find("Intro Text").gameObject.SetActive(true);
    }

    if (_creationTime + 8f < Time.timeSinceLevelLoad) {
      transform.Find("Intro Text").gameObject.SetActive(false);
      transform.Find("Title").gameObject.SetActive(true);
    }

    if (_creationTime + 14f < Time.timeSinceLevelLoad) {
      SceneManager.LoadScene("Level");
    }
	}
}
