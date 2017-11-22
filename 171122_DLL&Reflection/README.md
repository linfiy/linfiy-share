###一.DLL
这些.dll库文件一般都放在Assets/Plugins/目录下

[lib/.a和dll/.so的区别](http://www.cppblog.com/amazon/archive/2009/09/04/95318.html)

（1）lib是编译时用到的，dll是运行时用到的。如果要完成源代码的编译，只需要lib；如果要使动态链接的程序运行起来，只需要dll。

（2）如果有dll文件，那么lib一般是一些索引信息，记录了dll中函数的入口和位置，dll中是函数的具体内容；如果只有lib文件，那么这个lib文件是静态编译出来的，索引和实现都在其中。使用静态编译的lib文件，在运行程序时不需要再挂动态库，缺点是导致应用程序比较大，而且失去了动态库的灵活性，发布新版本时要发布新的应用程序才行。

参考:

(我的简书里的文章,有流程)[C#与Unity的DLL交互](http://www.jianshu.com/p/d0dfef0d6da0)

[C++ 写DLL](http://blog.csdn.net/qq_33747722/article/details/53608616)
[如何将Unity中的脚本文件转为dll文件](http://blog.csdn.net/qq_15267341/article/details/51747000)

下面这几个也很有用

(了解)[VS中的dumpbin的使用](http://blog.csdn.net/fengbingchun/article/details/43956673)


(实用)[反编译工具dotPeek](http://blog.csdn.net/mseol/article/details/54381584)


(了解)[关于extern扩展非静态的方法](https://www.zhihu.com/question/48002587/answer/108677110)


(官方参考)[官方文档托管dll](https://docs.unity3d.com/Manual/Plugins.html)

###二.Reflection(C#自带的一个库,查看dll里的内容)
 [详解C#中的反射](http://www.cnblogs.com/Stephenchao/p/4481995.html) ;
 
 [C#之玩转反射](https://www.cnblogs.com/yaozhenfa/p/CSharp_Reflection_1.html);
 
 [C#反射详解](http://blog.csdn.net/jiankunking/article/details/50758924)
***
业余爱好[Unity防破解,加密Dll与Key保护](http://www.cnblogs.com/lixiang-share/p/5979981.html)

###欢迎关注我的简书账号,时不时有些个人的学习笔记和心得,以及一些优秀的文章搬运
[wang_liang](http://www.jianshu.com/u/02a9742375c4)
