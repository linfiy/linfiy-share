using LitJson;
using System.Linq;
public class BDRule : IRule {
	// 静态属性
	const int MIN_NUM = 108;
	const int MAX_NUM = 136;
	public static RuleBase ruleBase;
	
	public static BDRule FormatRule (JsonData json) {

		int gameNumber = (int)json["set_num"];
		int playerCount = (int)json["player_count"];

		bool hasWind = (int)json["is_feng"] == 0 ? false : true;
		bool hasPaozi = (int)json["is_paozi"] == 0 ? false : true;
		bool hasMutiHu = (int)json["is_yipao_duoxiang"] == 0 ? false : true;
		bool hasHun = (int)json["is_fanhun"] == 0 ? false : true;
		bool hasChi = (int)json["is_chipai"] == 0 ? false : true;
		bool hasFollowDealer = (int)json["is_genzhuang"] == 0 ? false : true;
		bool hasDa8Tiles = (int)json["is_da8zhang"] == 0 ? false : true;

		bool qingyise = (int)json["is_qingyise_fan"] == 0 ? false : true;
		bool yitiaolong = (int)json["is_yitiaolong_fan"] == 0 ? false : true;
		bool shisanyao = (int)json["is_shisanyao_fan"] == 0 ? false : true;
		bool ganghua = (int)json["is_ganghua_fan"] == 0 ? false : true;
		bool qidui = (int)json["is_qidui_fan"] == 0 ? false : true;
		bool tiandihu = (int)json["is_tiandi_hu_fan"] == 0 ? false : true;

		// bool hasWind = (int)json["is_feng"] != 0;
		// bool hasPaozi = (int)json["is_paozi"] != 0;
		// bool hasMutiHu = (int)json["is_yipao_duoxiang"] != 0;
		// bool hasHun = (int)json["is_fanhun"] != 0;
		// bool hasChi = (int)json["is_chipai"] != 0;
		// bool hasFollowDealer = (int)json["is_genzhuang"] != 0;
		// bool hasDa8Tiles = (int)json["is_da8zhang"] != 0;

		// bool qingyise = (int)json["is_qingyise_fan"] != 0;
		// bool yitiaolong = (int)json["is_yitiaolong_fan"] != 0;
		// bool shisanyao = (int)json["is_shisanyao_fan"] != 0;
		// bool ganghua = (int)json["is_ganghua_fan"] != 0;
		// bool qidui = (int)json["is_qidui_fan"] != 0;
		// bool tiandihu = (int)json["is_tiandi_hu_fan"] != 0;
		
		return new BDRule(
			gameNumber, playerCount, 
			hasDa8Tiles,

			hasPaozi, hasMutiHu, hasHun, hasChi, hasFollowDealer,
			qingyise, yitiaolong, tiandihu, ganghua, qidui, shisanyao
		);
	}

	/*
	 * 实例属性
	 */
	/****** IRule 公共规则 *****/
	// 规则名称
	public RuleName name {
		get { return RuleName.BAODING; }
	}

	public string nameForDisplay {
		get { return "保定麻将"; }
	}
	// 游戏局数
	int _gameNum; // 4局 8局
	public int gameNumber { 
		get { return _gameNum; }
		set {
		// 	switch (value) {
		// 		case 4: _createType = 1; break;
		// 		case 8: _createType = 2; break;
		// 		case 16: _createType = 3; break;
		// 		default:
		// //			Console.Warn (LogType.GAME, "游戏局数无效，必须为 8 或 16 局");
		// 			break;
		// 	}
			_createType = _gameNum = value;
		}
	}

	int _currency;
	public int currency {
		get { return _currency; }
	}

	// 开房人数
	int _playerCount; 
	public int playerCount {
		get { return _playerCount; }
	}
	// 总牌数
	int _total;
	public int total {
		get { return _total; }
	}
	// 玩家在东西方向，牌库切分方式
	int[] _spliceDotsEW;
	public int[] spliceDotsEW {
		get { return _spliceDotsEW; }
	}
	// 玩家在南北方向，牌库切分方式
	int[] _spliceDotsSN;
	public int[] spliceDotsSN {
		get { return _spliceDotsSN; }
	}
	 // 1 = [2, 4], 2 = [3,8], 3 = [4, 16] [扣几钻, 几局游戏] 
	int _createType;
	public int createType {
		get { return _createType; }
	}

