namespace ImageMorphing
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.input_imagePB = new System.Windows.Forms.PictureBox();
            this.input_imageTB = new System.Windows.Forms.TextBox();
            this.input_imageBtn = new System.Windows.Forms.Button();
            this.save_resultBtn = new System.Windows.Forms.Button();
            this.save_imageTB = new System.Windows.Forms.TextBox();
            this.output_imagePB = new System.Windows.Forms.PictureBox();
            this.label3 = new System.Windows.Forms.Label();
            this.transform_angleTB = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.transform_typeCB = new System.Windows.Forms.ComboBox();
            this.interpolation_methodCB = new System.Windows.Forms.ComboBox();
            this.transform_radiusTB = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.transformBtn = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.input_imagePB)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.output_imagePB)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 25.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(376, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(525, 43);
            this.label1.TabIndex = 0;
            this.label1.Text = "Image Morphing Project";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(650, 103);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(252, 28);
            this.label2.TabIndex = 1;
            this.label2.Text = "Presented by wzy";
            // 
            // input_imagePB
            // 
            this.input_imagePB.Location = new System.Drawing.Point(310, 157);
            this.input_imagePB.Name = "input_imagePB";
            this.input_imagePB.Size = new System.Drawing.Size(450, 450);
            this.input_imagePB.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.input_imagePB.TabIndex = 2;
            this.input_imagePB.TabStop = false;
            // 
            // input_imageTB
            // 
            this.input_imageTB.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.input_imageTB.Location = new System.Drawing.Point(310, 652);
            this.input_imageTB.Name = "input_imageTB";
            this.input_imageTB.Size = new System.Drawing.Size(273, 30);
            this.input_imageTB.TabIndex = 3;
            // 
            // input_imageBtn
            // 
            this.input_imageBtn.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.input_imageBtn.Location = new System.Drawing.Point(606, 638);
            this.input_imageBtn.Name = "input_imageBtn";
            this.input_imageBtn.Size = new System.Drawing.Size(121, 69);
            this.input_imageBtn.TabIndex = 4;
            this.input_imageBtn.Text = "Input Image";
            this.input_imageBtn.UseVisualStyleBackColor = true;
            this.input_imageBtn.Click += new System.EventHandler(this.input_imageBtn_Click);
            // 
            // save_resultBtn
            // 
            this.save_resultBtn.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.save_resultBtn.Location = new System.Drawing.Point(1085, 638);
            this.save_resultBtn.Name = "save_resultBtn";
            this.save_resultBtn.Size = new System.Drawing.Size(121, 69);
            this.save_resultBtn.TabIndex = 7;
            this.save_resultBtn.Text = "Save Result";
            this.save_resultBtn.UseVisualStyleBackColor = true;
            this.save_resultBtn.Click += new System.EventHandler(this.save_resultBtn_Click);
            // 
            // save_imageTB
            // 
            this.save_imageTB.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.save_imageTB.Location = new System.Drawing.Point(789, 652);
            this.save_imageTB.Name = "save_imageTB";
            this.save_imageTB.Size = new System.Drawing.Size(273, 30);
            this.save_imageTB.TabIndex = 6;
            // 
            // output_imagePB
            // 
            this.output_imagePB.Location = new System.Drawing.Point(789, 157);
            this.output_imagePB.Name = "output_imagePB";
            this.output_imagePB.Size = new System.Drawing.Size(450, 450);
            this.output_imagePB.TabIndex = 5;
            this.output_imagePB.TabStop = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(59, 349);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(190, 24);
            this.label3.TabIndex = 8;
            this.label3.Text = "Transform Angle";
            // 
            // transform_angleTB
            // 
            this.transform_angleTB.Font = new System.Drawing.Font("宋体", 12F);
            this.transform_angleTB.Location = new System.Drawing.Point(166, 403);
            this.transform_angleTB.Name = "transform_angleTB";
            this.transform_angleTB.Size = new System.Drawing.Size(83, 30);
            this.transform_angleTB.TabIndex = 9;
            this.transform_angleTB.Text = "0.0";
            this.transform_angleTB.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("宋体", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(21, 236);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(250, 24);
            this.label4.TabIndex = 10;
            this.label4.Text = "Interpolation Method";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("宋体", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.Location = new System.Drawing.Point(71, 130);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(178, 24);
            this.label5.TabIndex = 11;
            this.label5.Text = "Transform Type";
            // 
            // transform_typeCB
            // 
            this.transform_typeCB.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.transform_typeCB.FormattingEnabled = true;
            this.transform_typeCB.Location = new System.Drawing.Point(103, 180);
            this.transform_typeCB.Name = "transform_typeCB";
            this.transform_typeCB.Size = new System.Drawing.Size(146, 28);
            this.transform_typeCB.TabIndex = 12;
            // 
            // interpolation_methodCB
            // 
            this.interpolation_methodCB.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.interpolation_methodCB.FormattingEnabled = true;
            this.interpolation_methodCB.Location = new System.Drawing.Point(103, 290);
            this.interpolation_methodCB.Name = "interpolation_methodCB";
            this.interpolation_methodCB.Size = new System.Drawing.Size(146, 28);
            this.interpolation_methodCB.TabIndex = 13;
            // 
            // transform_radiusTB
            // 
            this.transform_radiusTB.Font = new System.Drawing.Font("宋体", 12F);
            this.transform_radiusTB.Location = new System.Drawing.Point(166, 525);
            this.transform_radiusTB.Name = "transform_radiusTB";
            this.transform_radiusTB.Size = new System.Drawing.Size(83, 30);
            this.transform_radiusTB.TabIndex = 15;
            this.transform_radiusTB.Text = "0.0";
            this.transform_radiusTB.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("宋体", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.Location = new System.Drawing.Point(47, 464);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(202, 24);
            this.label6.TabIndex = 14;
            this.label6.Text = "Transform Radius";
            // 
            // transformBtn
            // 
            this.transformBtn.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.transformBtn.Location = new System.Drawing.Point(75, 587);
            this.transformBtn.Name = "transformBtn";
            this.transformBtn.Size = new System.Drawing.Size(157, 69);
            this.transformBtn.TabIndex = 16;
            this.transformBtn.Text = "Transform!";
            this.transformBtn.UseVisualStyleBackColor = true;
            this.transformBtn.Click += new System.EventHandler(this.transformBtn_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ClientSize = new System.Drawing.Size(1296, 755);
            this.Controls.Add(this.transformBtn);
            this.Controls.Add(this.transform_radiusTB);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.interpolation_methodCB);
            this.Controls.Add(this.transform_typeCB);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.transform_angleTB);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.save_resultBtn);
            this.Controls.Add(this.save_imageTB);
            this.Controls.Add(this.output_imagePB);
            this.Controls.Add(this.input_imageBtn);
            this.Controls.Add(this.input_imageTB);
            this.Controls.Add(this.input_imagePB);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.input_imagePB)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.output_imagePB)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox input_imagePB;
        private System.Windows.Forms.TextBox input_imageTB;
        private System.Windows.Forms.Button input_imageBtn;
        private System.Windows.Forms.Button save_resultBtn;
        private System.Windows.Forms.TextBox save_imageTB;
        private System.Windows.Forms.PictureBox output_imagePB;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox transform_angleTB;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox transform_typeCB;
        private System.Windows.Forms.ComboBox interpolation_methodCB;
        private System.Windows.Forms.TextBox transform_radiusTB;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button transformBtn;
    }
}

