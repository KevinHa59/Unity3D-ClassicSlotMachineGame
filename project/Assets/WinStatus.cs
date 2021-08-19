using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinStatus : MonoBehaviour
{
    public Material[] BGs;
    public GameObject[] Objects;
    public Image BG;
    public Text Amount;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("setDisable", 2f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setInfo(int amount, int matchCount)
    {

        if (matchCount == 2)
        {
            Objects[0].GetComponent<MeshRenderer>().material = BGs[0];
            Objects[1].GetComponent<MeshRenderer>().material = BGs[0];
            //BG.sprite = BGs[0];
        }
        else
        {
            Objects[0].GetComponent<MeshRenderer>().material = BGs[1];
            Objects[1].GetComponent<MeshRenderer>().material = BGs[1];
            //BG.sprite = BGs[1];
        }
        Amount.text = "$" + amount;
    }

    void setDisable() {
        this.gameObject.SetActive(false);
    }


}
