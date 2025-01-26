using UnityEngine;
using UnityEngine.EventSystems;

public class CardObject : MonoBehaviour, IPointerClickHandler {
    private PlayerController playerController;
    public Card thisCard;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    public void OnPointerClick(PointerEventData eventData) {
        playerController.PlayCard(this);
    }
}
