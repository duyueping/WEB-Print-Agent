# 关于：
> **WEB Print Agent** 是一个C#编写的C/S结构网页打印代理程序。

* 提供PC上直接调起打印机打印的能力
* 监听HTTP请求的31400端口，用来接收打印的JSON数据

# 目录结构：
**WindowsFormsPrint 源文件目录**

bin                    编译目录     
　　|- Debug               调试版本可执行程序生成目录       
　　|- Release             发行版本可执行程序生成目录      
obj                    用于存放编译过程中生成的中间临时文件      
　　|- Debug               调试版本编译过程中生成的中间临时文件      
　　|- Release             发行版本编译过程中生成的中间临时文件      
Properties             定义程序集的属性      

# 调试方法：

1.  下载源码，用宇宙第一IDE打开解决方案
2.  修改代码（当然不修改也可以直接运行o(*￣︶￣*)o  ）
3.  启动项目
4.  用浏览器打开demo.html
5.  点击submit按钮
6.  等待打印结果

# 使用方法：

1.  下载源码
2.  拷贝bin/Debug/目录下config.ini和WindowsFormsPrint.exe文件到任何文件夹下
3.   双击WindowsFormsPrint.exe运行
4.  用浏览器打开demo.html
5.  点击submit按钮
6.  等待打印结果

>bin/Debug/config.ini  程序配置文件 其中PrinterName=Microsoft XPS Document Writer 指定打印机名称    

-------------------
*© WEB Print Agent*