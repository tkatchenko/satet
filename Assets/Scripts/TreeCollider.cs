using UnityEngine;
using System.Collections;

public class TreeCollider : MonoBehaviour {
  public GameObject card;

  void OnTriggerEnter2D(Collider2D other) {
    Time.timeScale = 0.01f;
    card.SetActive(true);
  }
}