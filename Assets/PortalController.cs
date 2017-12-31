using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PortalController : MonoBehaviour {

	public Material[] materials;
	public Transform device;

	bool wasInfront;
	bool isInFront;
	bool insidePortal;

	void Start(){
		SetMaterials (false);
	}

	void SetMaterials(bool FullRender){
		if (FullRender) {
			foreach (var mat in materials) {
				mat.SetInt ("_StencilTest", (int)CompareFunction.NotEqual);
				Debug.Log ("Inside");
			}
		} else {
			foreach (var mat in materials) {
				mat.SetInt ("_StencilTest", (int)CompareFunction.Equal);
				Debug.Log ("Outside");
			}
		}
	}

	bool GetIsInFront(){
		Vector3 pos = transform.InverseTransformPoint (device.position);
		if (pos.z >= 0) {
			return true;
		} else {
			return false;
		}
	}

	void OnTriggerEnter(Collider col){
		wasInfront = GetIsInFront ();
	}

	void OnTriggerStay(Collider col){

		bool isInFront = GetIsInFront ();

		if ((isInFront && !wasInfront) || (wasInfront && !isInFront)) {
			insidePortal = !insidePortal;
			SetMaterials (insidePortal);
		}
		wasInfront = isInFront;
	}
}
