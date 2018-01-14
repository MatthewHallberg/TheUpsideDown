using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

namespace UnityEngine.XR.iOS
{
	public class PortalController : MonoBehaviour {
		
		private static PortalController _instance;
		public static PortalController Instance { get { return _instance; } }

		public Material[] materials;
		public MeshRenderer meshRenderer;
		public UnityARVideo unityARVideo;

		public AnimationBehavior animBehavior;

		private bool isInside = false;
		public bool isOutside = true;

		void Awake(){
			_instance = this;
		}

		void Start(){
			OutsidePortal ();
		}

		void OnTriggerEnter(Collider col){
			if (isOutside) {
				animBehavior.PlayAnim ();
			}
		}

		void OnTriggerStay(Collider col){
			Vector3 playerPos = Camera.main.transform.position + Camera.main.transform.forward * (Camera.main.nearClipPlane * 4);
			if (transform.InverseTransformPoint (playerPos).z <= 0) {
				if (isOutside) {
					isOutside = false;
					isInside = true;
					InsidePortal ();
					animBehavior.EnableSprite (false);
				}
			} else {
				if (isInside) {
					isInside = false;
					isOutside = true;
					OutsidePortal ();
					animBehavior.EnableSprite (true);
				}
			}
		}

		public void OutsidePortal(){
			StartCoroutine (DelayChangeMat (3));
			animBehavior.EnableSprite (true);
		}

		public void InsidePortal(){
			StartCoroutine (DelayChangeMat (6));
			animBehavior.EnableSprite (false);
		}

		IEnumerator DelayChangeMat(int stencilNum){
			unityARVideo.shouldRender = false;
			yield return new WaitForEndOfFrame ();
			meshRenderer.enabled = false;
			foreach (var mat in materials) {
				mat.SetInt ("_StencilTest", stencilNum);
			}
			yield return new WaitForEndOfFrame ();
			meshRenderer.enabled = true;
			unityARVideo.shouldRender = true;
		}
	}
}


