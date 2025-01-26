using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AddCurrency", menuName = "Card Game/Effects/AddCurrency")]
public class AddCurrency : CardEffect {

    public int currencyToAdd;
    public override void ApplyEffect(Dictionary<TargetType,GameObject>  targets){
        PlayerController playerController = targets[TargetType.Player].GetComponent<PlayerController>();
        if (playerController != null){
            playerController.AddCurrency(currencyToAdd);
        }
    }
}
