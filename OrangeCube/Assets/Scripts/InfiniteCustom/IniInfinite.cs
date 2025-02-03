using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GlobalCustom;
using LevelCustom;
using InfiniteCustom;
using System.IO;
using TMPro;

namespace InfiniteCustom
{
    public class IniInfinite : MonoBehaviour
    {
        private GameObject res_img_cube;
        private RectTransform tra_img_area;
        private RectTransform tra_img_mission;
        private GameObject[,] a_cube;
        private Vector2 v_origin_pos;
        private Vector2 v_cube_pad;
        private Sprite res_cube_yellow;
        private Sprite res_cube_grey;
        private int i_weight_count;
        private TMP_Text ui_text_level;
        private InfiniteAudio IA;
        
        void Awake()
        {
            res_img_cube = Resources.Load("Prefab/Infinitecube", typeof(GameObject)) as GameObject;
            res_cube_yellow = Resources.Load("Level/lattice", typeof(Sprite)) as Sprite;
            res_cube_grey = Resources.Load("Level/cube", typeof(Sprite)) as Sprite;
            tra_img_area = transform.Find("areabg").GetComponent<RectTransform>();
            GlobalValue.gamestatus = GameStatus.playing;
            GlobalValue.i_infinite_level = 1;
            Time.timeScale = 1;
        }
        void Start()
        {
            a_cube = new GameObject[GlobalValue.i_infinite_level, GlobalValue.i_infinite_level];
            ui_text_level = transform.Find("Level").GetComponent<TMP_Text>();
            ui_text_level.text = "关卡" + GlobalValue.i_infinite_level.ToString();
            CreateCube(GlobalValue.i_infinite_level);
            tra_img_mission = transform.parent.Find("MissionPanel").GetComponent<RectTransform>();
            GlobalValue.inflv = new Infinite(GlobalValue.i_infinite_maxlevel,GlobalValue.i_infinite_mintime);
            IA = GameObject.Find("InfiniteAudio").GetComponent<InfiniteAudio>();
        }
        public void DowithCubeClick(int r, int c)
        {
            IA.Playbgm(1);
            if (CheckRC(r + 1, c))
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
            if (i_weight_count == Mathf.Pow(GlobalValue.i_infinite_level, 2))
            {
                saveinfo();
                if (GlobalValue.i_infinite_level<GlobalValue.i_infinite_max_level)
                {
                    IA.Playbgm(2);
                    GlobalValue.i_infinite_level += 1;
                    ui_text_level.text = "关卡" + GlobalValue.i_infinite_level.ToString();
                    a_cube = new GameObject[GlobalValue.i_infinite_level, GlobalValue.i_infinite_level];
                    ClearOldCube();
                    CreateCube(GlobalValue.i_infinite_level);
                }
                else
                {
                    FinishMission();
                    Time.timeScale = 0;
                }
            }
        }
       void saveinfo()
        {
            string spendtime = transform.Find("Time").GetComponent<CountTime>().GetInfiniteTime();
            GlobalValue.inflv = new Infinite(GlobalValue.i_infinite_level, int.Parse(spendtime));
            Infinite temp = new Infinite(GlobalValue.i_infinite_maxlevel,GlobalValue.i_infinite_mintime);
            int newinfo = (int)Mathf.Pow(GlobalValue.inflv.Max_level, 2) * 20 - GlobalValue.inflv.Min_time;
            int oldinfo = (int)Mathf.Pow(temp.Max_level, 2) * 20 - temp.Min_time;
            if (newinfo>oldinfo)
            {
                GlobalValue.i_infinite_maxlevel = GlobalValue.i_infinite_level;
                GlobalValue.i_infinite_mintime = int.Parse(spendtime);
            }
            PlayerPrefs.SetInt("infmaxlevel", GlobalValue.i_infinite_maxlevel);
            PlayerPrefs.SetInt("infmintime", GlobalValue.i_infinite_mintime);
        }
       public  void FinishMission()
        {
            IA.Playbgm(3);
            tra_img_mission.localPosition = new Vector3(0,0,0);
            GlobalValue.gamestatus = GameStatus.completed;
            string spendtime = transform.Find("Time").GetComponent<CountTime>().GetInfiniteTime();
            string curinfo = "此次耗时 " + spendtime + " 通过 " + GlobalValue.i_infinite_level.ToString() + " 关";
            string bestinfo = Getbest(spendtime);
            tra_img_mission.GetComponent<Mission>().SetMissionInfo(curinfo,bestinfo);
        }
        public void PauseMission()
        {
            IA.Playbgm(3);
            tra_img_mission.localPosition = new Vector3(0, 0, 0);
            GlobalValue.gamestatus = GameStatus.completed;
            string spendtime = transform.Find("Time").GetComponent<CountTime>().GetInfiniteTime();
            string curinfo = "此次耗时 " + spendtime + " 到达 " + GlobalValue.i_infinite_level.ToString() + " 关";
            string bestinfo = "最佳耗时 "+GlobalValue.i_infinite_mintime+ " 通过 "+GlobalValue.i_infinite_maxlevel+" 关";
            tra_img_mission.GetComponent<Mission>().SetMissionInfo(curinfo, bestinfo);
        }
         string Getbest(string now)
        {
            
            int cur = (int)Mathf.Pow(GlobalValue.i_infinite_level, 2) * 20- int.Parse(now);
            int old = (int)Mathf.Pow(GlobalValue.inflv.Max_level, 2) * 20 - GlobalValue.inflv.Min_time;
            if (cur>=old)
            {
                GlobalValue.inflv = new Infinite(GlobalValue.i_infinite_level, int.Parse(now));
                
                return "最佳耗时 " + now + " 通过 " + GlobalValue.i_infinite_level.ToString() + " 关";
            }
            else
            {
                return "最佳耗时 " + GlobalValue.inflv.Min_time + " 通过 " + GlobalValue.inflv.Max_level + " 关";
            }
        }
        private void ClearOldCube()
        {
            for (int i = 0; i < tra_img_area.childCount; i++)
            {
                Destroy(tra_img_area.GetChild(i).gameObject);
            }
        }
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
        private bool CheckRC(int r, int c)
        {
            return (r >= 0) && (r < GlobalValue.i_infinite_level) && (c >= 0) && (c < GlobalValue.i_infinite_level);
        }
        private int GetWeight()
        {
            int weightCount = 0;
            for (int row = 0; row < GlobalValue.i_infinite_level; row++)
            {
                for (int column = 0; column < GlobalValue.i_infinite_level; column++)
                {
                    weightCount += a_cube[row, column].GetComponent<LevelCube>().i_cube_weight;
                    if (a_cube[row, column].GetComponent<LevelCube>().i_cube_weight == 1)
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
        private void CreateCube(int rowColumn)
        {
            //float f_temp = (float)(rowColumn * rowColumn - 1) / (float)(rowColumn * rowColumn);
            float f_area_width = tra_img_area.sizeDelta.x * 0.9F;
            float f_cube_width = f_area_width / rowColumn;
            v_origin_pos = new Vector2(-(f_area_width - f_cube_width) / 2, (f_area_width - f_cube_width) / 2);
            GameObject cubepre;
            for (int row = 0; row < rowColumn; row++)
            {
                for (int column = 0; column < rowColumn; column++)
                {
                    cubepre = Instantiate(res_img_cube) as GameObject;
                    cubepre.transform.SetParent(tra_img_area);
                    cubepre.transform.localScale = new Vector3(1, 1, 1);
                    cubepre.GetComponent<RectTransform>().sizeDelta = new Vector2(f_cube_width, f_cube_width);
                    v_cube_pad = new Vector2(row * f_cube_width, -column * f_cube_width);
                    cubepre.transform.localPosition = v_origin_pos + v_cube_pad;
                    cubepre.GetComponent<LevelCube>().i_cube_row = row;
                    cubepre.GetComponent<LevelCube>().i_cube_column = column;
                    a_cube[row, column] = cubepre;
                }
            }
        }
    }

}
