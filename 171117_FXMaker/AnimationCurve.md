## UnityEngine.AnimationCurve
* 创建：创建脚本挂载在物体上，定义public AnimationCurve curve；然后在Inspector面板就可以看到，点击后可进行编辑  
* 简单使用：
```csharp
public void Update()
{
transform.position = new Vector3(Time.time, anim.Evaluate(Time.time), 0);
}
```
* 简单案例：  
利用曲线控制小球的运动轨迹和颜色
```csharp
using UnityEngine;
public class curvetest : MonoBehaviour
{
  public AnimationCurve curve;
  public Color oldColor;
  public Color newColor;
  public float timeScale = 1;
  bool startAnimation = false;
  float startTime = 0;
  float currentTime = 0;
  Material mat;
  void Start()
  {
    mat = transform.GetComponent<Renderer>().material;
    Invoke("ChangeState", 2f);
  }
  void ChangeState()
  {
    startTime = Time.time;
    startAnimation = true;
  }
  float CurrentTime
  {
    get
    {
      if (startAnimation)
      {
        currentTime = ((Time.time - startTime) * timeScale) % 1;
      }
      else
        currentTime = 0;
      return currentTime;
    }
  }
  void Update()
  {

    if (startAnimation)
      UpdateAnimation();
  }
  void UpdateAnimation()
  {
    transform.localPosition = new Vector3(0, curve.Evaluate(CurrentTime), 0);
    mat.SetColor("_Color", Color.Lerp(oldColor, newColor, curve.Evaluate(CurrentTime)));    
  }
}
```