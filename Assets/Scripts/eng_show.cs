using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class eng_show : MonoBehaviour {
    public Text _engText;

    // Use this for initialization
    private void Awake()
    {
        enabled = false;
    }

    private void Start()
    {
        
    }

    // Update is called once per frame
    void Update () {
		
	}

    public void ShowText(string content){
        _engText.text = content;
        enabled = true;
    }

    public void HideText() {
        enabled = false;
    }
}
