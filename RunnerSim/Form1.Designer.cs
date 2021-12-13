namespace RunnerSim
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.pictureBoxCanvas = new System.Windows.Forms.PictureBox();
            this.numericUpDownRunners = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.numericUpDownTracks = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.timerRace = new System.Windows.Forms.Timer(this.components);
            this.buttonStart = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxCanvas)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownRunners)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTracks)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBoxCanvas
            // 
            this.pictureBoxCanvas.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.pictureBoxCanvas.Location = new System.Drawing.Point(8, 8);
            this.pictureBoxCanvas.Name = "pictureBoxCanvas";
            this.pictureBoxCanvas.Size = new System.Drawing.Size(600, 656);
            this.pictureBoxCanvas.TabIndex = 0;
            this.pictureBoxCanvas.TabStop = false;
            this.pictureBoxCanvas.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBoxCanvas_Paint);
            // 
            // numericUpDownRunners
            // 
            this.numericUpDownRunners.Location = new System.Drawing.Point(720, 8);
            this.numericUpDownRunners.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDownRunners.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownRunners.Name = "numericUpDownRunners";
            this.numericUpDownRunners.Size = new System.Drawing.Size(120, 23);
            this.numericUpDownRunners.TabIndex = 1;
            this.numericUpDownRunners.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownRunners.ValueChanged += new System.EventHandler(this.numericUpDownRunners_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(616, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(93, 15);
            this.label1.TabIndex = 2;
            this.label1.Text = "Кол-во бегунов";
            // 
            // numericUpDownTracks
            // 
            this.numericUpDownTracks.Location = new System.Drawing.Point(720, 56);
            this.numericUpDownTracks.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDownTracks.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownTracks.Name = "numericUpDownTracks";
            this.numericUpDownTracks.Size = new System.Drawing.Size(120, 23);
            this.numericUpDownTracks.TabIndex = 1;
            this.numericUpDownTracks.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownTracks.ValueChanged += new System.EventHandler(this.numericUpDownTracks_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(616, 56);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(97, 15);
            this.label2.TabIndex = 2;
            this.label2.Text = "Кол-во дорожек";
            // 
            // timerRace
            // 
            this.timerRace.Tick += new System.EventHandler(this.timerRace_Tick);
            // 
            // buttonStart
            // 
            this.buttonStart.Location = new System.Drawing.Point(616, 96);
            this.buttonStart.Name = "buttonStart";
            this.buttonStart.Size = new System.Drawing.Size(224, 23);
            this.buttonStart.TabIndex = 3;
            this.buttonStart.Text = "Старт";
            this.buttonStart.UseVisualStyleBackColor = true;
            this.buttonStart.Click += new System.EventHandler(this.buttonStart_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1033, 676);
            this.Controls.Add(this.buttonStart);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.numericUpDownTracks);
            this.Controls.Add(this.numericUpDownRunners);
            this.Controls.Add(this.pictureBoxCanvas);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxCanvas)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownRunners)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTracks)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBoxCanvas;
        private System.Windows.Forms.NumericUpDown numericUpDownRunners;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown numericUpDownTracks;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Timer timerRace;
        private System.Windows.Forms.Button buttonStart;
    }
}