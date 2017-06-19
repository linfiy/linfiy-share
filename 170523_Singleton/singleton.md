# 单例

## Tips

### 修饰符 sealed

[msdn](https://docs.microsoft.com/zh-cn/dotnet/articles/csharp/language-reference/keywords/sealed)

- 应用于某个类时，sealed 修饰符可阻止其他类继承自该类。 
- 还可以对替代基类中的虚方法或属性的方法或属性使用 sealed 修饰符。 这使你可以允许类派生自你的类并防止它们替代特定虚方法或属性。

### 判断为空

1. `??`

``` csharp
// a.

if (button == null) button = GetComponent<Button>();

// b.

button = button ?? GetComponent<Button>();

```

2. `?.`

``` csharp
// a.

if (button != null) button.onClick.AddListener(Foo);

// b.

button?.onClick.AddListener(Foo);
```

## 单例



1. 

```csharp
public sealed class UserData {
  static UserData uniqueInstance;

  public static UserData singleton {
    get { return uniqueInstance ?? (uniqueInstance = new UserData()); }
  }

  UserData () {}
}
```

2. example.a

```csharp
public sealed class UserData {
  static UserData uniqueInstance = new UserData();

  public static UserData singleton {
    get { return uniqueInstance; }
  }
}
```

3. example.b

```csharp
public sealed class UserData {
  //...
  UserData () {}
}
```

4. example.c

```csharp
public sealed class UserData {
  //...
  static UserData uniqueInstance;
  static readonly object syncObject = new Object();

  public static UserData singleton {
    get {
      if (uniqueInstance == null)
      lock (syncObject)
      if (uniqueInstance == null) uniqueInstance = new UserData();

      return uniqueInstance;
    }
  }
}
```

- example.a 惰性实例化，占用内存少
- example.b 私有化构造函数，确保单例
- example.c 线程安全(多线程)

## Unity 项目中的单例

1. Awake

``` csharp
public class UIController : MonoBehaviour {

  public static UIController instance;

  void Awake () {

    instance = this;

  }
}
```

> 确保一个类只有一个实例，并提供一个全局访问点

**关键词**: `Singleton`, `Unique Instance`, `Lazy Instantiate`


协程说到底还是异步操作，不能完全代替构造函数来对实例进行初始化。

## 单例的适用场景

使用场景：数据库连接，线程池，对话框，缓存。

不能单纯的为了分离而使用单例模式。

```
table
  |- outedStore
  |- Undraw
  |- EWSN
  |- List<PlayerArea>
      |- Discard
      |- HandTile
      ...
  ...
```

```
table.playersArea[2].Discard.Push();
GameManager.instance.tableManager
interface.user.UpdateCurrency();

[x] TableManager.instance.undraw;
[x] User.singleton.UpdateCurrency();
```

## exLink

[Unity组件的基类单例模式](http://blog.csdn.net/tab_space/article/details/51104824)
[c#单例模式（Singleton）的6种实现](http://www.jb51.net/article/100783.htm)

``` java
// java >　1.4
public class Singleton {
  private volatile static Singleton uniqueInstance;
  
  private Singletion () {}

  public static Singleton getInstance () {
    if (uniqueInstance == null) {
      synchronized (Singleton.class) {
        if (uniqueInstance == null) uniqueInstance = new Singleton();
      }
    }

    return uniqueInstance;
  }
}
```