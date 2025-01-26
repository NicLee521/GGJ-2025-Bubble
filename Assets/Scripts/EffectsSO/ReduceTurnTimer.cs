using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ReduceTimer", menuName = "Card Game/Effects/ReduceTimer")]
public class ReduceTurnTimer : CardEffect {

    public int timerReduction;
    public override void ApplyEffect(Dictionary<TargetType,GameObject>  targets){
        TurnController turnController = targets[TargetType.Timer].GetComponent<TurnController>();
        if (turnController != null){
            turnController.ReduceTimer(timerReduction);
        }
    }
}
