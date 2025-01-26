using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "ExtendTimer", menuName = "Card Game/Effects/ExtendTimer")]
public class ExtendTimer : CardEffect {

    public int timerIncrease;
    public override void ApplyEffect(Dictionary<TargetType,GameObject>  targets){
        TurnController turnController = targets[TargetType.Timer].GetComponent<TurnController>();
        if (turnController != null){
            turnController.AddToTimer(timerIncrease);
        }
    }
}
