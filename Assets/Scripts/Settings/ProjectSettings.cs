using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ProjectSettings", menuName = "ProjectSettings", order = 51)]
public class ProjectSettings : ScriptableObject
{
   public NetworkSettings networkSettings;
}
