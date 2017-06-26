# Git - Step 2

<img src="./images/banner.jpg" style="width: 100%">

## 什么时候是使用分支的好时机？

*  一个分支通常代表一个单独的客户发布版，如果你想开始项目的1.1版本。但你知道一些客户想要保持1.0版本，那就把旧版本留作一个单独的分支

* 一个分支可以封装一个开发阶段，比如原型、测试、稳定或邻近发布。你也可以认为1.1版本发布是一个单独的阶段，也就是维护版本。

* 一个分支可以隔离一个特性的开发或者研究特别复杂的bug。例如，可以引入一个分支来完成一个明确定义的、概念上孤立的任务，或在发布前帮助几个分支合并。
  
  只是为了解决一个 bug 就创建一个新分支，这看起来可能是杀鸡用牛刀了，但 Git 的分支系统恰恰鼓励这种小规模的使用。

* 每一个分支可以代表单个贡献者的工作。另一个分支——“集成”分支——可以专门用于凝聚力量。

> 《Git 版本控制管理（第二版）》

## 命令

### 1. 查看分支

- `git branch` 查看分支名
- `git show-branch` 查看各个分支详细信息

#### 参数
- `-r` 仅查看远程（remote）分支
- `-a` 查看所有（local/remote）分支

**ps:** 上面两个命令都有这两个参数

### 2. `git checkout <branchName>` 切换分支

### 3. 创建分支

1. `git branch <branchName>` 创建一个新的分支
2. `git checkout -b <branchName>` 创建一个分支，并切换到这个新的分支上

### 4. `git branch -D <branchName>` 删除分支

ps: 不能删除当前分支

``` bash
$(branch1) git branch -D <branchName>
# error: Cannot delete the branch 'branchName' which you are currently on.
```


#### 回退：根据 SHA1 值重新创建

1. 误删：删除成功后，会提供删除的 SHA1 值

```
$(master) git branch -D testDel
Deleted branch testDel (was cef8ed7).

$(master) git branch testDel cef8ed7
```

2. 不记得SHA1了，使用 `git reflog` 命令查看那个分支最近一次提交的 SHA1。

``` bash
$(master) git reflog
fb69e96 HEAD@{0}: commit: save
b7781e0 HEAD@{1}: checkout: moving from testDel to learnGitStepTwo
cef8ed7 HEAD@{2}: commit: testDel save  # 这是被删除的分支最后一次提交
....

$(master) git branch testDel cef8ed7
```

[参考](http://blog.csdn.net/u010940300/article/details/47832791)


### 5. `git merge <branchName>` 将<branchName>合并到当前分支

**ps:** 工作目录和暂存区中的修改，在与合并项无关的时候才会合并。所以尽量保证合并时工作目录和
暂存区是干净的，这样合并会容易的多。

#### 回退：

合并后没有新的提交：
  用 `git log` 查找到合并前的最后一次 commit。执行命令 `git reset --hard <SHA1>`

  ps:
  1. 也可以使用  `git reset --hard HEAD~`，当然在你能熟练掌握 HEAD、^ 和 ~ 的意义时才行，
  2. 如果不使用 `--hard` 参数，被合并的分支内容会保留到当前分支的暂存区。所以你需要视情况使用。
  有可能你在合并之后又再次提交了，这是你考虑保留你之前的修改。可以考虑不使用 --hard

#### 处理合并的冲突

``` bash
$ git init
$(master) touch 1.txt
           |
           | edit master
|-------------------------|      
| master/1.txt            |
| add one line for init   | 
|-------------------------|
           |
           |------------>>>------------|
           |                           |
           | edit master               | new branch b1 and edit
|-------------------------|   |-------------------------|
| master/1.txt            |   | b1/1.txt                |
| add one line for init   |   | add one line for init   |
|                         |   | add one line for b1     |
| add one line for master |   |                         |
|-------------------------|   |-------------------------|
           |                           |
           |    merge b1 to master     |
           |------------<<<------------|

$(master) git merge b1

# Auto-merging 1.txt
# CONFLICT (content): Merge conflict in 1.txt
# Automatic merge failed; fix conflicts and then commit the result.           
           
           |
|-------------------------|      
| master/1.txt            |
| add one line for init   |
| <<<<<<< HEAD            |
|                         |
| add one line for master |
| =======                 | 
| add one line for b1     |
| >>>>>>> b1              |
|-------------------------|
           |
           | handle confliction
           |
|-------------------------|      
| master/1.txt            |
| add one line for init   |
| add one line for b1     |
| add one line for master |
|-------------------------|
           |
           |

$(master) git add .
$(master) git commit -m "fix conflict between line2-3 and merge b1 to master"
# [master d299587] fix conflict between line2-3 and merge b1 to master
$(master) git log --graph --pretty=oneline

# *   d29958 fix conflict between line2-3 and merge b1 to master
# |\
# | * 6b84ea edit b1
# * | 632673 master edit
# |/
# * b79b59 master init
```

#### 回退：

如果使用合并命令发现有冲突，此时想取消使用 

``` bash
$(master) git merge --abort
```

**ps:** 多个文件冲突，需要分别处理后再提交。提交后会自动视为合并成功

## HEAD

指针，分支可达

### 树对象保存内容间的关系

### `~` 和 `^` 的区别

- `~` 第几级（第一个）父级
- `^` 第几个父级

```
         A      A =      = A^0
        / \
       /   \
      B     C   B = A^   = A^1     = A~1  
     /|\    |   C = A^2  = A^2
    / | \   |
   /  |  \ /    D = A^^  = A^1^1   = A~2
  D   E   F     E = B^2  = A^^2
 / \     / \    F = B^3  = A^^3
G   H   I   J
                G = A^^^ = A^1^1^1 = A~3
                H = D^2  = B^^2    = A^^^2  = A~2^2
                I = F^   = B^3^    = A^^3^
                J = F^2  = B^3^2   = A^^3^2
```

eg: [参考 - 翻墙](https://stackoverflow.com/questions/2221658/whats-the-difference-between-head-and-head-in-git)


## 分支名规范

- 可以使用 `/` 创建一个分层的命名方案。但是，该分支名不能以斜线结尾。
- 分支名不能以减号 `-` 开头
- 以 `/` 分割的组件不能以 `.` 开头。eg: `feature/.new`
- 分支名的任何地方都不能包含两个连续的点 `..`
- 此外分支名不能包含以下内容
  * 任何空格或其他空白符
  * 在 Git 中具有特殊含义的字符，包括波浪线 `~'`、插入符 `^`、冒号 `:`、问号 `?`、星号 `*`、左方括号 `[`
  * ASCII码控制字符，即小于八进制 `\040` 的字符，或 `DEL` 字符（八进制`\177`）

这些规范由底层命令 `git check-ref-format` 强制检测

## 常用的分支名

- `revert`
- `topic`
- `dev` 开发版 - `15.6-dev`
- `stable` 稳定版 - `0.5-stable`
- `bug`
- `test`
- `feature` 特性
- `patch` 补丁
