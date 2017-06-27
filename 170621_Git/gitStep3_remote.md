# Git - Step 3

> 开始工作

<img src="./images/banner.jpg" style="width: 100%">

## 生成 SSH Key

先查看电脑的用户主目录下有没有 `.ssh` 目录，再看看这个目录下有没有 `id_rsa` 和 
`id_rsa.pub` 这两个文件, 如果有就不用重新生成了。

用户主目录：
  - OSX：`~/.ssh`
  - Windows: `C:\Users\yourUserName\.ssh` = `C:\用户\yourUserName\.ssh`

``` bash
$ ssh-keygen -t rsa -C "youremail@example.com"
```

上面的命令会生成 `id_rsa` 和 `id_rsa.pub` 两个文件，如果你想换个名字可以在命令后加上参数 `-f`

``` bash
$ ssh-keygen -t rsa -C "youremail@example.com” -f ~/.ssh/newName_rsa
```

### 私钥和公钥

`id_rsa` 是私钥文件保留在你的机器上即可；`id_rsa.pub` 是公钥文件，需要将内容提供给远程
服务器的管理者。

``` bash
# id_rsa.pub
ssh-rsa AAAAB3NzaC1yc2EAAAADAQABAAABAQDcN2Eaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa/f/JyLu/v+c/0faYZM+aq6VWeZQo7UbC1lQKgezKJiHwEExhPsiqo68YdWeL4yqWlZrsNw3qGhcWfyv6RAmlsmumiB+M+g62EVThHv65P9SxHvWpMyXuZvp6jnj0sCbkQnRxLmhNF4lnFgdnihFRh5/02p5bwGQ8gRIycj0oDgEeE84toqLCHy7NgW6JU9E9K/dD9wm/8N8cWC48/0R5KkItAfrbLpKaTooYjuKN3U3PMptGt4i6ASxC0sTCk4LaP35s5RDTDnmDVf4vVwLHcDTmLkD9+taWgl4koG5 fsm114000@163.com
```

eg: 将公钥保存到 github 上
1. 注册账号
2. 点击右上角的头像，在弹出的框体中选择 Setting 选项
3. 点击左侧的 `SSH and GPG keys` 后再点击右边的 `New SSH key`
4. Title 用于备忘你的 Key 的作用，在 Key 的输入框中粘贴你公钥的文本内容。点击 `add SSH key`

### [管理多个SSH-key](https://my.oschina.net/stefanzhlg/blog/529403)

## 远程项目 remote

### 源头 `.git`

创建项目
1. 登录远程服务器，再使用 `git init`

2. 修改 `/gitosis-admin`，再在本地创建推送到远程服务器


### 附属的小文件

1. `.gitignore`

* 空行会被忽略，以 `#` 开头的行可以用于注释。然而如果 `#` 跟在其他文本后面就表示注释了。
* 一个简单的字面文件名会匹配任意目录下的同名文件
* 目录名由末尾的反斜线 `/` 标记。这能匹配同名的目录和子目录。但不匹配文件的连接或符号
* 包含 shell 通配符，如 `*` 号，这种模式可扩展为 shell 通配模式。正如标准的 shell  

2. `README.md`

每个项目的跟目录下必须要有一个 `README.md` 文件（尤其是开源项目，你必须让使用你项目的人知道你的开源项目协议），
`README.md` 要以精简的方式向用户传递信息，帮助他们运行并配置你的程序。关于 readme 的写法参考放在的文末。

## 命令

```
local  | remote
                        
  add remote
    ------>           创建 
      clone
    <------           

      push
      ----->          更新
      pull
    <------
```

### `git ls-remote FOO.git` 显示一个远程版本库的引用列表

可以用来查看项目是否有更新，如果不加版本库的地址，则显示与本地版本对用的远程版本库的地址

eg:

``` bash
$ git ls-remote git@github.com:vuejs/vue.git
$(master/otherBranch) git ls-remote
```

### `git clone FOO.git` 从远程服务器克隆到本地

你可以为本地版本库起一个不同于远程的名字

eg:

``` bash
$ git clone git:aaaxxx:/projectName.git <newProjectName>
```

### 将本地已有的工作目录推送到远程版本库作为初始化的版本

``` bash
$ git init
$(master) git add .
$(master) git commit -m "init a project"
$(master) git remote add origin FOO.git
```

当你的项目完成初始化后，标志符 `origin` 就指向了远程版本库地址 `FOO.git`


### 关于分支

#### 新建远程分支

先在本地创建分支，再推送到远程版本库

