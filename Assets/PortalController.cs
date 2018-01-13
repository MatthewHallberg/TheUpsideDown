using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PortalController : MonoBehaviour {
	
	private static PortalController _instance;
	public static PortalController Instance { get { return _instance; } }

	public GameObject strangerSprite;
	public Material[] materials;
	public MeshRenderer meshRenderer;

	private bool isInside = false;
	private bool isOutside = true;

	public Animation strangerAnim;

	void Awake(){
		_instance = this;
	}

	void Start(){
		OutsidePortal ();
	}

	public void InsidePortal(){
		StartCoroutine (DelayChangeMat (6));
	}

	void OnTriggerStay(Collider col){
		Vector3 playerPos = Camera.main.transform.position + Camera.main.transform.forward * (Camera.main.nearClipPlane + .01f);
		if (transform.InverseTransformPoint (playerPos).z <= 0) {
			if (isOutside) {
				isOutside = false;
				isInside = true;
				InsidePortal ();
			}
		} else {
			if (isInside) {
				isInside = false;
				isOutside = true;
				OutsidePortal ();
			}
		}
	}

	public void OutsidePortal(){
		StartCoroutine (DelayChangeMat (3));
	}

	IEnumerator DelayChangeMat(int stencilNum){
		meshRenderer.enabled = false;
		foreach (var mat in materials) {
			mat.SetInt ("_StencilTest", stencilNum);
		}
		yield return new WaitForEndOfFrame ();
		meshRenderer.enabled = true;
	}
}


