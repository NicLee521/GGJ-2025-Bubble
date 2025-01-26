using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class CustomerController : MonoBehaviour
{
    public CustomerObject currentCustomer;
    private GameObject currentCustomerGameObject;
    public List<GameObject> customerQueue = new List<GameObject>();
    public List<Sprite> customerSprites = new List<Sprite>();
    public List<Card> enemyCards = new List<Card>();
    public GameObject uiCanvas;
    public GameObject dayOverCanvas;
    public GameObject loseCanvas;
    private PlayerController playerController;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start(){
        currentCustomer = GetFirstCustomerInQueue();
        playerController = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
    }

    public void NextCustomer() {
        if(currentCustomerGameObject != null) {
            Destroy(currentCustomerGameObject);
        } 
        if(customerQueue.Count > 0) {
            StartCoroutine(WaitBeforeNextCustomer());
            return;
        }
        DayOver();
    }

    private IEnumerator WaitBeforeNextCustomer() {
        yield return new WaitForSeconds(2f); // Wait for 2 seconds
        currentCustomer = GetFirstCustomerInQueue();
    }   

    private CustomerObject GetFirstCustomerInQueue() {
        currentCustomerGameObject = Instantiate(customerQueue[0], uiCanvas.transform);
        currentCustomerGameObject.GetComponent<UnityEngine.UI.Image>().sprite = customerSprites[UnityEngine.Random.Range(0, customerSprites.Count)];
        CustomerObject firstCustomer = currentCustomerGameObject.GetComponent<CustomerObject>();
        firstCustomer.SetUpCustomer(this);
        customerQueue.RemoveAt(0);
        return firstCustomer;
    }

    public void PlayCardOnCurrentCustomer() {
        currentCustomer.PlayCard();
    }

    public void AddRage(int rageToAdd) {
        currentCustomer.AddRage(rageToAdd);
    }

    public void RemoveRage(int rageToRemove) {
        currentCustomer.RemoveRage(rageToRemove);
    }

    public void DayOver() {
        playerController.DiscardHand();
        uiCanvas.SetActive(false);
        dayOverCanvas.SetActive(true);
    }

    public void Lose() {
        uiCanvas.SetActive(false);
        loseCanvas.SetActive(true);
    }
}
