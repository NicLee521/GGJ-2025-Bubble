using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DamagePlayer", menuName = "Card Game/Effects/DamagePlayer")]
public class DamagePlayer : CardEffect{
    public int damageAmount;

    public override void ApplyEffect(Dictionary<TargetType,GameObject>  targets){
        PlayerController playerController = targets[TargetType.Player].GetComponent<PlayerController>();
        if (playerController != null){
            playerController.TakeDamage(damageAmount);
        }
    }
}
