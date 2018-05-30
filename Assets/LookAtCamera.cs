using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour {

    Animator animator;
    public bool isLookAtCamera;
    float lookWeight = 1f;
    float lerpFactor = 5f;

    // Use this for initialization
    void Start () {
        animator = GetComponent<Animator>();
	}

    // Update is called once per frame
    void Update()
    {
        if (isLookAtCamera)
        {
            lookWeight=Mathf.Lerp(lookWeight, 1f, Time.deltaTime * lerpFactor);
        }
        else
        {
            lookWeight=Mathf.Lerp(lookWeight, 0f, Time.deltaTime * lerpFactor);
        }
    }

    private void OnAnimatorIK(int layerIndex)
    {
        animator.SetLookAtPosition(Camera.main.transform.position);
        animator.SetLookAtWeight(lookWeight);
    }
}
