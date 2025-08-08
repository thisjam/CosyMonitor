using LibreHardwareMonitor.Hardware;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Timer = System.Windows.Forms.Timer;


namespace CosyMonitor
{
    public partial class ChildForm : Form
    {
        private Computer _computer;
        private Timer _timer;

    

        public ChildForm()
        {
        
            InitializeComponent();
       
            InitializeHardwareMonitor();
            SetupTimer();
        }

        private void ChildForm_Load(object sender, EventArgs e)
        {
            this.ShowInTaskbar = false;
            this.FormBorderStyle = FormBorderStyle.None;
            //this.BackColor = Color.Black;
            this.BackColor = Color.FromArgb(1, 1, 1);
            //this.TransparencyKey = Color.Black;

            //this.TransparencyKey = Color.FromArgb(1, 1, 1);


            this.TopMost = false;//窗体始终置顶显示

            


            // 初始化所有标签颜色
            SetLabelColors(Color.White);
        }
 


        private void SetLabelColors(Color color)
        {
            lbCpuTips.ForeColor = color;
            lbCpuTips.Font = new Font("微软雅黑", 9f, FontStyle.Regular, GraphicsUnit.Point);

            lbCpuM.ForeColor = color;
            lbCpuT.ForeColor = color;

            lbGpuTips.ForeColor = color;
            lbGpuTips.Font = new Font("微软雅黑", 9f, FontStyle.Regular, GraphicsUnit.Point);
            lbGpuM.ForeColor = color;
            lbGpuT.ForeColor = color;

            lbGpuRam.ForeColor = color;
            lbGpuRamTotal.ForeColor = color;

            lbMemoryTips.ForeColor = color;
            lbMemoryTips.Font = new Font("微软雅黑", 9f, FontStyle.Regular, GraphicsUnit.Point);
            lbMemoryM.ForeColor = color;
            lbMemoryT.ForeColor = color;
        }

        private void SetupTimer()
        {
            _timer = new Timer();
            _timer.Interval = 1000; // 每秒更新一次
            _timer.Tick += Timer_Tick;
            _timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            _computer.Accept(new UpdateVisitor());

            float? cpuLoad = null, cpuTemp = null;
            float? gpuLoad = null, gpuTemp = null;
            float? memoryLoad = null, memoryTemp = null;
            float? gpuMemoryUsed = null;
            float? gpuMemoryTotal = null;

            foreach (var hardware in _computer.Hardware)
            {
                switch (hardware.HardwareType)
                {
                    case HardwareType.Cpu:
                        foreach (var sensor in hardware.Sensors)
                        {
                            if (sensor.SensorType == SensorType.Load && sensor.Name == "CPU Total")
                                cpuLoad = sensor.Value;

                            if (sensor.SensorType == SensorType.Temperature)
                            {
                                string name = sensor.Name.ToLower();
                                if ((name.Contains("package") || name.Contains("cpu") ||
                                     name.Contains("die") || name.Contains("tctl") ||
                                     name.Contains("tdie") || name.Contains("core") ||
                                     name.Contains("socket")) &&
                                    sensor.Value.HasValue && sensor.Value > 0)
                                {
                                    cpuTemp = sensor.Value;
                                    break;
                                }
                            }
                        }
                        break;

                    case HardwareType.GpuNvidia:
                    case HardwareType.GpuIntel:
                    case HardwareType.GpuAmd:
                        foreach (var sensor in hardware.Sensors)
                        {
             
                            if (sensor.SensorType == SensorType.Load &&
                                (sensor.Name.Contains("GPU Core") || sensor.Name == "GPU"))
                                gpuLoad = sensor.Value;

                            if (sensor.SensorType == SensorType.Temperature &&
                                sensor.Name.Contains("GPU"))
                                gpuTemp = sensor.Value;

                            if (sensor.SensorType == SensorType.SmallData || sensor.SensorType == SensorType.Data)
                            {
                                if (sensor.Name.Contains("Memory Used", StringComparison.OrdinalIgnoreCase))
                                    gpuMemoryUsed = sensor.Value; // 单位：MB

                                if (sensor.Name.Contains("Memory Total", StringComparison.OrdinalIgnoreCase) ||
                                    sensor.Name.Contains("VRAM Size", StringComparison.OrdinalIgnoreCase))
                                    gpuMemoryTotal = sensor.Value; // 单位：MB
                            }
                        }
                        break;

                    case HardwareType.Memory:
                        foreach (var sensor in hardware.Sensors)
                        {


                            string name = sensor.Name;

                            // 包含物理内存关键词，且不包含虚拟内存等干扰词
                            bool isPhysicalMemory =
                                (name.Contains("Memory", StringComparison.OrdinalIgnoreCase) ||
                                 name.Contains("RAM", StringComparison.OrdinalIgnoreCase)) &&
                                !name.Contains("Virtual", StringComparison.OrdinalIgnoreCase) &&
                                !name.Contains("Page", StringComparison.OrdinalIgnoreCase) &&
                                !name.Contains("Swap", StringComparison.OrdinalIgnoreCase);

                            if (isPhysicalMemory)
                            {
                                memoryLoad = sensor.Value;
                            }

                            if (sensor.SensorType == SensorType.Temperature &&
                                sensor.Name.Contains("Memory", StringComparison.OrdinalIgnoreCase))
                                memoryTemp = sensor.Value;

                            //if(sensor.SensorType == SensorType.Temperature)
                            //{

                            //}

                        }
                        break;
                }
            }

            // 更新 UI（跨线程安全）
            if (this.InvokeRequired)
            {
                //this.Invoke(new Action(() => UpdateDisplay(cpuLoad, cpuTemp, gpuLoad, gpuTemp, memoryLoad, memoryTemp )));
                this.Invoke(new Action(() => UpdateDisplay(cpuLoad, cpuTemp, gpuLoad, gpuTemp, memoryLoad, memoryTemp, gpuMemoryUsed, gpuMemoryTotal)));
            }
            else
            {
                UpdateDisplay(cpuLoad, cpuTemp, gpuLoad, gpuTemp, memoryLoad, memoryTemp, gpuMemoryUsed, gpuMemoryTotal);
                //UpdateDisplay(cpuLoad, cpuTemp, gpuLoad, gpuTemp, memoryLoad, memoryTemp );
            }
        }


