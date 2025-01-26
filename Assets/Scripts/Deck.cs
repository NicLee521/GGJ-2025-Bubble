using System.Collections.Generic;
using System.Linq;
using System;

public class Deck{
    public List<Card> cardsInDeck = new List<Card>();
    public List<Card> cardsInDiscard = new List<Card>();
    public Deck(List<Card> startingDeck = null) {
        if(startingDeck != null) {
            cardsInDeck.AddRange(startingDeck);
        }
    }

    public void AddCard(Card card) { 
        cardsInDeck.Add(card);
    }

    public void AddCards(List<Card> cards) { 
        cardsInDeck.AddRange(cards);
    }

    public List<Card> DrawCards(int numberToDraw) {
        if(cardsInDeck.Count < numberToDraw) {
            Reshuffle();
        }
        List<Card> cardsToHand = cardsInDeck.Take(numberToDraw).ToList();
        cardsInDeck.RemoveRange(0, Math.Min(numberToDraw, cardsInDeck.Count));
        return cardsToHand;
    }

    public void Reshuffle() {
        for (int i = cardsInDiscard.Count - 1; i > 0; i--) {
            int randomIndex = UnityEngine.Random.Range(0, i + 1);
            Card temp = cardsInDiscard[i];
            cardsInDiscard[i] = cardsInDiscard[randomIndex];
            cardsInDiscard[randomIndex] = temp;
        }
        cardsInDeck.AddRange(cardsInDiscard);
        cardsInDiscard.Clear();
    }

    public void Shuffle() {
        for (int i = cardsInDeck.Count - 1; i > 0; i--) {
            int randomIndex = UnityEngine.Random.Range(0, i + 1);
            Card temp = cardsInDeck[i];
            cardsInDeck[i] = cardsInDeck[randomIndex];
            cardsInDeck[randomIndex] = temp;
        }
    }

    public void DiscardCard(Card card) {
        cardsInDiscard.Add(card);
    }

    public void DiscardCards(List<Card> cards) {
        cardsInDiscard.AddRange(cards);
    }
}
