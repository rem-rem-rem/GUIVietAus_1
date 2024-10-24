using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectManagerSetting : Singleton<ProjectManagerSetting>
{
    public void SutdownCallBack()
    {
        Application.Quit();
    }
}
