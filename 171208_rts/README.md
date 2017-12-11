# Unity实战 RTS3D即时战略游戏开发（一）：场景

  1.将地形资源放入场景

  2.写控制脚本，定义玩家信息

# Unity实战 RTS3D即时战略游戏开发（二）：玩家状态显示

  1.对地形进行导航路径bake

  Windows-> Navigation ->Bake选项(设定筛选地形参数)->bake

  2.定义相机控制类

  鼠标：使用滚轴设置camera视口，鼠标获横向移动设置camera旋转

  键盘：使用键位WASD 设置camera x,z 方向位移

  3.制作预制体
  Asset Store购买Free modle -> modle 挂载 script -> add collider(select) -> add animator ->完成prefab

# Unity实战 RTS3D即时战略游戏开发（三）：单位选中处理

  1.定义抽象选中类,抽象方法 Select/DeSelect -> prefab 挂载

  2.Select 触发特效 / DeSelect 特效消失 (我网上copy的脚本)
  

# Unity实战 RTS3D即时战略游戏开发（四）：鼠标管理器

  实现选中(可多选)效果:  
  
  定义Input控制类 -> list存储select -> update 更信息 

  mouseDow 触发 , 选中add list 设置select /list clear 未点击的目标DeSelect  , shift 控制多选 

  参考:

  Unity3D实现鼠标选中“高亮”显示功能

  http://blog.csdn.net/iothua/article/details/52328266

  结合轮廓显示，实现完整的框选目标(附Demo代码) 

  http://www.ceeger.com/forum/read.php?tid=3682

# Unity实战 RTS3D即时战略游戏开发（五）：Navigation Mesh 自动寻路

  1.select add NavMeshAgent 

  2.Input捕捉mouse右键点击，ray + 地形collider 获取目标地址 

  3.select 触发 ，animator agent执行动作 / DeSelect animator agent停止

# Unity实战 RTS3D即时战略游戏开发(六) ：信息显示

  构建单位modle

# Unity实战 RTS3D即时战略游戏开发(七)： HUD的使用 小地图显示

  1.创建UI/mapScript

  2.设置地图两角 -> 计算出地图vector2 -> object创建的时候创建pointPrefab->获取gameObject位置，将其转化为canvas Vector2

  3.将预制体全部设置该script

# Unity实战 RTS3D即时战略游戏开发(八) ：HUD的使用 单位信息显示

  构建UI select 触发 更新 UI ,未选择任何对象则显示默认的UI 

# Unity实战 RTS3D即时战略游戏开发(九) ：行为管理器 Action的使用
  
  为modle绑定点击事件，将可以绑定的操作栏设置图片 & action 

# 原文地址

  http://gad.qq.com/article/detail/33983

 # 题外

 单例中的双重判断

  https://www.cnblogs.com/damonhuang/p/5431866.html

 对象池

 帧同步

 http://gad.qq.com/article/detail/28435

 http://gad.qq.com/article/detail/33828

 http://gad.qq.com/article/detail/32563