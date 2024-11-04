using UnityEditor.Experimental.GraphView;
using UnityEngine;
using System.Collections;

public class SimpleAudioSource : MonoBehaviour
{
    public AudioSource sfx;
    public AudioClip Trumpets;
    public AudioClip Sheep;
    public AudioClip DragonSpitFire;
    public AudioClip DestroyedVillage;
    public AudioClip DeadDragon;

    public void PlaySound(string ID)
    {
        if (ID == "Trumpets")
        {
            sfx.PlayOneShot(Trumpets);
        }
        else if (ID == "Sheep")
        {
            sfx.PlayOneShot(Sheep);
        }
        else if (ID == "DragonSpitFire")
        {
            sfx.PlayOneShot(DragonSpitFire);
        }
        else if (ID == "DestroyedVillage")
        {
            sfx.PlayOneShot(DestroyedVillage);
        }
        else if (ID == "DeadDragon")
        {
            sfx.PlayOneShot(DeadDragon);
        }
        else
        {
            Debug.LogError("Unknown audio ID" + ID);
        }

    }
}

