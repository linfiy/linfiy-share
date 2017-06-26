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


创建三要素

1. `.gitignore`

2. `README.md`

3. 

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

## `git ls-remote FOO.git` 显示一个远程版本库的引用列表

可以用来查看项目是否有更新，如果不加版本库的地址，则显示与本地版本对用的远程版本库的地址

eg:

``` bash
$ git ls-remote git@github.com:vuejs/vue.git
$(master/otherBranch) git ls-remote
```

## `git clone FOO.git` 从远程服务器克隆到本地

你可以为本地版本库起一个不同于远程的名字

eg:

``` bash
$ git clone git:aaaxxx:/projectName.git <newProjectName>
```

## 将本地已有的工作目录推送到远程版本库作为初始化的版本

``` bash
$ git init
$(master) git add .
$(master) git commit -m "init a project"
$(master) git remote add origin FOO.git
```

### `git push` 将自己的版本库推送到服务器

### `git pull` 将远程版本库的某个分支拉到本地


### 关于分支

#### 新建远程分支

先在本地创建分支，再推送到远程版本库

eg:

``` bash
$(master) git checkout b1
$(b1) git push origin b1
```

#### 删除远程分支


### 新建远程分支
### 拉取本地不存在的远程分支
### `git push --delete <branchName>` 删除远程分支



