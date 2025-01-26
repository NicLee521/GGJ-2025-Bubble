using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AddRageEffect", menuName = "Card Game/Effects/AddRage")]
public class AddRageEffect : CardEffect{
    public int rageAddition;

    public override void ApplyEffect(Dictionary<TargetType,GameObject>  targets){
        CustomerController customerController = targets[TargetType.CurrentCustomer].GetComponent<CustomerController>();
        if (customerController != null){
            customerController.AddRage(rageAddition);
        }
    }
}