using System.Collections.Generic;
using System.Linq;
using _Project.Script.Generic;
using _Project.Script.ScriptableObjects.Character;
using Script.Generic;
using UnityEngine;

namespace _Project.Script.Manager
{
    public class DataManager : MonoSingleton<DataManager>
    {
        private UserData _userData;
        public const int MergeCount = 3;
        
        public List<MonsterData> MonsterDataList;
        public List<HeroData> HeroDataList;

        private List<HeroData> minLevelHeros;

        public GameObject hitParticle;
        public GameObject mergeParticle;
        
        public int spawnCount;

        protected override void Awake()
        {
            base.Awake();
            
            _userData = new UserData();
            minLevelHeros = HeroDataList.Where(h => h.grade <= 1).ToList();
            spawnCount = 5;
        }

        #region Player Data Method
        
        public int GetCurrentGold() => _userData.gold;
        public int GetLifeCount() => _userData.lifeCount;
        public int GetCurrentStageLevel() => _userData.stageLevel;
        
        public void SetGold(int addValue)
        {
            int oldValue = _userData.gold;
            int newValue = oldValue + addValue;
            _userData.gold = Mathf.Max(0, newValue);
            
            EventHub.Instance.RaiseEvent(new GoldChangeEvent(oldValue, newValue));
        }
        public void SetLifeCount(int addValue)
        {
            int oldValue = _userData.lifeCount;
            int newValue = oldValue + addValue;
            _userData.lifeCount = Mathf.Max(0, newValue);
            
            EventHub.Instance.RaiseEvent(new LifeChangeEvent(oldValue, newValue));
        }
        public void SetStageLevel(int addValue)
        {
            int oldValue = _userData.stageLevel;
            int newValue = oldValue + addValue;
            _userData.stageLevel = Mathf.Max(0, newValue);
            
            EventHub.Instance.RaiseEvent(new StageLevelChangeEvent(oldValue, newValue));
        }
        
        #endregion

        public void GetRandomMonsterSpawn(in Queue<MonsterData> spawnQueue)
        {
            spawnQueue.Clear();

            int count = 10 + Mathf.RoundToInt(10 * _userData.stageLevel * 0.2f);
            
            for (int i = 0; i < count; i++)
            {
                var data = MonsterDataList[Random.Range(0, Mathf.Min(_userData.stageLevel, MonsterDataList.Count))];
                spawnQueue.Enqueue(data);
            }
        }

        public HeroData GetHeroData(int uid) => HeroDataList.FirstOrDefault(d => d.uid == uid);
        public HeroData GetRandomHeroData() => minLevelHeros[Random.Range(0, minLevelHeros.Count)];
        public HeroData GetMergeHeroData(int uid) => HeroDataList.FirstOrDefault(d => d.uid == uid)?.mergedHeroData;
    }
}