eg:

``` bash
$(master) git checkout -b b1
$(b1) git push origin b1

$(b1) git branch -a

* b1
  master
  remotes/origin/HEAD -> origin/master
  remotes/origin/b1
  remotes/origin/master

```

#### 删除远程分支

eg:

``` bash
$(b1) git push origin --delete b1
$(b1) git branch -a

* b1
  master
  remotes/origin/HEAD -> origin/master
  remotes/origin/master
```

如果你只删除远程分支而不删除本地分支，那么本地的分支修改回保存到工作目录中。

``` bash
$(b1) git status
On branch b1
Changes not staged for commit:
  (use "git add <file>..." to update what will be committed)
  (use "git checkout -- <file>..." to discard changes in working directory)

        modified:   gitStep3_remote.md

no changes added to commit (use "git add" and/or "git commit -a")
```

[删除本地分支](./gitStep2_branch.md/#delete_branch)

#### 拉取本地不存在的远程分支
``` bash
$(master) git checkout -b b1
$(b1) git pull origin b1
```

### `git push` 将自己的版本库推送到服务器

``` bash
$ git push <远程主机名> <本地分支名>:<远程分支名>
```

* 如果省略 `:<远程分支名>`，则表示将本地分支推送与之存在"追踪关系"的远程分支（通常两者同名），如果该远程分支不存在，则会被新建。

  ``` bash
  $ git push origin master
  ```

* 如果省略本地分支名，则表示删除指定的远程分支，因为这等同于推送一个空的本地分支到远程分支。

  ``` bash
  $ git push origin :master
  # 等同于
  $ git push origin --delete master
  ```

* 如果当前分支与远程分支之间存在追踪关系，则本地分支和远程分支都可以省略。

  ``` bash
  $ git push origin
  ```

* 将所有分支推送到远程服务器

  ``` bash
  $ git push --all origin
  ```



### `git pull` 将远程版本库的某个分支拉到本地


## README 参考

``` markdown
# 标题

说明，一两句话就好，要确保清晰准确地表达项目精神。这影响这读者后续的阅读体验，如果你想做的更精致些，可以把项目logo放进去。下面会继续补充理解我们代码所必须得信息。比如：

## 安装说明 installation instructions

通常会准备一个 getting started 或者 installation 部分，在初始配置方面提供帮助。必要时引入示例代码

## 项目依赖 libraries

## 通常用法 common usage

## 现存缺陷 known bugs


我们在此重申没有官方规定 readme 应该怎样去写，不要可以将 readme 保持在某一长度，视具体情况而定，你可以写得很长，也可以极短。这取决于项目的具体情况。重要的是你清晰准确地向读者传递着必要信息，并**避免默认读者已具备相关的知识**。哪些必要信息是由你决定的。你可以先问自己几个问题，例如：

1. 将环境配置好并成功运行需要哪几步
2. 需要预先安装配置哪些东西
3. 哪些会是读者难以理解的部分

要注意你对用户所应具备的知识做出的相关假设，你已经对代码非常熟悉。要换到新手视角重新检视才行。
比如我们要做班尼迪蛋，需要荷包蛋做材料。不跟新初始解释荷包蛋是什么可能会让他不知所措

在文档中涵盖多少细节也由你决定。对于困难的部分，最好解释的尽量详尽

## License

引入版权或者证书信息，至少加入一条连接。[如何选择证书](https://choosealicense.com/)

## Contributing

通常情况你会希望他人可以向你的项目贡献代码，这时你一定要声明证书信息，你可以直接写在 readme 里，也可以放置一条指向它的连接.

秉承开源的精神，项目潜在的贡献者需要了解的内容请写到 readme 里，简述操作步骤，代码风格等信息。这会给他们带来诸多便利。

## Code Status

有一些类似代码检查的服务，能为你的 readme 引入名为 shields 的特殊标记, 这些工具由第三方提供。帮助用户了解有关的代码信息。如果你还不太明白，不要在意。我刚开始写文档的时候也为此感到困惑。认为自己必须引入这种东西。在你的代码包含了很多测试时。shields确实有所帮助，但最开始的时候可以忽略他们。


记住别让事情变得过分复杂，随着代码的增长去更新文档，只在必要的时候加入新的信息

```


## exLink

- gitignore
  * [自动生成 gitignore](https://www.gitignore.io/)
  * [How to use Git for Unity source control? - stackoverflow](https://stackoverflow.com/questions/18225126/how-to-use-git-for-unity-source-control)




