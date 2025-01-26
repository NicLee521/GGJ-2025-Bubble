using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
public class CustomerObject : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public int rageAmount = 0;
    public int maxRage;
    private TargetSelector targetSelector;
    private CustomerController customerController;
    private Image telegraphImage;
    private Deck deck = new Deck();
    private Card cardToPlay;
    public GameObject tooltipPanel;
    public TextMeshProUGUI tooltipText;
    public TextMeshProUGUI currentRageText;
    public TextMeshProUGUI maxRageText;

    void Awake() {
        maxRage = UnityEngine.Random.Range(15,25);
        targetSelector = GameObject.Find("TargetSelector").GetComponent<TargetSelector>();
        telegraphImage = transform.Find("TelegraphIcon").gameObject.GetComponent<Image>();
        tooltipPanel.SetActive(false);
    }

    public void SetUpCustomer(CustomerController callingCustomerController) {
        customerController = callingCustomerController;
        ReDrawUI();
        deck.AddCards(customerController.enemyCards);
        deck.Shuffle();
        SetNextCard();
    }

    public void PlayCard() {
        Dictionary<TargetType,GameObject> targets = targetSelector.GetTargets();
        foreach (CardEffect effect in cardToPlay.effects){
            effect.ApplyEffect(targets);
        }
        deck.DiscardCard(cardToPlay);
        SetNextCard();
    } 

    private void SetNextCard() {
        cardToPlay = deck.DrawCards(1)[0];
        telegraphImage.sprite = cardToPlay.enemyIcon;
        telegraphImage.color = cardToPlay.enemyIconColor;
    }

    public void AddRage(int rageToAdd) {
        rageAmount += rageToAdd;
        ReDrawUI();
        if(rageAmount >= maxRage) {
            customerController.Lose();
        }
    }

    public void RemoveRage(int rageToRemove) {
        rageAmount -= rageToRemove;
        rageAmount = Math.Max(0, rageAmount);
        ReDrawUI();
    }

    public void ShowTelegraphToolTip() {
        tooltipText.text = cardToPlay.cardText;
        tooltipPanel.SetActive(true);
    }

    public void HideTelegraphToolTip() {
        tooltipPanel.SetActive(false);
    }

    void ReDrawUI() {
        currentRageText.text = rageAmount.ToString();
        maxRageText.text = maxRage.ToString();
    }
}
