# Git - Step One

版本管理工具

<img src="./images/banner.jpg" style="width: 100%">

> 我们已经有了 `svn` ? 为什么还要用 `git` 作为新的版本管理工具
>


## 集中式与分布式

[集中式vs分布式](http://www.liaoxuefeng.com/wiki/0013739516305929606dd18361248578c67b8067c8c017b000/001374027586935cf69c53637d8458c9aec27dd546a6cd6000)

虽然集中式和分布式是区分 svn 和 git 的最大特点，但却不是我们选择 git 作版本管理工具的最重要的原因。

## 分支能解决的问题

方便的分支操作才是！而且我们早就在用啦！

> 分支赋予我们穿行于多维空间的能力。 

### 场景1

高考的数学考场，最后一道数学证明题。证明EFGH是平行四边形。

1. 思路一：证明两组对边分别平行
2. 思路二：证明两组对边分别相等
3. 思路三：证明一组对边平行且相等
4. 思路四：证明两组对角相等的四边形
5. 思路五：对角线互相平分

你不大可能有思路1的时候便在试卷上开始答题。没错，此时草纸便成为了你的分支。当你过滤剩下了一两个可行方法之后你会把草纸分支上的代码合并到试卷上(主分支)


### 场景2

由于高考数学考爆炸了，你无奈学了设计，大学毕业后你每天忙于做海报。下周是建军节，老板让你用儿童节的海报改6稿让他决定用哪张做宣传。

你不大可能复制出6张儿童节的海报，然后在第一张上加一架飞机，在第二张海报上加一艘航母... 极有可能的你会新建一些图层加上这些飞机航母坦克，然后导出不同的组合。

此时图层就是分支。最后导出时 Photoshop 合并了所有可见的图层，完成老板想要的海报。

### 场景3

由于你忘记把海报里儿童的脸拉长了，因此暴露了公司造假的行为。因此建军节的棉花糖销售计划完全失败！你不得不回家待业。此时你找到了一个网络写手的工作。主编的要求不高，每天更新十章即可。这对你来说简直是小菜一碟。

这时你可能遇到这些问题

1. 在你发布新的章节的时候，并不想把写到一半的文章一并发布出去。

1. 在你正在修改 1000 - 2000 章剧情细节的时候，出版社正要你的初稿。

1. ...


这就是分支的能力，让你穿行于不同的世界线之间，这一切都是命运石之门的选择。

---

## git 工作区的构成

> 在我们拥有五维能力之前，让我们先回顾一下我们的四维能力 

```
                本地(local)/当前(current)版本库          | 远程版本库（remote repository）
    工作目录     |  暂存区 staged  |         HEAD        |  远程服务器
               add            commit                  push
             ----->           ----->                 ----->
   
   持有实际文件   | 临时保存你的改动 | 指向最后一次提交的结果  | 

           reset(mixed)     reset(soft)              revert
             <-----          <-----                  <-----
                            reset(mixed)
             <---------------------
                            reset(hard)
  <--------------------------------           
```
## 安装

- Mac 自带 Git
- [Windows msysgit](https://git-for-windows.github.io/)

## 基本命令

### 1. `git status` 查看版本库的当前状态

### 2. `git init` 创建版本库

创建唯一一个`.git` 隐藏文件夹在项目的顶层目录

#### 回退: `rm -rf .git`

### 3. `git add <fileName>` 将文件添加到版本库中

添加所有更改 `git add .` / `git add *`

#### 回退：   
  - `git rm (-r) --cached <fileName>` -r 允许递归删除
  - `git reset (--mixed)`  



### 4. `git commit` 将暂存区的代码提交到版本库中

#### `-m` 提交信息 
每次提交都需要输入提交的信息，`git commit -m "message"`, 如果你需要输入更为详细的信息，则直接输入 `git commit` 会唤醒 `vi`

#### `--amend` 修改上次提交信息

```
$ git add README.md
$ git commit -m "add README"

$ git add Branch.md
$ git commit --amend -m "add Branch.md"

// 此时只有一次提交
```

如果只想修改提交的描述信息，直接使用 `git commit --amend` 即可。

#### 回退：
  - `git reset --soft HEAD^` 保留工作区文件，保留 git add，取消 git commit
  - `git reset (--mixed) HEAD^` 保留工作区文件，取消 git add，取消 git commit
  - `git reset --head HEAD^` 取消工作区文件修改，取消 git add，取消 git commit

^ 与 ~ 延伸阅读：
- [git寻根——^和~的区别](http://mux.alimama.com/posts/799)
- [stackoverflow](https://stackoverflow.com/questions/2221658/whats-the-difference-between-head-and-head-in-git)

#### 如果想要唤醒你常用的编辑器进行编辑需要进行配置

- Windows

  1. 首先将你的编辑器添加到环境变量中
  eg: `我的电脑` - `右键属性` - `左侧高级系统设置` - `环境变量` - `PATH` 中添加 `D:\software\work\Microsoft VS Code\bin`
  
  2. 命令行执行 `git config core.editor code[subl][atom]...`

- OSX
  1. 添加编辑器的命令
  2. 命令行执行 `git config core.editor code[subl][atom]...`


没有改变文件关闭编辑模式，则会取消这次提交。

> Aborting commit due to empty commit message.

### 5. `git rm` 从版本库中删除文件

#### 回退：
  1. 删除动作 `git rm` 未提交：
    
  ```
  git reset HEAD hello.sh
  git checkout -- <fileName>
  ```
  
  2. 删除动作 `git rm` 已经提交: 
    
  ```
  git reset HEAD^ hello.sh
  git checkout -- <fileName>
  ```

#### 对修改了但未提交到版本库（HEAD）中的文件进行删除
  
  1. `git rm -f <filename>` 强制删除
  2. `git rm -cached <filename>` 删除版本库中的索引，但保留实际文件


#### 对未添加到暂存区的文件进行删除，则使用系统自带的 `rm -rf` 命令

### 6. `git mv` 移动/重命名

想当与 `git rm` 之后再创建一个新的文件。我们需要对新的文件使用 `git add` 后方能提交（commit）

### 7. `git log` 从新到旧查看 commit 日志，

可以添加文件名来查看某一个文件的提交信息

```
commit 8ee969d5a80a73d76c17bdb5cbe1312a086ebeff   - 提交码
Author: suikafan <406279919@qq.com>               - 作者
Date:   Tue Jun 20 15:05:58 2017 +0800            - 提交日期

    devide file                                   - 提交信息
```

#### --graph

以图表形式查看日志

#### --pretty=oneline

信息已单行显示

### 8. `git show 提交码` 查看修改的详细信息

```
git show 8ee969d5a80a73d76c17bdb5cbe1312a086ebeff
```

### 9. `git diff` 查看提交之间的区别

- `git diff 提交码1 提交码2` 比较两次提交间的区别
- `git diff 提交码1` 比较提交和当前缓存区的区别

--- 

> 想要更懒

## exLink

- [cmder(windows)](http://cmder.net/)
- [fish shell(OSX)](https://www.fishshell.com/)
- [Git 简明指南](https://rogerdudler.github.io/git-guide/index.zh.html)
- [廖雪峰 - Git 教程](http://www.liaoxuefeng.com/wiki/0013739516305929606dd18361248578c67b8067c8c017b000)

- Github入门与实践(svn)
- [关于reset](http://blog.csdn.net/w958796636/article/details/53611133)
- [如何在 Git 里撤销(几乎)任何操作](http://blog.jobbole.com/87700/)

