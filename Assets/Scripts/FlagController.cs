using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class FlagController : MonoBehaviour
{
    public GameObject EndRacePanel;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    void OnTriggerEnter2D(Collider2D collider)
    {
        //Provjera jel kolizija sa igracem
        if (collider.gameObject.tag=="Player")
        {
            //1. Otvori novi panel
            //2. Zaustavi igraca (IDLE)
            Debug.Log("Sudar objekta sa zastavom.");
            ReturnMainMenu();
            EndRacePanel.SetActive(true);
            
        }
    }
    void ReturnMainMenu()
    {
        SceneManager.LoadScene("Menu");
    }   

}
