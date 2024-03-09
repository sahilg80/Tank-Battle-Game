using UnityEngine;

namespace Assets.Scripts
{
    public class UIHandler : MonoBehaviour
    {
        [SerializeField]
        private GameObject tankSelectionUI;
        [SerializeField]
        private GameObject startMenuUI;

        private void Start()
        {
            
        }

        public void OnClickPlayButton()
        {
            tankSelectionUI.SetActive(true);
            startMenuUI.SetActive(false);
        }

        public void OnClickQuitButton()
        {
            Application.Quit();
        }

    }
}
