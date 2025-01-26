using UnityEngine;
using System.Collections;

public class AnimationAutoDestroy : MonoBehaviour {
	public float delay = 0f;

	void Start () {
		 
	}

    void OnDestroy(){
        Destroy (gameObject);
    }
}