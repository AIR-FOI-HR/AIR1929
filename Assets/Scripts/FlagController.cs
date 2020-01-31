using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Diagnostics;
using Debug = UnityEngine.Debug;
using Assets.Scripts.ERA;
using System.Linq;
using System;
using System.IO;
using Assets.Scripts;

/// <summary>
/// Klasa koja obavlja operacije povezane sa kolizijom objekta sa zastavicom
/// </summary>
public class FlagController : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    /// <summary>
    /// Funkcija koja provjerava da li je završena trka te zove funkciju za dohvacanje i upisivanje podataka lokalno
    /// </summary>
    /// <param name="collider"></param>
    void OnTriggerEnter2D(Collider2D collider)
    {
        
    }

    
}
