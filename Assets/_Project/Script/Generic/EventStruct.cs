using _Project.Script.Interface;
using UnityEngine;

namespace _Project.Script.Generic
{
    public struct GoldChangeEvent : IEvent
    {
        public int oldGold;
        public int newGold;

        public GoldChangeEvent(int oldGold, int newGold)
        {
            this.oldGold = oldGold;
            this.newGold = newGold;
        }
    }

    public struct LifeChangeEvent : IEvent
    {
        public int oldLife;
        public int newLife;

        public LifeChangeEvent(int oldLife, int newLife)
        {
            this.oldLife = oldLife;
            this.newLife = newLife;
        }
    }
    
    public struct StageLevelChangeEvent : IEvent
    {
        public int oldStageLevel;
        public int newStageLevel;

        public StageLevelChangeEvent(int oldValue, int newValue)
        {
            oldStageLevel = oldValue;
            newStageLevel = newValue;
        }
    }

    public struct HeroMergeEvent : IEvent
    {
        public int heroId;
        
        public HeroMergeEvent(int heroId)
        {
            this.heroId = heroId;
        }
    }
}
