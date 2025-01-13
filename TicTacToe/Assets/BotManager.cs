using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotManager : MonoBehaviour
{
    [SerializeField] private GameManager gm;
    [SerializeField] private int mistakeChance = 30; // chance for the bot to make a mistake (medium difficulty)

    // easy bot move simply picks a random available move
    public int EasyBotMove() {
        List<int> availableMoves = new();
        for (int i = 0; i < gm.fieldButtons.Length; i++) {
            if (!gm.fieldButtons[i].isSet) {
                availableMoves.Add(i);
            }
        }
        if (availableMoves.Count > 0) {
            int randomIndex = Random.Range(0, availableMoves.Count);
            return availableMoves[randomIndex];
        }
        return 0; // return 0 if no available moves
    }
    
    // same logic as hard bot move but less smart, medium bot move has a chance to make a mistake
    public int MediumBotMove() {
        List<int> availableMoves = new();
        for (int i = 0; i < gm.fieldButtons.Length; i++) {
            if (!gm.fieldButtons[i].isSet) {
                availableMoves.Add(i);
            }
        }

        // add a chance for the bot to make a mistake
        if (Random.Range(0, 100) < mistakeChance) { // chance to make a mistake and pick a random move rather than thinking about it
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

            for (int j = 0; j < gm.winPatterns.GetLength(1); j++) {
                int index = gm.winPatterns[i, j];
                if (gm.fieldButtons[index].isSet && gm.fieldButtons[index].x.enabled == true) {
                    playerCount++;
                } else if (!gm.fieldButtons[index].isSet) {
                    emptyCount++;
                    emptyIndex = index;
                }
            }

            if (playerCount == 2 && emptyCount == 1) {
                return emptyIndex;
            }
        }

        // Try to complete a win pattern
        for (int i = 0; i < gm.winPatterns.GetLength(0); i++) {
            int botCount = 0;
            int emptyCount = 0;
            int emptyIndex = -1;

            for (int j = 0; j < gm.winPatterns.GetLength(1); j++) {
                int index = gm.winPatterns[i, j];
                if (gm.fieldButtons[index].isSet && gm.fieldButtons[index].o.enabled == true) {
                    botCount++;
                } else if (!gm.fieldButtons[index].isSet) {
                    emptyCount++;
                    emptyIndex = index;
                }
            }

            if (botCount == 2 && emptyCount == 1) {
                return emptyIndex;
            }
        }

        // If no immediate win or block, pick a random available move
        if (availableMoves.Count > 0) {
            int randomIndex = Random.Range(0, availableMoves.Count);
            return availableMoves[randomIndex];
        }

        return 0; // return 0 if no available moves
    }

    // hard bot move picks the best possible move and tries to deny the player a win while trying to win itself
    public int HardBotMove() {
        List<int> availableMoves = new();
        for (int i = 0; i < gm.fieldButtons.Length; i++) {
            if (!gm.fieldButtons[i].isSet) {
                availableMoves.Add(i);
            }
        }

        // Check if the player is about to win and block them
        for (int i = 0; i < gm.winPatterns.GetLength(0); i++) {
            int playerCount = 0;
            int emptyCount = 0;
            int emptyIndex = -1;

            for (int j = 0; j < gm.winPatterns.GetLength(1); j++) {
                int index = gm.winPatterns[i, j];
                if (gm.fieldButtons[index].isSet && gm.fieldButtons[index].x.enabled == true) {
                    playerCount++;
                } else if (!gm.fieldButtons[index].isSet) {
                    emptyCount++;
                    emptyIndex = index;
                }
            }

            if (playerCount == 2 && emptyCount == 1) {
                return emptyIndex;
            }
        }

        // Try to complete a win pattern
        for (int i = 0; i < gm.winPatterns.GetLength(0); i++) {
            int botCount = 0;
            int emptyCount = 0;
            int emptyIndex = -1;

            for (int j = 0; j < gm.winPatterns.GetLength(1); j++) {
                int index = gm.winPatterns[i, j];
                if (gm.fieldButtons[index].isSet && gm.fieldButtons[index].o.enabled == true) {
                    botCount++;
                } else if (!gm.fieldButtons[index].isSet) {
                    emptyCount++;
                    emptyIndex = index;
                }
            }

            if (botCount == 2 && emptyCount == 1) {
                return emptyIndex;
            }
        }

        // If no immediate win or block, pick a random available move
        if (availableMoves.Count > 0) {
            int randomIndex = Random.Range(0, availableMoves.Count);
            return availableMoves[randomIndex];
        }

        return 0; // return 0 if no available moves
    }

    
}
