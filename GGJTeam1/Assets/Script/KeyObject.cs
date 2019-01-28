using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyObject : MonoBehaviour {

    public GameObject keyAnimationObject;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        keyAnimationObject.GetComponent<KeyAnimation>().StartAnimation();
        Destroy(this.gameObject);
    }
}
