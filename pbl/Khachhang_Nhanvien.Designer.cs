﻿namespace pbl
{
    partial class KhachHang_NhanVien
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.btn_them = new System.Windows.Forms.Button();
            this.btn_xoa = new System.Windows.Forms.Button();
            this.btn_sua = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.cbb_BoLoc = new System.Windows.Forms.ComboBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.btn_timkiem = new System.Windows.Forms.Button();
            this.txt_timkiem = new System.Windows.Forms.TextBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.btn_them);
            this.panel1.Controls.Add(this.btn_xoa);
            this.panel1.Controls.Add(this.btn_sua);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.dataGridView1);
            this.panel1.Location = new System.Drawing.Point(40, 34);
            this.panel1.Margin = new System.Windows.Forms.Padding(4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1559, 790);
            this.panel1.TabIndex = 2;
            // 
            // btn_them
            // 
            this.btn_them.BackColor = System.Drawing.Color.White;
            this.btn_them.FlatAppearance.BorderColor = System.Drawing.Color.LightGray;
            this.btn_them.FlatAppearance.BorderSize = 3;
            this.btn_them.Font = new System.Drawing.Font("Segoe UI Semibold", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.btn_them.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.btn_them.Image = global::pbl.Properties.Resources.add3;
            this.btn_them.Location = new System.Drawing.Point(215, 696);
            this.btn_them.Margin = new System.Windows.Forms.Padding(4);
            this.btn_them.Name = "btn_them";
            this.btn_them.Size = new System.Drawing.Size(250, 80);
            this.btn_them.TabIndex = 6;
            this.btn_them.Text = "Thêm";
            this.btn_them.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.btn_them.UseVisualStyleBackColor = false;
            this.btn_them.Click += new System.EventHandler(this.btn_them_Click);
            // 
            // btn_xoa
            // 
            this.btn_xoa.BackColor = System.Drawing.Color.White;
            this.btn_xoa.FlatAppearance.BorderColor = System.Drawing.Color.LightGray;
            this.btn_xoa.FlatAppearance.BorderSize = 3;
            this.btn_xoa.Font = new System.Drawing.Font("Segoe UI Semibold", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.btn_xoa.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.btn_xoa.Image = global::pbl.Properties.Resources.delete;
            this.btn_xoa.Location = new System.Drawing.Point(1096, 696);
            this.btn_xoa.Margin = new System.Windows.Forms.Padding(4);
            this.btn_xoa.Name = "btn_xoa";
            this.btn_xoa.Size = new System.Drawing.Size(250, 80);
            this.btn_xoa.TabIndex = 5;
            this.btn_xoa.Text = "Xoá";
            this.btn_xoa.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.btn_xoa.UseVisualStyleBackColor = false;
            this.btn_xoa.Click += new System.EventHandler(this.btn_xoa_Click);
            // 
            // btn_sua
            // 
            this.btn_sua.BackColor = System.Drawing.Color.White;
            this.btn_sua.FlatAppearance.BorderColor = System.Drawing.Color.LightGray;
            this.btn_sua.FlatAppearance.BorderSize = 3;
            this.btn_sua.Font = new System.Drawing.Font("Segoe UI Semibold", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.btn_sua.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.btn_sua.Image = global::pbl.Properties.Resources.sua2;
            this.btn_sua.Location = new System.Drawing.Point(666, 696);
            this.btn_sua.Margin = new System.Windows.Forms.Padding(4);
            this.btn_sua.Name = "btn_sua";
            this.btn_sua.Size = new System.Drawing.Size(250, 80);
            this.btn_sua.TabIndex = 4;
            this.btn_sua.Text = "Sửa";
            this.btn_sua.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.btn_sua.UseVisualStyleBackColor = false;
            this.btn_sua.Click += new System.EventHandler(this.btn_sua_Click);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(250)))));
            this.panel2.Controls.Add(this.cbb_BoLoc);
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Location = new System.Drawing.Point(20, 20);
            this.panel2.Margin = new System.Windows.Forms.Padding(4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1506, 75);
            this.panel2.TabIndex = 3;
            // 
            // cbb_BoLoc
            // 
            this.cbb_BoLoc.DisplayMember = "Sort By";
            this.cbb_BoLoc.Font = new System.Drawing.Font("Segoe UI", 14F);
            this.cbb_BoLoc.FormattingEnabled = true;
            this.cbb_BoLoc.Location = new System.Drawing.Point(1141, 18);
            this.cbb_BoLoc.Margin = new System.Windows.Forms.Padding(4);
            this.cbb_BoLoc.Name = "cbb_BoLoc";
            this.cbb_BoLoc.Size = new System.Drawing.Size(340, 39);
            this.cbb_BoLoc.TabIndex = 1;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.White;
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.btn_timkiem);
            this.panel3.Controls.Add(this.txt_timkiem);
            this.panel3.Location = new System.Drawing.Point(17, 17);
            this.panel3.Margin = new System.Windows.Forms.Padding(4);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1105, 42);
            this.panel3.TabIndex = 0;
            // 
            // btn_timkiem
            // 
            this.btn_timkiem.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.btn_timkiem.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btn_timkiem.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_timkiem.Image = global::pbl.Properties.Resources.search44;
            this.btn_timkiem.Location = new System.Drawing.Point(1003, 0);
            this.btn_timkiem.Margin = new System.Windows.Forms.Padding(4);
            this.btn_timkiem.Name = "btn_timkiem";
            this.btn_timkiem.Size = new System.Drawing.Size(101, 42);
            this.btn_timkiem.TabIndex = 2;
            this.btn_timkiem.UseVisualStyleBackColor = false;
            this.btn_timkiem.Click += new System.EventHandler(this.btn_timkiem_Click);
            // 
            // txt_timkiem
            // 
            this.txt_timkiem.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txt_timkiem.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.txt_timkiem.Location = new System.Drawing.Point(19, 5);
            this.txt_timkiem.Margin = new System.Windows.Forms.Padding(4);
            this.txt_timkiem.Name = "txt_timkiem";
            this.txt_timkiem.Size = new System.Drawing.Size(966, 27);
            this.txt_timkiem.TabIndex = 1;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(20, 131);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(4);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(1506, 544);
            this.dataGridView1.TabIndex = 0;
            // 
            // KhachHang_NhanVien
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(240)))), ((int)(((byte)(243)))));
            this.ClientSize = new System.Drawing.Size(1636, 860);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "KhachHang_NhanVien";
            this.Text = "Khachhang_Nhanvien";
            this.Load += new System.EventHandler(this.Khachhang_Nhanvien_Load);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btn_them;
        private System.Windows.Forms.Button btn_xoa;
        private System.Windows.Forms.Button btn_sua;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ComboBox cbb_BoLoc;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button btn_timkiem;
        private System.Windows.Forms.TextBox txt_timkiem;
        private System.Windows.Forms.DataGridView dataGridView1;
    }
}