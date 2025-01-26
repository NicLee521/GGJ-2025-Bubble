using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System;
using System.Linq;

public class PlayerController : MonoBehaviour {
    
    public PlayerStats stats;
    public List<CardObject> hand = new List<CardObject>();
    private TargetSelector targetSelector;
    private TurnController turnController;
    public TextMeshProUGUI energyText;
    public TextMeshProUGUI healthText;
    public Slider healthSlider;
    public TextMeshProUGUI currencyText;
    public TextMeshProUGUI shieldText;
    public TextMeshProUGUI dayOverCurrencyText;
    public TextMeshProUGUI dayOverHealthText;

    public TextMeshProUGUI daysText;

    public AudioClip shuffleSound;
    public AudioClip cardPlaySound;
    public AudioClip endTurnSound;
    private AudioSource audioSource;

    
    void Start(){
        stats = PlayerStats.instance;
        stats.deck.Reshuffle();
        stats.deck.Shuffle();
        ResetEnergy();
        targetSelector = GameObject.Find("TargetSelector").GetComponent<TargetSelector>();
        turnController = GameObject.Find("TurnController").GetComponent<TurnController>();
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
        audioSource.playOnAwake = false;
        TurnController.OnTurnChanged += OnTurnChanged;
        ReDrawUI();
        DrawHand();
    }

    private void OnDestroy()
    {
        TurnController.OnTurnChanged -= OnTurnChanged;
    }

    void OnTurnChanged(TurnState currentTurn){
        if(currentTurn == TurnState.PlayerTurn) {
            DrawHand();
            DissapateShield();
            ResetEnergy();
        }
        if(currentTurn == TurnState.EnemyTurn) {
            DiscardHand();
            ReDrawUI();
        }
    }

    public void EndPlayerTurn() {
        if(turnController.currentTurn != TurnState.PlayerTurn) {
            return;
        }
        audioSource.PlayOneShot(endTurnSound);
        turnController.EndPlayerTurn();
    }

    public void DrawHand() {
        audioSource.PlayOneShot(shuffleSound);
        DrawCards(stats.handSize);
    }

    public void DiscardHand() {
        List<Card> cardsToPutInDiscard = new List<Card>();
        foreach (CardObject cardObject in hand) {
            if(cardObject == null) {
                continue;
            }
            cardsToPutInDiscard.Add(cardObject.thisCard);
            Destroy(cardObject.gameObject);
        }
        stats.deck.DiscardCards(cardsToPutInDiscard);
        hand.Clear();
    }

    public void PlayCard(CardObject cardObject) {
        Card cardToPlay = cardObject.thisCard;
        if(cardToPlay.cardCost > stats.energy){
            Debug.Log("Cant Play");
            return;
        }
        audioSource.PlayOneShot(cardPlaySound);
        updateEnergy(cardToPlay.cardCost * -1);
        Dictionary<TargetType,GameObject> targets = targetSelector.GetTargets();
        foreach (CardEffect effect in cardToPlay.effects){
            effect.ApplyEffect(targets);
        }
        stats.deck.DiscardCard(cardToPlay);
        hand.Remove(cardObject);
        Destroy(cardObject.gameObject);
    }

    void updateEnergy(int energyToUpdate) {
        stats.energy += energyToUpdate;
        ReDrawUI();
    }

    public void AddShield(int numberOfShield) {
        stats.shieldAmount += numberOfShield;
        ReDrawUI();
    }

    void DissapateShield() {
        stats.shieldAmount = 0;
        ReDrawUI();
    }

    void ReDrawUI() {
        energyText.text = stats.energy.ToString();
        healthText.text = stats.health.ToString() + "/" + stats.maxHealth;
        currencyText.text = "$" + stats.currency.ToString();
        shieldText.text = stats.shieldAmount.ToString();
        healthSlider.value = (float)stats.health / stats.maxHealth;
        dayOverCurrencyText.text = "Current Currency: $" + stats.currency.ToString();
        dayOverHealthText.text = "Current Health: " + stats.health.ToString() + "/" + stats.maxHealth.ToString();
        daysText.text = stats.currentDay.ToString() + " of 2 days";
    }

    void ResetEnergy() {
        stats.energy = stats.maxEnergy;
        energyText.text = stats.energy.ToString();
    }

    public void NextDay() {
        stats.currentDay += 1;
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }

    public void MainMenu() {
        if(stats != null) {
            Destroy(stats.gameObject);
        }
        SceneManager.LoadScene("Main Menu");
    }

    public bool PayCurrency(int currencyOwed) {
        if(stats.currency < currencyOwed) {
            return false;
        }
        stats.currency -= currencyOwed;
        ReDrawUI();
        return true;
    }

    public void HealPlayer(int healthToAdd) {
        stats.health = Math.Min(stats.health + healthToAdd, stats.maxHealth);
        ReDrawUI();
    }

    public void AddCardToDeck(Card card) {
        stats.deck.AddCard(card);
    }

    public void AddCurrency(int currencyToAdd) {
        stats.currency += currencyToAdd;
        ReDrawUI();
    }

    public void DrawCards(int cardsToDraw) {
        List<Card> drawnCards = stats.deck.DrawCards(cardsToDraw);
        foreach(Card card in drawnCards) {
            hand.Add(card.InstantiateCardVisual());
        }
    }

    public void TakeDamage(int amount) {
        if (stats.shieldAmount > 0) {
            int shieldDamage = Mathf.Min(amount, stats.shieldAmount);
            stats.shieldAmount -= shieldDamage;
            amount -= shieldDamage;
        }
        if (amount > 0) {
            stats.health -= amount;
            stats.health = Mathf.Max(stats.health, 0); 
        }
        ReDrawUI();
        if (stats.health <= 0) {
            GameObject.Find("CustomerController").GetComponent<CustomerController>().Lose();
        }
    }
}
