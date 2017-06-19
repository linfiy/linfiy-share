using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Sprites;
using System;
using System.Net;
using System.IO;
using System.Text;
using LitJson;
using RSG;
using ReqRes;
using UniRx;
using UniRx.Triggers;

public class createRoomPanel : MonoBehaviour {

	// Use this for initialization
	//游戏类型
	public int gameType;

	private Button inning1Btn;
	private bool isDownInning1Btn;

	private Button inning2Btn;
	private bool isDownInning2Btn;

	private Button inning4Btn;
	private bool isDownInning4Btn;

	private Button inning8Btn;
	private bool isDownInning8Btn;

	//局数临时按钮
	private int tempBtn;
	//番数临时按钮
	private int tempFanBtn;
	//人数临时按钮
	private int tempPlayerBtn;

	Sprite radioBtnSpSel;
	Sprite radioBtnSpNor;

	//Sprite checkBtnSpSel;
	Sprite checkBtnSpNor;

	private Font hanyi;

	private SXRule rule;

	//红色
	public Color redColor = new Color(255f/256f, 0/256f, 0/256f, 256f/256f);
	//棕色
	public Color brownColor = new Color(144f/256f, 75f/256f, 44f/256f, 256f/256f);

	private int inning;
	private int manFan;
	private int zimo_rule;
	private int dian_gang_hua;

	GameManager gm;
	//2人
	Transform player2Text;
	private Button player2Btn;
	private bool isDownPlayer2Btn;

	//3人
	Transform player3Text;
	private Button player3Btn;
	private bool isDownPlayer3Btn;

	//4人
	Transform player4Text;
	private Button player4Btn;
	private bool isDownPlayer4Btn;


	Transform inning1Text;
	Transform inning2Text;
	Transform inning4Text;
	Transform inning8Text;

	//2番
	Transform fan2Text;
	private Button fan2Btn;
	private bool isDownFan2Btn;

	//3番
	Transform fan3Text;
	private Button fan3Btn;
	private bool isDownFan3Btn;

	//4番
	Transform fan4Text;
	private Button fan4Btn;
	private bool isDownFan4Btn;



	//自摸加底
	Transform jiadiText ;
	Transform jiadiImage ;
	private Button jiaDiBtn;
	private bool isDownZiMoDiBtn;

	//自摸加番
	Transform jiafanText ;
	Transform jiafanImage ;
	private Button jiaFanBtn;
	private bool isDownZiMoFanBtn;

	//点杠花(点炮)
	Transform dianpaoText;
	Transform dianpaoImage;
	private Button dianGangPaoBtn;
	private bool isDownDianGangPaoBtn;

	//点杠花(自摸)
	Transform zimoText;
	Transform zimoImage;
	private Button dianGangMoBtn;
	private bool isDownDianGangMoBtn;

	//换三张
	Button huansanzhangBtn;
	Transform huanSanZhangText ;
	Transform huanSanZhangImage;
	private bool isDownHuansanzhangBtn;


	//19将对
	Button jiangduiBtn ;
	Transform jiangduiText;
	Transform jiangduiImage ;
	private bool isDownJiangduiBtn;


	//门清中张
	Button menqingBtn;
	Transform menqingText ;
	Transform menqingImage;
	private bool isDownMenqingBtn;


	//天地胡
	Button tiandihuBtn;
	Transform tiandihuText ;
	Transform tiandihuImage ;
	private bool isDownTiandihuBtn;
	private string version_ios;
	private string version_ios_pre;

	void Awake()
	{
		gm = GameManager.instance;
		hanyi = (Font)Resources.Load ("Font/汉仪中圆简" );

		version_ios = PlayerPrefs.GetString ("c_version_ios");
		version_ios_pre = PlayerPrefs.GetString ("c_version_ios_pre");

		radioBtnSpSel = UITextureManager.getInstance().loadAtlasSprite("Image/bgSp","radioBtnSel");
		radioBtnSpNor = UITextureManager.getInstance().loadAtlasSprite("Image/bgSp","radioBtnNor");
		checkBtnSpNor = UITextureManager.getInstance().loadAtlasSprite("Image/bgSp","checkBoxNor");

		InitButtons ();
	}


