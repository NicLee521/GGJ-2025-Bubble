using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RemoveRageEffect", menuName = "Card Game/Effects/RemoveRage")]
public class RemoveRageEffect : CardEffect{
    public int rageReduction;

    public override void ApplyEffect(Dictionary<TargetType,GameObject>  targets){
        CustomerController customer = targets[TargetType.CurrentCustomer].GetComponent<CustomerController>();
        if (customer != null){
            customer.RemoveRage(rageReduction);
        }
    }
}
