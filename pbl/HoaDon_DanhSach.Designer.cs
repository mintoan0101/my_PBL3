namespace pbl
{
    partial class HoaDon_DanhSach
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
            this.panel2 = new System.Windows.Forms.Panel();
            this.cbb_BoLoc = new System.Windows.Forms.ComboBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.bt_search = new System.Windows.Forms.Button();
            this.txt_search = new System.Windows.Forms.TextBox();
            this.cbb_PhanLoai = new System.Windows.Forms.ComboBox();
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
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.dataGridView1);
            this.panel1.Location = new System.Drawing.Point(25, 24);
            this.panel1.Margin = new System.Windows.Forms.Padding(4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1449, 750);
            this.panel1.TabIndex = 2;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(250)))));
            this.panel2.Controls.Add(this.cbb_BoLoc);
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Location = new System.Drawing.Point(20, 20);
            this.panel2.Margin = new System.Windows.Forms.Padding(4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1397, 71);
            this.panel2.TabIndex = 3;
            // 
            // cbb_BoLoc
            // 
            this.cbb_BoLoc.DisplayMember = "Sort By";
            this.cbb_BoLoc.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.cbb_BoLoc.FormattingEnabled = true;
            this.cbb_BoLoc.Location = new System.Drawing.Point(1091, 18);
            this.cbb_BoLoc.Margin = new System.Windows.Forms.Padding(4);
            this.cbb_BoLoc.Name = "cbb_BoLoc";
            this.cbb_BoLoc.Size = new System.Drawing.Size(276, 36);
            this.cbb_BoLoc.TabIndex = 1;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.White;
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.bt_search);
            this.panel3.Controls.Add(this.txt_search);
            this.panel3.Controls.Add(this.cbb_PhanLoai);
            this.panel3.Location = new System.Drawing.Point(20, 12);
            this.panel3.Margin = new System.Windows.Forms.Padding(4);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1028, 42);
            this.panel3.TabIndex = 0;
            // 
            // bt_search
            // 
            this.bt_search.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.bt_search.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.bt_search.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bt_search.Image = global::pbl.Properties.Resources.search44;
            this.bt_search.Location = new System.Drawing.Point(924, -9);
            this.bt_search.Margin = new System.Windows.Forms.Padding(4);
            this.bt_search.Name = "bt_search";
            this.bt_search.Size = new System.Drawing.Size(117, 51);
            this.bt_search.TabIndex = 2;
            this.bt_search.UseVisualStyleBackColor = false;
            this.bt_search.Click += new System.EventHandler(this.bt_search_Click);
            // 
            // txt_search
            // 
            this.txt_search.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txt_search.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.txt_search.Location = new System.Drawing.Point(328, 9);
            this.txt_search.Margin = new System.Windows.Forms.Padding(4);
            this.txt_search.Name = "txt_search";
            this.txt_search.Size = new System.Drawing.Size(580, 27);
            this.txt_search.TabIndex = 1;
            // 
            // cbb_PhanLoai
            // 
            this.cbb_PhanLoai.DisplayMember = "Phân loại";
            this.cbb_PhanLoai.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbb_PhanLoai.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.cbb_PhanLoai.FormattingEnabled = true;
            this.cbb_PhanLoai.Items.AddRange(new object[] {
            "ID Hoá Đơn",
            "Nhân Viên",
            "Ngày Tạo Hoá Đơn",
            "Khách Hàng"});
            this.cbb_PhanLoai.Location = new System.Drawing.Point(-1, 0);
            this.cbb_PhanLoai.Margin = new System.Windows.Forms.Padding(4);
            this.cbb_PhanLoai.Name = "cbb_PhanLoai";
            this.cbb_PhanLoai.Size = new System.Drawing.Size(321, 36);
            this.cbb_PhanLoai.TabIndex = 0;
            this.cbb_PhanLoai.Text = "       Phân loại";
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(20, 115);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(4);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(1397, 607);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellDoubleClick);
            // 
            // HoaDon_DanhSach
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(240)))), ((int)(((byte)(243)))));
            this.ClientSize = new System.Drawing.Size(1498, 800);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "HoaDon_DanhSach";
            this.Text = "DanhSachHoaDon";
            this.Load += new System.EventHandler(this.HoaDon_DanhSach_Load);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ComboBox cbb_BoLoc;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button bt_search;
        private System.Windows.Forms.TextBox txt_search;
        private System.Windows.Forms.ComboBox cbb_PhanLoai;
        private System.Windows.Forms.DataGridView dataGridView1;
    }
}