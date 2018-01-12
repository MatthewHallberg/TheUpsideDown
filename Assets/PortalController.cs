using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

namespace UnityEngine.XR.iOS
{
	public class PortalController : MonoBehaviour {
		
		private static PortalController _instance;
		public static PortalController Instance { get { return _instance; } }

		public GameObject strangerSprite;
		public Material[] materials;
		public MeshRenderer meshRender;

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
			print ("IN");
			StartCoroutine (DelayChangeMat (6));
		}

		void OnTriggerStay(Collider col){
			Vector3 playerPos = Camera.main.transform.position + Camera.main.transform.forward * Camera.main.nearClipPlane;
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

			if (Mathf.Abs (transform.InverseTransformPoint (playerPos).z) < .5f) {
				meshRender.enabled = false;
				print ("HERE!");
			} else {
				meshRender.enabled = true;
			}
		}

		public void OutsidePortal(){
			print ("OUT");
			StartCoroutine (DelayChangeMat (3));
		}

		IEnumerator DelayChangeMat(int stencilNum){
			print ("FUCK!");
			yield return new WaitForEndOfFrame ();
			foreach (var mat in materials) {
				mat.SetInt ("_StencilTest", stencilNum);
			}
		}
	}
}

