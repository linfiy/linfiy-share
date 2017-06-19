using UnityEngine;
using System.Collections;
using UnityEngine.UI;







public class PanelHelpScroll : MonoBehaviour {
	public UscrollPanel scroll;
	Button[] button;
	Image[] buttonImage;
  //	TextSpacing[] texteffectSpacing;  !注释问题
	int buttoncount;  
	string helptext; // 变量名
	Transform buttonParent; // 变量名
	int curButtonIndex;
	void Awake(){
		buttonParent=transform.FindChild("Button"); // 写法要统一 = ()
		scroll = gameObject.GetComponent<UscrollPanel> (); // 写法要统一
		if (scroll==null) {
			scroll = gameObject.AddComponent<UscrollPanel> ();
		}
		curButtonIndex=0;
		buttoncount = 5;
		button = new Button[buttoncount];
		buttonImage=new Image[buttoncount];
	  //	texteffectSpacing=new TextSpacing[buttoncount];
		for (int i=0; i < buttoncount; i++) {
			button[i]=buttonParent.GetChild (i).GetComponent<Button> ();
			buttonImage[i]=buttonParent.GetChild (i).GetComponent<Image> ();
	//		texteffectSpacing[i]=buttonParent.GetChild (i).FindChild("Text").GetComponent<TextSpacing> ();
			//string buttonname = button [i].name;
			int index = i;
			button [i].onClick.AddListener (() => {
				OnClickButtonAtIndex(index);
			});
		}
		initSprite ();
		button [0].GetComponentInChildren<Text>().text= "保定麻将";
		button [1].GetComponentInChildren<Text>().text= "保定打八张";
		button [2].GetComponentInChildren<Text>().text= "邢台大胡";
    button[3].GetComponentInChildren<Text>().text = "邢台平胡";
    button[4].GetComponentInChildren<Text>().text = "邢台tuidao胡";
    OnClickButtonAtIndex(0);
	}
	void initSprite(){
		for (int i = 0; i < buttoncount; i++) {
			buttonImage[i].sprite = UITextureManager.getInstance ().loadAtlasSprite ("Image/LoginSpriteAlt","createBtn1");
		}
	}

	string bdHelpText;
	string bdDazhangHelpText;
	string xtDaHuPlayHelpText;
  string xtPingHuHelpText;
  string xtTuiDaoHuHelpText;
  void OnClickButtonAtIndex(int btn){
		switch (btn) {
		case 0:
			SetCurrent (0);
			if (bdHelpText == null) {
				bdHelpText = Resources.Load<TextAsset>("Text/bdPlayHelp").text;
			}
			helptext = bdHelpText;
			break;
		case 1:
			SetCurrent (1);
			if (bdDazhangHelpText == null) {
				bdDazhangHelpText = Resources.Load<TextAsset>("Text/bdBazhangPlayHelp").text;
			}
			helptext = bdDazhangHelpText;
			break;
		case 2:
			SetCurrent (2);
			if (xtDaHuPlayHelpText == null) {
				xtDaHuPlayHelpText = Resources.Load<TextAsset>("Text/xtDaHuPlayHelp").text;
			}
			helptext = xtDaHuPlayHelpText;
			break;
      case 3:
        SetCurrent(3);
        if (xtPingHuHelpText == null)
        {
          xtPingHuHelpText = Resources.Load<TextAsset>("Text/xtPingHuPlayHelp").text;
        }
        //Debug.Log(xtPingHuHelpText);
        helptext = xtPingHuHelpText;
        break;
      case 4:
        SetCurrent(4);
        if (xtTuiDaoHuHelpText == null)
        {
          xtTuiDaoHuHelpText = Resources.Load<TextAsset>("Text/xtTuiDaoHuPlayHelp").text;
        }
        helptext = xtTuiDaoHuHelpText;
        break;
    }
		scroll.SetViewText (helptext, "帮助");
    //Debug.Log(btn);
  }
	void SetCurrent(int cur){
		buttonImage[curButtonIndex].sprite = UITextureManager.getInstance ().loadAtlasSprite ("Image/LoginSpriteAlt", "createBtn1");
	//	texteffectSpacing [curButtonIndex]._textSpacing = 1;
		curButtonIndex = cur;
		buttonImage[curButtonIndex].sprite = UITextureManager.getInstance ().loadAtlasSprite ("Image/LoginSpriteAlt", "createBtnPressed");
    //	texteffectSpacing [curButtonIndex]._textSpacing = 8;
    //Debug.Log(cur);
	}
}

// https://github.com/ReactiveX/RxJava/blob/2.x/src/perf/java/io/reactivex/EachTypeFlatMapPerf.java
// https://github.com/facebook/react-native/blob/master/ReactCommon/cxxreact/Executor.h





public class PanelHelpScroll : MonoBehaviour {
	public UscrollPanel scroll;
	// Model
	const string TEXT_RESOURCE_URI = "Text/";
	const string PANEL_TITLE = "帮助";
	const string TEXTURE_URI = "Image/LoginSpriteAlt";
	public const string BTN_STATE_NORMAL = "createBtn";
	const string BTN_STATE_PRESSED = "createBtnPressed";
	const BUTTON_COUNT = 6;

	Transform buttonParent;
	int curButtonIndex = 0;

	Image[] buttonImages = new Image[BUTTON_COUNT];
	Button[] buttons =  = new Button[BUTTON_COUNT];

	readonly string[] buttonNames = new string[] {
		"保定麻将", "保定打八张", "邢台大胡", "邢台平胡", "邢台推倒胡",''
	};

	readonly string[] textResourceNames = new string[] {
		"bdPlayHelp", "bdPlayHelp2", "bdPlayHelp3", "bdPlayHelp4", "bdPlayHelp5", ''
	};

	Dictionary<string, string> textStore = new Dictionary<string, string>();

	
	string helpText;

	UITextureManager texture;

	void Awake () { // 场景加载或者重载的时候出发
		texture = UITextureManager.GetInstance();

		buttonParent = transform.FindChild("Button");
		scroll = gameObject.GetComponent<UscrollPanel>();
		if (scroll == null) {
			scroll = gameObject.AddComponent<UscrollPanel>();
		}

		for (int i = 0; i < BUTTON_COUNT; i++) {
			
			button[i] = buttonParent.GetChild(i).GetComponent<Button>();
			buttonImages[i] = buttonParent.GetChild(i).GetComponent<Image>();
			buttonImages[i].sprite = texture.LoadAtlasSprite(TEXTURE_URI, BTN_STATE_NORMAL);
			button[i].GetComponentInChildren<Text>().text = buttonNames[i];
			textStore.Add(buttonNames[i], null);
			int index = i;
			
			button[i].onClick.AddListener(() => {
				ChangeScrollContent(index);
			});
		}

    ChangeScrollContent(curButtonIndex);
	}
	// Controller
  void ChangeScrollContent(int index) {
		var textName = textResourceNames[index];

		buttonImages[curButtonIndex].sprite = texture.LoadAtlasSprite(TEXTURE_URI, BTN_STATE_NORMAL);
		buttonImages[curButtonIndex = index].sprite = texture.LoadAtlasSprite(TEXTURE_URI, BTN_STATE_PRESSED);

		if (textStore[textName] == null)
			textStore[textName] = Resources.Load<TextAsset>(TEXT_RESOURCE_URI + textResourceNames[i]).text;

		// View
		scroll.SetViewText(textStore[textName], PANEL_TITLE);
  }
}

