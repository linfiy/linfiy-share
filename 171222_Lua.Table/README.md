# Lua Table (表)


## 表的特点

1. table(表)是Lua中特有的比较强大的一种数据结构；

   1：table用来创建不同的数据类型；

   2：table是一个“关联数组”， 数组的索引可以是数字或字符串；

   3：table的大小是不固定的，可以根据需要进行调整；

   4：table的默认初始索引一般从1开始；


## 表的创建

最简单就是类似于构造函数一样{},这样就是一个空表  
 
    tab ={}

    tab[1]="sun";

    tab[2]=22;

    tab[3]=nil;  

    --print(tab[1],tab[2],tab[3])

    tab["aaa"]="xiaoli";  

    --print(tab["aaa"]);

##  表的操作

① table 连接：  table.concat   

     local tb1={"sun","xiao","li","123333","555"}

     print(table.concat(tb1))

     print(table.concat(tb1,"/"))
     print(table.concat(tb1,"?",1,2))  

--从指定的开始位置到指定的结束位置进行连接
 
② table 插入： table.insert

    table.insert(tb1,"congming")

    print(tb1[4])

    table.insert(tb1,2,"dali") 

    print(tb1[2])


③ table 移除： table.remove
 
     table.remove(tb1)    
     print(tb1[5])
     --table.remove(tb1,1)
     --print(tb1[1])


④ table 排序： table.sort

    --print("排序前")
    for k,v in pairs(tb1) do	
   	print(k,v)
    end

    --table.sort(tb1)
    --print("排序后")
    for k,v in pairs(tb1) do
    --	print(k,v)
    end

--⑤ table 最大值  (指定table中所有正数key值中最大的key值，如果不存在key值为正数的元素，则返回0)

    function tableMax(n)
    local mn = nil;
    for k,v in pairs(n) do
    	if(mn==nil)then
  		mn=v
    	end
    	if(mn<v)then
  		mn=v
    	end
    end
    return mn
    end
    local tb1={5,6,7,8}                     
    print(tableMax(tb1))   



--http://www.runoob.com/lua/lua-tables.html

--http://www.runoob.com/lua/lua-metatables.html




