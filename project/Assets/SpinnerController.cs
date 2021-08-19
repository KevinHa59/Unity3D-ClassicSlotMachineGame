using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpinnerController : MonoBehaviour
{
    public GameObject Spin1;
    public bool result1;
    string resultSymbol1;
    public GameObject Spin2;
    public bool result2;
    string resultSymbol2;
    public GameObject Spin3;
    public bool result3;
    string resultSymbol3;
    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;
    }

    // Update is called once per frame
    void Update()
    {
        if (isStopping == true && FindObjectOfType<UserInformation>().getAutoBetValue() == false) {
            result1 = Spin1.GetComponent<SpinnerUnitController>().getResult();
            result2 = Spin2.GetComponent<SpinnerUnitController>().getResult();
            result3 = Spin3.GetComponent<SpinnerUnitController>().getResult();
            if (result1 == true && result2 == true && result3 == true) {
                Invoke("CheckPride", 0.3f);
                isStopping = false;
                SpinButtonText.text = "Spin";
                ActiveSpin = false;
                result1 = result2 = result3 = false;
                GetComponent<AudioSource>().Stop();
            }
        }

        // Auto Spin
        if (FindObjectOfType<UserInformation>().getAutoBetValue() == true && FindObjectOfType<UserInformation>().getCreditBetAmoun() != 0) {
            if (ActiveSpin == false)
            {
                AutoBetActive();
                completeGetResult = false;
                ActiveSpin = true;
                SpinButtonText.text = "Stop";
                isStopping = false;
            }
            else {
                if (completeGetResult == false) { 
                    result1 = Spin1.GetComponent<SpinnerUnitController>().getResult();
                    result2 = Spin2.GetComponent<SpinnerUnitController>().getResult();
                    result3 = Spin3.GetComponent<SpinnerUnitController>().getResult();
                }
                if (result1 == true && result2 == true && result3 == true)
                {
                    
                    Invoke("CheckPride", 0.3f);
                    completeGetResult = true;
                    Invoke("ResetActiveSpinStatus",1);
                    GetComponent<AudioSource>().Stop();
                    result1 = result2 = result3 = false;
                    isStopping = false;
                    
                }
            }
            
        }



    }

    void CheckPride() {
        resultSymbol1 = Spin1.GetComponent<SpinnerUnitController>().itemSelected;
        resultSymbol2 = Spin2.GetComponent<SpinnerUnitController>().itemSelected;
        resultSymbol3 = Spin3.GetComponent<SpinnerUnitController>().itemSelected;
        bool win = FindObjectOfType<PrideController>().checkPride(resultSymbol1, resultSymbol2, resultSymbol3, FindObjectOfType<UserInformation>().getCreditBetAmoun());
        if (win == true)
        {
            FindObjectOfType<UserInformation>().AddCredit(FindObjectOfType<PrideController>().getWinPride());
            WinFX.SetActive(true);
            WinFX.GetComponent<WinStatus>().setInfo(FindObjectOfType<PrideController>().getWinPride(), FindObjectOfType<PrideController>().getMatchCount());
            Invoke("WinFXDisable", 2f);
        }
        if (FindObjectOfType<UserInformation>().getAutoBetValue() == false)
        {
            FindObjectOfType<UserInformation>().SetAutoBetButtonInteractable(true);
        }

    }

    // Spin Button
    [Header("Spin Button")]
    public Text SpinButtonText;
    bool isSpinning = false;
    bool isStopping = false;
    public void btn_SpinorStop() {
        if (FindObjectOfType<UserInformation>().getCreditBetAmoun() != 0 && isStopping == false && FindObjectOfType<UserInformation>().getAutoBetValue() == false && FindObjectOfType<UserInformation>().getManualBetValue() == true) {
           
            Spin1.GetComponent<SpinnerUnitController>().activeSpinner();
            Spin2.GetComponent<SpinnerUnitController>().activeSpinner();
            Spin3.GetComponent<SpinnerUnitController>().activeSpinner();
            isSpinning = !isSpinning;
            if (isSpinning)
            {
                ActiveSpin = true;
                SpinButtonText.text = "Stop";
                isStopping = false;
                GetComponent<AudioSource>().Play();
                FindObjectOfType<UserInformation>().DeductCredit();
            }
            else 
            {
                SpinButtonText.text = "Hold";
                isStopping = true;

                Spin1.GetComponent<SpinnerUnitController>().ResetSpinner(1f);
                Spin2.GetComponent<SpinnerUnitController>().ResetSpinner(1.5f);
                Spin3.GetComponent<SpinnerUnitController>().ResetSpinner(2f);

            }

        }

        if (SpinButtonText.text.ToString() == "Stop" && FindObjectOfType<UserInformation>().getManualBetValue() == false)
        {
            SpinButtonText.text = "Hold";
            isStopping = true;

            Spin1.GetComponent<SpinnerUnitController>().ResetSpinner(1f);
            Spin2.GetComponent<SpinnerUnitController>().ResetSpinner(1.5f);
            Spin3.GetComponent<SpinnerUnitController>().ResetSpinner(2f);
        }
    }

    bool ActiveSpin = false;
    bool completeGetResult = false;
    public void AutoBetActive() {
        StartSpin();
        float stopDelay = Random.Range(1f, 2f);
        Invoke("StopSpin", stopDelay);
    }

    void StartSpin() {

        Spin1.GetComponent<SpinnerUnitController>().activeSpinner();
        Spin2.GetComponent<SpinnerUnitController>().activeSpinner();
        Spin3.GetComponent<SpinnerUnitController>().activeSpinner();
        GetComponent<AudioSource>().Play();
        FindObjectOfType<UserInformation>().DeductCredit();
    }

    void StopSpin() {
        SpinButtonText.text = "Hold";
        isStopping = true;
        Spin1.GetComponent<SpinnerUnitController>().ResetSpinner(1f);
        Spin2.GetComponent<SpinnerUnitController>().ResetSpinner(1.5f);
        Spin3.GetComponent<SpinnerUnitController>().ResetSpinner(2f);
    }

    void ResetActiveSpinStatus() {
        ActiveSpin = false;
        SpinButtonText.text = "Spin";
    }

    public bool getActiveSpinStatus() {
        return ActiveSpin;
    }

    [Header("Win Effect")]
    public GameObject WinFX;

    void WinFXDisable() {
        WinFX.SetActive(false);
    }
}
