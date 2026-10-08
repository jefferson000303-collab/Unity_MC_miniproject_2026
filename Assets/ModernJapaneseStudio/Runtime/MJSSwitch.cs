using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ModernJapaneseStudio {
public class MJSSwitch : MonoBehaviour {
 public Light[] lights; public bool on=true;
 public void Toggle(){on=!on;foreach(var l in lights)if(l)l.enabled=on;}
}
}