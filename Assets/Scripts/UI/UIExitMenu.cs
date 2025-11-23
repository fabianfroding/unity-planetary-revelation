using System;
using UnityEngine;

public class UIExitMenu : UIBase
{
    private void OnEnable()
    {
        if (menuOpenSound)
            menuOpenSound.Play();
    }
    
    private void OnDisable()
    {
        if (menuCloseSound)
            menuCloseSound.Play();
    }

    public void HandleButtonCancel()
    {
        gameObject.SetActive(false);
    }

    public void HandleButtonExit()
    {
        Application.Quit();
    }
}
