### 关于分享

|weekly|15:00 - 15:30|
| --- | --- |
| 周一 | *业务相关分享（unity/C#，工具） |
| 周三 | Review Code、优化 |
| 周五 | *其他技术分享（JavaScript）|

---

### 本周预告: 
周一 C# tips:
周三 Review: 主题[DRY](https://en.wikipedia.org/wiki/Don't_repeat_yourself)
周五 JavaScript: 词法作用域和闭包

---

### 两本关于C#的书

1. [C#经典实例](http://www.ituring.com.cn/book/1746) 
2. [C# 6.0 in a Nutshell 电子版](https://s3-ap-southeast-1.amazonaws.com/mylekha-ebook/IT+%26+Programming/c_c%2B%2B_c%23/C%23+6.0+in+a+Nutshell.pdf)
3. [图灵社区](http://www.ituring.com.cn/)

### C# tips


#### 1.获取泛型默认值

``` c#
public T getDefault<T> () { return default(T); }

```

#### 2.常量

``` c#
public class Foo {
  // 静态常量: 声明时必须赋值, 只能编译时初始化
  const int MAX_COUNT = 6;

  // 实例常量: 可以在运行时初始化
  readonly Vector3 POSITION = new Vector3(1, 12, 3);
  readonly Color COLOR;
  public Foo (Color color) { COLOR = color; }
}
```



#### 3.约束类型

``` c#
T AddChildComponent<T> (string findPath) where T: MonoBehaviour {
  var childTransform = transform.FindChild(findPath);
  return childTransform.gameObject.AddComponent<T>();
}

```

#### 4.lamada 表达式代替匿名函数

``` c#
// 无参数，无返回值
() => { int a = 1; }   

// 无参数，有返回值
() => { 
     int a = 1;
     return a;
}
() => int a = 1;

// 有参数，有返回值
(a, b) => { return a + b; }
(a, b) => a + b
(int a, int b) => a + b

// 单参数可省略括号
a => a + 1

/*********/
a => b => a + b
```

#### 5.委托的简单简单写法
``` c#
using UnityEngine;
using System;
public class ShortDelegate: MonoBehaviour {

  delegate int IntegerPipe (int input);
  IntegerPipe AddOne = input => ++input;
  Func<int, int> ReduceOne = input => --input;

  delegate void NoReturnNoInput (); 
  NoReturnNoInput PrintInt = () => { Debug.Log(1); };
  Action PrintString = () => { Debug.Log("1"); };

  delegate bool ReturnBool (int input);
  ReturnBool GetIntegerValue = input => input != 0;
  Predicate<string> GetStringValue = input => input != "" && input != null;

  /*
  a. Delegate  至少0个参数，至多32个参数，可以无返回值，也可以指定返回值类型
  b. Func      至少0个参数，至多4个参数，根据最后一个泛型指定返回类型。必须有返回值，不可void 
  c. Action    至少0个参数，至多4个参数，无返回值 
  d. Predicate 至少1个参数，至多1个参数，返回值固定为bool
  e. 超过4个参数看 C# 经典实例
  */


  void Start () {
    Debug.Log(AddOne(0));           // int => 1
    Debug.Log(ReduceOne(0));        // int => -1

    PrintInt();                     // int => 1
    PrintString();                  // string => "1"

    Debug.Log(GetIntegerValue(0));  // bool => false
    Debug.Log(GetStringValue("a")); // bool => true
  }
}
```







