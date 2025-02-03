using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InfiniteCustom
{
    public class Infinite
    {
        int max_level;//无限模式最佳成绩关卡
        int min_time;//无线模式最佳成绩时间
        public Infinite(int max,int min)
        {
            this.max_level = max;
            this.min_time = min;
        }

        public int Max_level
        {
            get
            {
                return max_level;
            }
        }

        public int Min_time
        {
            get
            {
                return min_time;
            }
        }
    }
}