	//confirnBtnOnClick
	public void confirmBtnOnClick()
	{	
		loading (this.transform);
		Debug.Log ("番数:"+tempFanBtn);

		SCRule rule = new SCRule (tempBtn,tempPlayerBtn, tempFanBtn, zimo_rule, dian_gang_hua, isDownHuansanzhangBtn, isDownJiangduiBtn, isDownMenqingBtn, isDownTiandihuBtn);
		OpenRoomParams createParams = new OpenRoomParams (gm.userData.uid, gm.userData.key, rule.createType, rule.GetData());

		// HTTP: 创建房间
		Request.Get (createParams)
		.Catch(e => Promise.Rejected(new ApplicationException("HTTP^open_room: 创建房间失败")))
		.Then (createRes => {
				Debug.Log(createRes);
			int rid = (int)createRes ["rid"];
			gm.userData.rid = rid;
			gm.userData.rKey = (string)createRes ["rid_key"];
			gm.userData.tcp = (string)createRes ["tcp_s"] [0];
			gm.userData.roomOwner = true;

			JoinRoomParams joinParams = new JoinRoomParams (gm.userData.uid ,gm.userData.key, rid);
			// HTTP: 加入房间
			return Request.Get(joinParams).Then(joinRes => {
					
				gm.gameBeginHandleModule.ConnectCreateGame();
					CancelLoadView();
			})
			.Catch(e => Promise.Rejected(new ApplicationException("HTTP^join_room: 创建房间成功，加入房间失败")));
						

			}).Catch(error => {
				CancelLoadView();
				gm.noticeManager.HandleCatch(error);
			});
	}

	void loading(Transform parent)
	{

		LoadView.Instance(parent,"正在创建房间");
		//使用携程旋转加载的图片
		IDisposable dispose = 
			Observable
				.FromCoroutine(LoadRotate)
				.Repeat()
				.Subscribe();
		LoadView.single.dispose = dispose;
	}

	IEnumerator LoadRotate(){
		if(LoadView.single!=null&&LoadView.single.Exist()){
			LoadView.single.RotateAround ();
		}
		yield return new WaitForSeconds(0);
	}

	void CancelLoadView(){

		if(LoadView.single!=null&&LoadView.single.Exist()){
			LoadView.single.Destroy ();
		}	
	}


	//2人按钮
	public void player2BtnOnClick()
	{
		if ((isDownPlayer3Btn == true || isDownPlayer4Btn == true) &&(tempPlayerBtn == 3 || tempPlayerBtn == 4) )
		{
			Debug.Log ("2人");
			tempPlayerBtn = 2;
			isDownPlayer2Btn = true;
			player2Btn.GetComponent<Image> ().sprite = radioBtnSpSel;
			player3Btn.GetComponent<Image> ().sprite = radioBtnSpNor;
			player4Btn.GetComponent<Image> ().sprite = radioBtnSpNor;
			player2Text.GetComponent<Text> ().color = redColor;
			player3Text.GetComponent<Text> ().color = brownColor;
			player4Text.GetComponent<Text> ().color = brownColor;
		}
	}
	//3人按钮
	public void player3BtnOnClick()
	{
		if ((isDownPlayer2Btn == true || isDownPlayer4Btn == true) &&(tempPlayerBtn == 2 || tempPlayerBtn == 4) )
		{
			Debug.Log ("3Fan");
			tempPlayerBtn = 3;
			isDownPlayer3Btn = true;
			player2Btn.GetComponent<Image> ().sprite = radioBtnSpNor;
			player3Btn.GetComponent<Image> ().sprite = radioBtnSpSel;
			player4Btn.GetComponent<Image> ().sprite = radioBtnSpNor;
			player2Text.GetComponent<Text> ().color = brownColor;
			player3Text.GetComponent<Text> ().color = redColor;
			player4Text.GetComponent<Text> ().color = brownColor;
		}
	}
	//4人按钮
	public void player4BtnOnClick()
	{
		if ((isDownPlayer2Btn == true || isDownPlayer3Btn == true) &&(tempPlayerBtn == 2 || tempPlayerBtn == 3))
		{
			Debug.Log ("4Fan");
			tempPlayerBtn = 4;
			isDownPlayer4Btn = true;
			player2Btn.GetComponent<Image> ().sprite = radioBtnSpNor;
			player3Btn.GetComponent<Image> ().sprite = radioBtnSpNor;
			player4Btn.GetComponent<Image> ().sprite = radioBtnSpSel;
			player2Text.GetComponent<Text> ().color = brownColor;
			player3Text.GetComponent<Text> ().color = brownColor;
			player4Text.GetComponent<Text> ().color = redColor;
		}
	}
	//1局按钮
	public void inning1BtnOnClick()
	{
		Debug.Log ("inning1BtnOnClick");

		if (isDownInning1Btn == false && tempBtn != 1)
		{
			tempBtn = 1;
			isDownInning1Btn = true;
			isDownInning2Btn = false;
			isDownInning4Btn = false;
			isDownInning8Btn = false;
			inning1Btn.GetComponent<Image> ().sprite = radioBtnSpSel;
			inning2Btn.GetComponent<Image> ().sprite = radioBtnSpNor;
			inning4Btn.GetComponent<Image> ().sprite = radioBtnSpNor;
			inning8Btn.GetComponent<Image> ().sprite = radioBtnSpNor;
			inning1Text.GetComponent<Text> ().color = redColor;
			inning2Text.GetComponent<Text> ().color = brownColor;
			inning4Text.GetComponent<Text> ().color = brownColor;
			inning8Text.GetComponent<Text> ().color = brownColor;
			gameType = 1;
		}
	}

