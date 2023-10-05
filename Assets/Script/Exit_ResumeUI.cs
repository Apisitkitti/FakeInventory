using UnityEngine;
using UnityEngine.UI;

public class Exit_ResumeUI : MonoBehaviour
{
    public GameObject newUI;
    public GameObject OldUI;
    public GameObject resumebutton;
    private GameObject lastClosedUI;
    public void CloseUINEW()
    {
        newUI.SetActive(false);
        resumebutton.SetActive(true);
        lastClosedUI = newUI;
    }
    public void CloseUIOld()
    {
        OldUI.SetActive(false);
        resumebutton.SetActive(true);
        lastClosedUI = OldUI;
    }
    public void ClickedResumeUI()
    {
       if(lastClosedUI == newUI)
       {
            lastClosedUI.SetActive(true);
            resumebutton.SetActive(false);
         
       }
       else if(lastClosedUI == OldUI)
       {
            lastClosedUI.SetActive(true);
            resumebutton.SetActive(false);
         
       }
    }
}
