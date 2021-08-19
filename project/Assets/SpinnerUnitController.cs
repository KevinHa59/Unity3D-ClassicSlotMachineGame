using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinnerUnitController : MonoBehaviour
{
    bool activeSpin = false;
    public GameObject Spinner;
    public int SpinSpeed;
    float speedUpdate = 0;
    float SpinX = 0;
    // Start is called before the first frame update
    void Start()
    {
        //SpinSpeed = 400;
    }

    // Update is called once per frame
    void Update()
    {
        if (activeSpin) {
            SpinnerSpinning();
        }
    }

    void SpinnerSpinning()
    {
        if (speedUpdate < SpinSpeed)
        {
            speedUpdate += Time.deltaTime * 600;
        }
        SpinX += Time.deltaTime * speedUpdate;
        Spinner.transform.rotation = Quaternion.Euler(-SpinX, 0f, 0f);
        spinxAngle = 360 - SpinX % 360;
    }

    // Spin Stop check
    int[] range = new int[] { 0, 18, 36, 54, 72, 90, 108, 126, 144, 162, 180, 198, 216, 234, 252, 270, 288, 306, 324, 342, 360  };
    float spinxAngle = 0;
    float sFinalAngle = 0;


    int CheckSpinAngle(float angle)
    {
        int x1SelectedID = 0;
        for (int i = 0; i < range.Length - 1; i++)
        {
            if (angle >= range[i] && angle <= range[i + 1])
            {
                x1SelectedID = i;
            }
        }

        float left = angle - range[x1SelectedID];
        float right = range[x1SelectedID + 1] - angle;

        return (left <= right) ? range[x1SelectedID] : range[x1SelectedID + 1];

    }
    public void activeSpinner() {
        activeSpin = true;
        complete = false;
    }
    public void ResetSpinner(float delay)
    {
        float dl;
        dl = Random.Range(delay-0.2f, delay+0.2f);
        //dl = 1f;
        Invoke("SpinCheck",dl);

    }
    void SpinCheck() {
        activeSpin = false;
        speedUpdate = 0;
        sFinalAngle = CheckSpinAngle(spinxAngle);
        Spinner.transform.rotation = Quaternion.Euler(-sFinalAngle, 0f, 0f);
        transform.GetComponent<Animator>().Play("Spinner", 0, 0);
        SpinX = sFinalAngle;
        complete = true;
        GetComponent<AudioSource>().Play();
    }

    public bool complete = false;
    public bool getResult() {
        return complete;
    }


    public string itemSelected = "";
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "symbol") {
            itemSelected = other.GetComponent<symbolProperties>().symbol;
        }
    }
}
