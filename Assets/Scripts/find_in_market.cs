using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class find_in_market : MonoBehaviour {
    public single_market[] markets;

    private int _now = 0;
    public int now {
        get {
            return _now;
        }
        set {
            if (value >= 0 && value < markets.Length) _now = value;
        }
    }

    public single_market AtMarket
    {
        get {
            if (now < 0) return null;
            else return this.markets[now];
        }
    }
        

	// Use this for initialization
	void Start () {
        _FlushMarket();
    }

    private void _FlushMarket() {
        if (AtMarket) {
            AtMarket.Show();
        }
    }
	
	// Update is called once per frame
	void Update () {
    
    }

    public void NextMarket() 
    {
        if (this.markets.Length == 0) return;

        if (AtMarket) AtMarket.Hide();

        now += 1;
        if (now >= this.markets.Length) now = -1;
        if (AtMarket) AtMarket.Show();
    }

#if UNITY_EDITOR
    void FlushMarketInEdtor() {
        foreach (var m in markets) {
            string sortingLayerName = "DisabledMarket";
            if (AtMarket == m) sortingLayerName = "EnabledMarket";
            m.GetComponent<SpriteRenderer>().sortingLayerName = sortingLayerName;
        }
    }
    void OnValidate() {
        FlushMarketInEdtor();
    }

    public void EditorSetNow(int now) {
        this.now = now;
        FlushMarketInEdtor();
    }

#endif
}
