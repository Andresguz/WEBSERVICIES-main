using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
public class scrollSkin : MonoBehaviour
{
    [SerializeField] Text levelNumberText;
    [SerializeField] int numberOflevel;
    [SerializeField] GameObject levlebtnPref;
    [SerializeField] Transform levlebtnParent;
    [SerializeField] string option;
    void Start()
    {
        if (option == "players")
        {

        }
         //  StartCoroutine()
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void LoadLevelButtons(Player[] players)
    {
        for (int i = 0; i < players.Length; i++)
        {
            GameObject levelBtnObj = Instantiate(levlebtnPref,levlebtnParent);
           // levelBtnObj.GetComponent<LevelButtonItem>().player
        }
    }
}
