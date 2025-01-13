using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static GameManager;

public class DifficultyButton : MonoBehaviour
{
    [SerializeField] private GameManager gm;
    [SerializeField] private TextMeshProUGUI buttonText;
    private Difficulty[] difficulties = { Difficulty.easy, Difficulty.medium, Difficulty.hard };
    
    private void Start()
    {
        UpdateButtonText();
    }

    public void SwitchDifficulty()
    {
        int currentIndex = System.Array.IndexOf(difficulties, gm.currentDifficulty);
        int nextIndex = (currentIndex + 1) % difficulties.Length;
        gm.currentDifficulty = difficulties[nextIndex];
        UpdateButtonText();
    }

    private void UpdateButtonText()
    {
        buttonText.text = gm.currentDifficulty.ToString();
    }
    
}