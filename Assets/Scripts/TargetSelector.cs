using System.Collections.Generic;
using UnityEngine;

public class TargetSelector : MonoBehaviour
{
    public GameObject player;
    public GameObject turnController;
    public GameObject customerController;
    void Start () {
        player = GameObject.FindWithTag("Player");
        customerController = GameObject.Find("CustomerController");
        turnController = GameObject.Find("TurnController");
    }
    public Dictionary<TargetType,GameObject> GetTargets() {
        Dictionary<TargetType,GameObject> targets = new Dictionary<TargetType,GameObject> {
            {TargetType.Player, player},
            {TargetType.CurrentCustomer, customerController},
            {TargetType.Timer, turnController},
        };
        return targets;
    }
}
