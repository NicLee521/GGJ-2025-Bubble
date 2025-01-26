using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ShieldEffect", menuName = "Card Game/Effects/AddShield")]
public class AddShieldEffect : CardEffect{
    public int shieldAmount;

    public override void ApplyEffect(Dictionary<TargetType,GameObject>  targets){
        PlayerController playerController = targets[TargetType.Player].GetComponent<PlayerController>();
        if (playerController != null){
            playerController.AddShield(shieldAmount);
        }
    }
}
