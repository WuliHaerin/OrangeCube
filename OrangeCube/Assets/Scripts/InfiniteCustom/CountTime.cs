using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GlobalCustom;

namespace InfiniteCustom
{
    public class CountTime : MonoBehaviour
    {
        //无限模式下计时
        private Text ui_text_time;
        void Start()
        {
            ui_text_time = GetComponent<Text>();
        }

        public string GetInfiniteTime()
        {
            string result = ui_text_time.text;
            return result;
        }
        void FixedUpdate()
        {
            ui_text_time.text = ((int)Time.timeSinceLevelLoad).ToString();
        }
    }

}
