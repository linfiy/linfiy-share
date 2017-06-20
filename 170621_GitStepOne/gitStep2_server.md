# Git - Step Two

<img src="./images/banner.jpg" style="width: 100%">

## 分布式的优势

### 。svn 为什么每级都有？

## 命令

### 1. git ls-remote

显示一个给定的远程版本库的引用列表，这条命令简介回答了问题：是否有更新可用

```
From git@github.com:linfiy/linfiy-share.git
d431c4ab06529ae85db61a1945f8e1c01246fbaf	HEAD
8e2a6deca7d5e6b26e5f497411ea7eea605d1a55	refs/heads/learnGit
d431c4ab06529ae85db61a1945f8e1c01246fbaf	refs/heads/master
```

### 2. git show-ref

列出当前版本库的引用

```
8ee969d5a80a73d76c17bdb5cbe1312a086ebeff refs/heads/learnGit
d431c4ab06529ae85db61a1945f8e1c01246fbaf refs/heads/master
8e2a6deca7d5e6b26e5f497411ea7eea605d1a55 refs/remotes/origin/learnGit
d431c4ab06529ae85db61a1945f8e1c01246fbaf refs/remotes/origin/master
```

## 创建远程版本库

新建一个本地版本库，然后放到远程服务器上成为远程版本库

``` bash
$ git init
$ git remote add origin <server>
```
### git clone 

### git push

### git pull

### 