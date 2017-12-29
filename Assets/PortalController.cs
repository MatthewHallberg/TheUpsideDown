using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PortalController : MonoBehaviour {

	public Material[] materials;

	void Start(){
		OutsidePortal ();
	}

	void OutsidePortal(){
		foreach (var mat in materials) {
			mat.SetInt ("_StencilTest", (int)CompareFunction.Equal);
		}
	}

	void InsidePortal(){
		foreach (var mat in materials) {
			mat.SetInt ("_StencilTest", (int)CompareFunction.NotEqual);
		}
	}

	void OnTriggerStay(Collider col){
		if (transform.position.z < col.transform.position.z) {
			InsidePortal ();
		} else {
			OutsidePortal ();
		}		
	}
}