        /// <summary>
        /// 将字节数格式化为可读字符串（如 2.1 GB），支持 float 输入
        /// </summary>
        /// 
        private string FormatMemorySize(float megabytes)
        {
            if (megabytes <= 0) return "0.00";
            return $"{(megabytes / 1024.0):F2}";
        }

        //private string FormatMemorySize(float bytes)
        //{
        //    if (bytes <= 0) return "0 B";

        //    string[] units = { "B", "KB", "MB", "GB", "TB" };
        //    double size = bytes;  // float → double（安全）
        //    int unitIndex = 0;

        //    while (size >= 1024 && unitIndex < units.Length - 1)
        //    {
        //        size /= 1024;
        //        unitIndex++;
        //    }

        //    //return $"{size:F2} {units[unitIndex]}";
        //    return $"{size:F2}";
        //}

        private void UpdateDisplay(
            float? cpuLoad, float? cpuTemp,
            float? gpuLoad, float? gpuTemp,
            float? memoryLoad, float? memoryTemp,
            float? gpuMemoryUsed, float? gpuMemoryTotal
            )
        {
            // CPU
            lbCpuM.Text = $"{cpuLoad?.ToString("F1") ?? "N/A"}%";
            lbCpuT.Text = $"{cpuTemp?.ToString("F1") ?? "N/A"}°C";
            SetColor(lbCpuM, cpuLoad, 80);
            SetColor(lbCpuT, cpuTemp, 80);

            // GPU
            lbGpuM.Text = $"{gpuLoad?.ToString("F1") ?? "N/A"}%";
            lbGpuT.Text = $"{gpuTemp?.ToString("F1") ?? "N/A"}°C";
      
          
            SetColor(lbGpuM, gpuLoad, 80);
            SetColor(lbGpuT, gpuTemp, 80);

            // Memory
            lbMemoryM.Text = $"{memoryLoad?.ToString("F1") ?? "N/A"}%";
            lbMemoryT.Text = $"{memoryTemp?.ToString("F1") ?? "N/A"}°C";
            SetColor(lbMemoryM, memoryLoad, 80);
            SetColor(lbMemoryT, memoryTemp, 80);

            // === 新增：GPU 显存显示 ===原始数据单位是MB
            if (gpuMemoryTotal.HasValue)
            {
                string total = FormatMemorySize(gpuMemoryTotal.Value); // 转为字节再格式化
                //string total = FormatMemorySize(gpuMemoryTotal.Value * 1024 * 1024); // 转为字节再格式化
               
                lbGpuRamTotal.Text = $"/ {total}GB";

                if (gpuMemoryUsed.HasValue)
                {
                    //string used = FormatMemorySize(gpuMemoryUsed.Value * 1024 * 1024);
                    string used = FormatMemorySize(gpuMemoryUsed.Value);
                    lbGpuRam.Text = used;
                }
                else
                {
                    lbGpuRam.Text = $"?";
                }
            }
            else
            {
                lbGpuRam.Text = "?";
            }

            // 可选：根据使用率变色
            if (gpuMemoryUsed.HasValue && gpuMemoryTotal.HasValue)
            {
                float usagePercent = (gpuMemoryUsed.Value / gpuMemoryTotal.Value) * 100;
                if (usagePercent >= 80)
                {
                    lbGpuRam.ForeColor = Color.Red;
                }
                else
                {
                    lbGpuRam.ForeColor = Color.White;
                }
         
            }

          

        }

        private void SetColor(Label label, float? value, float threshold)
        {
            if (value.HasValue && value >= threshold)
                label.ForeColor = Color.Red;
            else
                label.ForeColor = Color.LimeGreen; // 比纯绿更亮，适合黑背景
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            _timer?.Stop();
            _timer?.Dispose();
            _computer?.Close();
            base.OnFormClosing(e);
        }

        private void InitializeHardwareMonitor()
        {
            _computer = new Computer
            {
                IsCpuEnabled = true,
                IsGpuEnabled = true,
                IsMemoryEnabled = true
            };
            _computer.Open();
        }
    }

    // 必须定义一个 Visitor 来更新传感器数据
    public class UpdateVisitor : IVisitor
    {
        public void VisitComputer(IComputer computer)
        {
            computer.Traverse(this);
        }

        public void VisitHardware(IHardware hardware)
        {
            hardware.Update();
            foreach (IHardware subHardware in hardware.SubHardware)
                subHardware.Accept(this);
        }

        public void VisitSensor(ISensor sensor) { }
        public void VisitParameter(IParameter parameter) { }
    }
}