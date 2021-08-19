using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightControl : MonoBehaviour
{
    public GameObject Light;
    GameObject[] Lights;
    // Start is called before the first frame update
    void Start()
    {
        Lights = new GameObject[48];
        for (int i = 0; i < transform.childCount; i++) {
            Lights[i] = transform.Find((i + 1).ToString()).gameObject;
        }
        //SpawnLight();
    }

    // Update is called once per frame
    void Update()
    {
        //LightSpining();
        LightSwitch();
    }
    int xPos = -500;
    int yPos = -3;
    int id = 1;
    void SpawnLight() {
        for (int i = 0; i < 8; i++) {
            GameObject _l = Instantiate(Light, transform);
            _l.name = (id).ToString();
            _l.transform.localPosition = new Vector3(xPos, _l.transform.localPosition.y + yPos, 0);
            xPos += 125;
            id++;
        }
        for (int i = 0; i < 16; i++)
        {
            GameObject _l = Instantiate(Light, transform);
            _l.name = (id).ToString();
            _l.transform.localPosition = new Vector3(xPos, _l.transform.localPosition.y + yPos, 0);
            yPos -= 129;
            id++;
        }

        for (int i = 0; i < 8; i++)
        {
            GameObject _l = Instantiate(Light, transform);
            _l.name = (id).ToString();
            _l.transform.localPosition = new Vector3(xPos, _l.transform.localPosition.y + yPos, 0);
            xPos -= 125;
            id++;
        }
        for (int i = 0; i < 16; i++)
        {
            GameObject _l = Instantiate(Light, transform);
            _l.name = (id).ToString();
            _l.transform.localPosition = new Vector3(xPos, _l.transform.localPosition.y + yPos, 0);
            yPos += 129;
            id++;
        }
    }

    public float speed;
    int lightID = 0;
    float countDown = 0;
    void LightSpining() {
        if (countDown <= 0)
        {
            Lights[lightID].GetComponent<Animator>().Play("Light", 0, 0);
            countDown = speed;
            if (lightID < Lights.Length - 1)
            {
                lightID++;
            }
            else {
                lightID = 0;
            }
        }
        else {
            countDown -= Time.deltaTime;
        }
    }

    public float switchSpeed;
    bool oddLight = true;
    float switchCountDown = 0;
    void LightSwitch() {
        if (switchCountDown <= 0)
        {
            oddLight = !oddLight;
            switchCountDown = switchSpeed;
        }
        else {
            switchCountDown -= Time.deltaTime;
        }

        if (oddLight)
        {
            foreach (GameObject child in Lights)
            {
                if (int.Parse(child.transform.name.ToString()) % 2 == 0)
                {
                    child.GetComponent<Animator>().Play("Light", 0, 0);
                }
            }
        }
        else {
            foreach (GameObject child in Lights)
            {
                if (int.Parse(child.transform.name.ToString()) % 2 != 0)
                {
                    child.GetComponent<Animator>().Play("Light", 0, 0);
                }
            }
        }
    }
}
