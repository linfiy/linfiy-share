# <font color=MidnightBlue>C# Socket的简单应用</font>

## <font color=RoyalBlue>Socket的定义</font>
  socket英文的含义为插座、孔，在我们的网络应用中通常称为套接字，大致理解为在tcp/ip网络抽象层中使用套接字ip+端口的网络通信协议,可认为是介于传输层与应用层中抽象出的socket层，我们可以使用它的接口来解决复杂的网络请求。
 ![GitHub](http://images.cnitblog.com/blog/349217/201312/05225723-2ffa89aad91f46099afa530ef8660b20.jpg)
## <font color=RoyalBlue>Tcp/IP </font>

  ![GitHub](http://images.cnitblog.com/blog/349217/201312/05230830-04807bb739954461a8bfc7513707f253.jpg)


**<font style="font-weight:bold;">定义： </font>
  Transmission Control Protocol/Internet Protocol的简写，中译名为传输控制协议/因特网互联协议，又名网络通讯协议，是Internet最基本的协议、Internet国际互联网络的基础，由网络层的IP协议和传输层的TCP协议组成。**

<font style="font-weight:bold;">组成： </font>
  由四部分組成，从低至高分別为链路层、网络层、传输层和应用层  
* 链路层：数据链路层是负责接收IP数据包并通过网络发送，或者从网络上接收物理帧，抽出IP数据包，交给IP层。主要表现为物理驱动，使用物理的方式进行数据交互。

* 网络层：负责相邻计算机之间的通信。确定网络地址IP，对网络链接状况获取相关数据。

* 传输层：提供应用程序间的通信。使用 tcp 或 udp 形式对主机进行网络请求，客户端与服务端使用流各自操作本地数据文本，以代理的形式实现通信。

* 应用层：向用户提供一组常用的应用程序，主机上对网络获取的数据进行交互和效果展示。

## <font color=RoyalBlue>Socket通信过程</font>
![GitHub](http://images.cnitblog.com/blog/349217/201312/05232335-fb19fc7527e944d4845ef40831da4ec2.png)

### 名词解析：

* IP：网络IP地址，网络进程唯一标识符。

* 端口：应用进程中用来数据交互的接口，每个应用都有唯一的端口标识符。

* IPv4，是互联网协议（Internet Protocol，IP）的第四版，也是第一个被广泛使用，构成现今互联网技术的基石的协议。1981年Jon Postel 在RFC791中定义了IP，Ipv4可以运行在各种各样的底层网络上，比如端对端的串行数据链路（PPP协议和SLIP协议) ，卫星链路等等。局域网中最常用的是以太网。  
IPv4中规定IP地址长度为32，即有2^32-1（符号^表示升幂，下同）个地址

* IPv6：
IPv6是Internet Protocol Version 6的缩写，其中Internet Protocol译为“互联网协议”。IPv6是IETF（互联网工程任务组，Internet Engineering Task Force）设计的用于替代现行版本IP协议（IPv4）的下一代IP协议。  
IPv6中IP地址的长度为128，即有2^128-1个地址。  
IPv6具有更高的安全性。在使用IPv6网络中用户可以对网络层的数据进行加密并对IP报文进行校验，极大的增强了网络的安全性。

## <font color=RoyalBlue>代码实现</font>

* 初始化: 根据地址和端口创建socket资源
``` csharp
    string[] handled = tcp.Split(new Char[]{':'});
		string host = handled[0];
		int hostPort = handled.Length > 1 ? int.Parse(handled[1]) : 80;
    //地址封装
		IPEndPoint port = new IPEndPoint (Dns.GetHostAddresses(handled[0])[0], hostPort);		
    //创建socket资源，新实例初始化 Socket 类使用指定的地址族、 套接字类型和协议。	
		socket = new Socket (
			AddressFamily.InterNetwork,
			System.Net.Sockets.SocketType.Stream,
			ProtocolType.Tcp
		);
```
* 请求连接： 异步向服务器发送连接请求,连接成功向服务器发送心跳包(心跳：服务器与客户端进行通信，客户端实现双向保活),socket进行异步挂载，准备接收数据
``` csharp

    ...
    //连接服务器
    IAsyncResult result = socket.BeginConnect (
			port, ConnectCallBack, socket
		);
    //5s内判断是否连接成功
    bool isSucced = result.AsyncWaitHandle.WaitOne (5000, true);

    ...
    void ConnectCallBack(IAsyncResult result){
			Socket socket = (Socket) result.AsyncState;
      //异步挂载，等待数据接收
			socket.BeginReceive (receiveData, 0, receiveData.Length, SocketFlags.None, new AsyncCallback (RecivieCallBack), socket);
		}
```

* 接收数据：客服端开启异步处理，开启挂载对服务器的数据进行监听，接收到数据进行拆包处理，发送给各个应用模块。之后再将socket进行异步挂载，准备接收数据
``` csharp
  void ConnectCallBack(IAsyncResult result){
		Socket socket = (Socket) result.AsyncState;
		socket.BeginReceive (receiveData, 0, receiveData.Length, SocketFlags.None, new AsyncCallback (RecivieCallBack), socket);
	}

  private void RecivieCallBack(IAsyncResult result){
		Socket socket = (Socket)result.AsyncState;
		//接收长度
		int length = socket.EndReceive (result);
		if(length <= 0){
			Console.Log(LogType.SOCKET, "server does not return data, but not disconnect");
			return;
		}
    //保持数据同步，对二进制字节拆包处理
		lock(receiveData){
			SplitePackage(receiveData,length);
		}
		try{
			if(respsonList.Count>0){
				respsonList.ForEach(packet => {
					sub.OnNext(packet);
				});
				respsonList.Clear ();
			}
			socket.BeginReceive (receiveData,0, receiveData.Length, SocketFlags.None, new AsyncCallback (RecivieCallBack), socket);
			}
			catch(Exception e){
				//Debug.LogError (e.ToString());
		}
```

* 拆包：将从服务器接收的二进制数据进行拆包处理,根据包头获取数据长度，对二进制数据进行转化(* 处理粘包->使用循环，轮询处理 断包->将断包处保存再memoryStream中，再次接收数据的时候将包重新拼接)
``` csharp
  private void SaveBreak(byte[] data,int index){
			streamLen = data.Length - index;
			byte[] breakData = new byte[streamLen];
			Array.Copy (data,index,breakData,0,breakData.Length);
			stream.SetLength (streamLen);
			stream.Write (breakData, 0, breakData.Length);
			isBreakPacket = true;
	}

  private void EndBreak(){
	  if (stream != null) {
      stream.Close ();
      stream = new MemoryStream ();
      streamLen = 0;
      isBreakPacket = false;
		}
  }

  //拆包
  public void SplitePackage(byte[] data ,int bufferLen){
    //包头位置
    int index = 0;
    //断包判断
		byte[] bytes = new byte[bufferLen];
		Array.Copy (data,bytes,bufferLen);
		//处于断包状态
		if(isBreakPacket){
			byte[] packetData = stream.ToArray ();
			byte[] newData = new byte[streamLen + bufferLen];
			Array.Copy (packetData, 0, newData, 0, streamLen);
			index += streamLen;
			Array.Copy (bytes, 0, newData, index, bufferLen);
			bytes = newData;
			index=0;
			EndBreak ();
		}
		//循环拆包（粘包处理）
		while(index<bytes.Length){
			//断包头
			if((index+4)>=bytes.Length){
				SaveBreak (bytes, index);
				return;
			}
			//拆包头
			byte[] head =new byte[4];
			Array.Copy (bytes,index,head,0,4);
			index += 4;
			Array.Reverse(head);
			int count = BitConverter.ToInt32 (head,0);
			//异常处理
			if(count>8192){
				EndBreak ();
				HandleError ("包头过长");
				return;
			}
			//断内容处理
			if((index+count)>bytes.Length){
				index -= 4;
				SaveBreak (bytes,index);
				return;
			}
			//正常包处理
			byte[] callbackData =new byte[count];
			Array.Copy(bytes,index,callbackData,0,count);
			string iContent =Encoding.UTF8.GetString (callbackData);
			if(count==0){
				EndBreak();
				HandleError ("接收数据为空");
			}
			respsonList.Add (iContent);
			index += callbackData.Length;
		}
	}
```

* 发送请求：客户端将发送的数据转化成二进制字节，进行装包处理发送给服务器
``` csharp

  public void SendMessage(string str)
	{
		if(socket!=null&&!socket.Connected) {
			EndBreak();
			HandleError("socket is not connecting || socket is not existed");
			return;
		}
		byte[] msg = Encoding.UTF8.GetBytes(str);
		int mesgLength = msg.Length;
		byte[] head = BitConverter.GetBytes (mesgLength);
		byte[] sendData = new byte[mesgLength+head.Length];
		Array.Reverse (head);//服务器与客户端的包头的解析顺序是相反的
		Array.Copy (head, sendData, head.Length);
		Array.Copy (msg,0,sendData,head.Length,msg.Length);
		try {
			IAsyncResult asyncSend = socket.BeginSend (sendData,0,sendData.Length,SocketFlags.None,new AsyncCallback (sendCallback),socket);
			bool success = asyncSend.AsyncWaitHandle.WaitOne( 7000, true );
			if (!success) {
				throw new ApplicationException();
			}
			else {
        Console.Log (LogType.SOCKET_SEND, str);
			}
		}
		catch {
			Console.Log(LogType.SOCKET, "send message exceptionally");
		}
	}
  //请求成功
	private void sendCallback (IAsyncResult asyncSend)
	{
          ...
	}

```


* 关闭：断开连接,关闭socket资源
``` csharp
  public void Closed() {
		try{
			if(socket != null && socket.Connected) {
        //先停止接收和发送的服务
				socket.Shutdown(SocketShutdown.Both);
        //关闭socket资源
				socket.Close();
				if(disposable!=null){
					disposable.Dispose();
				}
					socket = null;
			}
		}
    //必须加上，客户端使用轮询处理断线重连时候有可能多次调用，异步导致报错使程序直接卡死
		catch{

		}
	}

```

## <font color=RoyalBlue>常见问题</font>
* 数据同步： unity引擎中MonBehaviour只能处理主线程的数据，而我们socket请求数据是异步请求的
* 拆包：当出现粘包、断包
* 重连：应用保活与断线重连
* 异常处理：socket异步访问异常导致程序卡死

## <font color=RoyalBlue>优化改进</font>
* buffer:创建缓冲类，将每次的数据进行管理
* 创建包体实体类，对游戏数据进行封装

## <font color=RoyalBlue>参考链接</font>
* 百度TCP/IP协议:
[百度](http://baike.baidu.com/item/TCP%2FIP%E5%8D%8F%E8%AE%AE?fromId=7729#3_3)
* 百度Socket：
[百度](http://baike.baidu.com/item/socket/281150)
* Socket_API：
[微软官网](https://msdn.microsoft.com/zh-cn/library/system.net.sockets.socket(v=vs.110).aspx)
* Socket拆包处理：
[Socket/TCP粘包、多包和少包, 断包](http://blog.csdn.net/pi9nc/article/details/17165171)