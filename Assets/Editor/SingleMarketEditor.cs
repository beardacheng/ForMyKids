using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(single_market))]
public class SingleMarketEditor : Editor {

    private void OnEnable() {
        var singleMarket = (single_market)target;
        singleMarket.OnEditorClick();
    }
}
