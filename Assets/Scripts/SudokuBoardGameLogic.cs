using System;
using System.IO;
using UnityEngine;
using Newtonsoft.Json;
using System.Collections.Generic;
using UnityEngine.Networking;

public class SudokuBoardGameLogic
{
    private SudokuBoard m_Board;

    public SudokuBoardGameLogic()
    {
        m_Board = new SudokuBoard();
    }

    public SudokuBoard GetBoard()
    {
        return m_Board;
    }

    public void LoadSudokuBoard(string filename)
    {
#if UNITY_EDITOR || UNITY_IOS
        string filePath = Application.dataPath + "/StreamingAssets" + filename;
        string dataAsJson = File.ReadAllText(filePath);
        m_Board = JsonConvert.DeserializeObject<SudokuBoard>(dataAsJson);
#elif UNITY_ANDROID
    string filePath = "jar:file://" + Application.dataPath + "!/assets" + filename;
           WWW reader = new WWW(filePath);
           while (!reader.isDone) { }
           string jsonString = reader.text;
           m_Board = JsonConvert.DeserializeObject<SudokuBoard>(jsonString);
#endif
    }

    public void Solve()
    {
        SolveBoard(m_Board);
    }

    private static bool SolveBoard(SudokuBoard board)
    {
        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                if (board.GetSudokuTileValue(i,j) == 0)
                {
                    for (int c = 1; c <= 9; c++)
                    {
                        if (board.TestSudokuValueValidity(i,j,c))
                        {
                            board.SetSudokuValue(i, j, c);
                            board.PrintBoard();
                            if (SolveBoard(board))
                                return true;
                            else
                                board.ResetSudokuValue(i, j);
                        }
                    }
                    return false;
                }
            }
        }
        return true;
    }
}