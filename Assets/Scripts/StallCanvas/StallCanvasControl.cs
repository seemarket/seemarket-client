using System;
using UnityEngine;
using UnityEngine.UI;

namespace StallCanvas
{
    public class StallCanvasControl: UICanvasBase
    {
        /// <summary>
        /// 현재 시각을 보여준다.
        /// </summary>
        public Text titleText;
        /// <summary>
        /// 시뮬레이션 시작 버튼
        /// </summary>
        public Button startSimulationButton;
        


        void Start()
        {
            startSimulationButton.onClick.AddListener(StartSimulation);
        }

        
        private void Update()
        {
            this.titleText.text = DateTime.Now.ToString("현재시각 : HH시 mm분 ss초");
        }

        private void StartSimulation()
        {
            CWebData.Instance.FireSimulation();
        }

        public override void OnBackKey()
        {
            
            base.OnBackKey();
            
        }
    }
}