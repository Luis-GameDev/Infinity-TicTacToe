using System;
using System.Collections.Generic;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // enum to store all different difficulties
    [HideInInspector] public enum Difficulty { easy, medium, hard }

    // bool to check if the player can do his next move or not
    [HideInInspector] public bool canMove = true;

    // current difficulty of the game
    public Difficulty currentDifficulty = Difficulty.easy;

    private int maxMoveCount = 7; // max. amount of fields that can be set at the same time
    private int matchCount = 0; // amount of matches played
    private int winCount = 0; // amount of wins

    // win patterns for the game board
    [HideInInspector] public int[,] winPatterns = new int[,] {
        {0, 1, 2}, // Row 1
        {3, 4, 5}, // Row 2
        {6, 7, 8}, // Row 3
        {0, 3, 6}, // Column 1
        {1, 4, 7}, // Column 2
        {2, 5, 8}, // Column 3
        {0, 4, 8}, // Diagonal 1
        {2, 4, 6}  // Diagonal 2
    };
    
    [Header("Debugging")]
    [SerializeField] private List<int> moveHistory = new List<int>(); // To store the sequence of moves by field indexes

    [Header("UI Elements")]
    [SerializeField] private TextMeshProUGUI winnerText;  // Text for displaying the winner
    [SerializeField] public GameButton[] fieldButtons;  // Buttons for the fields on the game board
    [SerializeField] private TextMeshProUGUI winCountText; // reference to the win count text in the stats
    [SerializeField] private TextMeshProUGUI matchCountText; // reference to the match count text in the stats
    [SerializeField] private TextMeshProUGUI winrateText; // reference to the winrate text in the stats
    [SerializeField] private GameObject winParticles; // reference to the win particles
    [SerializeField] private TextMeshProUGUI turnText; // reference to the turn text

    [Header("References")]
    [SerializeField] private BotManager AI; // Reference to the BotManager script
    

    // function to handle the field button clicks, doesnt necessarily need to be clicked with the mouse, but can also be called by the bot
    // every button itself handles the click event and then calls this function with the index of the button
    // if called by a button the isPlayer bool is true, if called by the bot it is false
    public void ButtonClicked(int index, bool isPlayer)
    {
        // check if fields need to be reset, do it before the move is performed so it doesnt count as win
        if(moveHistory.Count >= maxMoveCount) {
            ResetField(moveHistory[0]);
            moveHistory.RemoveAt(0);
        }

        // perform move and occupy the field based on the isPlayer bool
        if(isPlayer && canMove) {
            fieldButtons[index].x.enabled = true;
            canMove = false;
            turnText.text = "Bot's turn!";
            moveHistory.Add(index);
            StartCoroutine(SelectBotMove());
        } else if(!isPlayer && !canMove) {
            fieldButtons[index].o.enabled = true;
            fieldButtons[index].isSet = true;
            canMove = true;
            turnText.text = "Your turn!";
            moveHistory.Add(index);
        }
        // after each move check if someone won
        CheckWin();
    }
    
    // giveup button to reset the game and give the bot a win
    public void GiveUp() {
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

    // function to reset a field based on the given index
    private void ResetField(int index)
    {
        fieldButtons[index].x.enabled = false;
        fieldButtons[index].o.enabled = false;
        fieldButtons[index].isSet = false;
    }

    // function to check if a winpattern was completed by the bot or player
    private void CheckWin() {

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
                    StartCoroutine(WinAnimation(true));
                    winCountText.text = "Wins: " + winCount;
                    matchCountText.text = "Played: " + matchCount;
                    winrateText.text = "Winrate: " + MathF.Round((float)winCount / matchCount * 100) + "%";
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
                    StartCoroutine(WinAnimation(false));
                    matchCountText.text = "Played: " + matchCount;
                    winrateText.text = "Winrate: " + MathF.Round((float)winCount / matchCount * 100) + "%";
                    return;
                }
            }
        }
    }

    public IEnumerator WinAnimation(bool playerWon = true) {
        if(playerWon) winParticles.SetActive(true);
        yield return new WaitForSeconds(2);
        winParticles.SetActive(false);
        winnerText.text = "";
    }

    // function to select the bot move based on the current difficulty selected
    public IEnumerator SelectBotMove() {
        // Wait for 2 seconds to simulate thinking time
        yield return new WaitForSeconds(2);

        switch (currentDifficulty) {
            case Difficulty.easy:
                ButtonClicked(AI.EasyBotMove(), false);
                break;
            case Difficulty.medium:
                ButtonClicked(AI.MediumBotMove(), false);
                break;
            case Difficulty.hard:
                ButtonClicked(AI.HardBotMove(), false);
                break;
            default:
                ButtonClicked(AI.EasyBotMove(), false);
                break;
        }
    }
}