	//2局按钮
	public void inning2BtnOnClick()
	{
		Debug.Log ("inning2BtnOnClick");

		if (isDownInning2Btn == false && tempBtn != 2)
		{
			tempBtn = 2;
			isDownInning1Btn = false;
			isDownInning2Btn = true;
			isDownInning4Btn = false;
			isDownInning8Btn = false;
			inning1Btn.GetComponent<Image> ().sprite = radioBtnSpNor;
			inning2Btn.GetComponent<Image> ().sprite = radioBtnSpSel;
			inning4Btn.GetComponent<Image> ().sprite = radioBtnSpNor;
			inning8Btn.GetComponent<Image> ().sprite = radioBtnSpNor;
			inning1Text.GetComponent<Text> ().color = brownColor;
			inning2Text.GetComponent<Text> ().color = redColor;
			inning4Text.GetComponent<Text> ().color = brownColor;
			inning8Text.GetComponent<Text> ().color = brownColor;
		}
	}


	//4局按钮
	public void inning4BtnOnClick()
	{
		Debug.Log ("inning4BtnOnClick");

		if (isDownInning4Btn == false && tempBtn != 4)
		{
			tempBtn = 4;
			isDownInning1Btn = false;
			isDownInning2Btn = false;
			isDownInning4Btn = true;
			isDownInning8Btn = false;
			
			inning1Btn.GetComponent<Image> ().sprite = radioBtnSpNor;
			inning2Btn.GetComponent<Image> ().sprite = radioBtnSpNor;
			inning4Btn.GetComponent<Image> ().sprite = radioBtnSpSel;
			inning8Btn.GetComponent<Image> ().sprite = radioBtnSpNor;
			inning1Text.GetComponent<Text> ().color = brownColor;
			inning2Text.GetComponent<Text> ().color = brownColor;
			inning4Text.GetComponent<Text> ().color = redColor;
			inning8Text.GetComponent<Text> ().color = brownColor;
			gameType = 1;
		}
	}

	//8局按钮
	public void inning8BtnOnClick()
	{
		Debug.Log ("inning8BtnOnClick");

		if (isDownInning8Btn == false && tempBtn != 8)
		{
			tempBtn = 8;
			isDownInning1Btn = false;
			isDownInning2Btn = false;
			isDownInning4Btn = false;
			isDownInning8Btn = true;
			inning1Btn.GetComponent<Image> ().sprite = radioBtnSpNor;
			inning2Btn.GetComponent<Image> ().sprite = radioBtnSpNor;
			inning4Btn.GetComponent<Image> ().sprite = radioBtnSpNor;
			inning8Btn.GetComponent<Image> ().sprite = radioBtnSpSel;
			inning1Text.GetComponent<Text> ().color = brownColor;
			inning2Text.GetComponent<Text> ().color = brownColor;
			inning4Text.GetComponent<Text> ().color = brownColor;
			inning8Text.GetComponent<Text> ().color = redColor;
		}
	}
	//2番按钮
	public void fan2BtnOnClick()
	{
		if ((isDownFan3Btn == true || isDownFan4Btn == true) &&(tempFanBtn == 6 || tempFanBtn == 8) )
		{
			Debug.Log ("2Fan");
			tempFanBtn = 4;
			isDownFan2Btn = true;
			fan2Btn.GetComponent<Image> ().sprite = radioBtnSpSel;
			fan3Btn.GetComponent<Image> ().sprite = radioBtnSpNor;
			fan4Btn.GetComponent<Image> ().sprite = radioBtnSpNor;
			fan2Text.GetComponent<Text> ().color = redColor;
			fan3Text.GetComponent<Text> ().color = brownColor;
			fan4Text.GetComponent<Text> ().color = brownColor;
		}
	}
	//3番按钮
	public void fan3BtnOnClick()
	{
		if ((isDownFan2Btn == true || isDownFan4Btn == true) &&(tempFanBtn == 4 || tempFanBtn == 8) )
		{
			Debug.Log ("3Fan");
			tempFanBtn = 6;
			isDownFan3Btn = true;
			fan2Btn.GetComponent<Image> ().sprite = radioBtnSpNor;
			fan3Btn.GetComponent<Image> ().sprite = radioBtnSpSel;
			fan4Btn.GetComponent<Image> ().sprite = radioBtnSpNor;
			fan2Text.GetComponent<Text> ().color = brownColor;
			fan3Text.GetComponent<Text> ().color = redColor;
			fan4Text.GetComponent<Text> ().color = brownColor;
		}
	}
	//4番按钮
	public void fan4BtnOnClick()
	{
		if ((isDownFan2Btn == true || isDownFan3Btn == true) &&(tempFanBtn == 4 || tempFanBtn == 6))
		{
			Debug.Log ("4Fan");
			tempFanBtn = 8;
			isDownFan4Btn = true;
			fan2Btn.GetComponent<Image> ().sprite = radioBtnSpNor;
			fan3Btn.GetComponent<Image> ().sprite = radioBtnSpNor;
			fan4Btn.GetComponent<Image> ().sprite = radioBtnSpSel;
			fan2Text.GetComponent<Text> ().color = brownColor;
			fan3Text.GetComponent<Text> ().color = brownColor;
			fan4Text.GetComponent<Text> ().color = redColor;
		}
	}


