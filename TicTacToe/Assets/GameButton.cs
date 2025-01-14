using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameButton : MonoBehaviour {

    // bool to check if the field is occupied or not, if so it cannot be clicked again
    public bool isSet = false;

    // index of the field, used to determine its position on the game board
    [SerializeField] public int index;

    // reference to the x and o images to display on the field
    public Image x;
    public Image o;

    // reference to the GameManager script
    [SerializeField] private GameManager gm;

    // start function to get the references to the x and o images
    void Start() {
        x = transform.Find("x").GetComponent<Image>();
        o = transform.Find("o").GetComponent<Image>();
    }

    // if the field is clicked and not occupied, call the ButtonClicked function in the GameManager script
    public void OnClick() {
        if(!isSet && gm.canMove) {
            isSet = true;
            gm.ButtonClicked(index, true);
        }
    }
}