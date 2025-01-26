using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI; 
using System;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "NewCard", menuName = "Card Game/Card")]
public class Card : ScriptableObject {
    public string cardName;
    public Sprite cardImage;
    public int cardCost;
    public string cardText;
    public Sprite enemyIcon;
    public Color enemyIconColor;
    public CardEffect[] effects;
    public GameObject cardLayout;
    private GameObject handArea;
    public int shopCost;
    public void Awake() {
        cardLayout = (GameObject)Resources.Load("Card");
    }

    public void OnEnable() {
        handArea = GameObject.FindGameObjectWithTag("Hand Area");
    }

    public CardObject InstantiateCardVisual(){
        if(handArea == null){
            handArea = GameObject.FindGameObjectWithTag("Hand Area");
        }
        GameObject cardInstance = Instantiate(cardLayout, handArea.transform);
        GameObject costField = cardInstance.transform.Find("Cost")?.gameObject;
        costField.GetComponent<TMP_Text>().SetText(cardCost.ToString());
        GameObject titleField = cardInstance.transform.Find("Title")?.gameObject;
        titleField.GetComponent<TMP_Text>().SetText(cardName);
        GameObject cardTextField = cardInstance.transform.Find("Card Text")?.gameObject;
        cardTextField.GetComponent<TMP_Text>().SetText(cardText);
        GameObject cardImageField = cardInstance.transform.Find("Card Image")?.gameObject;
        cardImageField.GetComponent<Image>().sprite = cardImage;
        CardObject cardObject = cardInstance.AddComponent<CardObject>();
        cardObject.thisCard = this;
        return cardObject;
    }
}

public enum TargetType
{
    Player,
    CurrentCustomer,
    Timer,
}

public abstract class CardEffect : ScriptableObject{
    public abstract void ApplyEffect(Dictionary<TargetType,GameObject> targets); // Define behavior when applied
}

public class ReduceRageEffect : CardEffect{
    public int rageReduction;

    public override void ApplyEffect(Dictionary<TargetType,GameObject> targets){
        CustomerController customer = targets[TargetType.CurrentCustomer].GetComponent<CustomerController>();
        if (customer != null){
            customer.RemoveRage(rageReduction);
        }
    }
}