using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InfiniteCustom;
using GlobalCustom;
namespace InfiniteCustom
{
    public class LevelCube : MonoBehaviour
    {

        public int i_cube_row;
        public int i_cube_column;
        public int i_cube_weight;
        private IniInfinite IF;
        void Start()
        {
            i_cube_weight = 0;
            IF = transform.parent.parent.GetComponent<IniInfinite>();
        }
        public void Click()
        {
            if (GlobalValue.gamestatus == GameStatus.playing)
            {
                if (i_cube_weight == 0)
                {
                    i_cube_weight = 1;
                }
                else
                {
                    i_cube_weight = 0;
                }
                IF.DowithCubeClick(i_cube_row, i_cube_column);
            }

        }
        // Update is called once per frame
        void Update()
        {

        }
    }
}