	//自摸加底
	public void ziMoDiBtnOnClick()
	{
		if (isDownZiMoFanBtn == true && zimo_rule == 1)
		{
			zimo_rule = 0;
			isDownZiMoDiBtn = true;
			isDownZiMoFanBtn = false;
			jiadiText.GetComponent<Text> ().color = redColor;
			jiafanText.GetComponent<Text> ().color = brownColor;
			jiaDiBtn.GetComponent<Image> ().sprite = radioBtnSpSel;
			jiaFanBtn.GetComponent<Image> ().sprite = radioBtnSpNor;
		} 
	}

	//自摸加番
	public void ziMoFanBtnOnClick()
	{
		if (isDownZiMoDiBtn == true && zimo_rule == 0)
		{
			zimo_rule = 1;
			isDownZiMoDiBtn = false;
			isDownZiMoFanBtn = true;
			jiadiText.GetComponent<Text> ().color = brownColor;
			jiafanText.GetComponent<Text> ().color = redColor ;
			jiaDiBtn.GetComponent<Image> ().sprite = radioBtnSpNor;
			jiaFanBtn.GetComponent<Image> ().sprite = radioBtnSpSel;
		}
	}

	//点杠花(点炮)
	public void dianGangPaoBtnOnClick()
	{
		if (isDownDianGangMoBtn == true && dian_gang_hua == 1)
		{
			dian_gang_hua = 0;
			isDownDianGangPaoBtn = true;
			dianpaoText.GetComponent<Text> ().color = redColor;
			zimoText.GetComponent<Text> ().color = brownColor;
			dianGangPaoBtn.GetComponent<Image> ().sprite = radioBtnSpSel;
			dianGangMoBtn.GetComponent<Image> ().sprite = radioBtnSpNor;
		}
	}
	//点杠花(自摸)
	public void dianGangMoBtnOnClick()
	{
		if (isDownDianGangPaoBtn == true && dian_gang_hua == 0)
		{
			dian_gang_hua = 1;
			isDownDianGangMoBtn = true;
			dianpaoText.GetComponent<Text> ().color = brownColor;
			zimoText.GetComponent<Text> ().color = redColor ;
			dianGangPaoBtn.GetComponent<Image> ().sprite = radioBtnSpNor;
			dianGangMoBtn.GetComponent<Image> ().sprite = radioBtnSpSel;
		}
	}

	//换三张
	public void huansanzhangBtnOnClick()
	{
		if (isDownHuansanzhangBtn == true) 
		{
			isDownHuansanzhangBtn = false;
			huanSanZhangImage.gameObject.SetActive (false);
		} 
		else 
		{
			isDownHuansanzhangBtn = true;
			huanSanZhangImage.gameObject.SetActive (true);
		}
		huanSanZhangText.GetComponent<Text> ().color = isDownHuansanzhangBtn ? redColor : brownColor;
	}

	//19将对
	public void jiangDuiBtnOnClick()
	{
		if (isDownJiangduiBtn == true) 
		{
			isDownJiangduiBtn = false;
			jiangduiImage.gameObject.SetActive (false);
		} 
		else 
		{
			isDownJiangduiBtn = true;
			jiangduiImage.gameObject.SetActive (true);
		}
		jiangduiText.GetComponent<Text> ().color = isDownJiangduiBtn ? redColor : brownColor;

	}

	//门清中张
	public void menQingOnClick()
	{
		if (isDownMenqingBtn == true) 
		{
			isDownMenqingBtn = false;
			menqingImage.gameObject.SetActive (false);
		} 
		else 
		{
			isDownMenqingBtn = true;
			menqingImage.gameObject.SetActive (true);
		}
		menqingText.GetComponent<Text> ().color = isDownMenqingBtn ? redColor : brownColor;
	}

	//天地胡
	public void tianDiHuBtnOnClick()
	{
		if (isDownTiandihuBtn == true) 
		{
			isDownTiandihuBtn = false;
			tiandihuImage.gameObject.SetActive (false);
		} 
		else 
		{
			isDownTiandihuBtn = true;
			tiandihuImage.gameObject.SetActive (true);
		}
		tiandihuText.GetComponent<Text> ().color = isDownTiandihuBtn ? redColor : brownColor;
	}

