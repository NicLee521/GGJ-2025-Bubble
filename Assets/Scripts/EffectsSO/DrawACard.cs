using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DrawACard", menuName = "Card Game/Effects/DrawACard")]
public class DrawACard : CardEffect {

    public int cardsToDraw;
    public override void ApplyEffect(Dictionary<TargetType,GameObject> targets){
        PlayerController playerController = targets[TargetType.Player].GetComponent<PlayerController>();
        if (playerController != null){
            playerController.DrawCards(cardsToDraw);
        }
    }
}
