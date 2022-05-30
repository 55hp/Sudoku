using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsButton : MonoBehaviour
{
    [SerializeField] GameObject[] credits;

    private int counter = 0;

    /// <summary>
    /// Shows the credits labels one by one.
    /// </summary>
    public void ShowCreditPanel()
    {
        if(counter < credits.Length)
        {
            credits[counter].SetActive(true);
            counter++;
        }
    }
}
