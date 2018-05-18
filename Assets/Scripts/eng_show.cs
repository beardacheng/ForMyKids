using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class eng_show : MonoBehaviour {
    public Text _engText;

    private Image __image;
    private Image _image {
        get {
            if (__image == null) __image = gameObject.GetComponent<Image>();
            return __image;
        }
    }

    // Use this for initialization
    private void Awake()
    {
        gameObject.SetActive(false);
    }

    private void Start()
    {
        
    }

    // Update is called once per frame
    void Update () {
		
	}

    public void ShowText(string content){
        _engText.text = content;
        _image.raycastTarget = true;
        gameObject.SetActive(true);
    }

    public void HideText() {
        PlayEnd();
        gameObject.SetActive(false);
    }

    public void PlayEnd() {
        _image.raycastTarget = false;
    }
}
