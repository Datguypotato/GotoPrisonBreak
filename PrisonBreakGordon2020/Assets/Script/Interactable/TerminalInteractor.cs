using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerminalInteractor : BaseInteractable, IInteractable
{
    MALEnterUsername enterUsername;

    protected override void Start()
    {
        base.Start();
        enterUsername = FindObjectOfType<MALEnterUsername>();
    }

    public override void Action()
    {
        if (Input.GetKeyDown(KeyCode.O) && enterUsername.inputObject.gameObject.activeSelf == false)
        {
            enterUsername.SetupLogin();
        }
    }
}
