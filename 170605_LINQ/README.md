# LINQ (Language Integrated Query)

> ns: System.Linq;

LINQ 为 C# 引入了 sql 语句和函数式编程的能力

## `IEnumerable` 和 `IEnumerable<T>`

我们只能对实现了 `IEnumerable` 或 `IEnumerable<T>` 的接口的对象进行 Linq; 
因为他们实现 `GetEnumerator {IEnumerator}` 方法。换句话说我们只能对可枚举的对象进行 Linq。

### 实现了这两个接口的类型

1. `T[]`
2. `List<T>`
3. `Dictionary<Tkey, TValue>`
4. `Lookup<Tkey, TValue> = Dictionary<Tkey, List<TValue>>`

## Operator

_ps:花括号里的是返回类型_
_ps:为了更好理解操作符的意义，以下三级标题参考[ReactiveX操作符](http://reactivex.io/documentation/operators.html)列出，但并没有包含 ReactiveX 扩展的操作符_

### 数学的或聚合的操作符 Mathematical and Aggregate Operators

#### `Count {int}`

``` csharp
int count = new int[] {1, 2, 3, 4, 5, 6, 7}.Count(num => num > 4);

// count[int]: 3
```

#### `Concat {IEnumerable<T>}`

_ps:函数调用后会返回一个全新的 IEnumerable, 而不是之前的引用_ 

``` csharp
var groupA = string[]{ "米莎", "霍弗", "雷欧克" };
var groupB = string[]{ "哈拉萨", "斯比雷" };

string[] rexxarGroup = groupA.Concat(groupB).ToArray();

// string[]: [ "米莎", "霍弗", "雷欧克", "哈拉萨", "斯比雷" ]
```

#### `Average {decimal}`

(略)求平均值

#### `Aggregate {T}`

_ps:对应ReactiveX 中的 `reduce` 操作符_

``` csharp
var array = new string[]{ "q", "n", "i", "L", ".", "m", "e", "t", "s", "y", "S"};

string result = array.Aggregate((leftElement, rightResult) => rightResult += leftElement);

// string: "System.Linq"
```

|times|leftElement|rightResult|Return|
|:---:|:---|:---|:---|
|1|"S"|""|"S"|
|2|"y"|"S"|"Sy"|
|3|"s"|"Sy"|"Sys"|
|n-1|"n"|"System.Li"|"System.Lin"|
|n|"q"|"System.Lin"|"System.Linq"|

#### `Sum {decimal}`

(略)求和

#### `Max {T}`

(略)求最大值

#### `Min {T}`

(略)求最小值

---

### 过滤用操作符 Filtering Observables

#### `First/Last {T} `

_ps: Linq 中还包含 FirstOrDefault, LastOrDefault 方法_

[Fist/Last/Single 操作符的对比](http://www.cnblogs.com/MrEggplant/p/5394593.html)


#### `Skip {IEnumerable<T>}`

``` csharp
new int[]{1, 2, 3, 4, 5}.Skip(2).ToArray();

// int[]: [3, 4, 5]
```

#### `Take {IEnumerable<T>}`

``` csharp
new int[]{1, 2, 3, 4, 5}.Take(2).ToArray();

// int[]: [1, 2]
```

### `Where {IEnumerable<T>}`

_ps:对应ReactiveX 中的 `Filter` 操作符_

``` csharp
int result = new int[] {1, 2, 3, 4, 5, 6, 7}.Where(num => num > 4).ToArray();

// int[]: [5, 6, 7]
```

#### `Distinct {IEnumerable<T>}`

去除重复的元素

``` csharp
int result = new int[] {1, 2, 1, 2, 5, 6, 7}.Distinct(num => num > 4).ToArray();

// int[]: [5, 6, 7]
```

#### `ElementAt {T}`

取索引

``` csharp
int result = new int[] {11, 22}.ElementAt(1);

// int: 22
```

---

### 条件和布尔值操作符 Conditional and Boolean Operators

#### `All {bool}` 

`&&`

``` csharp
bool result = new int[] {11, 22}.All(num => num > 10);

// bool: true
```

#### `Any(同Contains) {bool}`

(略)包含则为真 `||`

#### `DefaultIfEmpty {IEnumerable<T>}`

**later**

#### `SequenceEqual {bool}`


``` csharp
var some = new string[]{"1", "2", "3"};
var some2 = new string[]{"1", "2", "3"};

some.SequenceEqual(some2);
// bool: True

var some3 = new Userdata[]{new Userdata()};
var some4 = new Userdata[]{new Userdata()};
			
some3.SequenceEqual(some4);
// bool: False

```

ps: 与 Equals 的区别


#### `SkipWhile {IEnumerable<T>}`

``` csharp
var result = new int[]{1, 2, 3, 1, 5, 0}.SkipWhile(number => number < 3).ToArray();

// int[]: [3, 1, 5, 0]

```

#### `TakeWhile {IEnumerable<T>}`

``` csharp
var result = new int[]{1, 2, 3, 1, 5, 0}.SkipWhile(number => number < 3).ToArray();

// int[]: [1, 2]

```
---


### 变换操作符 Transforming Observables

#### `Cast(同Select) {IEnumerable<T>}`

``` csharp
new int[]{1, 2, 3, 4, 5}.Cast(num => num++);

// int[]: [2, 3, 4, 5, 6]
```

#### `GroupBy {IEnumerable<IGrouping<TKey, T>>}`
分组

```csharp

var numberArray = new int[]{1, 2, 3, 4, 5, 6};

/*
numberArray.GroupBy
keyFunc: num => num % 2
valueFunc: num => num
{
  1: [1, 3, 5]
  0: [2, 4, 6]
}
*/
```

#### `SelectMany` 
展平, 与 GroupBy 相对可以将多重结构合并成一个，详情参考下方 exLink

_ps:对应ReactiveX 中的 `FlatMap` 操作符_

---
### 组合操作符 Combining Observables

#### `Join(同GroupJoin)`

---
### 其他操作符

#### `ToArray {T[]}`

#### `ToList {List<T>}`

#### `ToLookup {Lookup<Tkey, T>}`

#### `ToDictionary {Dictionary<TKey, T>}`

ps: List 的实例还具有其他结构不具备的方法 `ForEach Action<T>`



## [为啥Linq 跟 ReactiveX 的操作符这么像？](http://www.open-open.com/lib/view/open1440166491833.html)

---




## exLink

- c# 经典实例第四章
- [ReactiveX Operators](http://reactivex.io/documentation/operators.html)
- [c# Lookup](http://www.cnblogs.com/williamwsj/p/6108990.html)
- [SelectMany](http://www.cnblogs.com/manupstairs/archive/2012/11/27/2790114.html)


## 思考一下
1. DefaultIfEmpty, SelectMany 和 GroupBy 怎么使用