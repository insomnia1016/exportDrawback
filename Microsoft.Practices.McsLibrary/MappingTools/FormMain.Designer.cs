namespace MappingTools
{
    partial class FormMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtClassPath = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtMapPath = new System.Windows.Forms.TextBox();
            this.btnGenerating = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.txtNameSpace = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtAssemblyName = new System.Windows.Forms.TextBox();
            this.dlgFolderBrowser = new System.Windows.Forms.FolderBrowserDialog();
            this.btnClassPath = new System.Windows.Forms.Button();
            this.btnMapPath = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.txtLogicNameSpace = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtLogicPath = new System.Windows.Forms.TextBox();
            this.btnLogicPath = new System.Windows.Forms.Button();
            this.chkGenHelper = new System.Windows.Forms.CheckBox();
            this.btnRefreshData = new System.Windows.Forms.Button();
            this.lstTables = new System.Windows.Forms.CheckedListBox();
            this.btnSelectAll = new System.Windows.Forms.Button();
            this.btnReverseSelect = new System.Windows.Forms.Button();
            this.btnRefresTables = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "表名称：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 212);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(101, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "类文件存放位置：";
            // 
            // txtClassPath
            // 
            this.txtClassPath.Location = new System.Drawing.Point(12, 227);
            this.txtClassPath.Name = "txtClassPath";
            this.txtClassPath.Size = new System.Drawing.Size(448, 21);
            this.txtClassPath.TabIndex = 4;
            this.txtClassPath.Text = "d:\\code";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 338);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(113, 12);
            this.label3.TabIndex = 0;
            this.label3.Text = "映射文件存放位置：";
            // 
            // txtMapPath
            // 
            this.txtMapPath.Location = new System.Drawing.Point(12, 353);
            this.txtMapPath.Name = "txtMapPath";
            this.txtMapPath.Size = new System.Drawing.Size(448, 21);
            this.txtMapPath.TabIndex = 9;
            this.txtMapPath.Text = "d:\\code";
            // 
            // btnGenerating
            // 
            this.btnGenerating.Location = new System.Drawing.Point(12, 386);
            this.btnGenerating.Name = "btnGenerating";
            this.btnGenerating.Size = new System.Drawing.Size(75, 24);
            this.btnGenerating.TabIndex = 11;
            this.btnGenerating.Text = "产生";
            this.btnGenerating.UseVisualStyleBackColor = true;
            this.btnGenerating.Click += new System.EventHandler(this.btnGenerating_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(10, 172);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(77, 12);
            this.label4.TabIndex = 0;
            this.label4.Text = "类命名空间：";
            // 
            // txtNameSpace
            // 
            this.txtNameSpace.Location = new System.Drawing.Point(12, 187);
            this.txtNameSpace.Name = "txtNameSpace";
            this.txtNameSpace.Size = new System.Drawing.Size(209, 21);
            this.txtNameSpace.TabIndex = 2;
            this.txtNameSpace.Text = "ExportDrawbackManagement.Biz";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(249, 172);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 12);
            this.label5.TabIndex = 0;
            this.label5.Text = "类库名称：";
            // 
            // txtAssemblyName
            // 
            this.txtAssemblyName.Location = new System.Drawing.Point(251, 187);
            this.txtAssemblyName.Name = "txtAssemblyName";
            this.txtAssemblyName.Size = new System.Drawing.Size(209, 21);
            this.txtAssemblyName.TabIndex = 3;
            this.txtAssemblyName.Text = "ExportDrawbackManagement.Biz";
            // 
            // btnClassPath
            // 
            this.btnClassPath.Location = new System.Drawing.Point(473, 225);
            this.btnClassPath.Name = "btnClassPath";
            this.btnClassPath.Size = new System.Drawing.Size(75, 23);
            this.btnClassPath.TabIndex = 5;
            this.btnClassPath.Text = "Browse";
            this.btnClassPath.UseVisualStyleBackColor = true;
            this.btnClassPath.Click += new System.EventHandler(this.btnClassPath_Click);
            // 
            // btnMapPath
            // 
            this.btnMapPath.Location = new System.Drawing.Point(473, 351);
            this.btnMapPath.Name = "btnMapPath";
            this.btnMapPath.Size = new System.Drawing.Size(75, 23);
            this.btnMapPath.TabIndex = 10;
            this.btnMapPath.Text = "Browse";
            this.btnMapPath.UseVisualStyleBackColor = true;
            this.btnMapPath.Click += new System.EventHandler(this.btnMapPath_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(10, 253);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(101, 12);
            this.label6.TabIndex = 0;
            this.label6.Text = "帮助类命名空间：";
            // 
            // txtLogicNameSpace
            // 
            this.txtLogicNameSpace.Location = new System.Drawing.Point(12, 268);
            this.txtLogicNameSpace.Name = "txtLogicNameSpace";
            this.txtLogicNameSpace.Size = new System.Drawing.Size(209, 21);
            this.txtLogicNameSpace.TabIndex = 6;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(10, 295);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(125, 12);
            this.label7.TabIndex = 0;
            this.label7.Text = "帮助类文件存放位置：";
            // 
            // txtLogicPath
            // 
            this.txtLogicPath.Location = new System.Drawing.Point(12, 310);
            this.txtLogicPath.Name = "txtLogicPath";
            this.txtLogicPath.Size = new System.Drawing.Size(448, 21);
            this.txtLogicPath.TabIndex = 7;
            // 
            // btnLogicPath
            // 
            this.btnLogicPath.Location = new System.Drawing.Point(473, 308);
            this.btnLogicPath.Name = "btnLogicPath";
            this.btnLogicPath.Size = new System.Drawing.Size(75, 23);
            this.btnLogicPath.TabIndex = 8;
            this.btnLogicPath.Text = "Browse";
            this.btnLogicPath.UseVisualStyleBackColor = true;
            this.btnLogicPath.Click += new System.EventHandler(this.btnLogicPath_Click);
            // 
            // chkGenHelper
            // 
            this.chkGenHelper.AutoSize = true;
            this.chkGenHelper.Enabled = false;
            this.chkGenHelper.Location = new System.Drawing.Point(251, 270);
            this.chkGenHelper.Name = "chkGenHelper";
            this.chkGenHelper.Size = new System.Drawing.Size(84, 16);
            this.chkGenHelper.TabIndex = 12;
            this.chkGenHelper.Text = "生成帮助类";
            this.chkGenHelper.UseVisualStyleBackColor = true;
            this.chkGenHelper.CheckedChanged += new System.EventHandler(this.chkGenHelper_CheckedChanged);
            // 
            // btnRefreshData
            // 
            this.btnRefreshData.Location = new System.Drawing.Point(112, 387);
            this.btnRefreshData.Name = "btnRefreshData";
            this.btnRefreshData.Size = new System.Drawing.Size(75, 23);
            this.btnRefreshData.TabIndex = 13;
            this.btnRefreshData.Text = "刷新缓存";
            this.btnRefreshData.UseVisualStyleBackColor = true;
            this.btnRefreshData.Click += new System.EventHandler(this.btnRefreshData_Click);
            // 
            // lstTables
            // 
            this.lstTables.CheckOnClick = true;
            this.lstTables.FormattingEnabled = true;
            this.lstTables.Location = new System.Drawing.Point(14, 24);
            this.lstTables.Name = "lstTables";
            this.lstTables.Size = new System.Drawing.Size(446, 132);
            this.lstTables.TabIndex = 14;
            // 
            // btnSelectAll
            // 
            this.btnSelectAll.Location = new System.Drawing.Point(473, 24);
            this.btnSelectAll.Name = "btnSelectAll";
            this.btnSelectAll.Size = new System.Drawing.Size(75, 23);
            this.btnSelectAll.TabIndex = 15;
            this.btnSelectAll.Text = "全选";
            this.btnSelectAll.UseVisualStyleBackColor = true;
            this.btnSelectAll.Click += new System.EventHandler(this.btnSelectAll_Click);
            // 
            // btnReverseSelect
            // 
            this.btnReverseSelect.Location = new System.Drawing.Point(473, 53);
            this.btnReverseSelect.Name = "btnReverseSelect";
            this.btnReverseSelect.Size = new System.Drawing.Size(75, 23);
            this.btnReverseSelect.TabIndex = 16;
            this.btnReverseSelect.Text = "反向";
            this.btnReverseSelect.UseVisualStyleBackColor = true;
            this.btnReverseSelect.Click += new System.EventHandler(this.btnReverseSelect_Click);
            // 
            // btnRefresTables
            // 
            this.btnRefresTables.Location = new System.Drawing.Point(473, 83);
            this.btnRefresTables.Name = "btnRefresTables";
            this.btnRefresTables.Size = new System.Drawing.Size(75, 23);
            this.btnRefresTables.TabIndex = 17;
            this.btnRefresTables.Text = "刷新列表";
            this.btnRefresTables.UseVisualStyleBackColor = true;
            this.btnRefresTables.Click += new System.EventHandler(this.btnRefresTables_Click);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(566, 426);
            this.Controls.Add(this.btnRefresTables);
            this.Controls.Add(this.btnReverseSelect);
            this.Controls.Add(this.btnSelectAll);
            this.Controls.Add(this.lstTables);
            this.Controls.Add(this.btnRefreshData);
            this.Controls.Add(this.chkGenHelper);
            this.Controls.Add(this.btnMapPath);
            this.Controls.Add(this.btnLogicPath);
            this.Controls.Add(this.btnClassPath);
            this.Controls.Add(this.btnGenerating);
            this.Controls.Add(this.txtMapPath);
            this.Controls.Add(this.txtLogicPath);
            this.Controls.Add(this.txtClassPath);
            this.Controls.Add(this.txtAssemblyName);
            this.Controls.Add(this.txtLogicNameSpace);
            this.Controls.Add(this.txtNameSpace);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label1);
            this.Name = "FormMain";
            this.Text = "FormMain";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtClassPath;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtMapPath;
        private System.Windows.Forms.Button btnGenerating;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtNameSpace;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtAssemblyName;
        private System.Windows.Forms.FolderBrowserDialog dlgFolderBrowser;
        private System.Windows.Forms.Button btnClassPath;
        private System.Windows.Forms.Button btnMapPath;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtLogicNameSpace;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtLogicPath;
        private System.Windows.Forms.Button btnLogicPath;
        private System.Windows.Forms.CheckBox chkGenHelper;
        private System.Windows.Forms.Button btnRefreshData;
        private System.Windows.Forms.CheckedListBox lstTables;
        private System.Windows.Forms.Button btnSelectAll;
        private System.Windows.Forms.Button btnReverseSelect;
        private System.Windows.Forms.Button btnRefresTables;
    }
}