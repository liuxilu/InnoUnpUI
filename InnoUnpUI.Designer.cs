namespace InnoUnpUI {
    partial class InnoUnpUI {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            this.treeViewMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.本层全选MenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.本层反选MenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.本层选文件MenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.下层全选MenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.下层反选MenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.下层选文件MenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.statusLabelTitle = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusLabelContent = new System.Windows.Forms.ToolStripStatusLabel();
            this.panelControls = new System.Windows.Forms.Panel();
            this.chkShowProg = new System.Windows.Forms.CheckBox();
            this.panelTxt = new System.Windows.Forms.Panel();
            this.txtExecPath = new System.Windows.Forms.TextBox();
            this.txtFilePath = new System.Windows.Forms.TextBox();
            this.txtTargetPath = new System.Windows.Forms.TextBox();
            this.btnExtract = new System.Windows.Forms.Button();
            this.panelLabel = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.panelBtn = new System.Windows.Forms.Panel();
            this.btnOpenExec = new System.Windows.Forms.Button();
            this.btnOpenFile = new System.Windows.Forms.Button();
            this.btnOpenTarget = new System.Windows.Forms.Button();
            this.btnLoadFile = new System.Windows.Forms.Button();
            this.splitViews = new System.Windows.Forms.SplitContainer();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.treeViewMenu.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.panelControls.SuspendLayout();
            this.panelTxt.SuspendLayout();
            this.panelLabel.SuspendLayout();
            this.panelBtn.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitViews)).BeginInit();
            this.splitViews.Panel1.SuspendLayout();
            this.splitViews.Panel2.SuspendLayout();
            this.splitViews.SuspendLayout();
            this.SuspendLayout();
            // 
            // treeViewMenu
            // 
            this.treeViewMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.本层全选MenuItem,
            this.本层反选MenuItem,
            this.本层选文件MenuItem,
            this.menuItemSeparator1,
            this.下层全选MenuItem,
            this.下层反选MenuItem,
            this.下层选文件MenuItem});
            this.treeViewMenu.Name = "contextMenuStrip1";
            this.treeViewMenu.Size = new System.Drawing.Size(171, 178);
            // 
            // 本层全选MenuItem
            // 
            this.本层全选MenuItem.Name = "本层全选MenuItem";
            this.本层全选MenuItem.Size = new System.Drawing.Size(170, 28);
            this.本层全选MenuItem.Text = "本层全选";
            this.本层全选MenuItem.Click += new System.EventHandler(this.本层选MenuItem_Click);
            // 
            // 本层反选MenuItem
            // 
            this.本层反选MenuItem.Name = "本层反选MenuItem";
            this.本层反选MenuItem.Size = new System.Drawing.Size(170, 28);
            this.本层反选MenuItem.Text = "本层反选";
            this.本层反选MenuItem.Click += new System.EventHandler(this.本层选MenuItem_Click);
            // 
            // 本层选文件MenuItem
            // 
            this.本层选文件MenuItem.Name = "本层选文件MenuItem";
            this.本层选文件MenuItem.Size = new System.Drawing.Size(170, 28);
            this.本层选文件MenuItem.Text = "本层选文件";
            this.本层选文件MenuItem.Click += new System.EventHandler(this.本层选MenuItem_Click);
            // 
            // menuItemSeparator1
            // 
            this.menuItemSeparator1.Name = "menuItemSeparator1";
            this.menuItemSeparator1.Size = new System.Drawing.Size(167, 6);
            // 
            // 下层全选MenuItem
            // 
            this.下层全选MenuItem.Name = "下层全选MenuItem";
            this.下层全选MenuItem.Size = new System.Drawing.Size(170, 28);
            this.下层全选MenuItem.Text = "下层全选";
            this.下层全选MenuItem.Click += new System.EventHandler(this.下层选MenuItem_Click);
            // 
            // 下层反选MenuItem
            // 
            this.下层反选MenuItem.Name = "下层反选MenuItem";
            this.下层反选MenuItem.Size = new System.Drawing.Size(170, 28);
            this.下层反选MenuItem.Text = "下层反选";
            this.下层反选MenuItem.Click += new System.EventHandler(this.下层选MenuItem_Click);
            // 
            // 下层选文件MenuItem
            // 
            this.下层选文件MenuItem.Name = "下层选文件MenuItem";
            this.下层选文件MenuItem.Size = new System.Drawing.Size(170, 28);
            this.下层选文件MenuItem.Text = "下层选文件";
            this.下层选文件MenuItem.Click += new System.EventHandler(this.下层选MenuItem_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.RestoreDirectory = true;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusLabelTitle,
            this.statusLabelContent});
            this.statusStrip1.Location = new System.Drawing.Point(0, 427);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.ShowItemToolTips = true;
            this.statusStrip1.Size = new System.Drawing.Size(784, 30);
            this.statusStrip1.TabIndex = 0;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // statusLabelTitle
            // 
            this.statusLabelTitle.Name = "statusLabelTitle";
            this.statusLabelTitle.Size = new System.Drawing.Size(0, 25);
            // 
            // statusLabelContent
            // 
            this.statusLabelContent.AutoSize = false;
            this.statusLabelContent.AutoToolTip = true;
            this.statusLabelContent.Name = "statusLabelContent";
            this.statusLabelContent.Size = new System.Drawing.Size(769, 25);
            this.statusLabelContent.Spring = true;
            this.statusLabelContent.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.statusLabelContent.ToolTipText = "点击复制";
            this.statusLabelContent.Click += new System.EventHandler(this.statusLabelContent_Click);
            // 
            // panelControls
            // 
            this.panelControls.Controls.Add(this.chkShowProg);
            this.panelControls.Controls.Add(this.panelTxt);
            this.panelControls.Controls.Add(this.panelLabel);
            this.panelControls.Controls.Add(this.panelBtn);
            this.panelControls.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControls.Location = new System.Drawing.Point(0, 0);
            this.panelControls.Name = "panelControls";
            this.panelControls.Size = new System.Drawing.Size(784, 157);
            this.panelControls.TabIndex = 5;
            // 
            // chkShowProg
            // 
            this.chkShowProg.AutoSize = true;
            this.chkShowProg.Location = new System.Drawing.Point(15, 121);
            this.chkShowProg.Name = "chkShowProg";
            this.chkShowProg.Size = new System.Drawing.Size(181, 23);
            this.chkShowProg.TabIndex = 24;
            this.chkShowProg.Text = "显示进度(慢百倍)";
            this.chkShowProg.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chkShowProg.UseVisualStyleBackColor = true;
            this.chkShowProg.CheckedChanged += new System.EventHandler(this.chkShowProg_CheckedChanged);
            // 
            // panelTxt
            // 
            this.panelTxt.Controls.Add(this.txtExecPath);
            this.panelTxt.Controls.Add(this.txtFilePath);
            this.panelTxt.Controls.Add(this.txtTargetPath);
            this.panelTxt.Controls.Add(this.btnExtract);
            this.panelTxt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelTxt.Location = new System.Drawing.Point(53, 0);
            this.panelTxt.Name = "panelTxt";
            this.panelTxt.Size = new System.Drawing.Size(627, 157);
            this.panelTxt.TabIndex = 26;
            this.panelTxt.Resize += new System.EventHandler(this.panelTxt_Resize);
            // 
            // txtExecPath
            // 
            this.txtExecPath.Location = new System.Drawing.Point(3, 14);
            this.txtExecPath.Name = "txtExecPath";
            this.txtExecPath.Size = new System.Drawing.Size(622, 29);
            this.txtExecPath.TabIndex = 21;
            this.txtExecPath.TextChanged += new System.EventHandler(this.txtExecPath_TextChanged);
            // 
            // txtFilePath
            // 
            this.txtFilePath.Location = new System.Drawing.Point(3, 49);
            this.txtFilePath.Name = "txtFilePath";
            this.txtFilePath.Size = new System.Drawing.Size(622, 29);
            this.txtFilePath.TabIndex = 22;
            this.txtFilePath.TextChanged += new System.EventHandler(this.txtFilePath_TextChanged);
            // 
            // txtTargetPath
            // 
            this.txtTargetPath.Location = new System.Drawing.Point(3, 84);
            this.txtTargetPath.Name = "txtTargetPath";
            this.txtTargetPath.Size = new System.Drawing.Size(622, 29);
            this.txtTargetPath.TabIndex = 23;
            this.txtTargetPath.TextChanged += new System.EventHandler(this.txtTargetPath_TextChanged);
            // 
            // btnExtract
            // 
            this.btnExtract.AutoSize = true;
            this.btnExtract.Location = new System.Drawing.Point(533, 119);
            this.btnExtract.Name = "btnExtract";
            this.btnExtract.Size = new System.Drawing.Size(92, 29);
            this.btnExtract.TabIndex = 23;
            this.btnExtract.Text = "提取";
            this.btnExtract.UseVisualStyleBackColor = true;
            this.btnExtract.Click += new System.EventHandler(this.btnExtract_Click);
            // 
            // panelLabel
            // 
            this.panelLabel.AutoSize = true;
            this.panelLabel.Controls.Add(this.label1);
            this.panelLabel.Controls.Add(this.label2);
            this.panelLabel.Controls.Add(this.label3);
            this.panelLabel.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelLabel.Location = new System.Drawing.Point(0, 0);
            this.panelLabel.Name = "panelLabel";
            this.panelLabel.Size = new System.Drawing.Size(53, 157);
            this.panelLabel.TabIndex = 25;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 19);
            this.label1.TabIndex = 20;
            this.label1.Text = "程序";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 52);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 19);
            this.label2.TabIndex = 21;
            this.label2.Text = "文件";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 87);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 19);
            this.label3.TabIndex = 22;
            this.label3.Text = "目标";
            // 
            // panelBtn
            // 
            this.panelBtn.AutoSize = true;
            this.panelBtn.Controls.Add(this.btnOpenExec);
            this.panelBtn.Controls.Add(this.btnOpenFile);
            this.panelBtn.Controls.Add(this.btnOpenTarget);
            this.panelBtn.Controls.Add(this.btnLoadFile);
            this.panelBtn.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelBtn.Location = new System.Drawing.Point(680, 0);
            this.panelBtn.Name = "panelBtn";
            this.panelBtn.Padding = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.panelBtn.Size = new System.Drawing.Size(104, 157);
            this.panelBtn.TabIndex = 27;
            // 
            // btnOpenExec
            // 
            this.btnOpenExec.AutoSize = true;
            this.btnOpenExec.Location = new System.Drawing.Point(6, 14);
            this.btnOpenExec.Name = "btnOpenExec";
            this.btnOpenExec.Size = new System.Drawing.Size(92, 29);
            this.btnOpenExec.TabIndex = 22;
            this.btnOpenExec.Text = "浏览…";
            this.btnOpenExec.UseVisualStyleBackColor = true;
            this.btnOpenExec.Click += new System.EventHandler(this.btnOpenExec_Click);
            this.btnOpenExec.Resize += new System.EventHandler(this.btnOpenExec_Resize);
            // 
            // btnOpenFile
            // 
            this.btnOpenFile.Location = new System.Drawing.Point(6, 49);
            this.btnOpenFile.Name = "btnOpenFile";
            this.btnOpenFile.Size = new System.Drawing.Size(92, 29);
            this.btnOpenFile.TabIndex = 23;
            this.btnOpenFile.Text = "浏览…";
            this.btnOpenFile.UseVisualStyleBackColor = true;
            this.btnOpenFile.Click += new System.EventHandler(this.btnOpenFile_Click);
            // 
            // btnOpenTarget
            // 
            this.btnOpenTarget.Location = new System.Drawing.Point(6, 84);
            this.btnOpenTarget.Name = "btnOpenTarget";
            this.btnOpenTarget.Size = new System.Drawing.Size(92, 29);
            this.btnOpenTarget.TabIndex = 24;
            this.btnOpenTarget.Text = "浏览…";
            this.btnOpenTarget.UseVisualStyleBackColor = true;
            this.btnOpenTarget.Click += new System.EventHandler(this.btnOpenTarget_Click);
            // 
            // btnLoadFile
            // 
            this.btnLoadFile.Location = new System.Drawing.Point(6, 119);
            this.btnLoadFile.Name = "btnLoadFile";
            this.btnLoadFile.Size = new System.Drawing.Size(92, 29);
            this.btnLoadFile.TabIndex = 22;
            this.btnLoadFile.Text = "读取";
            this.btnLoadFile.UseVisualStyleBackColor = true;
            this.btnLoadFile.Click += new System.EventHandler(this.btnLoadFile_Click);
            // 
            // splitViews
            // 
            this.splitViews.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitViews.Location = new System.Drawing.Point(0, 157);
            this.splitViews.Name = "splitViews";
            // 
            // splitViews.Panel1
            // 
            this.splitViews.Panel1.Controls.Add(this.treeView1);
            // 
            // splitViews.Panel2
            // 
            this.splitViews.Panel2.Controls.Add(this.listBox1);
            this.splitViews.Size = new System.Drawing.Size(784, 270);
            this.splitViews.SplitterDistance = 391;
            this.splitViews.TabIndex = 4;
            // 
            // treeView1
            // 
            this.treeView1.CheckBoxes = true;
            this.treeView1.ContextMenuStrip = this.treeViewMenu;
            this.treeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView1.HideSelection = false;
            this.treeView1.HotTracking = true;
            this.treeView1.Location = new System.Drawing.Point(0, 0);
            this.treeView1.Name = "treeView1";
            this.treeView1.ShowLines = false;
            this.treeView1.Size = new System.Drawing.Size(391, 270);
            this.treeView1.TabIndex = 12;
            this.treeView1.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterCheck);
            // 
            // listBox1
            // 
            this.listBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 19;
            this.listBox1.Location = new System.Drawing.Point(0, 0);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(389, 270);
            this.listBox1.Sorted = true;
            this.listBox1.TabIndex = 13;
            // 
            // InnoUnpUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 457);
            this.Controls.Add(this.splitViews);
            this.Controls.Add(this.panelControls);
            this.Controls.Add(this.statusStrip1);
            this.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.KeyPreview = true;
            this.Name = "InnoUnpUI";
            this.Text = "InnoUnpUI";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.InnoUnpUI_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.InnoUnpUI_KeyUp);
            this.treeViewMenu.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.panelControls.ResumeLayout(false);
            this.panelControls.PerformLayout();
            this.panelTxt.ResumeLayout(false);
            this.panelTxt.PerformLayout();
            this.panelLabel.ResumeLayout(false);
            this.panelLabel.PerformLayout();
            this.panelBtn.ResumeLayout(false);
            this.panelBtn.PerformLayout();
            this.splitViews.Panel1.ResumeLayout(false);
            this.splitViews.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitViews)).EndInit();
            this.splitViews.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel statusLabelContent;
        private System.Windows.Forms.ToolStripStatusLabel statusLabelTitle;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.ContextMenuStrip treeViewMenu;
        private System.Windows.Forms.ToolStripMenuItem 本层全选MenuItem;
        private System.Windows.Forms.ToolStripMenuItem 本层反选MenuItem;
        private System.Windows.Forms.ToolStripMenuItem 本层选文件MenuItem;
        private System.Windows.Forms.ToolStripSeparator menuItemSeparator1;
        private System.Windows.Forms.ToolStripMenuItem 下层全选MenuItem;
        private System.Windows.Forms.ToolStripMenuItem 下层反选MenuItem;
        private System.Windows.Forms.ToolStripMenuItem 下层选文件MenuItem;
        private System.Windows.Forms.Panel panelControls;
        private System.Windows.Forms.CheckBox chkShowProg;
        private System.Windows.Forms.Button btnExtract;
        private System.Windows.Forms.SplitContainer splitViews;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Panel panelLabel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panelTxt;
        private System.Windows.Forms.TextBox txtExecPath;
        private System.Windows.Forms.TextBox txtTargetPath;
        private System.Windows.Forms.TextBox txtFilePath;
        private System.Windows.Forms.Panel panelBtn;
        private System.Windows.Forms.Button btnOpenExec;
        private System.Windows.Forms.Button btnOpenTarget;
        private System.Windows.Forms.Button btnOpenFile;
        private System.Windows.Forms.Button btnLoadFile;
    }
}

