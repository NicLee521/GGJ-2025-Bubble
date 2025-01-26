using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class PlayerStats : MonoBehaviour {
    public static PlayerStats instance;
    public int health;
    public int maxHealth;
    public int shieldAmount = 0;
    public int handSize;
    public int maxEnergy = 3;
    public int energy;
    public int currency = 0;
    public int currentDay = 1;
    public Deck deck;
    public List<Card> startingDeck = new List<Card>();
    void Awake() {
        if(instance == null) {
            instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }
        health = maxHealth;
        energy = maxEnergy;
        deck = new Deck(startingDeck);
    }
}
