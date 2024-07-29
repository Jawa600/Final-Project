using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spin : MonoBehaviour
{
    public Vector3 spinRotationSpeed = new Vector3(0, 180, 0);
    AudioSource pickupSource;

        
    private void Awake()
    {
        pickupSource = GetComponent<AudioSource>();
    }


    void Update()
    {
        transform.eulerAngles += spinRotationSpeed * Time.deltaTime;
        
    }
}

