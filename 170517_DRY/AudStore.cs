using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace AUD {
	public class AudStore: MonoBehaviour {

		Dictionary<string, AudioClip> common = new Dictionary<string, AudioClip> ();
		Dictionary<string, AudioClip> ui = new Dictionary<string, AudioClip> ();
		Dictionary<string, AudioClip> effect = new Dictionary<string, AudioClip> ();
		Dictionary<string, AudioClip> boy = new Dictionary<string, AudioClip> ();
		Dictionary<string, AudioClip> girl = new Dictionary<string, AudioClip> ();

		public Dictionary<AudType, Dictionary<string, AudioClip>> store = new Dictionary<AudType, Dictionary<string, AudioClip>> ();

		void Awake () {
			CacheBaseAudio();
			CachePlayingAudio();
		}
		public void CacheBaseAudio () {
			StartCoroutine(CacheBaseCoroutine());
		}

		public void CachePlayingAudio () {
			StartCoroutine(CachePlayingCoroutine());
		}
		
		IEnumerator CacheBaseCoroutine () {
			ResourceRequest request;
			AudioClip clip;
			foreach (var type in AudCommon.types) {
				request = Resources.LoadAsync(AudCommon.RESOURCES_URI + type);
				yield return request;
				clip = request.asset as AudioClip;
				common.Add(type, clip);
			}

			store.Add(AudType.COMMON, common);

			foreach (var type in AudUI.types) {
				request = Resources.LoadAsync(AudUI.RESOURCES_URI + type);
				yield return request;
				clip = request.asset as AudioClip;
				ui.Add(type, clip);
			}

			store.Add(AudType.UI, ui);
			request = null;
		}

		IEnumerator CachePlayingCoroutine () {
			ResourceRequest request;
			AudioClip clip;
			
			foreach (var type in AudEffect.types) {
				request = Resources.LoadAsync(AudEffect.RESOURCES_URI + type);
				yield return request;
				clip = request.asset as AudioClip;
				effect.Add(type, clip);
			}
			
			store.Add(AudType.EFFECT, effect);

			foreach (var type in AudVoice.types) {
				request = Resources.LoadAsync(AudVoice.RESOURCES_BOY_URI + type);
				yield return request;
				clip = request.asset as AudioClip;
				boy.Add(type, clip);

				request = Resources.LoadAsync(AudVoice.RESOURCES_GIRL_URI + type);
				yield return request;
				clip = request.asset as AudioClip;
				girl.Add(type, clip);
			}
			store.Add(AudType.BOY, boy);
			store.Add(AudType.GIRL, girl);
			request = null;
		}
	}
}




















using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace AUD {
	public class AudStore: MonoBehaviour {

		Dictionary<string, AudioClip> common = new Dictionary<string, AudioClip> ();
		Dictionary<string, AudioClip> ui = new Dictionary<string, AudioClip> ();
		Dictionary<string, AudioClip> effect = new Dictionary<string, AudioClip> ();
		Dictionary<string, AudioClip> boy = new Dictionary<string, AudioClip> ();
		Dictionary<string, AudioClip> girl = new Dictionary<string, AudioClip> ();

		public Dictionary<AudType, Dictionary<string, AudioClip>> store = new Dictionary<AudType, Dictionary<string, AudioClip>> ();

		void Awake () {
			store.Add(AudType.COMMON, common);
			store.Add(AudType.UI, ui);
			store.Add(AudType.EFFECT, effect);
			store.Add(AudType.BOY, boy);
			store.Add(AudType.GIRL, girl);

			CacheBaseAudio();
			CachePlayingAudio();
		}
		public void CacheBaseAudio () {
			StartCoroutine(Cache(common, AudCommon.RESOURCES_URI, AudCommon.types));
			StartCoroutine(Cache(ui, AudUI.RESOURCES_URI, AudUI.types));
		}

		public void CachePlayingAudio () {
			StartCoroutine(Cache(effect, AudEffect.RESOURCES_URI, AudEffect.types));
			StartCoroutine(Cache(boy, AudVoice.RESOURCES_BOY_URI, AudVoice.types));
			StartCoroutine(Cache(girl, AudVoice.RESOURCES_GIRL_URI, AudVoice.types));
		}


		IEnumerator Cache (
			Dictionary<string, AudioClip> store, string resourceUri, string[] types
		) {
			ResourceRequest request;
			AudioClip clip;

			foreach (var type in types) {
				request = Resources.LoadAsync(resourceUri + type);
				yield return request;
				clip = request.asset as AudioClip;
				
				store.Add(type, clip);
			}
		}
	}
}

