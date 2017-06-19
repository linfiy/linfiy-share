## 一、SQLiteManager 

SQLiteManager是一个支持多国语言基于Web的SQLite数据库管理工具。它的特点包括多数据库管理，创建和连接；表格，数据，索引操作；视图，触发器，和自定义函数管理。数据导入/导出；数据库结构导出。

> 安装 [SQLiteManager](https://sqlitemanager.en.softonic.com/) 

  在SVN的目录下 \_EXAMPLE\md\170614_Unity_ Database\Tools\

### SQLiteManager的使用：

打开SQLiteManager，新建一个数据库，文件格式为*.sqlite,最终要放在Unity编辑器的StreamingAssets目录.

ps: [Unity特殊文件夹的说明](http://www.jianshu.com/p/1d3216b42d0b)

### 数据库文件在各平台的路径

**TIP:** 安卓平台把打出的包.apk 改为.zip格式，解压出来，会在 asset/下找到 数据库文件
![OD5WRTDZ_2)PNNRF58S_Y0E.png](http://upload-images.jianshu.io/upload_images/3139616-0c3f4028e52c32a0.png?imageMogr2/auto-orient/strip%7CimageView2/2/w/1240)

## 二、SQL语法：

[深入研究参考网址](http://www.jianshu.com/p/18c7db2c1d71)

### 基础语句

主要用到的是第11个，有些在SQLiteManager里就可以提前操作

1. 创建数据库(create)
  ``` sql
  CREATE DATABASE database-name
  create table if not exist students (ID integer, name text);
  ```
2. 删除数据库(drop)
  ``` sql
  drop database dbname
  drop table teachers;
  ```
3. 备份sql server (backup)
  - 创建 备份数据的 device
    ``` sql
    USE master
    EXEC sp_addumpdevice ‘disk’, ‘testBack’, ‘c:\mssql7backup\MyNwind_1.dat’
    ```
  - 开始 备份
    ``` sql
    BACKUP DATABASE pubs TO testBack
    ```

4. 创建表(create)
  ``` sql
  create table tabname(col1 type1 [not null] [primary key],col2 type2 [not null],..)
  ```
  根据已有的表创建新表：
  - A：(使用旧表创建新表)
  ``` sql
  create table tab_new like tab_old
  ```
  - B：
  ``` sql
  create table tab_new as select col1,col2… from tab_old definition only
  ```

5. 删除表(drop)
  ``` sql
  drop table tabname
  ```

6. 增加一个列(alter...column)
  ``` sql
  Alter table tabname add column col type
  ```
  **注：** 列增加后将不能删除。DB2中列加上后数据类型也不能改变，唯一能改变的是增加varchar类型的长度。

7. 添加主键(alter)
  ``` sql
  Alter table tabname add primary key(col)
  ```

8. 删除主键(alter)
  ``` sql
  Alter table tabname drop primary key(col)
  ```

9. 索引
  - 创建索引
    ``` sql
    create [unique] index idxname on tabnam (col….)
    ```
  - 删除索引
    ``` sql
    drop index idxname
    ```
  **注：** 索引是不可更改的，想更改必须删除重新建。

10. 视图
  - 创建视图
    ``` sql
    create view viewname as select statement
    ```
  - 删除视图
    ``` sql
    drop view viewname
    ```

11. 几个简单的基本的sql语句

- 选择：
  ``` sql
  select * from table1 where 范围
  ```

- 插入：
  ``` sql
  insert into table1(field1,field2) values (value1,value2)
  ```

- 删除：
  ``` sql
  delete from table1 where 范围
  ```

- 更新：
  ``` sql
  update table1 set field1=value1 where 范围
  ```

- 查找：
  ``` sql
  select * from table1 where field1 like’%value1%’ —like的语法很精妙，查资料!
  ```

- 排序：
  ``` sql
  select @ from table1 order by field1,field2 [desc]
  ```

- 总数：
  ``` sql
  select count as totalcount from table1
  ```

- 求和：
  ``` sql
  select sum(field1) as sumvalue from table1
  ```

- 平均：
  ``` sql
  select avg(field1) as avgvalue from table1
  ```

- 最大：
  ``` sql
  select max(field1) as maxvalue from table1
  ```

- 最小：
  ``` sql
  select min(field1) as minvalue from table1
  ```

## 三、Unity编辑器里的准备
(在demo工程里都有)

Unity编辑器Plugins下需要的库： 

```
Mono.Data.Sqlite.dll  
sqlite3.dll   
System.Data.dll   
```

Android平台需要 `Plugins/Android/libsqlite3.so
StreamingAssets/`

在SQLiteManager 里创建的数据库名称.sqlite

## 四、封装数据库连接的工具DataBaseTool

例子里的DataBaseTool脚本

1. 创建数据库连接。     
2. 打开数据库连接    
3. 创建Command对象并设置sql语句 
4. 调用ExecuteNonQuery ExecuteReader方法,执行sql语句并获得结果
5. 关闭连接。

## 五、使用上述工具类进行数据操作
参见例子demo里的 DataTest脚本

``` csharp
using UnityEngine;
using System.Collections.Generic;
using LitJson;
using UnityEngine.UI;
using System.IO;

public class DataTest : MonoBehaviour {
  List<Dictionary<string, object>> result;
  string database_Name;
  string sql;
  
  void Start()
  {
    //StreamingAssets目录下的数据库文件
    database_Name = "mydatabase.sqlite";
    
    //这个地方可以连着写sql语句,用分号隔开，这样数据库连接就只用走一次
    sql = "insert into Player values('qaq','1001','24');delete from Player where Name='wangwu' ;select * from Player ";
    result = DataBaseTool.GetInstance(database_Name).ExcuteAllRresult(sql);
}

```

## 六、总结

### DataBaseTool进行了简化
核心 `public List<Dictionary<string, object>> ExcuteAllRresult(string sql){}
`

构造方法 `private DataBaseTool(string databaseName){}`

Android 平台得注意数据库的路径 `public string AndriodPaltformSet(string databaseName){}`

### 一些问题
1. 数据库不要实时更新，多用于存档
2. 设计表的时候空值的问题，查找的结果不会报错，在SQLiteManager里看到为NULL
3. 其他关于中途数据库断开连接的问题，可以自行尝试，这些异常都会在 SqliteException 类里抛出 参考Try...Catch...的使用

