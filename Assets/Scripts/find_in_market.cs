using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class find_in_market : MonoBehaviour {
    public single_market[] markets;

    int _now = 0;
    public single_market AtMarket
    {
        get {
            if (_now < 0) return null;
            else return this.markets[_now];
        }
    }
        

	// Use this for initialization
	void Start () {
        foreach (var market in markets) {
            market.gameObject.SetActive(false);
        }

        if (AtMarket) {
            AtMarket.ShowMarket();
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void NextMarket() 
    {
        if (this.markets.Length == 0) return;

        _now += 1;
        if (_now >= this.markets.Length) _now = -1;
        else this.AtMarket.ShowMarket();
    }
}
