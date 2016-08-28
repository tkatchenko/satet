using UnityEngine;
using System.Collections;

public class SelfDestruct : MonoBehaviour {
  public float Time = 0f;

	// Use this for initialization
	void Start () {
    Destroy(gameObject, Time);	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
