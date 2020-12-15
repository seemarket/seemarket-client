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

        public Button moveSimulationButton;

        public Button changeSimulationButton;

        public Button stopSimulationButton;
        
        
        /// <summary>
        /// 뒤로 가기 버
        /// </summary>
        public Button backButton;

        public CanvasControl parent;


        void Start()
        {
            backButton.onClick.AddListener(goToBack);
            startSimulationButton.onClick.AddListener(() =>
            {
                StartSimulation(SimulationType.START);
            });
            stopSimulationButton.onClick.AddListener(() =>
            {
                StartSimulation(SimulationType.STOP);
            });
            changeSimulationButton.onClick.AddListener(() =>
            {
                StartSimulation(SimulationType.CHANGE);
            }); 
            moveSimulationButton.onClick.AddListener(() =>
            {
                StartSimulation(SimulationType.MOVE);
            });
        }

        
        private void Update()
        {
            this.titleText.text = DateTime.Now.ToString("현재시각 : HH시 mm분 ss초");
        }

        private void StartSimulation(SimulationType e)
        {
            CLocalDatabase.Instance.FireSimulation(e);
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