	//初始化Buttons
	void InitButtons()
	{

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


		Sprite trueImg = UITextureManager.getInstance().loadAtlasSprite("Image/bgSp","checkBoxSel");

		//人数
		transform.Find ("playerNum").GetComponent<Text>().font = hanyi;
		transform.Find ("playerNum").GetComponent<Text>().text  = "人数";

		//2人
		player2Btn = transform.Find ("playerNum/2Fan").GetComponent<Button> ();
		player2Btn.GetComponent<Image> ().sprite = radioBtnSpNor;
		player2Text = transform.Find ("playerNum/2Fan/Text");
		player2Text.GetComponent<Text> ().color = brownColor;
		player2Text.GetComponent<Text> ().font = hanyi;
		player2Text.GetComponent<Text> ().text = "2人";
		player2Text.GetComponent<RectTransform> ().sizeDelta = new Vector2 (100, 60);
		player2Text.GetComponent<RectTransform> ().localPosition = new Vector3 (85, 0, 0);
		tempPlayerBtn = 4;
		isDownPlayer2Btn = false;
		player2Btn.onClick.AddListener (delegate() {
			player2BtnOnClick();
		});

		//3人
		player3Btn = transform.Find ("playerNum/3Fan").GetComponent<Button> ();
		player3Text = transform.Find ("playerNum/3Fan/Text");
		player3Text.GetComponent<Text> ().font = hanyi;
		player3Text.GetComponent<Text> ().text = "3人";
		player3Text.GetComponent<Text> ().color = brownColor;
		player3Text.GetComponent<RectTransform> ().sizeDelta = new Vector2 (100, 60);
		player3Text.GetComponent<RectTransform> ().localPosition = new Vector3 (85, 0,0);
		player3Btn.GetComponent<Image> ().sprite = radioBtnSpNor;
		isDownPlayer3Btn = false;
		player3Btn.onClick.AddListener (delegate() {
			player3BtnOnClick();
		});

		//4人
		player4Btn = transform.Find ("playerNum/4Fan").GetComponent<Button> ();
		player4Text = transform.Find ("playerNum/4Fan/Text");
		player4Text.GetComponent<Text> ().font = hanyi;
		player4Text.GetComponent<Text> ().text = "4人";
		player4Text.GetComponent<Text> ().color = redColor;
		player4Text.GetComponent<RectTransform> ().sizeDelta = new Vector2 (100, 60);
		player4Text.GetComponent<RectTransform> ().localPosition = new Vector3 (85, 0,0);
		player4Btn.GetComponent<Image> ().sprite = radioBtnSpSel;

		isDownPlayer4Btn = true;
		player4Btn.onClick.AddListener (delegate() {
			player4BtnOnClick();
		});


		//局数
		Transform inningNum = transform.Find ("InningNum");
		inningNum.GetComponent<Text> ().font = hanyi;
		inningNum.GetComponent<Text> ().text = "局数";

		inning1Btn = transform.Find ("InningNum/1InningBtn").GetComponent<Button> ();
		inning1Btn.GetComponent<Image> ().sprite = radioBtnSpNor;
		inning1Text = transform.Find ("InningNum/1InningBtn/Text");
		inning1Text.GetComponent<Text> ().alignment = TextAnchor.MiddleLeft;
		inning1Text.GetComponent<Text> ().color = brownColor;
		inning1Text.GetComponent<Text> ().font = hanyi;
		inning1Text.GetComponent<Text> ().text = "1局1钻";
		inning1Text.GetComponent<RectTransform> ().localPosition = new Vector3 (140, 0, 0);
		isDownInning1Btn = false;
		inning1Btn.onClick.AddListener (delegate() {
			inning1BtnOnClick();
		});

		inning2Btn = transform.Find ("InningNum/2InningBtn").GetComponent<Button> ();
		inning2Btn.GetComponent<Image> ().sprite = radioBtnSpNor;
		inning2Text = transform.Find ("InningNum/2InningBtn/Text");
		inning2Text.GetComponent<Text> ().alignment = TextAnchor.MiddleLeft;
		inning2Text.GetComponent<Text> ().color = brownColor;
		inning2Text.GetComponent<Text> ().font = hanyi;
		inning2Text.GetComponent<Text> ().text = "2局2钻";
		inning2Text.GetComponent<RectTransform> ().localPosition = new Vector3 (140, 0, 0);
		isDownInning2Btn = false;
		inning2Btn.onClick.AddListener (delegate() {
			inning2BtnOnClick();
		});

		inning4Btn = transform.Find ("InningNum/4InningBtn").GetComponent<Button> ();
		inning4Btn.GetComponent<Image> ().sprite = radioBtnSpSel;
		inning4Text = transform.Find ("InningNum/4InningBtn/Text");
		inning4Text.GetComponent<Text> ().alignment = TextAnchor.MiddleLeft;
		inning4Text.GetComponent<Text> ().color = redColor;
		inning4Text.GetComponent<Text> ().font = hanyi;
		inning4Text.GetComponent<Text> ().text = "4局3钻";
		inning4Text.GetComponent<RectTransform> ().localPosition = new Vector3 (140, 0, 0);
		isDownInning4Btn = true;
		tempBtn = 4;
		inning4Btn.onClick.AddListener (delegate() {
			inning4BtnOnClick();
		});

		inning8Btn = transform.Find ("InningNum/8InningBtn").GetComponent<Button> ();
		inning8Btn.GetComponent<RectTransform> ().localPosition = new Vector3 (960.0f,0,0);
		inning8Btn.GetComponent<Image> ().sprite = radioBtnSpNor;
		inning8Text = transform.Find ("InningNum/8InningBtn/Text");
		inning8Text.GetComponent<Text> ().alignment = TextAnchor.MiddleLeft;
		inning8Text.GetComponent<Text> ().font = hanyi;
		inning8Text.GetComponent<Text> ().color = brownColor;
		inning8Text.GetComponent<Text> ().text = "8局4钻";
		inning8Text.GetComponent<RectTransform> ().localPosition = new Vector3 (140, 0, 0);
		isDownInning8Btn = false;
		inning8Btn.onClick.AddListener (delegate() {
			inning8BtnOnClick();
		});




		//封顶
		transform.Find ("MaxFan").GetComponent<Text>().font = hanyi;
		transform.Find ("MaxFan").GetComponent<Text>().text  = "封顶";

		//4番
		fan2Btn = transform.Find ("MaxFan/2Fan").GetComponent<Button> ();
		fan2Btn.GetComponent<Image> ().sprite = radioBtnSpSel;
		fan2Text = transform.Find ("MaxFan/2Fan/Text");
		fan2Text.GetComponent<Text> ().color = redColor;
		fan2Text.GetComponent<Text> ().font = hanyi;
		fan2Text.GetComponent<Text> ().text = "4番";
		fan2Text.GetComponent<RectTransform> ().sizeDelta = new Vector2 (100, 60);
		fan2Text.GetComponent<RectTransform> ().localPosition = new Vector3 (85, 0, 0);
		tempFanBtn = 4;
		isDownFan2Btn = true;
		fan2Btn.onClick.AddListener (delegate() {
			fan2BtnOnClick();
		});

		//6番
		fan3Btn = transform.Find ("MaxFan/3Fan").GetComponent<Button> ();
		fan3Text = transform.Find ("MaxFan/3Fan/Text");
		fan3Text.GetComponent<Text> ().font = hanyi;
		fan3Text.GetComponent<Text> ().text = "6番";
		fan3Text.GetComponent<Text> ().color = brownColor;
		fan3Text.GetComponent<RectTransform> ().sizeDelta = new Vector2 (100, 60);
		fan3Text.GetComponent<RectTransform> ().localPosition = new Vector3 (85, 0,0);
		fan3Btn.GetComponent<Image> ().sprite = radioBtnSpNor;
		isDownFan3Btn = false;
		fan3Btn.onClick.AddListener (delegate() {
			fan3BtnOnClick();
		});

		//8番
		fan4Btn = transform.Find ("MaxFan/4Fan").GetComponent<Button> ();
		fan4Text = transform.Find ("MaxFan/4Fan/Text");
		fan4Text.GetComponent<Text> ().font = hanyi;
		fan4Text.GetComponent<Text> ().text = "8番";
		fan4Text.GetComponent<Text> ().color = brownColor;
		fan4Text.GetComponent<RectTransform> ().sizeDelta = new Vector2 (100, 60);
		fan4Text.GetComponent<RectTransform> ().localPosition = new Vector3 (85, 0,0);
		fan4Btn.GetComponent<Image> ().sprite = radioBtnSpNor;

		isDownFan4Btn = false;
		fan4Btn.onClick.AddListener (delegate() {
			fan4BtnOnClick();
		});

		//自摸加底
		Transform playMethodText = transform.Find ("PlayMethod");
		playMethodText.GetComponent<Text>().font = hanyi;
		playMethodText.GetComponent<Text>().text = "玩法";

		jiaDiBtn = transform.Find ("PlayMethod/jiadi").GetComponent<Button> ();
		jiadiText = transform.Find ("PlayMethod/jiadi/Text");
		jiadiImage = transform.Find ("PlayMethod/jiadi/Image");
		jiaDiBtn.GetComponent<Image> ().sprite = radioBtnSpNor;
		jiadiText.GetComponent<Text> ().color = brownColor;
		jiadiText.GetComponent<Text> ().font = hanyi;
		jiadiText.GetComponent<Text> ().text = "自摸加底";
		jiadiText.GetComponent<RectTransform>().localPosition = new Vector3 (140, 0,0);
		jiadiImage.GetComponent<Image> ().sprite = trueImg;
		jiadiImage.gameObject.SetActive (false);
		isDownZiMoDiBtn = false;

		jiaDiBtn.onClick.AddListener (delegate() {
			ziMoDiBtnOnClick();
		});

		//自摸加番
		jiaFanBtn = transform.Find ("PlayMethod/jiafan").GetComponent<Button> ();
		jiafanText = transform.Find ("PlayMethod/jiafan/Text");
		jiafanImage = transform.Find ("PlayMethod/jiafan/Image");
		jiaFanBtn.GetComponent<Image> ().sprite = radioBtnSpSel;
		jiafanText.GetComponent<Text> ().color = redColor;
		jiafanText.GetComponent<Text> ().font = hanyi;
		jiafanText.GetComponent<Text> ().text = "自摸加番";
		jiafanText.GetComponent<RectTransform> ().localPosition = new Vector3 (140, 0,0);
		jiafanImage.GetComponent<Image>().sprite = trueImg;
		jiafanImage.gameObject.SetActive (false);
		isDownZiMoFanBtn = true;
		zimo_rule = 1;
		jiaFanBtn.onClick.AddListener (delegate() {
			ziMoFanBtnOnClick();
		});

		//点杠花(点炮)
		dianGangPaoBtn = transform.Find ("DianGang/dianpao").GetComponent<Button> ();
		dianpaoText = transform.Find ("DianGang/dianpao/Text");
		dianpaoImage = transform.Find ("DianGang/dianpao/Image");
		dianGangPaoBtn.GetComponent<Image> ().sprite = radioBtnSpNor;
		dianpaoText.GetComponent<Text> ().color = brownColor;
		dianpaoText.GetComponent<Text> ().font = hanyi;
		dianpaoText.GetComponent<Text> ().text = "点杠花(点炮)";
		dianpaoText.GetComponent<RectTransform>().localPosition = new Vector3 (185, 0,0);
		dianpaoImage.GetComponent<Image> ().sprite = trueImg;
		dianpaoImage.gameObject.SetActive (false);
		isDownDianGangPaoBtn = false;
		dianGangPaoBtn.onClick.AddListener (delegate() {
			dianGangPaoBtnOnClick();
		});

		//点杠花(自摸)
		dianGangMoBtn = transform.Find ("DianGang/zimo").GetComponent<Button> ();
		zimoText = transform.Find ("DianGang/zimo/Text");
		zimoImage = transform.Find ("DianGang/zimo/Image");
		dianGangMoBtn.GetComponent<Image> ().sprite = radioBtnSpSel;
		zimoText.GetComponent<Text> ().color = redColor;
		zimoText.GetComponent<Text> ().font = hanyi;
		zimoText.GetComponent<Text> ().text = "点杠花(自摸)";
		zimoText.GetComponent<RectTransform>().localPosition = new Vector3 (185, 0,0);
		zimoImage.GetComponent<Image>().sprite = trueImg;
		zimoImage.gameObject.SetActive (false);
		isDownDianGangMoBtn = true;
		dian_gang_hua = 1;
		dianGangMoBtn.onClick.AddListener (delegate() {
			dianGangMoBtnOnClick();
		});

		//换三张
		huansanzhangBtn = transform.Find ("HuanSanZhang/huansanzhang").GetComponent<Button> ();
		huanSanZhangText = transform.Find ("HuanSanZhang/huansanzhang/Text");
		huanSanZhangImage = transform.Find ("HuanSanZhang/huansanzhang/Image");
		huansanzhangBtn.GetComponent<Image> ().sprite = checkBtnSpNor;
		huanSanZhangText.GetComponent<Text>().color = redColor;
		huanSanZhangText.GetComponent<Text>().font = hanyi;
		huanSanZhangText.GetComponent<Text>().text = "换三张";
		huanSanZhangText.GetComponent<RectTransform>().localPosition = new Vector3 (120,0,0);
		huanSanZhangText.GetComponent<RectTransform> ().sizeDelta = new Vector2 (180, 60);
		huanSanZhangImage.GetComponent<Image>().sprite = trueImg;
		huanSanZhangImage.GetComponent<RectTransform>().localPosition = new Vector3 (10,10,0);
		huanSanZhangImage.gameObject.SetActive (true);
		isDownHuansanzhangBtn = true;
		huansanzhangBtn.onClick.AddListener (delegate() {
			huansanzhangBtnOnClick();
		});

		//19将对
		jiangduiBtn = transform.Find ("HuanSanZhang/19jiangdui").GetComponent<Button> ();
		jiangduiText = transform.Find ("HuanSanZhang/19jiangdui/Text");
		jiangduiImage = transform.Find ("HuanSanZhang/19jiangdui/Image");
		jiangduiBtn.GetComponent<Image> ().sprite = checkBtnSpNor;
		jiangduiText.GetComponent<Text> ().color = redColor;
		jiangduiText.GetComponent<Text> ().font = hanyi;
		jiangduiText.GetComponent<Text> ().text = "幺九将对";
		jiangduiText.GetComponent<RectTransform> ().sizeDelta = new Vector2 (180, 60);
		jiangduiText.GetComponent<RectTransform>().localPosition = new Vector3 (140, 0,0);
		jiangduiImage.GetComponent<Image>().sprite = trueImg;
		jiangduiImage.GetComponent<RectTransform>().localPosition = new Vector3 (10,10,0);
		jiangduiImage.gameObject.SetActive (true);
		isDownJiangduiBtn = true;
		jiangduiBtn.onClick.AddListener (delegate() {
			jiangDuiBtnOnClick();
		});

		//门清中张
		menqingBtn = transform.Find ("MenQing/menqingzhongzhang").GetComponent<Button> ();
		menqingText = transform.Find ("MenQing/menqingzhongzhang/Text");
		menqingImage = transform.Find ("MenQing/menqingzhongzhang/Image");
		menqingBtn.GetComponent<Image> ().sprite = checkBtnSpNor;
		menqingText.GetComponent<Text> ().color = redColor;
		menqingText.GetComponent<Text> ().font = hanyi;
		menqingText.GetComponent<Text> ().text = "门清中张";
		menqingText.GetComponent<RectTransform> ().sizeDelta = new Vector2 (180,60);
		menqingText.GetComponent<RectTransform>().localPosition = new Vector3 (140, 0,0);
		menqingImage.GetComponent<Image>().sprite = trueImg;
		menqingImage.GetComponent<RectTransform>().localPosition = new Vector3(10,10,0);
		menqingImage.gameObject.SetActive (true);
		isDownMenqingBtn = true;
		menqingBtn.onClick.AddListener (delegate() {
			menQingOnClick();
		});

		//天地胡
		tiandihuBtn = transform.Find ("MenQing/tiandihu").GetComponent<Button>();
		tiandihuText = transform.Find ("MenQing/tiandihu/Text");
		tiandihuImage = transform.Find ("MenQing/tiandihu/Image");
		tiandihuBtn.GetComponent<Image> ().sprite = checkBtnSpNor;
		tiandihuText.GetComponent<Text> ().color = redColor;
		tiandihuText.GetComponent<Text>().font = hanyi;
		tiandihuText.GetComponent<Text>().text = "天地胡";
		tiandihuText.GetComponent<RectTransform> ().sizeDelta = new Vector2 (180, 60);
		tiandihuText.GetComponent<RectTransform> ().localPosition = new Vector3 (120, 0,0);
		tiandihuImage.GetComponent<Image>().sprite = trueImg;
		tiandihuImage.GetComponent<RectTransform>().localPosition = new Vector3(10,10,0);
		tiandihuImage.gameObject.SetActive (true);
		isDownTiandihuBtn = true;
		tiandihuBtn.onClick.AddListener (delegate() {
			tianDiHuBtnOnClick();
		});

//		transform.Find ("Image").GetComponent<Image> ().sprite = UITextureManager.getInstance().loadAtlasSprite("Image/BgmiSp","createRoomBackground_interface");
		transform.Find ("Image").GetComponent<Image> ().sprite = Resources.Load<Sprite>("Image/createRoomBackground");
		transform.Find ("TipsText").GetComponent<Text>().font = hanyi;

		Sprite lineSp = UITextureManager.getInstance().loadAtlasSprite("Image/bgSp","line_createroom");
		transform.Find ("lineImg").GetComponent<Image> ().sprite = lineSp;
		transform.Find ("lineImg/lineImg (1)").GetComponent<Image> ().sprite = lineSp;
		transform.Find ("lineImg/lineImg (2)").GetComponent<Image> ().sprite = lineSp;
		transform.Find ("lineImg/lineImg (3)").GetComponent<Image> ().sprite = lineSp;
		transform.Find ("lineImg/lineImg (4)").GetComponent<Image> ().sprite = lineSp;
		transform.Find ("lineImg/lineImg (5)").GetComponent<Image> ().sprite = lineSp;

		Button confirmBtn = transform.Find ("confirmBtn").GetComponent<Button> ();
		confirmBtn.GetComponent<Image> ().sprite = UITextureManager.getInstance ().loadAtlasSprite ("Image/BtnSp","greenConfirmNor");
		confirmBtn.transition = Selectable.Transition.SpriteSwap;
		SpriteState confirm = new SpriteState ();
		confirm.pressedSprite =  UITextureManager.getInstance().loadAtlasSprite("Image/BtnSp","greenConfirmSel");
		confirmBtn.spriteState = confirm;
		confirmBtn.onClick.RemoveAllListeners ();
		confirmBtn.onClick.AddListener (delegate() {

			Debug.Log("confirmBtnClick");
			confirmBtnOnClick();
		});


		#if UNITY_ANDROID

		#elif UNITY_IPHONE

		if (!version_ios.Equals(Sys.VERSION) && version_ios_pre.Equals(Sys.VERSION)) 
		{
			inning1Text.GetComponent<Text> ().text = "1局1钻";
			inning2Text.GetComponent<Text> ().text = "2局2钻";
			inning4Text.GetComponent<Text> ().text = "4局3钻";
			inning8Text.GetComponent<Text> ().text = "8局4钻";
			transform.Find ("TipsText").gameObject.SetActive(false);
		}

		#endif
	}

	void destoryPanel()
	{
		for(int i = 0;i<transform.childCount;i++)
		{
			Destroy (this.gameObject);
		}
	}

	public void ToggleActive () 
	{
		gameObject.SetActive (!gameObject.activeSelf);
	}

}
