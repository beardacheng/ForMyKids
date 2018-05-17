using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartEngItem : eng_item {

    // Use this for initialization
    protected override void Start () {
        base.Start();
        GetComponent<PolygonCollider2D>().enabled = false;

        base._PlayEng();
    }


}
