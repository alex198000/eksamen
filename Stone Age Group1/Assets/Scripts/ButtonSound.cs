using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSound : MonoBehaviour
{
    public AudioSource buttonSdp;
    public AudioClip navod;
    public AudioClip click;

    // Update is called once per frame
    public void HoverSound()
    {
        buttonSdp.PlayOneShot(navod);
    }

    public void KlickSound()
    {
        buttonSdp.PlayOneShot(click);
    }
}
