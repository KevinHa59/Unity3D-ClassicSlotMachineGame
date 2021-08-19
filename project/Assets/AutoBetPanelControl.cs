using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AutoBetPanelControl : MonoBehaviour
{
    int totalBetAmount = 0;
    int selectedAmount = 5;
    public Text totalBetAmountText;

    public GameObject[] SelectAmountButtons;
    private void Start()
    {
        btn_selectAmount(5);
    }

    private void Update()
    {
        ResetBottonHoldControl();
    }

    public void btn_Add()
    {
        if (checkReachMax() == false) { 
            totalBetAmount += selectedAmount;
            AssignAmountText(totalBetAmount);
        }
    }

    public void btn_Minus() {
        if (totalBetAmount > 0) {
            if (totalBetAmount > selectedAmount)
            {
                totalBetAmount -= selectedAmount;
            }
            else {
                totalBetAmount = 0;
            }
            AssignAmountText(totalBetAmount);
        }
    }

    public void btn_selectAmount(int amount) {
        selectedAmount = amount;
        AmountButtonClick();
    }

    void AssignAmountText(int amount) {
        totalBetAmountText.text = "$"+amount;
    }

    bool checkReachMax() {
        if (FindObjectOfType<UserInformation>().getTotalCredit() < totalBetAmount + selectedAmount)
        {
            return true; 
        }
        else {
            return false;
        }
    }

    // Button Start
    public void btn_Start() {
        FindObjectOfType<UserInformation>().setAutoBetAmount(totalBetAmount);
        FindObjectOfType<UserInformation>().btn_StartAutoBet();
        btn_Exit();
    }

    // Button Reset
    public GameObject ResetButtonSlider;
    bool resetHold = false;
    bool resetHoldActive = false;
    float resetHoldCount = 1f;
    public void btn_ResetHold() {
        if (resetHoldActive == false) { 
            resetHold = true;
        }
    }
    public void btn_ResetRelease() {
        resetHold = false;
        resetHoldActive = false;
        resetHoldCount = 1f;
        ResetButtonSlider.GetComponent<Image>().fillAmount = 1f - resetHoldCount;

    }
    void ResetBottonHoldControl() {
        if (resetHold == true) {
            if (resetHoldCount >= 0)
            {
                resetHoldCount -= Time.deltaTime *2;
            }
            else {
                ActiveReset();
                resetHoldActive = true;
                resetHold = false;
                resetHoldCount = 1f;
            }
            ResetButtonSlider.GetComponent<Image>().fillAmount = 1f-resetHoldCount;
        }
    }

    void ActiveReset() {
        totalBetAmount = 0;
        btn_selectAmount(5);
        AssignAmountText(0);
    }

    // Active and Exit Button
    public void ActivePanel() {
        GetComponent<Animator>().Play("AutobetPanel", 0, 0);
    }
    public void btn_Exit() {
        GetComponent<Animator>().Play("AutobetPanelHide",0,0);

    }



    // Select Button FX
    public void AmountButtonClick() {
        int buttonID = 0;
        if (selectedAmount == 10)
        {
            buttonID = 1;
        }
        else if (selectedAmount == 100) {
            buttonID = 2;
        }
        foreach (GameObject child in SelectAmountButtons) {
            child.GetComponent<Outline>().enabled = false;
        }
        SelectAmountButtons[buttonID].GetComponent<Outline>().enabled = true;
    }

    // Export Value
    public int getAutoBetAmount() {
        return totalBetAmount;
    }

}
