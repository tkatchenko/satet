using UnityEngine;
using System.Collections;

public class CardCollider : MonoBehaviour {
  public GameObject card;

  void OnTriggerEnter2D(Collider2D other) {
    card.SetActive(true);
    Destroy(gameObject);
  }
}
