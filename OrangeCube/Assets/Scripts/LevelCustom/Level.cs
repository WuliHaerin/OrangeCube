using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LevelCustom
{
    public class Level 
    {
        private int i_level;//关卡
        private int i_best;//最佳
        private int i_mission;//要求

        public int I_level
        {
            get
            {
                return i_level;
            }
        }

        public int I_best
        {
            get
            {
                return i_best;
            }
        }

        public int I_mission
        {
            get
            {
                return i_mission;
            }
        }

        public Level(int level,int best)
        {
            this.i_level = level;
            this.i_best = best;
            this.i_mission = level*level*5;
        }
    }

}
