 #PlayMaker简介：

PlayMaker是由第三方软件开发商Hotong Games开发完成。它的Logo是一个中文的“玩”字（PS：看来中国文化声名远播）。它既是一个可视化脚本工具，又是一个分层逻辑框架。
设计师、程序员使用PlayMaker能够很快的完成游戏原型动作，既适合独立开发者，又特别适合团队合作。

##PlayMaker的优点：

 1. 很多动作行为（例如：跑，跳，攻击等）只需要通过简单状态机FSM（Finite state machine）就能实现，根本不用写一句代码；
 2. 方便简洁的图表管理每个状态机；
 3. 播放游戏，可以实时错误检查；
 4. 集成的帮助，可以让我们快速查看行为说明；
 5. 设置断点和单步执行状态
 6. 可以编写自定义行为，让其出现在我们的行为列表
 7. 支持各类插件配合使用，加速开发进程（例如Ngui, 2D Toolkit, A* Pathfinding等）

##PlayMaker缺点：
 1. 所有的功能必须对应一个状态，本来很简单的几句代码就能实行的动作，PlayMaker需要很多状态才能完成；
 2. 虽说可以无需编写一个代码就能制作完整的游戏，但是对于制作商业级的游戏，PM就不靠谱了，太多的东西实现不了，不过官方一直在更新；
 3. 网络功能还不完善，Bug太多；

##PlayMaker界面：

    --- FSM:当前正在编辑的FSM
    --- State:当前选中的State，可在其上添加Action，即相应的行为，例如：GetButtonDown
    --- Events:State的Event，即State进行切换时的条件
    --- Variables:变量；分为本地变量和全局变量。顾名思义，本地变量只可在当前FSM内使用，而全局变量(Global)在其他的FSM内也可使用，可以用与记录Horizontal以及Vertical的该变量

##PlayMaker说明：

    以现实中的生活例子做说明：
    我们一天中会做很多事情，但我们可以把这些事情归纳为几个状态：睡觉；吃饭；上课；打游戏等。如果把早上作为一天的开始，我们的生活逻辑是这样的：
    一开始我们在睡觉（初始状态），但设了个闹钟（触发条件）；到7点了，闹钟响起来（满足触发条件），我们去吃饭（状态转换）；吃饭完成（满足触发条件），我们去上课（状态转换）；
    下课铃响（满足触发条件），我们又去吃饭（状态转换）；吃完（满足触发条件）看看时间（新状态哦），如果还早（满足触发条件1），就打打游戏（状态转换）；
    如果时间不早了（满足触发条件2），就赶紧去上课（状态转换）；

##步骤：

 1. 创建Fsm

  在PlayMaker中，Fsm是被作为component（组件）添加给GameObject的。因此，一个Fsm可以被看做是一个独立的脚本程序，用以实现一个独立的功能。

  方法一：
  从菜单PlayMaker > PlayMaker Editor中打开PlayMaker编辑器。
  选择需要添加FSM的gameObject，打开PlayMaker编辑器，在编辑器中按照提示点击鼠标右键，选择Add FSM，就为该gameObject附加了全新的FSM类型的组件（Component）：

  方法二：
  直接在检视面板内AddComponent

 2. 编辑Fsm
   1-取名字

   给当前的FSM冠以全新的名称。

   2-添加state

 给当前选中的state冠以一个十分酷炫吊炸天的EnglishNameState
  在空白处点击鼠标右键，选择Add State，可以添加一个新的状态State 2。可以看到新的State 2上是没有触发条件的，这说明State 2完全没可能被激活，也就是完全不起作用。 可以在State 2上单击右键并选择Set as Start State，那么State 2就会变成我们的初始状态，State 1不起任何作用。

 ## Action：

  如果说使用Fsm、States、Events和Transitions可以搭出一个合理的交互逻辑的框架的话，这个交互逻辑在添加Action之前就完全是一个空架子，一个设计而已。
  只有添加了Action，State才变得有意义，GameObject才会随着PlayMaker设计的这个逻辑来行动。

  PlayMaker有非常多的Action，而且还有很多开发者在为PlayMaker编写各式各样的第三方Action（可以理解成有人为PlayMaker这个插件开发插件），
  一个Action通常执行一项或几项Unity3D的“操作”，比如获取某个GameObject的位置，在场景中新建一个Cube，改变一个材质球的颜色，为一个变量赋值等等

  大致分为以下七类
  1. 用来获取参数/变量数值的
  2. 用来改变参数/变量数值
  3. 用来创建或删除游戏物体
  4. 用来给游戏物体添加或删除组件
  5. 用来执行某个组件（或脚本）中的特定功能函数（Function）
  6. 用来触发Fsm事件
  7. 对整个游戏系统进行控制，比如暂停、退出、载入场景等等

 ##Events

 在State 2上点击右键，选择Add Transition > FINISHED。（“FINISHED”也是一个系统事件，代表“本状态已经执行完所有操作的意思,也可以自己添加需要的Evnets），添加完Event之后，需要
 用鼠标拖动报错的Event，拖出一条又细又短的线，然后拖给满足条件后你想执行的State；

 ## System Events：
            APPLICATION PAUSE：游戏暂停时
            APPLICATION QUIT：游戏退出时
            BECAME INVISIBLE：物体不可见时
            BECAME VISIBLE：物体可见时
            COLLISION ENTER：碰撞体进入时
            COLLICION ENTER 2D：2D碰撞体进入时
            COLLISION EXIT：碰撞体离开时
            COLLISION EXIT 2D：2D碰撞体离开时
            COLLISION STAY：碰撞体停留期间
            COLLISION STAY 2D：2D碰撞体停留期间
            CONTROLLER COLLIDER HIT：Controller类碰撞体被触碰时
            JOINT BREAK：骨骼断开时
            JOINT BREAK 2D：2D骨骼断开时
            LEVEL LOADED；关卡载入时
            MOUSE DOWN：鼠标在物体上被按下时
            MOUSE DRAG：鼠标在物体上被按下然后拖动时
            MOUSE ENTER：鼠标滑入物体时
            MOUSE EXIT：鼠标滑出物体时
            MOUSE OVER：鼠标悬停物体之上时
            MOUSE UP：鼠标在物体上按下并松开时（单击）
            MOUSE UP AS BUTTON：鼠标单击（作为按钮）
            PARTICLE COLLISION：粒子碰到碰撞体时
            TRIGGER ENTER：触发器被进入时
            TRIGGER ENTER 2D：2D触发器被进入时
            TRIGGER EXIT：触发器被离开时
            TRIGGER EXIT 2D：2D触发器被离开时
            TRIGGER STAY：触发器被停留期间
            TRIGGER STAY 2D：2D触发器被停留期间          
   ## Variables
        变量是用来储存数据/数值的
        Unity3D自身有变量，不同的Component都有很多或私有（private）或公开（public）的变量，PlayMaker可以通过Action去调用它们（Get Property)或者直接对其赋值（Set Property)。
        PlayMaker自身也有变量，我们叫做Fsm变量，以区别于Unity3D的变量。调用其他Fsm的变量需要用到Get FSM Variable这个Action，为其他Fsm变量赋值要用到Set FSM Variable这个Action。


## PlayMaker入门介绍：
(http://www.jianshu.com/p/ce791bef66bb)

相关教学视频网站：https://www.youtube.com/watch?v=0shjECpuDIk&list=PLYYCDVRRWC7cQsjx0oWtTIyS2Yj41cYnh（需要翻墙）  

PlayMaker关联脚本简单应用：
http://blog.csdn.net/wuyt2008/article/details/51050489 
注：如有问题，欢迎来Q讨论

