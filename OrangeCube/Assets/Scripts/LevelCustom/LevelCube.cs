using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InfiniteCustom;
using GlobalCustom;
namespace LevelCustom
{
    public class LevelCube : MonoBehaviour
    {

        public int i_cube_row;//当前方块在二维数组中的行
        public int i_cube_column;//当前方块在二维数组中的列
        public int i_cube_weight;//当前方块的权值，灰色为0，橙色为1
        private IniGame IG;//处理游戏点击的对象
        void Start()
        {
            i_cube_weight = 0;//所有方块初始权值为0
            IG = transform.parent.parent.GetComponent<IniGame>();
        }
        //方块点击
        public void Click()
        {
            //判断游戏是否正在进行，防止暂停方块仍然可以点击的问题
            //点击后及时处理方块的权值问题
            if (GlobalValue.gamestatus==GameStatus.playing)
            {
                if (i_cube_weight == 0)
                {
                    i_cube_weight = 1;
                }
                else
                {
                    i_cube_weight = 0;
                }
                //发送当前方块的信息
                IG.DowithCubeClick(i_cube_row, i_cube_column);
            }
           
        }
    }
}

