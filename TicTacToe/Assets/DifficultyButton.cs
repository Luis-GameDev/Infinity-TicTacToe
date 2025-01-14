using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static GameManager;
using UnityEngine.UI;

public class DifficultyButton : MonoBehaviour
{
    // references to the GameManager script and the button text
    [SerializeField] private GameManager gm;
    [SerializeField] private TextMeshProUGUI buttonText;

    // array to store all different difficulties
    private Difficulty[] difficulties = { Difficulty.easy, Difficulty.medium, Difficulty.hard };

    private void Start()
    {
        UpdateButtonText();
    }

    // function to switch the difficulty of the game when the button is clicked, it will cycle through the difficulties
    public void SwitchDifficulty()
    {
        int currentIndex = System.Array.IndexOf(difficulties, gm.currentDifficulty);
        int nextIndex = (currentIndex + 1) % difficulties.Length;
        gm.currentDifficulty = difficulties[nextIndex];
        UpdateButtonText();
    }

    // updates the text inside the button to display the current difficulty aswell as its color
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