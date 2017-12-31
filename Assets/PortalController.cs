using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PortalController : MonoBehaviour {

	public GameObject strangerSprite;
	public Material[] materials;
	public Transform device;

	bool wasInfront;
	bool isInFront;
	bool insidePortal;
	bool animCanPlay = true;
	Animation strangerAnim;

	void Start(){
		SetMaterials (false);
		strangerAnim = strangerSprite.GetComponent<Animation> ();
	}

	void SetMaterials(bool FullRender){
		if (FullRender) {
			animCanPlay = true;
			strangerSprite.GetComponent<SpriteRenderer> ().enabled = false;
			foreach (var mat in materials) {
				mat.SetInt ("_StencilTest", (int)CompareFunction.NotEqual);
				Debug.Log ("Inside");
			}
		} else {
			if (animCanPlay) {
				strangerSprite.GetComponent<SpriteRenderer> ().enabled = true;
			}
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
		if (!strangerAnim.isPlaying && animCanPlay && !insidePortal) {
			animCanPlay = false;
			strangerAnim.Play ();
		}
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
