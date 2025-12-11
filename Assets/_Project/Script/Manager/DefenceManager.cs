using System;
using System.Collections.Generic;
using System.Linq;
using _Project.Script.Character;
using _Project.Script.Generic;
using _Project.Script.ScriptableObjects.Character;
using Script.Generic;
using UnityEngine;

namespace _Project.Script.Manager
{
    public class DefenceManager : MonoSingleton<DefenceManager>
    {
        [SerializeField] private Transform startPoint;
        [SerializeField] private Transform endPoint;
        [SerializeField] private List<Transform> wayPoints;
        [SerializeField] private Transform mergePoint;

        private int monsterCount;

        private Dictionary<int, List<HeroObject>> heroDic;
        private Dictionary<int, int> mergeDic;

        public int MonsterCount => monsterCount;
        public Vector3 StartPosition => startPoint ? startPoint.position : Vector3.zero;
        public Vector3 EndPosition => endPoint ? endPoint.position : Vector3.zero;
        public Vector3 MergePosition => mergePoint ? mergePoint.position : Vector3.zero;

        protected override void Awake()
        {
            base.Awake();
            
            heroDic = new Dictionary<int, List<HeroObject>>();
            mergeDic = new Dictionary<int, int>();
        }

        public void SetPoints(Transform start, Transform end, Transform merge, List<Transform> wayPoints)
        {
            startPoint = start;
            endPoint = end;
            mergePoint = merge;
            this.wayPoints = wayPoints;
        }

        public bool GetNextPoint(int nextIndex, out Vector3 nextPoint)
        {
            nextPoint = endPoint.position;
            
            if (nextIndex >= wayPoints.Count) return false;
            
            nextPoint = wayPoints[nextIndex].position;
            return true;
        }

        public void IncreaseMonsterCount() => monsterCount++;
        public void DecreaseMonsterCount() => monsterCount = Mathf.Max(monsterCount - 1, 0);
        

        public void CheckMerge(HeroObject obj)
        {
            int key = obj.uid;
            
            if (heroDic.ContainsKey(key) == false) heroDic.Add(key, new List<HeroObject>());
            
            if (heroDic[key].Contains(obj)) return;
            
            heroDic[key].Add(obj);
            
            if (heroDic[key].Count < DataManager.MergeCount) return;
            
            var nextData = DataManager.Instance.GetMergeHeroData(key);
            
            if (nextData == null) return;
            
            foreach (var heroObject in heroDic[key])
            {
                heroObject.ChangeState(HeroState.Merge);
            }
            heroDic[key].Clear();
            
            var merge = ObjectPoolManager.Instance.Get(nextData);
            
            Vector3 pos = obj.transform.position;
            pos.y += 100f;
            merge.transform.position = pos;
            merge.TryGetComponent(out HeroObject mergeHero);
            mergeHero.ChangeState(HeroState.Fall);
            
        }
    }
}
