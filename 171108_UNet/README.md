# UNet概念
 Unet是一个网络引擎， 为开发者发布全新的多玩家在线工具、技术和服务。
 该技术的内部项目名称为 UNET，全称为 Unity Networking。

 博客：(./http://blog.csdn.net/u010019717/article/details/46845753) — 孙广东

 ## Demo创建：
   1. 创建空物体，NetworkManager，添加NetworkManager和NetworkManagerHUD组件；

   2. 创建Player玩家预制b并拖入manager；

     Player身上需要添加NetWork Identity,勾选Local Player Authority；添加NetWorkTransfprm；
     将预制体拖入NetworkManager的Spawn Info的槽中，直接从预置实例化游戏对象。
     Player Spawn Method:自定义生成位置的选择；
       Random : 在几个 startPosition 中随机选择
     √ Round Robin：在几个 startPosition 中循环选择

   3. 添加NetWorkStartPosition来定位生成位置；

   4. 运行客户端和服务器。

     两玩家相同移动，则需要添加IsLocal判断；
     发现位置信息不同步，添加NetWork Transform组件，


## 组件介绍：
  1. NetworkManager：网络管理器：提供了简单的方法来  启动和停止  客户端和服务器，以及 管理场景，而且具有虚拟函数，用户代码可以使用实现网络事件的处理程序。（网络管理器可以使用完全不用脚本。允许在inspector配置其所有功能);
 
    onlineScene：服务器或主机启动时，将加载online在线的场景。连接到该服务器的任何客户端将奉命加载这个场景。
    offlineScene：网络停止时，加载offline脱机的场景；
    Spawn：注册相应的prefab；
    
  2. HUD：提供简单、默认的用户界面，用来显示OnGUI界面。

    LAN Host：既是服务器也是客户端； 
    LAN Client：客户端；
  3. NetWork Identity：用来确定netID，netID相同，表明是同一物体；
  4. NetWork Transform：该组件用来同步transform中的位置角度以及大小信息，以及刚体信息； 



## 脚本命令解释
    注意：需要引入 using UnityEngine.NetWoring 命名空间；
         继承 NetWorkBehavior；
    
    [SyncVar]
    //成员变量通过[SyncVar]标签被配置成同步的变量 
    //同步变量可以是基础类型，如整数，字符串和浮点数。
    //也可以是Unity内置数据类型，如Vector3和用户自定义的结构体，
    //但是对结构体类型的同步变量，如果只有几个字段的数值有变化，整个结构体都会被发送。
    //每个NetworkBehaviour脚本可以有最多32个同步变量，包括同步列表

    .Spawn
    //Spawn:简单来说，把服务器上的GameObject，根据上面的NetworkIdentity组件找到对应监视连接，在监视连接里生成相应的GameObject.


 

 ## 函数调用
   网络系统 具有网络中执行操作actions 的方法。这些类型的actions 有时是调用远程过程调用。在网络系统中有两种类型的 Rpc ：

   1. Commands 命令 -  从客户端调用 和 运行在服务器上。

          给方法名加前缀Cmd_,暗示将[Command]自定义特性添加给该方法； 

   2. ClientRpc calls - 在服务器上调用 和 客户端上运行。

          添加Rpc前缀；
   
   
   系统定义在客户端和服务器调用的函数

   1. 在Server/Host上调用的：
    OnServerConnect/OnServerDisconnect/OnServerReady/OnServerAddPlayer/OnServerRemovePlayer/OnServerError；

   2. 在客户端调用：
    OnClientConnect/ OnClientDisconnect/OnClientError/OnClientNotReady/OnStartLocalPlayer；


      
