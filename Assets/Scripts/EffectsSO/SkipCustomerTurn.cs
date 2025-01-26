using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SkipTurn", menuName = "Card Game/Effects/SkipTurn")]
public class SkipCustomerTurn : CardEffect {

    public int turnsToSkip;
    public override void ApplyEffect(Dictionary<TargetType,GameObject>  targets){
        TurnController turnController = targets[TargetType.Timer].GetComponent<TurnController>();
        if (turnController != null){
            turnController.SkipCustomerTurns(turnsToSkip);
        }
    }
}
