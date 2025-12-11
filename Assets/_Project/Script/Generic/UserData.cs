using System;
using System.Collections.Generic;

namespace _Project.Script.Generic
{
    [Serializable]
    public class UserData
    {
        public int lifeCount;
        public int gold;
        public int stageLevel;

        public Dictionary<int, int> placeMap;

        public UserData()
        {
            gold = 10;
            lifeCount = 10;
            stageLevel = 1;
            
            placeMap = new Dictionary<int, int>();
        }
    }
}
