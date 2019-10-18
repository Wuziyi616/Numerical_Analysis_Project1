namespace FaceMorphing
{
    partial class Form1
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
            this.authorLb = new System.Windows.Forms.Label();
            this.titleLb = new System.Windows.Forms.Label();
            this.save_resultBtn = new System.Windows.Forms.Button();
            this.save_imageTB = new System.Windows.Forms.TextBox();
            this.output_imagePB = new System.Windows.Forms.PictureBox();
            this.input_imageBtn = new System.Windows.Forms.Button();
            this.input_imageTB = new System.Windows.Forms.TextBox();
            this.input_imagePB = new System.Windows.Forms.PictureBox();
            this.reference_imagePB = new System.Windows.Forms.PictureBox();
            this.select_referenceBtn = new System.Windows.Forms.Button();
            this.select_referenceTB = new System.Windows.Forms.TextBox();
            this.linecapPB = new System.Windows.Forms.PictureBox();
            this.interpolation_methodCB = new System.Windows.Forms.ComboBox();
            this.interpolation_methodLb = new System.Windows.Forms.Label();
            this.transformBtn = new System.Windows.Forms.Button();
            this.input_keypointsBtn = new System.Windows.Forms.Button();
            this.result_keypointsBtn = new System.Windows.Forms.Button();
            this.reference_keypointsBtn = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.output_imagePB)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.input_imagePB)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.reference_imagePB)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.linecapPB)).BeginInit();
            this.SuspendLayout();
            // 
            // authorLb
            // 
            this.authorLb.AutoSize = true;
            this.authorLb.Font = new System.Drawing.Font("宋体", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.authorLb.Location = new System.Drawing.Point(502, 84);
            this.authorLb.Name = "authorLb";
            this.authorLb.Size = new System.Drawing.Size(252, 28);
            this.authorLb.TabIndex = 3;
            this.authorLb.Text = "Presented by wzy";
            // 
            // titleLb
            // 
            this.titleLb.AutoSize = true;
            this.titleLb.Font = new System.Drawing.Font("宋体", 25.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.titleLb.Location = new System.Drawing.Point(252, 29);
            this.titleLb.Name = "titleLb";
            this.titleLb.Size = new System.Drawing.Size(502, 43);
            this.titleLb.TabIndex = 2;
            this.titleLb.Text = "Face Morphing Project";
            this.titleLb.Click += new System.EventHandler(this.label1_Click);
            // 
            // save_resultBtn
            // 
            this.save_resultBtn.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.save_resultBtn.Location = new System.Drawing.Point(958, 589);
            this.save_resultBtn.Name = "save_resultBtn";
            this.save_resultBtn.Size = new System.Drawing.Size(121, 69);
            this.save_resultBtn.TabIndex = 13;
            this.save_resultBtn.Text = "Save Result";
            this.save_resultBtn.UseVisualStyleBackColor = true;
            this.save_resultBtn.Click += new System.EventHandler(this.save_resultBtn_Click);
            // 
            // save_imageTB
            // 
            this.save_imageTB.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.save_imageTB.Location = new System.Drawing.Point(679, 612);
            this.save_imageTB.Name = "save_imageTB";
            this.save_imageTB.Size = new System.Drawing.Size(273, 30);
            this.save_imageTB.TabIndex = 12;
            // 
            // output_imagePB
            // 
            this.output_imagePB.Location = new System.Drawing.Point(679, 262);
            this.output_imagePB.Name = "output_imagePB";
            this.output_imagePB.Size = new System.Drawing.Size(300, 300);
            this.output_imagePB.TabIndex = 11;
            this.output_imagePB.TabStop = false;
            // 
            // input_imageBtn
            // 
            this.input_imageBtn.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.input_imageBtn.Location = new System.Drawing.Point(345, 589);
            this.input_imageBtn.Name = "input_imageBtn";
            this.input_imageBtn.Size = new System.Drawing.Size(121, 69);
            this.input_imageBtn.TabIndex = 10;
            this.input_imageBtn.Text = "Input Image";
            this.input_imageBtn.UseVisualStyleBackColor = true;
            this.input_imageBtn.Click += new System.EventHandler(this.input_imageBtn_Click);
            // 
            // input_imageTB
            // 
            this.input_imageTB.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.input_imageTB.Location = new System.Drawing.Point(66, 612);
            this.input_imageTB.Name = "input_imageTB";
            this.input_imageTB.Size = new System.Drawing.Size(273, 30);
            this.input_imageTB.TabIndex = 9;
            // 
            // input_imagePB
            // 
            this.input_imagePB.Location = new System.Drawing.Point(66, 262);
            this.input_imagePB.Name = "input_imagePB";
            this.input_imagePB.Size = new System.Drawing.Size(300, 300);
            this.input_imagePB.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.input_imagePB.TabIndex = 8;
            this.input_imagePB.TabStop = false;
            // 
            // reference_imagePB
            // 
            this.reference_imagePB.Location = new System.Drawing.Point(427, 139);
            this.reference_imagePB.Name = "reference_imagePB";
            this.reference_imagePB.Size = new System.Drawing.Size(200, 200);
            this.reference_imagePB.TabIndex = 14;
            this.reference_imagePB.TabStop = false;
            // 
            // select_referenceBtn
            // 
            this.select_referenceBtn.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.select_referenceBtn.Location = new System.Drawing.Point(748, 176);
            this.select_referenceBtn.Name = "select_referenceBtn";
            this.select_referenceBtn.Size = new System.Drawing.Size(145, 69);
            this.select_referenceBtn.TabIndex = 16;
            this.select_referenceBtn.Text = "Select Reference";
            this.select_referenceBtn.UseVisualStyleBackColor = true;
            this.select_referenceBtn.Click += new System.EventHandler(this.select_referenceBtn_Click);
            // 
            // select_referenceTB
            // 
            this.select_referenceTB.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.select_referenceTB.Location = new System.Drawing.Point(748, 124);
            this.select_referenceTB.Name = "select_referenceTB";
            this.select_referenceTB.Size = new System.Drawing.Size(331, 30);
            this.select_referenceTB.TabIndex = 15;
            // 
            // linecapPB
            // 
            this.linecapPB.Location = new System.Drawing.Point(400, 345);
            this.linecapPB.Name = "linecapPB";
            this.linecapPB.Size = new System.Drawing.Size(247, 111);
            this.linecapPB.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.linecapPB.TabIndex = 17;
            this.linecapPB.TabStop = false;
            // 
            // interpolation_methodCB
            // 
            this.interpolation_methodCB.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.interpolation_methodCB.FormattingEnabled = true;
            this.interpolation_methodCB.Location = new System.Drawing.Point(139, 182);
            this.interpolation_methodCB.Name = "interpolation_methodCB";
            this.interpolation_methodCB.Size = new System.Drawing.Size(146, 28);
            this.interpolation_methodCB.TabIndex = 19;
            // 
            // interpolation_methodLb
            // 
            this.interpolation_methodLb.AutoSize = true;
            this.interpolation_methodLb.Font = new System.Drawing.Font("宋体", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.interpolation_methodLb.Location = new System.Drawing.Point(89, 124);
            this.interpolation_methodLb.Name = "interpolation_methodLb";
            this.interpolation_methodLb.Size = new System.Drawing.Size(250, 24);
            this.interpolation_methodLb.TabIndex = 18;
            this.interpolation_methodLb.Text = "Interpolation Method";
            // 
            // transformBtn
            // 
            this.transformBtn.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.transformBtn.Location = new System.Drawing.Point(447, 493);
            this.transformBtn.Name = "transformBtn";
            this.transformBtn.Size = new System.Drawing.Size(164, 69);
            this.transformBtn.TabIndex = 20;
            this.transformBtn.Text = "Transform!";
            this.transformBtn.UseVisualStyleBackColor = true;
            this.transformBtn.Click += new System.EventHandler(this.transformBtn_Click);
            // 
            // input_keypointsBtn
            // 
            this.input_keypointsBtn.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.input_keypointsBtn.Location = new System.Drawing.Point(139, 662);
            this.input_keypointsBtn.Name = "input_keypointsBtn";
            this.input_keypointsBtn.Size = new System.Drawing.Size(150, 69);
            this.input_keypointsBtn.TabIndex = 21;
            this.input_keypointsBtn.Text = "Show Keypoints";
            this.input_keypointsBtn.UseVisualStyleBackColor = true;
            this.input_keypointsBtn.Click += new System.EventHandler(this.input_keypointsBtn_Click);
            // 
            // result_keypointsBtn
            // 
            this.result_keypointsBtn.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.result_keypointsBtn.Location = new System.Drawing.Point(754, 662);
            this.result_keypointsBtn.Name = "result_keypointsBtn";
            this.result_keypointsBtn.Size = new System.Drawing.Size(150, 69);
            this.result_keypointsBtn.TabIndex = 22;
            this.result_keypointsBtn.Text = "Show Keypoints";
            this.result_keypointsBtn.UseVisualStyleBackColor = true;
            this.result_keypointsBtn.Click += new System.EventHandler(this.result_keypointsBtn_Click);
            // 
            // reference_keypointsBtn
            // 
            this.reference_keypointsBtn.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.reference_keypointsBtn.Location = new System.Drawing.Point(929, 176);
            this.reference_keypointsBtn.Name = "reference_keypointsBtn";
            this.reference_keypointsBtn.Size = new System.Drawing.Size(150, 69);
            this.reference_keypointsBtn.TabIndex = 23;
            this.reference_keypointsBtn.Text = "Show Keypoints";
            this.reference_keypointsBtn.UseVisualStyleBackColor = true;
            this.reference_keypointsBtn.Click += new System.EventHandler(this.reference_keypointsBtn_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ClientSize = new System.Drawing.Size(1132, 766);
            this.Controls.Add(this.reference_keypointsBtn);
            this.Controls.Add(this.result_keypointsBtn);
            this.Controls.Add(this.input_keypointsBtn);
            this.Controls.Add(this.transformBtn);
            this.Controls.Add(this.interpolation_methodCB);
            this.Controls.Add(this.interpolation_methodLb);
            this.Controls.Add(this.linecapPB);
            this.Controls.Add(this.select_referenceBtn);
            this.Controls.Add(this.select_referenceTB);
            this.Controls.Add(this.reference_imagePB);
            this.Controls.Add(this.save_resultBtn);
            this.Controls.Add(this.save_imageTB);
            this.Controls.Add(this.output_imagePB);
            this.Controls.Add(this.input_imageBtn);
            this.Controls.Add(this.input_imageTB);
            this.Controls.Add(this.input_imagePB);
            this.Controls.Add(this.authorLb);
            this.Controls.Add(this.titleLb);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.output_imagePB)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.input_imagePB)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.reference_imagePB)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.linecapPB)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label authorLb;
        private System.Windows.Forms.Label titleLb;
        private System.Windows.Forms.Button save_resultBtn;
        private System.Windows.Forms.TextBox save_imageTB;
        private System.Windows.Forms.PictureBox output_imagePB;
        private System.Windows.Forms.Button input_imageBtn;
        private System.Windows.Forms.TextBox input_imageTB;
        private System.Windows.Forms.PictureBox input_imagePB;
        private System.Windows.Forms.PictureBox reference_imagePB;
        private System.Windows.Forms.Button select_referenceBtn;
        private System.Windows.Forms.TextBox select_referenceTB;
        private System.Windows.Forms.PictureBox linecapPB;
        private System.Windows.Forms.ComboBox interpolation_methodCB;
        private System.Windows.Forms.Label interpolation_methodLb;
        private System.Windows.Forms.Button transformBtn;
        private System.Windows.Forms.Button input_keypointsBtn;
        private System.Windows.Forms.Button result_keypointsBtn;
        private System.Windows.Forms.Button reference_keypointsBtn;
    }
}

