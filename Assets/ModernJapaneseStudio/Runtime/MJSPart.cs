using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ModernJapaneseStudio {
public class MJSPart : MonoBehaviour {
 public string kind; public Vector3 axis,closedPosition; public Quaternion closedRotation;
 public float openValue,currentValue,restValue; public bool open; public string lastBlocker="";
 public void Apply(float value){currentValue=value;if(kind=="SLIDE")transform.localPosition=closedPosition+axis*value;else transform.localRotation=closedRotation*Quaternion.AngleAxis(value,-axis);Physics.SyncTransforms();}
 public bool TryApply(float value){float old=currentValue;Apply(value);lastBlocker="";
  foreach(var box in GetComponentsInChildren<BoxCollider>()){
   var half=Vector3.Scale(box.size,box.transform.lossyScale)*.5f;half=new Vector3(Mathf.Abs(half.x),Mathf.Abs(half.y),Mathf.Abs(half.z));
   foreach(var hit in Physics.OverlapBox(box.transform.TransformPoint(box.center),half,box.transform.rotation,~0,QueryTriggerInteraction.Ignore)){
    if(hit.transform.IsChildOf(transform.parent))continue;
    Vector3 dir;float depth;
    if(Physics.ComputePenetration(box,box.transform.position,box.transform.rotation,hit,hit.transform.position,hit.transform.rotation,out dir,out depth)&&depth>.0015f){lastBlocker=hit.transform.parent?hit.transform.parent.name:hit.name;Apply(old);return false;}
   }
  }return true;
 }
 public void Toggle(){open=!open;}
 void Update(){float target=open?openValue:restValue;if(Mathf.Abs(target-currentValue)>.0001f)TryApply(Mathf.MoveTowards(currentValue,target,(kind=="SLIDE"?1f:110f)*Time.deltaTime));}
}
}