using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartEngItem : eng_item {

    // Use this for initialization
    protected override void Start () {
        base.Start();
        base._PlayEng();
    }

    protected override void OnMouseUp() {
        _speaker.HideText();
        GameObject.FindGameObjectWithTag("MarketController").GetComponent<find_in_market>().NextMarket();
    }
}
