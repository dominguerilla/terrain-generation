using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flyer : MonoBehaviour
{
    [SerializeField] float forwardSpeed = 10f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(transform.InverseTransformDirection(transform.forward) * Time.deltaTime * forwardSpeed);

        //transform.Rotate(new Vector3(Input.GetAxis("Vertical"), Input.GetAxis("Horizontal"), 0));
        transform.Rotate(Input.GetAxis("Vertical"), Input.GetAxis("Horizontal"), 0);
    }
}
