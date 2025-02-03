using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InfiniteCustom;
namespace GlobalCustom
{
   public enum LevelStatus
    {
        non=0,
        grade1=1,
        grade2=2,
        grade3=3
    }
    public enum GameStatus
    {
        playing=0,
        pause=1,
        completed=2,
        failed=3,
        temp=4
    }
    public class GlobalValue
    {
        public static int i_cookies;//标记
        public static float f_soundvalue;//音量值
        public static List<int> levelgrade;//关卡评分
        public static List<int> levelbest;//关卡最佳成绩
        public static int i_player_max_level;//玩家到达最高关卡
        public static string s_next_sceneName;//将要进入的场景
        public static int i_want_level;//即将进入的关卡
        public static int i_want_level_best;//将进入关卡的最佳成绩
        public static int i_page_count=2;//选关页
        public static  GameStatus gamestatus;//游戏状态
        public static int i_current_grade;//当前关卡的评分
        public static int i_current_time;//当前关卡耗费时间
        public static int i_infinite_level;//无限模式下当前关卡
        public static int i_infinite_mintime;//无限模式最佳成绩花费最短时间
        public static int i_infinite_maxlevel;//无限模式最佳成绩到达的关卡
        public static int i_infinite_max_level=10;//无限模式下最高关卡
        public static Infinite inflv;//无限模式下创建的对象

    }
}

