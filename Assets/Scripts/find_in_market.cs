using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class find_in_market : MonoBehaviour {
    public single_market[] markets;

    public int now = 0;
    public single_market AtMarket
    {
        get {
            if (now < 0) return null;
            else return this.markets[now];
        }
    }
        

	// Use this for initialization
	void Start () {
        foreach (var market in markets) {
            market.gameObject.SetActive(false);
        }

        _FlushMarket();
    }

    private void _FlushMarket() {
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

        now += 1;
        if (now >= this.markets.Length) now = -1;
        else this.AtMarket.ShowMarket();
    }

#if UNITY_EDITOR
    void OnValidate() {
        if (now < 0 || now >= markets.Length) return;

        foreach (var m in markets)
        {
            m.GetComponent<SpriteRenderer>().sortingOrder = (markets[now] == m ? 10 : 0);
        }

        _FlushMarket();
    }

#endif
}
