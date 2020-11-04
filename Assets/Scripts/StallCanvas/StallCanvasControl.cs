using System;
using UnityEngine;
using UnityEngine.UI;

namespace StallCanvas
{
    public class StallCanvasControl: MonoBehaviour
    {
        /// <summary>
        /// 현재
        /// </summary>
        public Text titleText;
        


        void Start()
        {
            
        }

        private void Update()
        {
            this.titleText.text = DateTime.Now.ToString("현재시각 : HH시 mm분 ss초");
        }
    }
}