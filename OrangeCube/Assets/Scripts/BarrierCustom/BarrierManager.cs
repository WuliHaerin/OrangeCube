using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using GlobalCustom;
using System.Runtime.InteropServices;

namespace BarrierCustom
{
    public class BarrierManager : MonoBehaviour
    {
        private GameObject root;
        private Scrollbar ui_bar_horizontal;
        private Image ui_img_selectview;
        private float f_horizontal_value;
        private List<Transform> list_img_level = new List<Transform>();
        private int i_level_count;
        private int grade_count;
        private BarrierAudio BA;
        // Use this for initialization
        void Start()
        {
            root = GameObject.Find("Canvas");
            ui_bar_horizontal = root.transform.Find("SelectLevel/Horizontal").gameObject.GetComponent<Scrollbar>();
            ui_img_selectview = root.transform.Find("SelectLevel/SelectPanel/SelectView").gameObject.GetComponent<Image>();
            i_level_count = ui_img_selectview.transform.childCount;
            for (int levelindex=1;levelindex<i_level_count;levelindex++)
            {
                list_img_level.Add(ui_img_selectview.transform.Find("Level"+levelindex.ToString()).transform);
            }
            grade_count = 0;
            for(int listindex = 0; listindex < list_img_level.Count; listindex++)
            {
                grade_count += GlobalValue.levelgrade[listindex];
                list_img_level[listindex].GetComponent<Level>().levelstatus = (LevelStatus)GlobalValue.levelgrade[listindex];
                list_img_level[listindex].GetComponent<Level>().current_best = GlobalValue.levelbest[listindex];
                list_img_level[listindex].GetComponent<Level>().enabled = true;
            }
            root.transform.Find("SelectLevel/Text").GetComponent<Text>().text = grade_count.ToString() + " / 30";
            BA = GameObject.Find("BarrierAudio").GetComponent<BarrierAudio>();
        }

        // Update is called once per frame
        void Update()
        {

        }
        public void BackMenu()
        {
            GlobalValue.s_next_sceneName = "Start";
            BA.PlayBtnClickBgm();
            SceneManager.LoadScene("Load");

        }
        public void PrePage()
        {
            f_horizontal_value = ui_bar_horizontal.value - (float)1 / (GlobalValue.i_page_count - 1);
            if (f_horizontal_value>=0)
            {
                BA.PlayBtnClickBgm();
                ui_bar_horizontal.value = f_horizontal_value;
            }
        }
        public void NextPage()
        {
            f_horizontal_value = ui_bar_horizontal.value + (float)1 / (GlobalValue.i_page_count-1);
            if (f_horizontal_value <= 1)
            {
                BA.PlayBtnClickBgm();
                ui_bar_horizontal.value = f_horizontal_value;
            }
        }
    }

}