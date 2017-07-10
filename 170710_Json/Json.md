# **Json**   
JavaScript Object Notation(JavaScript 对象表示法）
## **Json简介**   
JSON是一种轻量级的数据交换格式，它基于JavaScript的一个子集，JSON采用完全独立于语言的文本格式。  
* **Json是一种基于文本的数据格式**  
举例：我们要给别人介绍你的一个朋友，你的朋友这时就作为一个对象存储在你的大脑，如何让别人了解他呢？  
这时我们就需要将你朋友的特征组织成一段语言描述给对方，描述他的姓名、性别、年龄、外貌等，这时你的语言就相当于一个文本。但是如果对方是外国人，那你需要使用对方能够理解的语言或方式来描述，这时你朋友的特征就转化成对方能够理解的数据格式被对方了解。   
JSON是基于文本的数据格式，JSON在传递的时候是传递符合JSON这种格式的字符串，我们常称为“JSON字符串”。
* **轻量级的数据格式**   
相比于其它数据格式（如XML），JSON比较轻量，相同数据JSON的格式占据的带宽更小，这在数据请求和传递的情况下是有明显优势的。  
* **易于阅读、编写和机器解析**   
JSON是易于阅读、编写和机器解析的，即JSON对人和机器都是非常友好的。 

这些特性使JSON成为理想的数据交换语言，易于人阅读和编写，同时也易于机器解析和生成。
## **Json语法**  
Json数据的书写格式是：键/值  
```csharp
"name" : "zhangsan"
```   
这个比较容易理解，等价于C#语句  
```csharp
name = "zhangsan"
```
Json的键使用字符串表示   
Json的值可以是:   
* 数值（整数或浮点数）  
* 字符串   
* 布尔值（true或false）  
* 数组（在中括号中）   
* 对象（在大括号中）   
* 空（null）  

数据直接用逗号隔开   
大括号保存数据 
### **Json的两种结构，对象和数组**   
#### 对象使用大括号保存数据
```csharp
{"name" : "ZhangSan", "ID" : 123456}
```      
#### 数组使用中括号保存数据 
```csharp  
{
    "Player" : [
        {"name" : "ZhangSan", "ID" : 123456},
        {"name" : "LiSi", "ID" : 123457},
        {"name" : "WangWu", "ID" : 123458}
    ] 
}
```
<font color=blue>JSON 是 JS 对象的字符串表示法，它使用文本表示一个 JS 对象的信息，本质是一个字符串。   
JSON是可以传输的，因为它是文本格式，但是JS对象是没办法传输的，在语法上，JSON会更加严格。</font>   
## **LitJson**
---
LitJson是一个.NET平台下处理Json格式数据的类库。   
LitJson是一个开源项目，比较小巧轻便，安装也很简单，在Unity里只需要把LitJson.dll放到Assets文件夹下，并在代码的最开头添加 “Using LitJson”命名空间就可以了。  
[LitJson Git地址](https://github.com/lbv/litjson)  
[LitJson网站](http://lbv.github.io/litjson/)    
## LitJson中常用的类
### **JsonData**   
* 使用C#认识的JsonData来处理生成Json字符串
```csharp
JsonData jsondata1 = new JsonData();
jsondata1["name"] = "ZhangSan";
jsondata1["ID"] = 123456;
jsondata1["sex"] = "male";
string json1 = jsondata1.ToJson();
print(json1);
```
运行结果：<font color=green>{"name":"ZhangSan","ID":123456,"sex":"male"}</font>  
* 对象中嵌套对象  
```csharp
JsonData jsondata2 = new JsonData();
jsondata2["name"] = "LiSi";
jsondata2["info"] = new JsonData();
jsondata2["info"]["ID"] = 123457;
jsondata2["info"]["sex"] = "female";
string json2 = jsondata2.ToJson();
print(json2);
```
运行结果：<font color=green>{"name":"LiSi","info":{"ID":123457,"sex":"female"}}</font>    
这里面使用了Jsondat中定义的方法
```csharp
    public string ToJson();
``` 
### **JsonMapper**   
* 使用JsonMapper将JsonData解析到Json字符串   
```csharp
JsonData jsondata = new JsonData();
jsondata["name"] = "ZhangSan";
jsondata["ID"] = 123456;
jsondata["sex"] = "male";
string json = JsonMapper.ToJson(jsondata);
print(json);
```
运行结果：<font color=green>{"name":"ZhangSan","ID":123456,"sex":"male"}</font>    
* 使用JsonMapper将对象解析到Json字符串

自定义Player类
```csharp
public class Player
{
  public string name;
  public int ID;
  public string sex;
}
```
```csharp
Player player = new Player();
player.name = "ZhangSan";
player.ID = 123456;
player.sex = "male";
string json = JsonMapper.ToJson(player);
print(json);
```
运行结果：<font color=green>{"name":"ZhangSan","ID":123456,"sex":"male"}</font>
* 使用JsonMapper将Json字符串解析到JsonData   
```csharp
    public static JsonData ToObject(string json);
``` 
将上例中的Json字符串解析成JsonData
```csharp
JsonData jsondata = JsonMapper.ToObject(json);
print(jsondata["name"] + "  " + jsondata["ID"] + "  " + jsondata["sex"]);
```
运行结果：<font color=green> ZhangSan  123456  male</font>
* 使用JsonMapper将Json字符串解析到对象   
```csharp
public static T ToObject<T>(string json);
```
将上例中的json字符串解析成player对象
```csharp
Player player1 = JsonMapper.ToObject<Player>(json);
print(player1.name +"  " + player1.ID +"  "+ player1.sex);
```
运行结果：<font color=green> ZhangSan  123456  male</font>
### **Jsondata的类型转换**
* Jsondata中定义了五种隐式转换，可以将（bool、double、int32、int64、string）类型隐式转换成Jsondata类型。  
```csharp
JsonData jsondata = new JsonData();
jsondata["name"] = "ZhangSan";
jsondata["ID"] = 123456;
//jsondata["info"] = new int[] { 1, 2, 3 };
jsondata["info"] = JsonMapper.ToObject(JsonMapper.ToJson(new int[] {1,2,3}));
string json = jsondata.ToJson();
print(json);
``` 
运行结果：<font color=green>{"name":"ZhangSan","ID":123456,"info":[1,2,3]}</font>
* Jsondata中定义了五种显式转换，可以将Jsondata类型显示转换成（bool、double、int32、int64、string）类型。     
```csharp
    string name = (string)jsondata["name"];
    int id = (int)jsondata["ID"];
    // int[] info = (int[])jsondata["info"];
    int[] info = new int[] { };
    info = JsonMapper.ToObject<int[]>(jsondata["info"].ToJson());
    foreach (int i in info) print(i);
```
### **推荐阅读：**   
[深入理解Json](http://www.jianshu.com/p/4638fa7555aa)