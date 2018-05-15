using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class single_market : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ShowMarket() {
        this.gameObject.SetActive(true);
        this.gameObject.transform.SetAsLastSibling();
    }
}
