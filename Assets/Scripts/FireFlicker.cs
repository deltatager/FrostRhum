using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireFlicker : MonoBehaviour
{

    private Light _light;
    
    // Start is called before the first frame update
    void Start()
    {
        _light = GetComponent<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Random.value > 0.97f)
        {
            _light.intensity = Random.Range(0.7f, 3);
        }
    }
}
