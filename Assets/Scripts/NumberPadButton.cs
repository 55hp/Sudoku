using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumberPadButton : MonoBehaviour
{
    [SerializeField] int myNumber;

    /// <summary>
    /// The Number Pad funcion.
    /// It tells what number has been pressed.
    /// </summary>
    public void PressMe()
    {
        GameManager.PressNumber(myNumber);
    }


    /// <summary>
    /// Hint button function.
    /// It searches for available tiles and if possibile, activate the hint.
    /// </summary>
    public void AskForHint()
    {
        GameManager.AskForHint();
        GameManager.SetHint();
    }
}
