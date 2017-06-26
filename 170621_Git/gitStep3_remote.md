# Git - Step 2

> 开始工作

<img src="./images/banner.jpg" style="width: 100%">

## SSH Key

### 私钥和公钥

你可以拷贝它到你用的机器上，然后

### 多个 SSH 的管理

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

## `git clone <URI>` 从远程服务器克隆到本地

你可以为本地版本库起一个不同于远程的名字

eg:

``` bash
$ git clone git:aaaxxx:/projectName.git <newProjectName>
```

## 将本地已有的工作目录推送到远程版本库作为初始化的版本

``` bash
git init
git add .
git commit -m "init a project"
git add remote <rotate>
```

### `git push` 将自己的版本库推送到服务器

### `git pull` 将远程版本库的某个分支拉到本地


### 新建远程分支
### 拉取本地不存在的远程分支
### `git push --delete <branchName>` 删除远程分支



