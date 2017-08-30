# 关于：
> **WEB Print Agent** 是一个C/S结构的B/S打印代理程序。

* 提供PC上直接调起打印机打印的能力
* 监听HTTP请求的31400端口，用来接收打印的JSON接口

# 目录结构：
**WindowsFormsPrint 源文件目录**

bin                 编译目录 
　　|- Debug         调试版本可执行程序生成目录  
		|- config.ini 程序配置文件 其中PrinterName=Microsoft XPS Document Writer 指定打印机名称
　　|- Release       发行版本可执行程序生成目录   
obj                 用于存放编译过程中生成的中间临时文件  
　　|- Debug         调试版本编译过程中生成的中间临时文件
　　|- Release       发行版本编译过程中生成的中间临时文件
Properties          定义程序集的属性


**test PHPUnit目录**

打印机配置文件

-------------------
*© WEB Print Agen*