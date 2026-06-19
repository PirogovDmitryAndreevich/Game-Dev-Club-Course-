using UnityEngine;

public class TutorialLogic : MonoBehaviour
{
    [SerializeField] private GameObject _desktopTutorial;
    [SerializeField] private GameObject _mobileTutorial;

    public void SetTutorial(bool isDesktop)
    {
        if (isDesktop)
        {
            _desktopTutorial.SetActive(true);
            _mobileTutorial.SetActive(false);
        }
        else
        {
            _desktopTutorial.SetActive(false);
            _mobileTutorial.SetActive(true);
        }
    }
}
