using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static GameManager;
using UnityEngine.UI;

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
        Image parentImage = buttonText.GetComponentInParent<Image>();
        switch (gm.currentDifficulty)
        {
            case Difficulty.easy:
            parentImage.color = Color.green;
            break;
            case Difficulty.medium:
            parentImage.color = new Color(1.0f, 0.64f, 0.0f); // Orange
            break;
            case Difficulty.hard:
            parentImage.color = Color.red;
            break;
        }
        buttonText.text = gm.currentDifficulty.ToString();
    }
    
}