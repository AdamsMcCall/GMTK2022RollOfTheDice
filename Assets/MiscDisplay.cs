using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiscDisplay : MonoBehaviour
{
    public GameObject prop1;
    public GameObject prop2;
    public GameObject prop3;
    public GameObject prop4;

    // Start is called before the first frame update
    void Start()
    {
        prop1.SetActive(false);
        prop2.SetActive(false);
        prop3.SetActive(false);
        prop4.SetActive(false);

        var rng = Random.Range(0, 5);

        switch (rng)
        {
            case 0:
                prop1.SetActive(true);
                break;
            case 1:
                prop2.SetActive(true);
                break;
            case 2:
                prop3.SetActive(true);
                break;
            case 3:
                prop4.SetActive(true);
                break;
            case 4:
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
