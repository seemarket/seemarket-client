using System;
using test;
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

        /// <summary>
        /// 뒤로 가기 버
        /// </summary>
        public Button backButton;

        public CanvasControl parent;


        void Start()
        {
            backButton.onClick.AddListener(goToBack);
            startSimulationButton.onClick.AddListener(StartSimulation);
        }

        
        private void Update()
        {
            this.titleText.text = DateTime.Now.ToString("현재시각 : HH시 mm분 ss초");
        }

        private void StartSimulation()
        {
            CLocalDatabase.Instance.FireSimulation();
        }

        /// <summary>
        /// 뒤로 가는 버튼
        /// </summary>
        private void goToBack()
        {
            parent.goOwner();
        }
        
        public override void OnBackKey()
        {
            goToBack();
        }
    }
}