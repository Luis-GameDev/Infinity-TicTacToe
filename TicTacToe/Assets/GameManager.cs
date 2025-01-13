using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [HideInInspector] public enum Difficulty { easy, medium, hard, impossible }
    [HideInInspector] public bool canMove = true;

    public Difficulty currentDifficulty = Difficulty.easy;

    private int maxMoveCount = 7; // max. amount of fields that can be set at the same time
    private int matchCount = 0; // amount of matches played
    private int winCount = 0; // amount of wins
    
    [Header("Debugging")]
    [SerializeField] private List<int> moveHistory = new List<int>(); // To store the sequence of moves (field indexes)

    [Header("UI Elements")]
    [SerializeField] private TextMeshProUGUI winnerText;  // Text for displaying the winner
    [SerializeField] private GameButton[] fieldButtons;  // Buttons for the fields on the game board

    [Header("References")]
    [SerializeField] private BotManager AI; // Reference to the BotManager script
    [SerializeField] private TextMeshProUGUI winCountText;
    [SerializeField] private TextMeshProUGUI matchCountText;
    [SerializeField] private TextMeshProUGUI winrateText;

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
            SelectBotMove();
        } else if(!isPlayer && !canMove) {
            fieldButtons[index].o.enabled = true;
            canMove = true;
            moveHistory.Add(index);
        }
        CheckWin();
    }

    private void ResetField(int index)
    {
        fieldButtons[index].x.enabled = false;
        fieldButtons[index].o.enabled = false;
        fieldButtons[index].isSet = false;
    }

    private void CheckWin() {
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

            if (fieldButtons[a].isSet && fieldButtons[b].isSet && fieldButtons[c].isSet)
            {
                if (fieldButtons[a].x.enabled && fieldButtons[b].x.enabled && fieldButtons[c].x.enabled)
                {
                    winnerText.text = "You won!";
                    for (int j = 0; j < fieldButtons.Length; j++)
                    {
                        ResetField(j);
                    }
                    moveHistory.Clear();
                    winCount++;
                    matchCount++;
                    winCountText.text = "Wins: " + winCount;
                    matchCountText.text = "Played: " + matchCount;
                    winrateText.text = "Winrate: " + ((float)winCount / matchCount * 100) + "%";
                    return;
                }
                else if (fieldButtons[a].o.enabled && fieldButtons[b].o.enabled && fieldButtons[c].o.enabled)
                {
                    winnerText.text = "Bot won!";
                    for (int j = 0; j < fieldButtons.Length; j++)
                    {
                        ResetField(j);
                    }
                    moveHistory.Clear();
                    matchCount++;
                    matchCountText.text = "Played: " + matchCount;
                    winrateText.text = "Winrate: " + ((float)winCount / matchCount * 100) + "%";
                    return;
                }
            }
        }
    }

    private void SelectBotMove()
    {
        switch (currentDifficulty)
        {
            case Difficulty.easy:
                ButtonClicked(AI.EasyBotMove(), false);
                break;
            case Difficulty.medium:
                ButtonClicked(AI.MediumBotMove(), false);
                break;
            case Difficulty.hard:
                ButtonClicked(AI.HardBotMove(), false);
                break;
            case Difficulty.impossible:
                ButtonClicked(AI.ImpossibleBotMove(), false);
                break;
        }
    }
}
