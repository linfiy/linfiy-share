using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class IAPanel : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	// 存放子对象的字典
	Dictionary<string,Transform>  allChild ;

	void InitialChild()
	{
		allChild = new Dictionary<string, Transform> ();
		for (int i = 0; i < transform.childCount; i++) 
		{
			allChild.Add (transform.GetChild (i).name, transform.GetChild (i));
		}
	}

	void Awake()
	{
		InitialChild ();
		Button exitBtn = transform.Find("Image/ExitBtn").GetComponent<Button>();
		exitBtn.GetComponent<Image> ().sprite = UITextureManager.getInstance ().loadAtlasSprite ("Image/BtnSp","exitBtnNor_interface");
		exitBtn.transition = Selectable.Transition.SpriteSwap;
		SpriteState exit = new SpriteState ();
		exit.pressedSprite =  UITextureManager.getInstance().loadAtlasSprite("Image/BtnSp","exitBtnSel_interface");
		exitBtn.spriteState = exit;
		exitBtn.onClick.RemoveAllListeners ();
		exitBtn.onClick.AddListener (delegate() {
			destoryPanel();
		});

		Button btn30 = allChild["Btn30"].GetComponent<Button>();
		btn30.GetComponent<Image>().sprite = UITextureManager.getInstance().loadAtlasSprite("Image/IAP","cny30");
		btn30.onClick.RemoveAllListeners ();
		btn30.onClick.AddListener (delegate() {
			IAPManager.instance.Btn30OnClick();
		});

		Button btn118 = allChild["Btn118"].GetComponent<Button>();
		btn118.GetComponent<Image>().sprite = UITextureManager.getInstance().loadAtlasSprite("Image/IAP","cny118");
		btn118.onClick.RemoveAllListeners ();
		btn118.onClick.AddListener (IAPManager.instance.Btn118OnClick);

		Button btn288 = allChild["Btn288"].GetComponent<Button>();
		btn288.GetComponent<Image>().sprite = UITextureManager.getInstance().loadAtlasSprite("Image/IAP","cny288");
		btn288.onClick.RemoveAllListeners ();
		btn288.onClick.AddListener (delegate() {
			IAPManager.instance.Btn288OnClick();
		});

		Button btn588 = allChild["Btn588"].GetComponent<Button>();
		btn588.GetComponent<Image>().sprite = UITextureManager.getInstance().loadAtlasSprite("Image/IAP","cny588");
		btn588.onClick.RemoveAllListeners ();
		btn588.onClick.AddListener (delegate() {
			IAPManager.instance.Btn588OnClick();
		});

		allChild ["Image"].GetComponent<Image> ().sprite = UITextureManager.getInstance ().loadAtlasSprite ("Image/IAP", "IAPBackground");

		Sprite backImage = UITextureManager.getInstance ().loadAtlasSprite ("Image/IAP", "iapBack");

		Transform backImg = allChild ["backImg"];
		backImg.GetComponent<Image> ().sprite = backImage;
		backImg.FindChild ("diamondImg").GetComponent<Image> ().sprite = UITextureManager.getInstance ().loadAtlasSprite ("Image/IAP", "diamond320");
		backImg.FindChild ("priceImg").GetComponent<Image> ().sprite = UITextureManager.getInstance ().loadAtlasSprite ("Image/IAP", "num320");

		Transform backImg1 = allChild ["backImg (1)"];
		backImg1.GetComponent<Image> ().sprite = backImage;
		backImg1.FindChild ("diamondImg").GetComponent<Image> ().sprite = UITextureManager.getInstance ().loadAtlasSprite ("Image/IAP", "diamond150");
		backImg1.FindChild ("priceImg").GetComponent<Image> ().sprite = UITextureManager.getInstance ().loadAtlasSprite ("Image/IAP", "num150");

		Transform backImg2 = allChild ["backImg (2)"];
		backImg2.GetComponent<Image> ().sprite = backImage;
		backImg2.FindChild ("diamondImg").GetComponent<Image> ().sprite = UITextureManager.getInstance ().loadAtlasSprite ("Image/IAP", "diamond60");
		backImg2.FindChild ("priceImg").GetComponent<Image> ().sprite = UITextureManager.getInstance ().loadAtlasSprite ("Image/IAP", "num60");

		Transform backImg3 = allChild ["backImg (3)"];
		backImg3.GetComponent<Image> ().sprite = backImage;
		backImg3.FindChild ("diamondImg").GetComponent<Image> ().sprite = UITextureManager.getInstance ().loadAtlasSprite ("Image/IAP", "diamond15");
		backImg3.FindChild ("priceImg").GetComponent<Image> ().sprite = UITextureManager.getInstance ().loadAtlasSprite ("Image/IAP", "num15");

	}

	void destoryPanel()
	{
		Destroy (this.gameObject);
	}
}
