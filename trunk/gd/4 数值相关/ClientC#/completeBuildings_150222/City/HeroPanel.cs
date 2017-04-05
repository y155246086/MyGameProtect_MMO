//CopyRight YingYuGang(232871714@qq.com) 2014-2015
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace BattleFramework.City{
	public class HeroPanel : BasePanel {

        public HeroCard heroCardPrefab;
        public List<HeroCard> heroCards;
        public UITable table;
        public UIButton close;

        protected override void Init()
        {
            List<BattleFramework.Data.HeroData> heros = mCity.user.heros;
            if(heros!=null && heros.Count > 0)
            {
                HeroCard heroCard;
                for(int i=0;i<heros.Count;i++)
                {
                    heroCard = (Instantiate(heroCardPrefab.gameObject) as GameObject).GetComponent<HeroCard>();
                    SetHeroCard(heroCard,heros[i]);
                    heroCard.transform.parent = table.transform;
                    heroCard.transform.localScale = Vector3.one;
                    heroCards.Add(heroCard);
                }
                table.Reposition();
            }
			close.onClick.Add(new EventDelegate(mCity.HideHero));
        }

        public void SetHeroCard(HeroCard heroCard,BattleFramework.Data.HeroData heroData)
        {
            heroCard.attack.text = heroData.attack.ToString();
            heroCard.defence.text = heroData.defence.ToString(); 
            heroCard.fight.text = heroData.fight.ToString();
            heroCard.health.text = heroData.health.ToString();
            heroCard.card.spriteName = heroData.card;
            //TODO
//            heroCard.star.spriteName = "start" + heroData.star;
        }

	}
}
