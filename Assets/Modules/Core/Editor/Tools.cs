using UnityEditor;
using UnityEngine;

namespace Modules.Core.Editor
{
  public class Tools 
  {
    [MenuItem("Tools/ClearPrefs")]
    public static void ClearPrefs()
    {
      PlayerPrefs.DeleteAll();
      PlayerPrefs.Save();
    }
  }
}
