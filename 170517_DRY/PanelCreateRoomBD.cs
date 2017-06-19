using UnityEngine;
using System.Collections;
using System.Linq;
public class PanelCreateRoomBD :  CreateroomBase {

	int playerCount;
	int gameNumber;

    void Start()
    {
		ruleName = RuleName.BAODING;
        ruleBase = BDRule.ruleBase;
        initName();
        InitDefualt();
        rulegroup[0].ToggleAddListener(2, rulegroup[2], 4);
    }



        //	bottomText.text = "开局以后扣砖";
        //4人局可选跟庄，4人局默认跟庄
        //rulegroup [0].ToggleAddListener(2,rulegroup [2],4);
		//只有勾选带风牌，才能有十三幺，（默认没有带风，需手动设置不可选十三幺）
		//rulegroup [2].ToggleAddListener(0,rulegroup [3],5,0,true);
		// rulegroup [3].utoggle[5].ChangeEnabled (false);

	//}
	//修改标签内容
	void initName(){
        var keys = ruleBase.info.Keys.ToArray();

        for (int i = 0; i < 3; i++)
        {
            var toggle = rulegroup[1].utoggle[i];
            if (i < keys.Length)
            {
                toggle.gameObject.SetActive(true);
                toggle.SetToggleName(string.Format(
                    "{0}局({1}钻)", keys[i], ruleBase.info[keys[i]]
                ));
            }
            else
            {
                toggle.gameObject.SetActive(false);
            }
        }
        //[局数] 0: 4局2钻, 1: 8局3钻,2:16局4钻
        // rulegroup [1].utoggle[0].SetToggleName("4局(2钻)");
        // rulegroup [1].utoggle[1].SetToggleName("8局(3钻)");
        // rulegroup [1].utoggle[2].SetToggleName("16局(4钻)");

        //[玩法] 0: 下跑儿, 1: 一炮多响, 2:翻混儿 3：可吃牌 4：跟庄(限四人)
        rulegroup [2].utoggle[0].SetToggleName("下跑儿");
		rulegroup [2].utoggle[1].SetToggleName("一炮多响");
		rulegroup [2].utoggle[2].SetToggleName("翻混儿");
		rulegroup [2].utoggle[3].SetToggleName("可吃牌");
		rulegroup [2].utoggle[4].SetToggleName("跟庄(限四人)");
		//[牌型] 0: 清一色, 1: 一条龙, 2:十三幺 3：杠上开花 4：七对子 5：天地胡
		rulegroup [3].utoggle[0].SetToggleName("清一色");
		rulegroup [3].utoggle[1].SetToggleName("一条龙");
		rulegroup [3].utoggle[2].SetToggleName("天地胡");
		rulegroup [3].utoggle[3].SetToggleName("杠上开花");
		rulegroup [3].utoggle[4].SetToggleName("七对子");
		rulegroup [3].utoggle[5].SetToggleName("十三幺");
	}

	public override void InitDefualt(){
		//[人数] 0: 2人, 1: 3人, 2:四人（默认四人）
		rulegroup [0].InitDefualt (2);
		//[局数] 0: 4局2钻, 1: 8局3钻,2:16局4钻（默认8局3钻）
		rulegroup [1].InitDefualt (ruleBase.def);
		//[玩法] 0: 带风牌, 1: 一炮多响, 2:翻混, 3:可吃牌, 4:跟庄(默认翻混，4人带跟庄)
		rulegroup [2].InitDefualt (1);
    rulegroup[2].InitDefualt(2);
    rulegroup [2].InitDefualt (4);
		//[牌型] 0: 清一色, 1: 一条龙, 2:天地胡, 3:杠上开花, 4:七对子, 5:十三幺
		for (int i = 0; i < 5; i++) {
			rulegroup [3].InitDefualt (i);
		}
	}
	public override	void UpdateRule(){
		PlayCount ();
		GameNum ();
		roomRule=new BDRule(
			gameNumber,
			playerCount,
			PanelCreateRoomX.curIndex ==0?false:true,//打八张
			//[玩法] 0: 带风牌, 1: 一炮多响, 2:翻混儿 3：可吃牌 4：跟庄(限四人)
			rulegroup [2].UgroupResult[0], // 带风牌
			rulegroup [2].UgroupResult[1], // 一炮多响
			rulegroup [2].UgroupResult[2], //翻混儿
			rulegroup [2].UgroupResult[3], // 可吃牌
			rulegroup [2].UgroupResult[4], // 跟庄
			//[牌型] 0: 清一色, 1: 一条龙, 2:十三幺 3：杠上开花 4：七对子 5：天地胡
			rulegroup [3].UgroupResult[0], // 清一色
			rulegroup [3].UgroupResult[1], // 一条龙
			rulegroup [3].UgroupResult[2], //十三幺
			rulegroup [3].UgroupResult[3], // 杠上开花
			rulegroup [3].UgroupResult[4], // 七对子
			rulegroup [3].UgroupResult[5] //天地胡
		);
    Debug.Log(PanelCreateRoomX.curIndex == 0 ? false : true);
//		Debug.Log (gameNumber+"  "+playerCount+"  \n"+
//			rulegroup [2].UgroupResult[0]+"  "+rulegroup [2].UgroupResult[1]+"  "+rulegroup [2].UgroupResult[2]+"  "+
//			rulegroup [2].UgroupResult[3]+"  "+rulegroup [2].UgroupResult[4]+"  \n"+
//			rulegroup [3].UgroupResult[0]+"  "+rulegroup [3].UgroupResult[1]+"  "+rulegroup [3].UgroupResult[2]+"  "+
//			rulegroup [3].UgroupResult[3]+"  "+rulegroup [3].UgroupResult[4]+"  "+rulegroup [3].UgroupResult[5]);


	}
	// 人数
	void PlayCount(){
		if (rulegroup [0].UgroupResult [0]) {
			playerCount = 2;
		} else if (rulegroup [0].UgroupResult [1]) {
			playerCount = 3;
		} else if (rulegroup [0].UgroupResult [2]) {
			playerCount = 4;
		}
	}
	// 局数
	void GameNum(){
		var keys = ruleBase.info.Keys.ToArray();
		for (int i = 0; i < keys.Length; i++) {
			if (rulegroup [1].UgroupResult [i]) {
				gameNumber = keys[i];
			}
		}
	}

}
