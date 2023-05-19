using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    public static BackgroundMusic backgroundMusic;
    
    private void Awake()
    {
        if (backgroundMusic == null)
        {
            backgroundMusic = this;
            DontDestroyOnLoad(backgroundMusic);
        }

        else
        {
            Destroy(gameObject);
        }
    }
}
