using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GlobalCustom;
using InfiniteCustom;

namespace LevelCustom
{
    public class IniGame : MonoBehaviour
    {

        public Level lv;//当前关卡信息
        private GameObject res_img_cube;//方块对象（预设）
        private RectTransform tra_img_area;//游戏背景区，也是所有方块的父元素
        private GameObject[,] a_cube;//储存所有方块的二维数组
        private Vector2 v_origin_pos;//原点
        private Vector2 v_cube_pad;//每个方块的宽高
        private Sprite res_cube_yellow;//加载到的黄色方块精灵
        private Sprite res_cube_grey;//加载到的灰色方块精灵
        private int i_weight_count;//总权值，根据权值判定是否通关
        private LevelAudio LA;//音源对象 
        //初始化信息
        void Awake()
        {
            res_img_cube = Resources.Load("Prefab/cube",typeof(GameObject)) as GameObject;
            res_cube_yellow = Resources.Load("Level/lattice", typeof(Sprite)) as Sprite;
            res_cube_grey = Resources.Load("Level/cube", typeof(Sprite)) as Sprite;
            lv = new Level(GlobalValue.i_want_level, GlobalValue.i_want_level_best);
            a_cube = new GameObject[lv.I_level,lv.I_level];
            tra_img_area = transform.Find("areabg").GetComponent<RectTransform>();
            LA = GameObject.Find("LevelAudio").GetComponent<LevelAudio>();
            GlobalValue.gamestatus = GameStatus.playing;
            Time.timeScale = 1;
        }
        //创建方块
        void Start()
        {
            CreateCube(lv.I_level);
        }
        //方块点击后处理
        public void DowithCubeClick(int r,int c)
        {
            LA.Playbgm(1);
            if (CheckRC(r + 1,c))
            {
                DoCube(a_cube[r + 1, c]);
            }
            if (CheckRC(r - 1, c))
            {
                DoCube(a_cube[r - 1, c]);
            }
            if (CheckRC(r, c + 1))
            {
                DoCube(a_cube[r, c + 1]);
            }
            if (CheckRC(r, c - 1))
            {
                DoCube(a_cube[r, c - 1]);
            }
            i_weight_count = 0;
            i_weight_count = GetWeight();
            //权值是否达到要求
            if (i_weight_count==Mathf.Pow(lv.I_level,2))
            {
                GameOver();
            }

        }
        //游戏通关
        private void GameOver()
        {
            
            int lefttime = int .Parse(transform.Find("Time").GetComponent<Text>().text);
            GlobalValue.i_want_level_best = Getbest(lefttime);
            GlobalValue.i_player_max_level = GetMaxLevel(GlobalValue.i_want_level);
            GlobalValue.gamestatus = GameStatus.completed;
        }
        //检查当前关卡是否为玩家可进入的最高关卡
        //若是，则更新玩家通过最高关卡
        int GetMaxLevel(int c)
        {
            if (c < GlobalValue.i_player_max_level)
            {
                return GlobalValue.i_player_max_level;
            }
            else
            {
                return c;
            }
        }
        //更新当前关卡最佳成绩
        int Getbest(int c)
        {
            if (lv.I_mission - c < lv.I_best)
            {
                return lv.I_mission - c;
            }
            else
            {
                return lv.I_best;
            }
        }
        //方块权值的变换
        void DoCube(GameObject obj)
        {

            if (obj.GetComponent<LevelCube>().i_cube_weight == 0)
            {
                obj.GetComponent<LevelCube>().i_cube_weight = 1;
            }
            else
            {
                obj.GetComponent<LevelCube>().i_cube_weight = 0;
            }
        }
        //检查点击方块的周围四个方块是否存在
        private bool CheckRC(int r,int c)
        {
            return (r >= 0) && (r < lv.I_level) && (c >= 0) && (c < lv.I_level);
        }
        //获得总权值，同时根据更新方块精灵图片
        private int GetWeight()
        {
            int weightCount = 0;
            for(int row = 0; row < lv.I_level; row++)
            {
                for (int column = 0; column<lv.I_level;column++)
                {
                    weightCount += a_cube[row, column].GetComponent<LevelCube>().i_cube_weight;
                    if (a_cube[row, column].GetComponent<LevelCube>().i_cube_weight==1)
                    {
                        a_cube[row, column].GetComponent<Image>().sprite = res_cube_yellow;
                    }
                    else
                    {
                        a_cube[row, column].GetComponent<Image>().sprite = res_cube_grey;
                    }
                }
            }
            return weightCount;
        }
        //创建方块
        private void CreateCube(int rowColumn)
        {
            float f_area_width = tra_img_area.sizeDelta.x*0.9F;//可创建方块区域的宽
            float f_cube_width = f_area_width / rowColumn;//每个方块的宽
            //第一个方块的坐标，根据第一个方块的坐标以及每个方快间的坐标差值设置其他方块的坐标
            //生成方块同时设置每个方块的基本信息
            v_origin_pos = new Vector2(-(f_area_width-f_cube_width)/2, (f_area_width-f_cube_width)/2);
            GameObject cubepre;
            for(int row = 0; row < rowColumn; row++)
            {
                for (int column=0;column<rowColumn;column++)
                {
                    cubepre = Instantiate(res_img_cube) as GameObject;
                    cubepre.transform.SetParent(tra_img_area);
                    cubepre.transform.localScale = new Vector3(1, 1, 1);
                    cubepre.GetComponent<RectTransform>().sizeDelta = new Vector2(f_cube_width,f_cube_width);
                    v_cube_pad = new Vector2(row * f_cube_width, -column * f_cube_width);
                    cubepre.transform.localPosition = v_origin_pos + v_cube_pad;
                    cubepre.GetComponent<LevelCube>().i_cube_row = row;
                    cubepre.GetComponent<LevelCube>().i_cube_column = column;
                    a_cube[row,column] = cubepre;
                }
            }
        }

    }
}
