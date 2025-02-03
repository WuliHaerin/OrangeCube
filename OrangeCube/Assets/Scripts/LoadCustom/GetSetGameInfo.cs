using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GlobalCustom;
namespace LoadCustom
{
    public class GetSetGameInfo : MonoBehaviour
    {
        //在过渡场景中重新获取整个游戏信息，这样可以保证游戏信息的及时更新
        List<string> list_level_best = new List<string>();//临时变量，方便获取本地关卡最佳信息
        List<string> list_level_grade = new List<string>();//临时变量，方便获取本地关卡评分信息
        void Start()
        {
            GlobalValue.levelgrade = new List<int>();
            GlobalValue.levelbest = new List<int>();
            for (int temp = 0; temp < GlobalValue.i_infinite_max_level; temp++)
            {
                list_level_best.Add("Level"+(temp+1).ToString());
                list_level_grade.Add("levelgrade"+(temp+1).ToString());
            }
            //首先判断是否存在key cookies 
            //若是存在证明并非首次进入游戏，即本地已存在基本游戏信息
            //若是不存在证明首次进入游戏，那么需要先设置游戏的基本信息
            if (PlayerPrefs.HasKey("cookies"))
            {
                GetInfo();
            }
            else
            {
                SetInfo();
                GetInfo();
            }

        }
        //设置游戏基本信息
        void SetInfo()
        {
            //首先设置cookies值，方便下次进入游戏时判断是否需要重置信息
            PlayerPrefs.SetInt("cookies",0);
            //首次进入游戏将每个关卡的评分设置为0
            //每个关卡的最佳成绩设置为1000，这样每个关卡的第一次通过成绩都可以比默认的高
            for (int temp = 0; temp < GlobalValue.i_infinite_max_level; temp++)
            {
                PlayerPrefs.SetInt(list_level_grade[temp], 0);
                PlayerPrefs.SetInt(list_level_best[temp],1000);
                
            }
            //首次进入游戏，默认玩家到达最高关卡为0
            PlayerPrefs.SetInt("playermaxlevel",0);
            //设置无限模式下，最佳成绩到达的关卡
            PlayerPrefs.SetInt("infmaxlevel", 0);
            //设置无限模式下，最佳成绩花费时间
            PlayerPrefs.SetInt("infmintime", 0);

        }
        //非首次进入，获取游戏基本信息
        void GetInfo()
        {
            //获取到关卡信息
            for (int temp = 0; temp < list_level_best.Count; temp++)
            {
                GlobalValue.levelgrade.Add( PlayerPrefs.GetInt(list_level_grade[temp]));
                GlobalValue.levelbest.Add(PlayerPrefs.GetInt(list_level_best[temp]));
            }
            //分别获取到设置的音量、玩家的最高关卡、无限模式最佳信息
            GlobalValue.i_player_max_level = PlayerPrefs.GetInt("playermaxlevel");
            GlobalValue.f_soundvalue = PlayerPrefs.GetFloat("bgsound");
            GlobalValue.i_infinite_maxlevel = PlayerPrefs.GetInt("infmaxlevel");
            GlobalValue.i_infinite_mintime = PlayerPrefs.GetInt("infmintime");
        }
      
    }
}

