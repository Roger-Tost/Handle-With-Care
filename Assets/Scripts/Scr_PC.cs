using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Scr_PC : MonoBehaviour
{
    public Scr_SistemaOrdenador PCinterfaz;
    public bool IsOnPC = false;
    [SerializeField] private AudioClip NotificationSoundClip;
    private void Start()
    {

    }


    private void Update()
    {
        if (IsOnPC && Input.GetKeyDown(KeyCode.E))
        {
            PCinterfaz.ActivarPC();

        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            IsOnPC = true;
            // PC NotifSound
            Scr_SoundManager.instance.PlaySoundFXClip(NotificationSoundClip, transform, 1f);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            IsOnPC = false;

            
        }
    }






}
