using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Boat : BaseInteractable, IInteractable
{
    public override void Action()
    {
        if(Inventory.instance.GetAmountLog() == 6 && Input.GetKeyDown(KeyCode.O))
        {
            SceneManager.LoadSceneAsync("WinScreen");
        }
    }
}
