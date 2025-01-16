using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_Pickup : MonoBehaviour
{
    public GameObject pickup;
    public Transform mano;

    private bool activo;



    // Update is called once per frame
    void Update()
    {
        if (activo == true)
        {

            if (Input.GetKeyDown(KeyCode.E))
            {

                pickup.transform.SetParent(mano);
                pickup.transform.position = mano.position;
                pickup.GetComponent<Rigidbody>().isKinematic = true;


            }

        }

        if (Input.GetKeyDown(KeyCode.R))
        { 
        
            pickup.transform.SetParent(null);
            pickup.GetComponent<Rigidbody>().isKinematic = false;

        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        { 
        
        activo = true;

        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {

            activo = false;

        }
    }


}