	public string display {
		get {
			return displayMutiLine.Replace("\n", "");
		}
	}

	public string displayMutiLine {
		get {
			string str = "";
			
			// if (hasWind) str += "带风牌 ";
			if (hasDa8Tiles) str += "打八张 ";
			else str += "推倒胡 ";
			if (hasPaozi) str += "下跑儿 ";
			if (hasMutiHu) str += "一炮多响 ";
			if (hasHun) str += "翻混儿 ";
			if (hasChi) str += "可吃牌 ";
			if (hasFollowDealer) str += "跟庄 ";

			str += "\n";

			if (qingyise) str += "清一色 ";
			if (yitiaolong) str += "一条龙 ";
			if (tiandihu) str += "天地胡 ";
			if (gangHua) str += "杠上开花 ";
			if (qidui) str += "七对子 ";
			if (shisanyao) str += "十三幺 ";
			return str;
		}
	}
	
	/***** 私有规则 *****/
	bool _hasWind;
	public bool hasWind { 
		get {
			return _hasWind;
		}
		set {
			_hasWind = value;
			if (value) {
				_total = MAX_NUM;
			}
				
			else {
				_total = MIN_NUM;
			}
				
		}
	} // 带风牌
	public bool hasPaozi;  // 下跑子
	public bool hasMutiHu; // 一炮多响
	public bool hasHun; // 翻混
	public bool hasFollowDealer; // 跟庄
	public bool hasDa8Tiles; // 打八张
	public bool hasChi; // 带吃牌

	public bool qingyise; // 清一色
	public bool yitiaolong; // 一条龙
	public bool shisanyao; // 十三幺
	public bool gangHua; // 杠上开花
	public bool qidui; // 七对
	public bool tiandihu; // 天地胡

	public BDRule () {}
	public BDRule (
		int gameNumber,
		int playerCount,

		bool hasDa8Tiles,

		bool hasPaozi,
		// bool hasWind,
		bool hasMutiHu,
		bool hasHun, 
		bool hasChi,
		bool hasFollowDealer,

		bool qingyise, // 清一色
		bool yitiaolong, // 一条龙
		bool tiandihu,
		bool gangHua,
		bool qidui,
		bool shisanyao // 十三幺
	) {
		this.gameNumber = gameNumber;
		_playerCount = playerCount;
		
		this.hasDa8Tiles = hasDa8Tiles;
		this.hasWind = true;

		this.hasPaozi = hasPaozi;
		this.hasMutiHu = hasMutiHu;
		this.hasHun = hasHun;
		this.hasChi = hasChi;
		this.hasFollowDealer = hasFollowDealer;

		this.qingyise = qingyise;	
		this.yitiaolong = yitiaolong;
		this.tiandihu = tiandihu;
		this.gangHua = gangHua;
		this.qidui = qidui;
		this.shisanyao = shisanyao;
		
		//_currency = ruleBase.info[gameNumber];
	}

	public JsonData GetData () {
		JsonData json = new JsonData ();
		json["game_type"] = (int)name;
		json["player_count"] = playerCount;
		json["set_num"] = _gameNum;
		json["min_fan"] = 0;
		json["top_fan"] = 255;

    json["is_da8zhang"] = hasDa8Tiles ? 1 : 0;
    json["is_feng"] = hasWind ? 1 : 0;
		json["is_yipao_duoxiang"] = hasMutiHu ? 1 : 0;
		json["is_fanhun"] = hasHun ? 1 : 0;
		json["is_chipai"] = hasChi ? 1 : 0;
		json["is_genzhuang"] = hasFollowDealer ? 1 : 0;
		json["is_paozi"] = hasPaozi ? 1 : 0;

		json["is_qingyise_fan"] = qingyise ? 1 : 0;
		json["is_yitiaolong_fan"] = yitiaolong ? 1 : 0;
		json["is_shisanyao_fan"] = shisanyao ? 1 : 0;
		json["is_ganghua_fan"] = gangHua ? 1 : 0;
		json["is_qidui_fan"] = qidui ? 1 : 0;
		json["is_tiandi_hu_fan"] = tiandihu ? 1 : 0;
		
		return json;
	}
}