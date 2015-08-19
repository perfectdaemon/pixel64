namespace SharpPixel.Engine
{
    partial class MainForm
    {
        /// <summary>
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.mainTimer = new System.Windows.Forms.Timer(this.components);
            this.renderSurface = new SharpPixel.Engine.RenderSurface();
            this.SuspendLayout();
            // 
            // mainTimer
            // 
            this.mainTimer.Enabled = true;
            this.mainTimer.Interval = 30;
            this.mainTimer.Tick += new System.EventHandler(this.mainTimer_Tick);
            // 
            // renderSurface
            // 
            this.renderSurface.Dock = System.Windows.Forms.DockStyle.Fill;
            this.renderSurface.Location = new System.Drawing.Point(0, 0);
            this.renderSurface.Name = "renderSurface";
            this.renderSurface.Size = new System.Drawing.Size(563, 321);
            this.renderSurface.TabIndex = 0;
            this.renderSurface.TabStop = false;
            this.renderSurface.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MainForm_KeyDown);
            this.renderSurface.KeyUp += new System.Windows.Forms.KeyEventHandler(this.MainForm_KeyUp);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(563, 321);
            this.Controls.Add(this.renderSurface);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Special for igdc#125 by perfect.daemon";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MainForm_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.MainForm_KeyUp);
            this.ResumeLayout(false);

        }

        #endregion

        private RenderSurface renderSurface;
        private System.Windows.Forms.Timer mainTimer;

    }
}

