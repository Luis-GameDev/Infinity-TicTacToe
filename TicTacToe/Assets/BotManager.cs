using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotManager : MonoBehaviour
{
    // Reference to the GameManager script
    [SerializeField] private GameManager gm;
    
    // Chance for the bot to make a mistake (medium difficulty)
    [SerializeField] private int mistakeChance = 30;

    // Easy bot move simply picks a random available move
    public int EasyBotMove() {
        // List to store available moves in (to sort between occupied fields and open ones)
        List<int> availableMoves = new();
        
        // Loop through all field buttons to find open fields
        for (int i = 0; i < gm.fieldButtons.Length; i++) {
            if (!gm.fieldButtons[i].isSet) {
                availableMoves.Add(i);
            }
        }
        
        // pick a random index from the available moves and return it to the GameManager to set the bots move
        if (availableMoves.Count > 0) {
            int randomIndex = Random.Range(0, availableMoves.Count);
            return availableMoves[randomIndex];
        }
        
        // Return 0 if no available moves (this should never be triggered since there should always be available moves)
        return 0;
    }
    
    // Medium bot move has a chance to make a mistake, but will try to block the player from winning while trying to win itself
    public int MediumBotMove() {
        // List to store available moves
        List<int> availableMoves = new();
        
        // Loop through all field buttons to find available moves
        for (int i = 0; i < gm.fieldButtons.Length; i++) {
            if (!gm.fieldButtons[i].isSet) {
                availableMoves.Add(i);
            }
        }

        // Add a chance for the bot to make a mistake
        if (Random.Range(0, 100) < mistakeChance) {
            // If there are available moves, pick one randomly
            if (availableMoves.Count > 0) {
                int randomIndex = Random.Range(0, availableMoves.Count);
                return availableMoves[randomIndex];
            }
        }

        // Check if the player is about to win and block them
        for (int i = 0; i < gm.winPatterns.GetLength(0); i++) {
            int playerCount = 0;
            int emptyCount = 0;
            int emptyIndex = -1;

            // Check each win pattern
            for (int j = 0; j < gm.winPatterns.GetLength(1); j++) {
                int index = gm.winPatterns[i, j];
                if (gm.fieldButtons[index].isSet && gm.fieldButtons[index].x.enabled == true) {
                    playerCount++;
                } else if (!gm.fieldButtons[index].isSet) {
                    emptyCount++;
                    emptyIndex = index;
                }
            }

            // If the player is about to win, block them
            if (playerCount == 2 && emptyCount == 1) {
                return emptyIndex;
            }
        }

        // Try to complete a win pattern
        for (int i = 0; i < gm.winPatterns.GetLength(0); i++) {
            int botCount = 0;
            int emptyCount = 0;
            int emptyIndex = -1;

            // Check each win pattern
            for (int j = 0; j < gm.winPatterns.GetLength(1); j++) {
                int index = gm.winPatterns[i, j];
                if (gm.fieldButtons[index].isSet && gm.fieldButtons[index].o.enabled == true) {
                    botCount++;
                } else if (!gm.fieldButtons[index].isSet) {
                    emptyCount++;
                    emptyIndex = index;
                }
            }

            // If the bot can win, complete the pattern
            if (botCount == 2 && emptyCount == 1) {
                return emptyIndex;
            }
        }

        // If no immediate win or block, pick a random available move
        if (availableMoves.Count > 0) {
            int randomIndex = Random.Range(0, availableMoves.Count);
            return availableMoves[randomIndex];
        }

        // Return 0 if no available moves
        return 0;
    }

    // Hard bot move picks the best possible move and tries to deny the player a win while trying to win itself
    // this is the same as the MediumBotMove function, but the bot will always make the best move and prioritize winning over blocking the player
    public int HardBotMove() {
        // List to store available moves
        List<int> availableMoves = new();
        
        // Loop through all field buttons to find available moves
        for (int i = 0; i < gm.fieldButtons.Length; i++) {
            if (!gm.fieldButtons[i].isSet) {
                availableMoves.Add(i);
            }
        }
        
        // Try to complete a win pattern before blocking the player
        for (int i = 0; i < gm.winPatterns.GetLength(0); i++) {
            int botCount = 0;
            int emptyCount = 0;
            int emptyIndex = -1;

            // Check each win pattern
            for (int j = 0; j < gm.winPatterns.GetLength(1); j++) {
                int index = gm.winPatterns[i, j];
                if (gm.fieldButtons[index].isSet && gm.fieldButtons[index].o.enabled == true) {
                    botCount++;
                } else if (!gm.fieldButtons[index].isSet) {
                    emptyCount++;
                    emptyIndex = index;
                }
            }

            // If the bot can win, complete the pattern
            if (botCount == 2 && emptyCount == 1) {
                return emptyIndex;
            }
        }

        // Check if the player is about to win and block them
        for (int i = 0; i < gm.winPatterns.GetLength(0); i++) {
            int playerCount = 0;
            int emptyCount = 0;
            int emptyIndex = -1;

            // Check each win pattern
            for (int j = 0; j < gm.winPatterns.GetLength(1); j++) {
                int index = gm.winPatterns[i, j];
                if (gm.fieldButtons[index].isSet && gm.fieldButtons[index].x.enabled == true) {
                    playerCount++;
                } else if (!gm.fieldButtons[index].isSet) {
                    emptyCount++;
                    emptyIndex = index;
                }
            }

            // If the player is about to win, block them
            if (playerCount == 2 && emptyCount == 1) {
                return emptyIndex;
            }
        }

        // If no immediate win or block, pick a random available move
        if (availableMoves.Count > 0) {
            int randomIndex = Random.Range(0, availableMoves.Count);
            return availableMoves[randomIndex];
        }

        // Return 0 if no available moves
        return 0;
    }
}
