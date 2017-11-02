#### 要求
Unity版本：Unity2017

#### TimeLine简介
Timeline功能是Unity2017版本中非常重要的一个功能，它的功能也十分强大。它不仅可以高效的实现场景中的动画，还可以制作更为复杂的过场动画及电影内容。<br>
Timeline 是一套基于时间轴的多轨道动画系统，它支持可视化编辑，实时预览，主要包括Timeline资源，PlayableDirector，组件以及Timeline编辑器。<br>

#### TimeLine基本轨道类型
Activation：用于控制物体的激活状态；<br>
Animation：用于播放AnimationClip或者关键帧动画；<br>
Audio：用于音效的播放，并不能控制播放的长短，就算把声音条拉小，依旧会全部播放；<br>
Control：用于控制粒子，或者另一个Timeline对象的播放；<br>
Playable：用于自定义的播放行为（自定义动画、图片的颜色、材质的某个参数等）；<br>
Cinemachine：用于控制Cinemachien相机系统的播放，它是一个单独的包，可以在Asset Store中下载，导入这个包以后，就可以在timeline中加入Cinemachine Track。

#### 创建TimeLine
Assets -> Create -> Timeline，创建完成后在project视图中会出现一个新的文件：New Timeline

#### 绑定TimeLine
将生成的Timeline绑定到GameObject上。在Hierarchy中创建一个Gameobject，然后把生成的Timeline拖拽到GameObject上。

#### 编辑TimeLine
Window -> Timeline Editor或者在AssetStore旁边选择Timeline，打开Timeline窗口；

#### 设置Playable Director 
在Timeline绑定的GameObject上，找到组件Playable Director，可以调节参数。<br>
Playable：要播放的序列；<br>
Update Method：回放将遵循（DSP，游戏，任何游戏，手动或固定）；<br>
Play on Awake：是否一开始就播放；<br>
Wrap Mode：当序列到达终点时会发生什么（循环，None，保持）；<br>
Initial Time：什么时候开始播放（秒）<br>
可以用代码动态控制Playable Director组件

``` C#
PlayableDirector director = gameObject.AddComponent<PlayableDirector> ();

director.initialTime = 0.0;
director.timeUpdateMode = DirectorUpdateMode.GameTime;

TimelineAsset timeline = (TimelineAsset)Resources.Load ("DisappearTimeline");

director.Play (timeline);
director.time = 42.0f;
```

#### Activation Track
Activation的作用主要是控制场景中的物体是否激活，若对象本身带有动画，那么对象隐藏后再出现，动画会重新播放。<br>
实例：<br>
使用Timeline使场景中的物体一会出现一会消失。<br>
1. 创建一个Timeline，Assets -> Create -> Timeline；<br>
2. 创建一个空对象用来存放Timeline，这个空对象命名为Director，把创建的Timeline拖拽到Director上，在Director的Timeline窗口，点击add添加一个Activation Track；<br>
3. 在scene中，创建一个对象，拖拽到Activation栏。<br>
4. 在Timeline窗口的右边有一个Active的长条，这个长条表示当前为激活状态，选中长条，可以调节长度，复制多个放在右面，中间留出一定空间，这样就可以实现场景中的物体一会出现一会消失。
![](/Users/jiangtao/Desktop/Activation.png)


#### Playable Track
实例：<br>
使用Timeline加速或减速游戏里的时间，这样就不需要修改代码也可以改变对象运动速度。<br>
1. 创建一个Timeline，Assets -> Create -> Timeline；<br>
2. 创建一个空对象用来存放Timeline，这个空对象命名为PlayableDirector，把创建的Timeline拖拽到PlayableDirector上，在PlayableDirector的Timeline窗口，点击add添加一个Playable Track用来管理时间代码；<br>
3. 在scene中，创建一个对象，然后挂在上旋转的脚本：

```C#
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour {

	void Update () {
		transform.Rotate (transform.up, 200 * Time.deltaTime);
	}
}
```
4.子弹时间的脚本BulletTimePlayable.cs，直接把脚本拖拽到Playable Track中，

```C#
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class BulletTimePlayable : BasicPlayableBehaviour {

	public float BulletTimeScale;	//大于1为加速，小于1为减速

	public float originalTimeScale = 1f;

	public virtual void ProcessFrame (Playable playable, FrameData info, object playerData){

		// 检查是否在播放，防止在短条前开始
		if (playable.GetTime () <= 0) return;

		Time.timeScale = Mathf.Lerp (originalTimeScale, BulletTimeScale, (float)(playable.GetTime () / playable.GetDuration ()));
	}

	public virtual void OnBehaviourPlay (Playable playable, FrameData info){

		originalTimeScale = Time.timeScale;
	}
}
```
![](/Users/jiangtao/Desktop/Playable.png)






#### 参考：
[unity timeline使用手册](http://blog.csdn.net/ilypl/article/details/78048593)<br>
[Unity2017 Timeline使用初探](http://blog.csdn.net/q568360447/article/details/75171470?locationNum=6&fps=1)














