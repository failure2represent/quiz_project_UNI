namespace quiz_gui
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
        /* <param name="disposing">true if managed resources should be disposed; otherwise, false.</param> */
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            startButton = new Button();
            scoreButton = new Button();
            exitButton = new Button();
            pictureBox1 = new PictureBox();
            btmButton = new Button();
            sbGrid = new DataGridView();
            nextQuButton = new Button();
            questionLabel = new Label();
            optionALabel = new Label();
            optionBLabel = new Label();
            optionCLabel = new Label();
            optionDLabel = new Label();
            answerTextBox = new TextBox();
            usernameOkButton = new Button();
            usernameLabel = new Label();
            usernameTextBox = new TextBox();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)sbGrid).BeginInit();
            SuspendLayout();
            // 
            // startButton
            // 
            startButton.Location = new Point(262, 251);
            startButton.Name = "startButton";
            startButton.Size = new Size(266, 41);
            startButton.TabIndex = 0;
            startButton.Text = "START";
            startButton.UseVisualStyleBackColor = true;
            startButton.Click += startButton_Click;
            // 
            // scoreButton
            // 
            scoreButton.Location = new Point(262, 298);
            scoreButton.Name = "scoreButton";
            scoreButton.Size = new Size(266, 41);
            scoreButton.TabIndex = 1;
            scoreButton.Text = "SCORE";
            scoreButton.UseVisualStyleBackColor = true;
            scoreButton.Click += scoreButton_Click;
            // 
            // exitButton
            // 
            exitButton.Location = new Point(262, 345);
            exitButton.Name = "exitButton";
            exitButton.Size = new Size(266, 41);
            exitButton.TabIndex = 2;
            exitButton.Text = "EXIT";
            exitButton.UseVisualStyleBackColor = true;
            exitButton.Click += exitButton_Click;
            // 
            // pictureBox1
            // 
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(195, 117);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(404, 100);
            pictureBox1.TabIndex = 3;
            pictureBox1.TabStop = false;
            // 
            // btmButton
            // 
            btmButton.Location = new Point(262, 345);
            btmButton.Name = "btmButton";
            btmButton.Size = new Size(266, 41);
            btmButton.TabIndex = 5;
            btmButton.Text = "MAIN MENU";
            btmButton.UseVisualStyleBackColor = true;
            btmButton.Visible = false;
            btmButton.Click += btmButton_Click;
            // 
            // sbGrid
            // 
            sbGrid.BackgroundColor = SystemColors.Desktop;
            sbGrid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            sbGrid.Location = new Point(262, 55);
            sbGrid.Name = "sbGrid";
            sbGrid.Size = new Size(266, 284);
            sbGrid.TabIndex = 6;
            sbGrid.Visible = false;
            // 
            // nextQuButton
            // 
            nextQuButton.Location = new Point(262, 298);
            nextQuButton.Name = "nextQuButton";
            nextQuButton.Size = new Size(266, 41);
            nextQuButton.TabIndex = 7;
            nextQuButton.Text = "NEXT";
            nextQuButton.UseVisualStyleBackColor = true;
            nextQuButton.Visible = false;
            nextQuButton.Click += nextQuButton_Click;
            // 
            // questionLabel
            // 
            questionLabel.Location = new Point(204, 90);
            questionLabel.Name = "questionLabel";
            questionLabel.Size = new Size(386, 44);
            questionLabel.TabIndex = 8;
            questionLabel.TextAlign = ContentAlignment.TopCenter;
            questionLabel.Visible = false;
            // 
            // optionALabel
            // 
            optionALabel.AutoSize = true;
            optionALabel.Location = new Point(323, 147);
            optionALabel.Name = "optionALabel";
            optionALabel.Size = new Size(0, 13);
            optionALabel.TabIndex = 9;
            optionALabel.Visible = false;
            // 
            // optionBLabel
            // 
            optionBLabel.AutoSize = true;
            optionBLabel.Location = new Point(323, 160);
            optionBLabel.Name = "optionBLabel";
            optionBLabel.Size = new Size(0, 13);
            optionBLabel.TabIndex = 10;
            optionBLabel.Visible = false;
            // 
            // optionCLabel
            // 
            optionCLabel.AutoSize = true;
            optionCLabel.Location = new Point(323, 174);
            optionCLabel.Name = "optionCLabel";
            optionCLabel.Size = new Size(0, 13);
            optionCLabel.TabIndex = 11;
            optionCLabel.Visible = false;
            // 
            // optionDLabel
            // 
            optionDLabel.AutoSize = true;
            optionDLabel.Location = new Point(323, 188);
            optionDLabel.Name = "optionDLabel";
            optionDLabel.Size = new Size(0, 13);
            optionDLabel.TabIndex = 12;
            optionDLabel.Visible = false;
            // 
            // answerTextBox
            // 
            answerTextBox.Location = new Point(323, 223);
            answerTextBox.Name = "answerTextBox";
            answerTextBox.Size = new Size(152, 21);
            answerTextBox.TabIndex = 13;
            answerTextBox.Visible = false;
            // 
            // usernameOkButton
            // 
            usernameOkButton.Location = new Point(262, 298);
            usernameOkButton.Name = "usernameOkButton";
            usernameOkButton.Size = new Size(266, 41);
            usernameOkButton.TabIndex = 14;
            usernameOkButton.Text = "OK";
            usernameOkButton.UseVisualStyleBackColor = true;
            usernameOkButton.Visible = false;
            usernameOkButton.Click += usernameOkButton_Click;
            // 
            // usernameLabel
            // 
            usernameLabel.AutoSize = true;
            usernameLabel.Location = new Point(354, 188);
            usernameLabel.Name = "usernameLabel";
            usernameLabel.Size = new Size(97, 13);
            usernameLabel.TabIndex = 15;
            usernameLabel.Text = "Zadejte přezdívku:";
            usernameLabel.Visible = false;
            // 
            // usernameTextBox
            // 
            usernameTextBox.Location = new Point(323, 223);
            usernameTextBox.Name = "usernameTextBox";
            usernameTextBox.Size = new Size(152, 21);
            usernameTextBox.TabIndex = 16;
            usernameTextBox.Visible = false;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(6F, 13F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(usernameTextBox);
            Controls.Add(usernameLabel);
            Controls.Add(usernameOkButton);
            Controls.Add(answerTextBox);
            Controls.Add(optionDLabel);
            Controls.Add(optionCLabel);
            Controls.Add(optionBLabel);
            Controls.Add(optionALabel);
            Controls.Add(questionLabel);
            Controls.Add(nextQuButton);
            Controls.Add(sbGrid);
            Controls.Add(btmButton);
            Controls.Add(pictureBox1);
            Controls.Add(exitButton);
            Controls.Add(scoreButton);
            Controls.Add(startButton);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "Form1";
            Text = "QUIZ";
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)sbGrid).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button startButton;
        private Button scoreButton;
        private Button exitButton;
        private PictureBox pictureBox1;
        private Button btmButton;
        private DataGridView sbGrid;
        private Button nextQuButton;
        private Label questionLabel;
        private Label optionALabel;
        private Label optionBLabel;
        private Label optionCLabel;
        private Label optionDLabel;
        private TextBox answerTextBox;
        private Button usernameOkButton;
        private Label usernameLabel;
        private TextBox usernameTextBox;
    }
}
