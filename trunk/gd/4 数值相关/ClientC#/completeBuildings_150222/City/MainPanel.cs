//CopyRight YingYuGang(232871714@qq.com) 2014-2015
using UnityEngine;
using System.Collections;

namespace BattleFramework.City{
	public class MainPanel : BasePanel {

        public UIButton battleBegin;
        public UIButton technology;
        public UIButton hero;
        public UIButton email;
        public UIButton setting;

		protected override void Init()
        {
            battleBegin.onClick.Add(new EventDelegate(mCity.BattleBegin));
			hero.onClick.Add(new EventDelegate(mCity.ShowHero));
        }
	}
}
