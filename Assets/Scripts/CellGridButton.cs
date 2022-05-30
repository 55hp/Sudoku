using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CellGridButton : MonoBehaviour
{
    [SerializeField] int myCol;
    [SerializeField] int myRow;

    bool hasValue = false;

    private void OnEnable()
    {
        GameManager.OnBoardLoaded += BoardLoaded;
        GameManager.OnCellSelected += AnotherCellHasBeenSelected;
        GameManager.OnNumberPressed += SetValue;
        GameManager.OnAskForHint += HintMe;
    }

    private void OnDisable()
    {
        GameManager.OnBoardLoaded -= BoardLoaded;
        GameManager.OnCellSelected -= AnotherCellHasBeenSelected;
        GameManager.OnNumberPressed -= SetValue;
        GameManager.OnAskForHint -= HintMe;
    }

    /// <summary>
    /// Observer function.
    /// When the board is loaded, if the tile has already a value, it sets his text whith it.
    /// </summary>
    /// <param name="board"></param>
    public void BoardLoaded(SudokuBoard board)
    {
        this.GetComponentInChildren<Text>().text = "";
        hasValue = false;
        int value = board.GetSudokuTileValue(myRow, myCol);
        if (value > 0 && value < 10)
        {
            this.GetComponentInChildren<Text>().text = "" + value;
            hasValue = true;
            GameManager.AddValidatedCell();
            GameManager.OnNumberPressed -= SetValue;
        }
    }

    /// <summary>
    /// Observer function.
    /// When a number is pressed, if the tile is the selected one and the number is equal to the value, it sets his text whith it.
    /// </summary>
    public void SetValue(int value)
    {
        if(GameManager.selectedCellRow == myRow && GameManager.selectedCellCol == myCol)
        {
            this.GetComponentInChildren<Text>().text = "" + value;
            hasValue = true;
            GameManager.AddValidatedCell();
            GameManager.OnNumberPressed -= SetValue;
        }
    }

    /// <summary>
    /// Observer function.
    /// When the hint button is pressed, if the tile is not valuated tells the game manager his availability to be a hint.
    /// </summary>
    public void HintMe()
    {
        if (!hasValue)
        {
            GameManager.hint = new Vector2(myRow, myCol);
        } 
    }

    /// <summary>
    /// If this tile is not valuated, it becomes the selected one.
    /// </summary>
    public void SelectMe()
    {
        if (!hasValue)
        {
            GameManager.SelectNewCell(myRow, myCol);
            Debug.Log("" + myRow + " " + myCol);
            this.GetComponent<Image>().color = Color.yellow;
        }
    }

    /// <summary>
    /// This tile is no more the selected one.
    /// </summary>
    /// <param name="r"></param>
    /// <param name="c"></param>
    public void AnotherCellHasBeenSelected(int r, int c)
    {
        //Reset color.
        this.GetComponent<Image>().color = Color.black;
    }
}
