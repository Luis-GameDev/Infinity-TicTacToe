using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameButton : MonoBehaviour {
    public bool isSet = false;
    [SerializeField] public int index;
    public Image x;
    public Image o;
    [SerializeField] private GameManager gm;

    void Start() {
        x = transform.Find("x").GetComponent<Image>();
        o = transform.Find("o").GetComponent<Image>();
    }

    public void OnClick() {
        if(!isSet) {
            isSet = true;
            gm.ButtonClicked(index, true);
        }
    }
}