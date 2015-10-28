namespace Editor
{
    partial class Form1
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
            this.selectedObject_pg = new System.Windows.Forms.PropertyGrid();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.gameEntities_lb = new System.Windows.Forms.ListBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.delete_btn = new System.Windows.Forms.Button();
            this.clone_btn = new System.Windows.Forms.Button();
            this.forward_btn = new System.Windows.Forms.Button();
            this.back_btn = new System.Windows.Forms.Button();
            this.toolsPointer_rb = new System.Windows.Forms.RadioButton();
            this.toolsRect_rb = new System.Windows.Forms.RadioButton();
            this.toolsCircle_rb = new System.Windows.Forms.RadioButton();
            this.toolsCustom_rb = new System.Windows.Forms.RadioButton();
            this.addNewProperty_btn = new System.Windows.Forms.Button();
            this.addPropName_txt = new System.Windows.Forms.TextBox();
            this.addPropType_cmb = new System.Windows.Forms.ComboBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.editProperty_btn = new System.Windows.Forms.Button();
            this.editPropType_cmb = new System.Windows.Forms.ComboBox();
            this.deleteProp_btn = new System.Windows.Forms.Button();
            this.editPropName_txt = new System.Windows.Forms.TextBox();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tabControl1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // selectedObject_pg
            // 
            this.selectedObject_pg.Location = new System.Drawing.Point(2, 81);
            this.selectedObject_pg.Name = "selectedObject_pg";
            this.selectedObject_pg.Size = new System.Drawing.Size(302, 290);
            this.selectedObject_pg.TabIndex = 0;
            this.selectedObject_pg.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.selectedObject_pg_PropertyValueChanged);
            this.selectedObject_pg.SelectedGridItemChanged += new System.Windows.Forms.SelectedGridItemChangedEventHandler(this.selectedObject_pg_SelectedGridItemChanged);
            this.selectedObject_pg.Click += new System.EventHandler(this.selectedObject_pg_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Location = new System.Drawing.Point(0, 27);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(808, 629);
            this.tabControl1.TabIndex = 1;
            // 
            // tabPage1
            // 
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(800, 600);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1126, 28);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tableLayoutPanel1);
            this.groupBox1.Location = new System.Drawing.Point(810, 27);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(161, 251);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Tools";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.toolsPointer_rb, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.toolsRect_rb, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.toolsCircle_rb, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.toolsCustom_rb, 1, 1);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(6, 25);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(144, 220);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // gameEntities_lb
            // 
            this.gameEntities_lb.FormattingEnabled = true;
            this.gameEntities_lb.ItemHeight = 16;
            this.gameEntities_lb.Location = new System.Drawing.Point(977, 27);
            this.gameEntities_lb.Name = "gameEntities_lb";
            this.gameEntities_lb.Size = new System.Drawing.Size(142, 196);
            this.gameEntities_lb.TabIndex = 4;
            this.gameEntities_lb.SelectedIndexChanged += new System.EventHandler(this.gameEntities_lb_SelectedIndexChanged);
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.delete_btn, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.clone_btn, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.forward_btn, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.back_btn, 1, 1);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(977, 229);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(142, 49);
            this.tableLayoutPanel2.TabIndex = 5;
            // 
            // delete_btn
            // 
            this.delete_btn.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.delete_btn.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.delete_btn.Location = new System.Drawing.Point(0, 0);
            this.delete_btn.Margin = new System.Windows.Forms.Padding(0);
            this.delete_btn.Name = "delete_btn";
            this.delete_btn.Size = new System.Drawing.Size(71, 24);
            this.delete_btn.TabIndex = 0;
            this.delete_btn.Text = "Delete";
            this.delete_btn.UseVisualStyleBackColor = true;
            // 
            // clone_btn
            // 
            this.clone_btn.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.clone_btn.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.clone_btn.Location = new System.Drawing.Point(71, 0);
            this.clone_btn.Margin = new System.Windows.Forms.Padding(0);
            this.clone_btn.Name = "clone_btn";
            this.clone_btn.Size = new System.Drawing.Size(71, 24);
            this.clone_btn.TabIndex = 0;
            this.clone_btn.Text = "Clone";
            this.clone_btn.UseVisualStyleBackColor = true;
            // 
            // forward_btn
            // 
            this.forward_btn.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.forward_btn.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.forward_btn.Location = new System.Drawing.Point(0, 24);
            this.forward_btn.Margin = new System.Windows.Forms.Padding(0);
            this.forward_btn.Name = "forward_btn";
            this.forward_btn.Size = new System.Drawing.Size(71, 25);
            this.forward_btn.TabIndex = 0;
            this.forward_btn.Text = "Forward";
            this.forward_btn.UseVisualStyleBackColor = true;
            // 
            // back_btn
            // 
            this.back_btn.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.back_btn.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.back_btn.Location = new System.Drawing.Point(71, 24);
            this.back_btn.Margin = new System.Windows.Forms.Padding(0);
            this.back_btn.Name = "back_btn";
            this.back_btn.Size = new System.Drawing.Size(71, 25);
            this.back_btn.TabIndex = 0;
            this.back_btn.Text = "Back";
            this.back_btn.UseVisualStyleBackColor = true;
            // 
            // toolsPointer_rb
            // 
            this.toolsPointer_rb.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.toolsPointer_rb.Appearance = System.Windows.Forms.Appearance.Button;
            this.toolsPointer_rb.AutoSize = true;
            this.toolsPointer_rb.Checked = true;
            this.toolsPointer_rb.Location = new System.Drawing.Point(3, 3);
            this.toolsPointer_rb.Name = "toolsPointer_rb";
            this.toolsPointer_rb.Size = new System.Drawing.Size(66, 49);
            this.toolsPointer_rb.TabIndex = 0;
            this.toolsPointer_rb.TabStop = true;
            this.toolsPointer_rb.Text = "Pointer";
            this.toolsPointer_rb.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.toolsPointer_rb.UseVisualStyleBackColor = true;
            this.toolsPointer_rb.CheckedChanged += new System.EventHandler(this.ToolRadioButtons_CheckedChanged);
            // 
            // toolsRect_rb
            // 
            this.toolsRect_rb.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.toolsRect_rb.Appearance = System.Windows.Forms.Appearance.Button;
            this.toolsRect_rb.AutoSize = true;
            this.toolsRect_rb.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolsRect_rb.Location = new System.Drawing.Point(75, 3);
            this.toolsRect_rb.Name = "toolsRect_rb";
            this.toolsRect_rb.Size = new System.Drawing.Size(66, 49);
            this.toolsRect_rb.TabIndex = 0;
            this.toolsRect_rb.Text = "Rectangle";
            this.toolsRect_rb.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.toolsRect_rb.UseVisualStyleBackColor = true;
            this.toolsRect_rb.CheckedChanged += new System.EventHandler(this.ToolRadioButtons_CheckedChanged);
            // 
            // toolsCircle_rb
            // 
            this.toolsCircle_rb.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.toolsCircle_rb.Appearance = System.Windows.Forms.Appearance.Button;
            this.toolsCircle_rb.AutoSize = true;
            this.toolsCircle_rb.Location = new System.Drawing.Point(3, 58);
            this.toolsCircle_rb.Name = "toolsCircle_rb";
            this.toolsCircle_rb.Size = new System.Drawing.Size(66, 49);
            this.toolsCircle_rb.TabIndex = 0;
            this.toolsCircle_rb.Text = "Circle";
            this.toolsCircle_rb.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.toolsCircle_rb.UseVisualStyleBackColor = true;
            this.toolsCircle_rb.CheckedChanged += new System.EventHandler(this.ToolRadioButtons_CheckedChanged);
            // 
            // toolsCustom_rb
            // 
            this.toolsCustom_rb.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.toolsCustom_rb.Appearance = System.Windows.Forms.Appearance.Button;
            this.toolsCustom_rb.AutoSize = true;
            this.toolsCustom_rb.Location = new System.Drawing.Point(75, 58);
            this.toolsCustom_rb.Name = "toolsCustom_rb";
            this.toolsCustom_rb.Size = new System.Drawing.Size(66, 49);
            this.toolsCustom_rb.TabIndex = 0;
            this.toolsCustom_rb.Text = "Custom";
            this.toolsCustom_rb.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.toolsCustom_rb.UseVisualStyleBackColor = true;
            this.toolsCustom_rb.CheckedChanged += new System.EventHandler(this.ToolRadioButtons_CheckedChanged);
            // 
            // addNewProperty_btn
            // 
            this.addNewProperty_btn.Location = new System.Drawing.Point(9, 21);
            this.addNewProperty_btn.Name = "addNewProperty_btn";
            this.addNewProperty_btn.Size = new System.Drawing.Size(48, 27);
            this.addNewProperty_btn.TabIndex = 6;
            this.addNewProperty_btn.Text = "Add";
            this.addNewProperty_btn.UseVisualStyleBackColor = true;
            this.addNewProperty_btn.Click += new System.EventHandler(this.addNewProperty_btn_Click);
            // 
            // addPropName_txt
            // 
            this.addPropName_txt.Location = new System.Drawing.Point(63, 23);
            this.addPropName_txt.Name = "addPropName_txt";
            this.addPropName_txt.Size = new System.Drawing.Size(107, 22);
            this.addPropName_txt.TabIndex = 7;
            // 
            // addPropType_cmb
            // 
            this.addPropType_cmb.FormattingEnabled = true;
            this.addPropType_cmb.Location = new System.Drawing.Point(173, 21);
            this.addPropType_cmb.Name = "addPropType_cmb";
            this.addPropType_cmb.Size = new System.Drawing.Size(131, 24);
            this.addPropType_cmb.TabIndex = 8;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.editPropType_cmb);
            this.groupBox2.Controls.Add(this.addPropType_cmb);
            this.groupBox2.Controls.Add(this.editPropName_txt);
            this.groupBox2.Controls.Add(this.addPropName_txt);
            this.groupBox2.Controls.Add(this.editProperty_btn);
            this.groupBox2.Controls.Add(this.deleteProp_btn);
            this.groupBox2.Controls.Add(this.addNewProperty_btn);
            this.groupBox2.Controls.Add(this.selectedObject_pg);
            this.groupBox2.Location = new System.Drawing.Point(810, 281);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(309, 375);
            this.groupBox2.TabIndex = 9;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Properties";
            // 
            // editProperty_btn
            // 
            this.editProperty_btn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.editProperty_btn.Location = new System.Drawing.Point(9, 51);
            this.editProperty_btn.Name = "editProperty_btn";
            this.editProperty_btn.Size = new System.Drawing.Size(48, 27);
            this.editProperty_btn.TabIndex = 6;
            this.editProperty_btn.Text = "Edit";
            this.editProperty_btn.UseVisualStyleBackColor = true;
            this.editProperty_btn.Click += new System.EventHandler(this.editProperty_btn_Click);
            // 
            // editPropType_cmb
            // 
            this.editPropType_cmb.FormattingEnabled = true;
            this.editPropType_cmb.Location = new System.Drawing.Point(174, 51);
            this.editPropType_cmb.Name = "editPropType_cmb";
            this.editPropType_cmb.Size = new System.Drawing.Size(130, 24);
            this.editPropType_cmb.TabIndex = 8;
            // 
            // deleteProp_btn
            // 
            this.deleteProp_btn.Location = new System.Drawing.Point(81, 81);
            this.deleteProp_btn.Name = "deleteProp_btn";
            this.deleteProp_btn.Size = new System.Drawing.Size(89, 25);
            this.deleteProp_btn.TabIndex = 6;
            this.deleteProp_btn.Text = "Delete";
            this.deleteProp_btn.UseVisualStyleBackColor = true;
            this.deleteProp_btn.Click += new System.EventHandler(this.deleteProp_btn_Click);
            // 
            // editPropName_txt
            // 
            this.editPropName_txt.Location = new System.Drawing.Point(63, 53);
            this.editPropName_txt.Name = "editPropName_txt";
            this.editPropName_txt.Size = new System.Drawing.Size(107, 22);
            this.editPropName_txt.TabIndex = 7;
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exportToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(44, 24);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // exportToolStripMenuItem
            // 
            this.exportToolStripMenuItem.Name = "exportToolStripMenuItem";
            this.exportToolStripMenuItem.Size = new System.Drawing.Size(175, 24);
            this.exportToolStripMenuItem.Text = "Export";
            this.exportToolStripMenuItem.Click += new System.EventHandler(this.exportToolStripMenuItem_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1126, 659);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.tableLayoutPanel2);
            this.Controls.Add(this.gameEntities_lb);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Form1";
            this.tabControl1.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PropertyGrid selectedObject_pg;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.ListBox gameEntities_lb;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Button delete_btn;
        private System.Windows.Forms.Button clone_btn;
        private System.Windows.Forms.Button forward_btn;
        private System.Windows.Forms.Button back_btn;
        private System.Windows.Forms.RadioButton toolsPointer_rb;
        private System.Windows.Forms.RadioButton toolsRect_rb;
        private System.Windows.Forms.RadioButton toolsCircle_rb;
        private System.Windows.Forms.RadioButton toolsCustom_rb;
        private System.Windows.Forms.Button addNewProperty_btn;
        private System.Windows.Forms.TextBox addPropName_txt;
        private System.Windows.Forms.ComboBox addPropType_cmb;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ComboBox editPropType_cmb;
        private System.Windows.Forms.Button editProperty_btn;
        private System.Windows.Forms.Button deleteProp_btn;
        private System.Windows.Forms.TextBox editPropName_txt;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportToolStripMenuItem;
    }
}

