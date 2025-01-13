using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotManager : MonoBehaviour
{
    // each of these functions returns a field index that will be set by the bot
    // the field index is calculated based on the current game state and intelligence level of the bot
    // each difficulty level has a different algorithm to find the best move, leading to different intelligence levels
    public int EasyBotMove() {
        return 0;
    }

    public int MediumBotMove() {
        return 0;
    }

    public int HardBotMove() {
        return 0;
    }

    public int ImpossibleBotMove() {
        return 0;
    }
}
