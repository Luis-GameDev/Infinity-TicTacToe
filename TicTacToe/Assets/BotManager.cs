using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotManager : MonoBehaviour
{
    [SerializeField] private GameManager gm;
    // each of these functions returns a field index that will be set by the bot
    // the field index is calculated based on the current game state and intelligence level of the bot
    // each difficulty level has a different algorithm to find the best move, leading to different intelligence levels

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

    public int MediumBotMove() {
        return 0;
    }

    public int HardBotMove() {
        return 0;
    }
}
