using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [HideInInspector] public enum Difficulty { easy, medium, hard, impossible }
    [HideInInspector] public bool canMove = true;

    public Difficulty currentDifficulty = Difficulty.easy;

    private int maxMoveCount = 7; // max. amount of fields that can be set at the same time
    
    [Header("Debugging")]
    [SerializeField] private List<int> moveHistory = new List<int>(); // To store the sequence of moves (field indexes)

    [Header("UI Elements")]
    [SerializeField] private Text winnerText;  // Text for displaying the winner
    [SerializeField] private GameButton[] fieldButtons;  // Buttons for the fields on the game board

    [Header("References")]
    [SerializeField] private BotManager AI; // Reference to the BotManager script

    void Start()
    {

    }

    public void ButtonClicked(int index, bool isPlayer)
    {
        // check if fields needs to be reset
        if(moveHistory.Count >= maxMoveCount) {
            ResetField(moveHistory[0]);
            moveHistory.RemoveAt(0);
        }

        // perform move
        if(isPlayer && canMove) {
            fieldButtons[index].x.enabled = true;
            canMove = false;
            moveHistory.Add(index);
        } else {
            fieldButtons[index].o.enabled = true;
            canMove = true;
            moveHistory.Add(index);
        }
    }

    private void ResetField(int index)
    {
        fieldButtons[index].x.enabled = false;
        fieldButtons[index].o.enabled = false;
        fieldButtons[index].isSet = false;
    }

    private void SelectBotMove()
    {
        switch (currentDifficulty)
        {
            case Difficulty.easy:
                AI.EasyBotMove();
                break;
            case Difficulty.medium:
                AI.MediumBotMove();
                break;
            case Difficulty.hard:
                AI.HardBotMove();
                break;
            case Difficulty.impossible:
                AI.ImpossibleBotMove();
                break;
        }
    }

    private void CheckGameStatus()
    {
        // Check for a win (horizontal, vertical, diagonal)
        int[,] winPatterns = new int[,] {
            {0, 1, 2}, // Row 1
            {3, 4, 5}, // Row 2
            {6, 7, 8}, // Row 3
            {0, 3, 6}, // Column 1
            {1, 4, 7}, // Column 2
            {2, 5, 8}, // Column 3
            {0, 4, 8}, // Diagonal 1
            {2, 4, 6}  // Diagonal 2
        };

        for (int i = 0; i < winPatterns.GetLength(0); i++)
        {
            int a = winPatterns[i, 0];
            int b = winPatterns[i, 1];
            int c = winPatterns[i, 2];
        }
    }
}
