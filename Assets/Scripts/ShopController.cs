using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ShopController : MonoBehaviour
{
    public List<Card> cardsToSell = new List<Card>();
    public PlayerController playerController;
    public List<GameObject> cardSlots = new List<GameObject>();

    public int amountToHealPlayerBy = 5;
    public int costOfHeal = 20;

    void Start() {
        playerController = GameObject.Find("PlayerController").GetComponent<PlayerController>();
        SetUpShop();
    }

    void SetUpShop() {
        if (cardsToSell.Count <= 3) {
            SetUpSlots(cardsToSell);
            return;
        }
        int firstRandom = Random.Range(0, cardsToSell.Count);
        int secondRandom = firstRandom;
        while(secondRandom == firstRandom) {
            secondRandom = Random.Range(0, cardsToSell.Count);
        }
        int thirdRandom = firstRandom;
        while (thirdRandom == firstRandom || thirdRandom == secondRandom) {
            thirdRandom = Random.Range(0, cardsToSell.Count);
        }
        List<Card> cardsToPutInSlots = new List<Card> {cardsToSell[firstRandom], cardsToSell[secondRandom], cardsToSell[thirdRandom]};
        SetUpSlots(cardsToPutInSlots);
    }

    void SetUpSlots(List<Card> cardsToPutInSlots) {
        for (int i = 0; i < cardsToPutInSlots.Count; i++) {
            Card card = cardsToPutInSlots[i];
            GameObject slot = cardSlots[i];
            GameObject costField = slot.transform.Find("Cost")?.gameObject;
            costField.GetComponent<TMP_Text>().SetText(card.cardCost.ToString());
            GameObject titleField = slot.transform.Find("Title")?.gameObject;
            titleField.GetComponent<TMP_Text>().SetText(card.cardName);
            GameObject cardTextField = slot.transform.Find("Card Text")?.gameObject;
            cardTextField.GetComponent<TMP_Text>().SetText(card.cardText);
            GameObject cardImageField = slot.transform.Find("Card Image")?.gameObject;
            cardImageField.GetComponent<Image>().sprite = card.cardImage;
            GameObject priceField = slot.transform.Find("Price")?.gameObject;
            priceField.GetComponent<TMP_Text>().SetText("$"+card.shopCost.ToString());
            ShopObject cardObject = slot.AddComponent<ShopObject>();
            cardObject.thisCard = card;
        }
    }

    public void HealPlayer() {
        if(playerController.PayCurrency(costOfHeal)) {
            playerController.HealPlayer(amountToHealPlayerBy);
        }
    }
}
