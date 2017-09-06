namespace WindowsFormsPrint
{
    partial class Form
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form));
            this.printDocument1 = new System.Drawing.Printing.PrintDocument();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.traymenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.洛佩塔系統打印服務ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.退出ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.traymenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // printDocument1
            // 
            this.printDocument1.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.printDocument1_PrintPage);
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.ContextMenuStrip = this.traymenu;
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Visible = true;
            // 
            // traymenu
            // 
            this.traymenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.洛佩塔系統打印服務ToolStripMenuItem,
            this.退出ToolStripMenuItem});
            this.traymenu.Name = "traymenu";
            this.traymenu.Size = new System.Drawing.Size(185, 48);
            // 
            // 洛佩塔系統打印服務ToolStripMenuItem
            // 
            this.洛佩塔系統打印服務ToolStripMenuItem.Name = "洛佩塔系統打印服務ToolStripMenuItem";
            this.洛佩塔系統打印服務ToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.洛佩塔系統打印服務ToolStripMenuItem.Text = "洛佩塔系統打印服務";
            // 
            // 退出ToolStripMenuItem
            // 
            this.退出ToolStripMenuItem.Name = "退出ToolStripMenuItem";
            this.退出ToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.退出ToolStripMenuItem.Text = "退出";
            this.退出ToolStripMenuItem.Click += new System.EventHandler(this.退出ToolStripMenuItem_Click);

            // 
            // Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDark;
            this.ClientSize = new System.Drawing.Size(129, 0);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Form";
            this.ShowInTaskbar = false;
            this.Text = "洛佩塔系統打印服務";
            this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
            this.traymenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Drawing.Printing.PrintDocument printDocument1;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.ContextMenuStrip traymenu;
        private System.Windows.Forms.ToolStripMenuItem 退出ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 洛佩塔系統打印服務ToolStripMenuItem;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
    }
}

