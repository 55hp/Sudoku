using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public static SudokuBoardGameLogic m_GameLogic;
    private bool playing = false;

    //-1 is the void value.
    public static int selectedCellRow, selectedCellCol = -1;
    private static int validatedCellCount;
    
    float time;
    [SerializeField] Text timeLabel;
    [SerializeField] Text finalScoreLabel;
    [SerializeField] GameObject gameOverScreen;

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        if (playing)
        {
            time += Time.deltaTime;
            timeLabel.text = "" + time.ToString("F0");
        }
    }

    /// <summary>
    /// Starting setup for the game.
    /// </summary>
    public void StartGame()
    {
        playing = true;
        m_GameLogic = new SudokuBoardGameLogic();
        m_GameLogic.LoadSudokuBoard("/Boards/Easy/easy_board.json");
        LoadBoard(m_GameLogic.GetBoard());
        m_GameLogic.Solve();
        time = 0;
    }

    /// <summary>
    /// Stop the game and opens the Game Over Screen.
    /// </summary>
    public void EndGame()
    {
        gameOverScreen.SetActive(true);
        playing = false;
        finalScoreLabel.text = "FINAL SCORE:\n" + ( 10000 - (int) time);
    }

    /// <summary>
    /// Closes the application.
    /// </summary>
    public void QuitGame()
    {
        Application.Quit();
    }

    /// <summary>
    /// This method keeps track of the sudoku progression. When the sudoku is completed, it call the EndGame function.
    /// </summary>
    public static void AddValidatedCell()
    {
        validatedCellCount++;
        if (validatedCellCount == 81)
        {
            instance.EndGame();
        }
    }

    public static Vector2 hint = new Vector2(0, 0);

    /// <summary>
    /// Sets and calls an available hint.
    /// </summary>
    public static void SetHint()
    {
        int r = (int)hint.x;
        int c = (int)hint.y;

        int value = m_GameLogic.GetBoard().GetSudokuTileValue(r, c);
        selectedCellCol = c;
        selectedCellRow = r;
        PressNumber(value);
    }

    #region EVENTS_REGION

    public delegate void OnBoardLoadHandler(SudokuBoard board);
    public static event OnBoardLoadHandler OnBoardLoaded;

    public static void LoadBoard(SudokuBoard board)
    {
        OnBoardLoaded?.Invoke(board);
    }


    public delegate void OnCellSelectHandler(int r, int c );
    public static event OnCellSelectHandler OnCellSelected;

    public static void SelectNewCell(int r, int c)
    {
        OnCellSelected?.Invoke(r,c);
        selectedCellRow = r;
        selectedCellCol = c;
    }


    public delegate void OnAskForHintHandler();
    public static event OnAskForHintHandler OnAskForHint;

    public static void AskForHint()
    {
        OnAskForHint?.Invoke();
    }


    public delegate void OnNumberPressHandler(int value);
    public static event OnNumberPressHandler OnNumberPressed;

    public static bool PressNumber(int buttonNumber)
    {
        if((m_GameLogic.GetBoard().GetSudokuTileValue(selectedCellRow , selectedCellCol) == buttonNumber) && selectedCellCol != -1)
        {
            OnNumberPressed?.Invoke(buttonNumber);
            //Selct the "void" cell.
            OnCellSelected?.Invoke(-1, -1);
            return true;
        }
        else
        {
            WrongNumber();
            return false;
        }
    }

    public delegate void OnWrongNumberPressHandler();
    public static event OnWrongNumberPressHandler OnWrongNumberPressed;

    public static void WrongNumber()
    {
        OnWrongNumberPressed?.Invoke();
    }

    #endregion
    
}