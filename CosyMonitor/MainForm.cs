using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CosyMonitor
{
    public partial class MainForm : Form
    {
        private ChildForm childForm;
        private NotifyIcon notifyIcon;
        private ContextMenuStrip contextMenu;

        public MainForm()
        {
            InitializeComponent();
            InitializeTray();
            InitForm();
            InitPos();
            initChild();

        }


        private void ExitApplication()
        {
            // 隐藏托盘图标
            notifyIcon.Visible = false;
            notifyIcon.Dispose();

            // 关闭所有窗体
            if (childForm != null && !childForm.IsDisposed)
            {
                childForm.Close();
            }

            // 退出应用程序
            Application.Exit();
        }

        // 假设你添加的资源名为：appicon（类型为“File”或“WPF Binary”）
        private Icon LoadIconFromResource()
        {
            try
            {
                byte[] iconData = Resource1.cosyMoniter; // 返回 byte[]
                using (var ms = new MemoryStream(iconData))
                {
                    return new Icon(ms);
                }
            }
            catch
            {
                return SystemIcons.Application; // 备用图标
            }
        }

        private void InitializeTray()
        {
            // 创建右键菜单
            contextMenu = new ContextMenuStrip();
            var exitItem = new ToolStripMenuItem("退出");
            exitItem.Click += (s, e) => ExitApplication();   
            contextMenu.Items.Add(exitItem);
          
           
            // 创建托盘图标
            notifyIcon = new NotifyIcon
            {


                Icon = LoadIconFromResource(),
                //Icon = SystemIcons.Application, // 使用你的图标
                //Icon = Resource1.cosyMoniter ?? SystemIcons.Application,


                Text = "硬件监控", // 鼠标悬停提示
                Visible = true,   // 显示在托盘
                ContextMenuStrip = contextMenu
            };

            //// 双击托盘图标显示子窗体
            //notifyIcon.MouseDoubleClick += (s, e) =>
            //{
            //    if (e.Button == MouseButtons.Left)
            //    {
            //        ShowChildForm();
            //    }
            //};
        }

        private void UpdateChildFormLocation()
        {
            if (childForm != null && !childForm.IsDisposed)
            {
                childForm.Location = this.Location;
            }
        }

        private void UpdateChildFormSize()
        {
            if (childForm != null && !childForm.IsDisposed)
            {
                childForm.Size = this.Size;
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            // 确保子窗体也被关闭
            if (childForm != null && !childForm.IsDisposed)
            {
                childForm.Close();
            }
        }


        void initChild()
        {
            // 创建并显示子窗体
            childForm = new ChildForm();
            childForm.Show();

            // 同步初始位置
            UpdateChildFormLocation();

            // 订阅位置改变事件
            this.LocationChanged += (sender, e) => UpdateChildFormLocation();
            // 订阅大小改变事件（如果需要同步大小）
            this.SizeChanged += (sender, e) => UpdateChildFormSize();
        }

    

        void InitPos()
        {
            // 获取屏幕工作区（排除任务栏）的尺寸
            Rectangle workingArea = Screen.PrimaryScreen.WorkingArea;

            // 计算右下角位置：窗口右上角对齐屏幕右下角，留出20px边距
            int x = workingArea.Right - this.Width - 20;  // 右边界减去窗口宽度和边距
            int y = workingArea.Bottom - this.Height - 20; // 下边界减去窗口高度和边距

            // 设置初始位置和大小
            StartPosition = FormStartPosition.Manual;
            Location = new Point(x, y);
            Size = new Size(500, 50);
            //Size = new Size(500, 500);
            this.MouseDown += (s, e) => { if (e.Button == MouseButtons.Left) { this.Capture = false; Message m = Message.Create(this.Handle, 0XA1, new IntPtr(2), IntPtr.Zero); this.WndProc(ref m); } };


        }



        private void InitForm()
        {
            this.ShowInTaskbar = false;
            // 设置悬浮窗体的样式...
            this.FormBorderStyle = FormBorderStyle.None;
            this.TopMost = false;//窗体始终置顶显示
            this.BackColor = Color.Black;
            this.Opacity = 0.2;

        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }
    }

}

