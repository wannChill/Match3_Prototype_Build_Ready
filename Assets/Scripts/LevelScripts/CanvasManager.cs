﻿using UnityEngine;
using UnityEngine.UI;

namespace Match3
{
    public class CanvasManager : MonoBehaviour, IData
    {
        [SerializeField]
        private Animator _lostScreen;
        [SerializeField]
        private Animator _winScreen;
        [SerializeField]
        private Animator _tutorialScreen;
        [SerializeField]
        private Animator _blackScreen;
        [SerializeField]
        private Animator _noMoveScreen;
        [SerializeField]
        private Button _nextLevel;
        public bool IsTutorialShown { get; private set; }
        private int _currentLevel;

        private void Start()
        {
            _blackScreen.SetTrigger(Extensions.Show);
            if (!IsTutorialShown)
                _tutorialScreen.SetTrigger(Extensions.Show);
        }

        public void CloseTutorial()
        {
            _tutorialScreen.SetTrigger(Extensions.Hide);
            IsTutorialShown = true;
            StartCoroutine(LevelManager.Singleton.ChipsShowUp());
        }

        public void CloseLostScreen(bool isExit)
        {
            _lostScreen.SetTrigger(Extensions.Hide);
            _blackScreen.SetTrigger(Extensions.Hide);
            LevelManager.Singleton.SetExitState(isExit);
            DataManager.Singleton.SaveGame();
        }

        public void CloseWinScreen(bool isExit)
        {
            _winScreen.SetTrigger(Extensions.Hide);
            _blackScreen.SetTrigger(Extensions.Hide);
            LevelManager.Singleton.SetExitState(isExit);
            DataManager.Singleton.SaveGame();
        }

        public void ShowLostScreen() => _lostScreen.SetTrigger(Extensions.Show);
        public void ShowWinScreen()
        {
            _winScreen.SetTrigger(Extensions.Show);
            if (_currentLevel >= 11)
                _nextLevel.interactable = false;
        }

        public void ShowNoMoveScreen() => _noMoveScreen.SetTrigger(Extensions.Show);
        public void HideNoMoveScreen() => _noMoveScreen.SetTrigger(Extensions.Hide);

        public void LoadData(GameData data)
        {
            IsTutorialShown = data.TutorialShown;
            _currentLevel = data.CurrentLevel;
        }

        public void SaveData(ref GameData data)
        {
            data.TutorialShown = IsTutorialShown;
        }
    }
}