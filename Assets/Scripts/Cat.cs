using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cat : MonoBehaviour
{
    public void SnackAudio()
    {
        SoundManager.instance.PlaySFX("Snack");
    }

    public void Meow()
    {
        SoundManager.instance.PlaySFX("Meow");
    }
}
