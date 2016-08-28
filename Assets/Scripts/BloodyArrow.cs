using UnityEngine;
using System.Collections;

public class BloodyArrow : MonoBehaviour {
  public Material Blood;
  public AudioSource HitSound;

  bool _soundPlayed = false;

  void OnCollisionEnter2D(Collision2D coll) {
    if (coll.gameObject.tag == "Enemy") {
      transform.GetChild(0).GetComponent<Renderer>().material = Blood;
      if (!_soundPlayed) {
        HitSound.Play();
        _soundPlayed = true;
        coll.gameObject.GetComponent<Walker>().Die();
      }
    }
  }
}
