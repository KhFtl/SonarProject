using System.ComponentModel;

namespace SonarProject;

partial class Settings
{
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private IContainer components = null;

    private System.Windows.Forms.Label labelPort;
    private System.Windows.Forms.ComboBox comboBoxPorts;
    private System.Windows.Forms.Button buttonRefresh;
    private System.Windows.Forms.Label labelBaudRate;
    private System.Windows.Forms.ComboBox comboBoxBaudRate;
    private System.Windows.Forms.Button buttonStart;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }

        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        this.components = new System.ComponentModel.Container();
        this.labelPort = new System.Windows.Forms.Label();
        this.comboBoxPorts = new System.Windows.Forms.ComboBox();
        this.buttonRefresh = new System.Windows.Forms.Button();
        this.labelBaudRate = new System.Windows.Forms.Label();
        this.comboBoxBaudRate = new System.Windows.Forms.ComboBox();
        this.buttonStart = new System.Windows.Forms.Button();
        this.SuspendLayout();
        // 
        // labelPort
        // 
        this.labelPort.AutoSize = true;
        this.labelPort.Location = new System.Drawing.Point(30, 30);
        this.labelPort.Name = "labelPort";
        this.labelPort.Size = new System.Drawing.Size(63, 15);
        this.labelPort.TabIndex = 0;
        this.labelPort.Text = "COM порт:";
        // 
        // comboBoxPorts
        // 
        this.comboBoxPorts.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        this.comboBoxPorts.FormattingEnabled = true;
        this.comboBoxPorts.Location = new System.Drawing.Point(30, 50);
        this.comboBoxPorts.Name = "comboBoxPorts";
        this.comboBoxPorts.Size = new System.Drawing.Size(180, 23);
        this.comboBoxPorts.TabIndex = 1;
        // 
        // buttonRefresh
        // 
        this.buttonRefresh.Location = new System.Drawing.Point(220, 50);
        this.buttonRefresh.Name = "buttonRefresh";
        this.buttonRefresh.Size = new System.Drawing.Size(90, 24);
        this.buttonRefresh.TabIndex = 2;
        this.buttonRefresh.Text = "Оновити";
        this.buttonRefresh.UseVisualStyleBackColor = true;
        this.buttonRefresh.Click += new System.EventHandler(this.buttonRefresh_Click);
        // 
        // labelBaudRate
        // 
        this.labelBaudRate.AutoSize = true;
        this.labelBaudRate.Location = new System.Drawing.Point(30, 90);
        this.labelBaudRate.Name = "labelBaudRate";
        this.labelBaudRate.Size = new System.Drawing.Size(117, 15);
        this.labelBaudRate.TabIndex = 3;
        this.labelBaudRate.Text = "Швидкість (Baud Rate):";
        // 
        // comboBoxBaudRate
        // 
        this.comboBoxBaudRate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        this.comboBoxBaudRate.FormattingEnabled = true;
        this.comboBoxBaudRate.Items.AddRange(new object[] {
            "4800",
            "9600",
            "19200",
            "38400",
            "57600",
            "115200"});
        this.comboBoxBaudRate.Location = new System.Drawing.Point(30, 110);
        this.comboBoxBaudRate.Name = "comboBoxBaudRate";
        this.comboBoxBaudRate.Size = new System.Drawing.Size(180, 23);
        this.comboBoxBaudRate.TabIndex = 4;
        // 
        // buttonStart
        // 
        this.buttonStart.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
        this.buttonStart.Location = new System.Drawing.Point(30, 160);
        this.buttonStart.Name = "buttonStart";
        this.buttonStart.Size = new System.Drawing.Size(280, 36);
        this.buttonStart.TabIndex = 5;
        this.buttonStart.Text = "Запустити радар";
        this.buttonStart.UseVisualStyleBackColor = true;
        this.buttonStart.Click += new System.EventHandler(this.buttonStart_Click);
        // 
        // Settings
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(344, 221);
        this.Controls.Add(this.buttonStart);
        this.Controls.Add(this.comboBoxBaudRate);
        this.Controls.Add(this.labelBaudRate);
        this.Controls.Add(this.buttonRefresh);
        this.Controls.Add(this.comboBoxPorts);
        this.Controls.Add(this.labelPort);
        this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
        this.MaximizeBox = false;
        this.MinimizeBox = false;
        this.Name = "Settings";
        this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        this.Text = "Налаштування COM-порту";
        this.ResumeLayout(false);
        this.PerformLayout();
    }

    #endregion
}