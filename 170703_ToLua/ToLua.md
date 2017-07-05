# Unity热更新——toLua的使用
## Agenda
* 热更新原理和热更新方案介绍
* Lua环境安装
* Lua语法
* C#与lua语法区别
* 工具
* C#和Lua之间的调用
* LuaFramwork
## 热更新原理和热更新方案介绍
<http://blog.csdn.net/guofeng526/article/details/52662994>
## Lua环境安装
* Linux系统上安装
Linux&Mac上安装。只需要下载源码包并在终端上解压编译即可（以5.3.0版本为例）

```
curl -R -O http://www.lua.org/ftp/lua-5.3.0.tar.gz
tar zxf lua-5.3.0.tar.gz
cd lua-5.3.0
make linux test
make install
```
* Mac OS X系统上安装
```
curl -R -O http://www.lua.org/ftp/lua-5.3.0.tar.gz
tar zxf lua-5.3.0.tar.gz
cd lua-5.3.0
make install
```
* Windows系统上安装Lua

windows下可以使用一个叫"SciTE"的IDE环境来执行lua程序，下载地址为：

Github 下载地址：<https://github.com/rjpcomputing/luaforwindows/releases>
双击安装即可。

Lua官方推荐的方法使用LuaDist:<http://luadist.org>


## Lua语法
<http://www.runoob.com/lua/lua-tutorial.html>
## C#与Lua语法的区别
### 变量
Site|Tag
:--|:--
声明变量|直接声明，不需要public int之类的
｛｝|do end 
nil|表示无效值(在条件表达式中相当于false)
bool|fasle和true
number|表示双精度的实浮点数
string|字符串由一对双引号或单引号来表示
function|由C或Lua编写的函数
userdata|表示任意储存在变量中的C数据结构
thread|表示执行的独立线路，用于执行协同程序
注释|单行注释用：--多行注释用：--[[  --]]

后续我会补全
## 工具
`sublime`

`Nodepad++`

`Vs code`

`...`

##  C#和Lua之间的调用
### `C#调用lua`:LuaState[变量名/函数名]

1.LuaState
* 执行lua代码段
```
DoString(String)
DoFile(.lua文件名)
Require（.lua文件名（但没有.lua后缀））
```
* 获取lua函数或者表
```
LuaFunction func = lua.GetFunction(函数名); 或者  LuaFunction func = lua[函数名] as LuaFunction;
LuaTable table = lua.GetTable(表名);

```
* Start():如果需要使用wrap,则需要调用该方法

2.LuaFunction
```
Call();
```
3.LuaTable
```
LuaTable[变量名/函数名]
ToArray()
```
### `lua调用c#`
在C#中将引用传递到lua中

1.通过"."来使用非静态的变量以及静态的变量与方法

2.通过":"来使用非静态的方法

3.通过"{}"来传递数组给c#

4.创建GameObject：newObject(变量)

5.摧毁GameObject：destroy(变量) 

6.获取组件：GetComponent('LuaBehaviour')

## LuaFramework
我们只需要在tolua里面写属于自己模块部分的逻辑就能简单的实现热更新。

eg.
![](https://ooo.0o0.ooo/2017/07/03/5959af3a72d72.png)


[Lua官方地址](http://www.ulua.org/)

* 从<https://github.com/topameng/tolua>上把代码下载下来，可以clone也可以直接下载zip并解压
* 把Assets下的文件复制到项目里的Assets目录下
* 把Unity5.x下的文件也复制过去，覆盖掉Plugins目录（OSX系统需要注意是合并而不是替换）
* Assets\ToLua\Examples这里面一共是tolua作者给的24个例子
（关于例子的介绍和解释在这里：<http://doc.ulua.org/article/ulua/toluadeexamples01helloworld.html>)




