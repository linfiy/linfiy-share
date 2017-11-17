## FXMaker
### 简介
    FXMaker是一款Unity特效制作插件。不但有超过300种效果预制体，还可以自己制作效果。包含Mesh Effect 和Particle Effect。  
    优点：资源库大，可以将消耗资源非常多的粒子效果转换为帧动画效果。当然也可以直接用不转帧动画的效果，这种效果是画面最好的，但是对显卡开销大。帧动画显卡开销小，占内存大，适合移动平台。  
### 使用步骤
    见FXMaker使用手册   
### 特效转换
* 将消耗资源非常多的粒子效果转换为帧动画效果。   
1. 选中一个粒子特效   
![1](PICTURE/1.JPG)
2. 右键点击UI界面中此物体，选择save prefab   
![2](PICTURE/2.PNG)
3. 选择要保存的位置，点击save   
![3](PICTURE/3.PNG)
4. 到对应文件夹找到保存的项目，点击右键，选择BuildSprite    
![4](PICTURE/4.PNG)
5. 通过鼠标滚轮放大缩小使特效完全位于红色方框以内    
![5](PICTURE/5.PNG)
6. 调整相应参数后，点击Build Sprite创建帧动画   
![6](PICTURE/6.PNG)
7. 创建成功，新特效位于原特效文件夹    
![7](PICTURE/7.PNG)
8. 右键选择Export，导出特效，会生成一个UnityPackage文件，导入到项目中就可以使用了    
![8](PICTURE/8.PNG)
### 提示
1. 刚导入的项目没有1 Project文件夹，大家自己新创建一个就可以了     
![9](PICTURE/9.PNG)   
![10](PICTURE/10.PNG)    
2. Build Sprite时会报错，因为项目中没有USerSprite文件夹，需要新创建一个    
![11](PICTURE/11.PNG)    