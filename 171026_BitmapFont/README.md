# Bitmap字体（图片字体）

- 工具: [Font Setter Packer](http://u3d.as/4bZ) $9

- 参考文章: [独立开发者必备技能 － 为Unity游戏制作Bitmap字体（图片字体）](http://www.jianshu.com/p/6bdd41a1bcfd)
- Unity 版本 5.4.3f1

## 使用步骤

1. 导入 Font Setter Packer。 
    - _PS_: 项目里不导入包只是使用打包后的图片字体的话，在手机上可能显示不出来，需要修改材质球的Shader. 二选一
2. 图片修改 Inspector 
    - Texture Type => `Advanced`
    - Read/Write Enabled => `true`
    - Alpha is Transparent => `true`
    - Default/Format => `Automatic Truecolor`
    - 点击 Apply 保存
    - _PS_: **原图片(png)**的大小一定要是 2^n。否则会在下一步弹出报错 `Error: Font Map is NPOT, Change the import Settings property for NPOT map to 'None'`。
3. 选择任务栏 `Window` 下的 `Font Editor`。
  - (1)在弹出窗口的右侧选框（New Font From Image[Texture2D]）中选择刚才修改过的图片。
  - (2)或者点选 `file - creatte font from image`

4. 选取文字对应的图片，两种方法
    1. 自动选取：Tools/Auto-set gylphs。每次自动选择后，键入 Character 中需要的文字，点击 Next，如果出现不需要的文字（或杂质），可以点击 Skip 跳过。点击 Stop 结束。
    2. 手动选取：点击右侧 `+1`，调整选取大小并设置 Character。
    - _PS_: 图片文字整齐的字体适合用手动选取，图片文字凌乱的适合用自动选取。
    - _PS_: 框选的字体需要设置锚点，勾选 Glyph Pivot。 锚点决定 Text 组件该从哪个地方渲染这个字体。锚点不对齐文字就很不整齐。

5. 选取完之后，点击 `Tools/Pack Font Map` 进行设置。再点击 `Pack` 进行打包。最后点击 `Accept Pack Result`。
    - 生成的文件包括一个 .fontsetting，一个 .mat 和一个 .png。
    - .fontsetting 的 Inspector 面板中可以设置行距以及字间距。注意改名时不要让其引用的材质丢失
    - _PS_: 点击 Pack 之后就已经进行打包了，此时文件已经写入当前文件夹。字体文件就可以使用了。最后的 `Accept Pack Result` 按钮有些情况点击会报错，不过不影响使用。
    - _PS_: Pack 会生成一些中间文件，可以删掉。最后只保留名字最后带`(Packed)` 的文件就可以了
    - _PS_: 如果字体呈现黑色，则需要设置 UnityEngine.UI.Text 组件上的 Color 为白色
    


