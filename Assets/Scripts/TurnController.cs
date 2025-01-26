using System;
using UnityEngine;
using TMPro;
using System.Collections;

public enum TurnState
{
    PlayerTurn,
    EnemyTurn,
    None // Optional: for transitional states
}
public class TurnController : MonoBehaviour
{
    public int timerSetDefault = 5;
    private int turnTimer;
    private int customerTurnsToSkip = 0;
    private CustomerController customerController;
    public TextMeshProUGUI turnTimerText;
    public GameObject bobaImage;
    public TurnState currentTurn = TurnState.PlayerTurn;
    
    public delegate void TurnChanged(TurnState newTurn);
    public static event TurnChanged OnTurnChanged;

    void Start() {
        customerController = GameObject.Find("CustomerController").GetComponent<CustomerController>();
        turnTimer = timerSetDefault;
        turnTimerText.text = turnTimer.ToString();
        bobaImage.SetActive(false);
    }

    public void EndPlayerTurn() {
        if (customerTurnsToSkip > 0) {
            OnTurnChanged?.Invoke(TurnState.EnemyTurn);
            OnTurnChanged?.Invoke(currentTurn);
            customerTurnsToSkip--;
            turnTimer -= 1;
            turnTimerText.text = turnTimer.ToString();
            if(turnTimer == 0) {
                StartCoroutine(FlickerBoba());
            }
            return;
        }
        currentTurn = TurnState.EnemyTurn;
        OnTurnChanged?.Invoke(currentTurn);
        StartEnemyTurn();
    }

    IEnumerator FlickerBoba() {
        bobaImage.SetActive(true);
        yield return new WaitForSeconds(1f);
        bobaImage.SetActive(false);
        customerController.NextCustomer();
        turnTimer = timerSetDefault;
        turnTimerText.text = turnTimer.ToString();
    }

    public void StartEnemyTurn()
    {
        Debug.Log("Enemy is acting...");
        customerController.PlayCardOnCurrentCustomer();
        Invoke(nameof(EndEnemyTurn), 2f);
    }

    public void EndEnemyTurn()
    {
        currentTurn = TurnState.PlayerTurn;
        turnTimer -= 1;
        turnTimerText.text = turnTimer.ToString();
        if(turnTimer == 0) {
            StartCoroutine(FlickerBoba());
        }
        OnTurnChanged?.Invoke(currentTurn);
        Debug.Log("Player's turn!");
    }

    public void ReduceTimer(int turnsToReduceBy) {
        turnTimer -= turnsToReduceBy;
        turnTimerText.text = turnTimer.ToString();
        if(turnTimer == 0) {
            StartCoroutine(FlickerBoba());
            OnTurnChanged?.Invoke(TurnState.EnemyTurn);
            OnTurnChanged?.Invoke(currentTurn);
        }
    }

    public void SkipCustomerTurns(int turnsToSkip) {
        customerTurnsToSkip = turnsToSkip;
    }

    public void AddToTimer(int turnsToAdd) {
        turnTimer += turnsToAdd;
        turnTimerText.text = turnTimer.ToString();
    }
}
