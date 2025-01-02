using System;
using System.Collections.Generic;
using System.Security.Policy;
using System.Windows.Forms;

namespace Pong
{
    partial class Form1
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private const int topBorder = 10;
        private const int bottomBorder = 50;
        private const int PaddleSpeed = 7;
        private int BallX = 0;
        private int BallY = 0;
        private int Player1 = 0;
        private int Player2 = 0;
        private Timer gameTimer = new Timer();
        private HashSet<Keys> pressedKeys = new HashSet<Keys>();

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
            if (e.KeyCode == Keys.R && this.RestartLabel.Visible == true)
            {
                this.restartGame();
            }
            else
            {
                pressedKeys.Add(e.KeyCode);
            }
            
        }

        protected override void OnKeyUp(KeyEventArgs e)
        {
            base.OnKeyUp(e);
            pressedKeys.Remove(e.KeyCode);
        }

        private void movePaddles(bool SoloPlayer)
        {
            if (SoloPlayer)
            {
                if (pressedKeys.Contains(Keys.W) && this.LeftPaddle.Top > topBorder)
                {
                    Console.WriteLine("Moving Left Paddle Up");
                    this.LeftPaddle.Top -= PaddleSpeed;
                }
                if (pressedKeys.Contains(Keys.S) && this.LeftPaddle.Bottom < this.Height - bottomBorder)
                {
                    Console.WriteLine("Moving Left Paddle Down");
                    this.LeftPaddle.Top += PaddleSpeed;
                }
                this.AI_Move();
            }
            else
            {
                if (pressedKeys.Contains(Keys.W) && this.LeftPaddle.Top > topBorder)
                {
                    this.LeftPaddle.Top -= PaddleSpeed;
                }
                if (pressedKeys.Contains(Keys.S) && this.LeftPaddle.Bottom < this.Height - bottomBorder)
                {
                    this.LeftPaddle.Top += PaddleSpeed;
                }
                if (pressedKeys.Contains(Keys.Up) && this.RightPaddle.Top > topBorder)
                {
                    this.RightPaddle.Top -= PaddleSpeed;
                }
                if (pressedKeys.Contains(Keys.Down) && this.RightPaddle.Bottom < this.Height - bottomBorder)
                {
                    this.RightPaddle.Top += PaddleSpeed;
                }
            }
        }

        private void AI_Move()
        {
            int MoveSpeed = 7;

            if(this.Ball.Top < this.RightPaddle.Top && this.RightPaddle.Top > topBorder)
            {
                this.RightPaddle.Top -= MoveSpeed;
            }
            if(this.Ball.Bottom > this.RightPaddle.Bottom && this.RightPaddle.Bottom < this.Height - bottomBorder)
            {
                this.RightPaddle.Top += MoveSpeed;
            }
        }

        private void restartGame()
        {
            this.Player1 = 0;
            this.Player2 = 0;
            this.RestartLabel.Visible = false;
            this.WinnerTag.Visible = false;
            this.PlayerMenu.Visible = true;
            this.Player1Ten.Visible = false;
            this.updateScore();
        }

        private void moveBall()
        {
            this.Ball.Left += BallX;
            this.Ball.Top += BallY;

            if(this.Ball.Top <= 0 + topBorder || this.Ball.Bottom >= this.Height - bottomBorder)
            {
                this.BallY = -this.BallY;
            }

            if(this.Ball.Right < 0)
            {
                this.Player2 += 1;
                this.resetBall();
                this.updateScore();
            }
            if(this.Ball.Left >= this.Width)
            {
                this.Player1 += 1;
                if(this.Player1 >= 10)
                {
                    this.Player1Ten.Visible = true;
                }
                this.Player1 %= 10;
                this.resetBall();
                this.updateScore();
            }

        }

        private void checkCollision()
        {
            if(this.Ball.Bounds.IntersectsWith(this.LeftPaddle.Bounds) || this.Ball.Bounds.IntersectsWith(this.RightPaddle.Bounds))
            {
                this.BallX = -this.BallX;
            }
        }

        private void updateScore()
        {
            this.Score.Text = this.Player1.ToString() + " - " + this.Player2.ToString();
            
            if(this.Player1 == 1 && this.Player1Ten.Visible)
            {
                this.WinnerTag.Text = "Player 1 - WINS !";
                this.BallX = 0;
                this.BallY = 0;
                this.WinnerTag.Visible = true;
                this.RestartLabel.Visible = true;
            }
            if(this.Player2 == 11)
            {
                this.WinnerTag.Text = "Player 2 - WINS !";
                this.BallX = 0;
                this.BallY = 0;
                this.WinnerTag.Visible= true;
                this.RestartLabel.Visible = true;
            }

        }

        private void resetBall()
        {
            this.Ball.Top = this.Height / 2 - this.Ball.Height / 2;
            this.Ball.Left = this.Width / 2 - this.Ball.Width / 2;
            this.BallX = -this.BallX;
        }

        private void GameTimer_Tick(object sender, EventArgs e)
        {
            this.movePaddles(this.SoloPlayer.Checked);
            this.moveBall();
            this.checkCollision();
            Invalidate();
        }

        private void StartGame(object sender, EventArgs e)
        {
            this.BallX = 8;
            this.BallY = 8;
            this.PlayerMenu.Visible = false;
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.LeftPaddle = new System.Windows.Forms.Panel();
            this.Ball = new System.Windows.Forms.Panel();
            this.RightPaddle = new System.Windows.Forms.Panel();
            this.gameTimer = new System.Windows.Forms.Timer(this.components);
            this.Player1Score = new System.Windows.Forms.Label();
            this.WinnerTag = new System.Windows.Forms.Label();
            this.RestartLabel = new System.Windows.Forms.Label();
            this.Score = new System.Windows.Forms.Label();
            this.MidField = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.PlayerMenu = new System.Windows.Forms.GroupBox();
            this.StartButton = new System.Windows.Forms.Button();
            this.TwoPlayer = new System.Windows.Forms.RadioButton();
            this.SoloPlayer = new System.Windows.Forms.RadioButton();
            this.Player1Ten = new System.Windows.Forms.Label();
            this.PlayerMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // LeftPaddle
            // 
            this.LeftPaddle.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.LeftPaddle.Location = new System.Drawing.Point(15, 195);
            this.LeftPaddle.Name = "LeftPaddle";
            this.LeftPaddle.Size = new System.Drawing.Size(10, 65);
            this.LeftPaddle.TabIndex = 1;
            // 
            // Ball
            // 
            this.Ball.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.Ball.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Ball.Location = new System.Drawing.Point(395, 227);
            this.Ball.Name = "Ball";
            this.Ball.Size = new System.Drawing.Size(10, 10);
            this.Ball.TabIndex = 2;
            // 
            // RightPaddle
            // 
            this.RightPaddle.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.RightPaddle.Location = new System.Drawing.Point(760, 195);
            this.RightPaddle.Name = "RightPaddle";
            this.RightPaddle.Size = new System.Drawing.Size(10, 65);
            this.RightPaddle.TabIndex = 3;
            // 
            // gameTimer
            // 
            this.gameTimer.Enabled = true;
            this.gameTimer.Interval = 20;
            this.gameTimer.Tick += new System.EventHandler(this.GameTimer_Tick);
            // 
            // Player1Score
            // 
            this.Player1Score.Location = new System.Drawing.Point(0, 0);
            this.Player1Score.Name = "Player1Score";
            this.Player1Score.Size = new System.Drawing.Size(100, 23);
            this.Player1Score.TabIndex = 8;
            // 
            // WinnerTag
            // 
            this.WinnerTag.AutoSize = true;
            this.WinnerTag.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.WinnerTag.Location = new System.Drawing.Point(360, 211);
            this.WinnerTag.Name = "WinnerTag";
            this.WinnerTag.Size = new System.Drawing.Size(27, 13);
            this.WinnerTag.TabIndex = 6;
            this.WinnerTag.Text = "This";
            this.WinnerTag.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.WinnerTag.Visible = false;
            // 
            // RestartLabel
            // 
            this.RestartLabel.AutoSize = true;
            this.RestartLabel.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.RestartLabel.Location = new System.Drawing.Point(342, 260);
            this.RestartLabel.Name = "RestartLabel";
            this.RestartLabel.Size = new System.Drawing.Size(124, 13);
            this.RestartLabel.TabIndex = 7;
            this.RestartLabel.Text = "Press \'R\' to restart game.";
            this.RestartLabel.Visible = false;
            // 
            // Score
            // 
            this.Score.AutoSize = true;
            this.Score.Font = new System.Drawing.Font("Gill Sans Ultra Bold", 54F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Score.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.Score.Location = new System.Drawing.Point(283, 10);
            this.Score.Name = "Score";
            this.Score.Size = new System.Drawing.Size(243, 102);
            this.Score.TabIndex = 4;
            this.Score.Text = "0 - 0";
            this.Score.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // MidField
            // 
            this.MidField.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.MidField.ForeColor = System.Drawing.SystemColors.ControlText;
            this.MidField.Location = new System.Drawing.Point(395, 0);
            this.MidField.Name = "MidField";
            this.MidField.Size = new System.Drawing.Size(10, 38);
            this.MidField.TabIndex = 9;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.panel1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.panel1.Location = new System.Drawing.Point(395, 87);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(10, 38);
            this.panel1.TabIndex = 10;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.panel2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.panel2.Location = new System.Drawing.Point(395, 164);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(10, 38);
            this.panel2.TabIndex = 11;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.panel3.ForeColor = System.Drawing.SystemColors.ControlText;
            this.panel3.Location = new System.Drawing.Point(395, 251);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(10, 38);
            this.panel3.TabIndex = 12;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.panel4.ForeColor = System.Drawing.SystemColors.ControlText;
            this.panel4.Location = new System.Drawing.Point(395, 334);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(10, 38);
            this.panel4.TabIndex = 11;
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.panel5.ForeColor = System.Drawing.SystemColors.ControlText;
            this.panel5.Location = new System.Drawing.Point(395, 421);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(10, 38);
            this.panel5.TabIndex = 12;
            // 
            // PlayerMenu
            // 
            this.PlayerMenu.BackColor = System.Drawing.SystemColors.MenuText;
            this.PlayerMenu.Controls.Add(this.StartButton);
            this.PlayerMenu.Controls.Add(this.TwoPlayer);
            this.PlayerMenu.Controls.Add(this.SoloPlayer);
            this.PlayerMenu.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.PlayerMenu.Location = new System.Drawing.Point(300, 145);
            this.PlayerMenu.Name = "PlayerMenu";
            this.PlayerMenu.Size = new System.Drawing.Size(200, 100);
            this.PlayerMenu.TabIndex = 13;
            this.PlayerMenu.TabStop = false;
            this.PlayerMenu.Text = "Player menu";
            // 
            // StartButton
            // 
            this.StartButton.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.StartButton.Location = new System.Drawing.Point(63, 63);
            this.StartButton.Name = "StartButton";
            this.StartButton.Size = new System.Drawing.Size(75, 23);
            this.StartButton.TabIndex = 2;
            this.StartButton.Text = "START";
            this.StartButton.UseVisualStyleBackColor = true;
            this.StartButton.Click += new System.EventHandler(this.StartGame);
            // 
            // TwoPlayer
            // 
            this.TwoPlayer.AutoSize = true;
            this.TwoPlayer.Location = new System.Drawing.Point(111, 22);
            this.TwoPlayer.Name = "TwoPlayer";
            this.TwoPlayer.Size = new System.Drawing.Size(77, 17);
            this.TwoPlayer.TabIndex = 1;
            this.TwoPlayer.TabStop = true;
            this.TwoPlayer.Text = "Two player";
            this.TwoPlayer.UseVisualStyleBackColor = true;
            // 
            // SoloPlayer
            // 
            this.SoloPlayer.AutoSize = true;
            this.SoloPlayer.Location = new System.Drawing.Point(17, 22);
            this.SoloPlayer.Name = "SoloPlayer";
            this.SoloPlayer.Size = new System.Drawing.Size(77, 17);
            this.SoloPlayer.TabIndex = 0;
            this.SoloPlayer.TabStop = true;
            this.SoloPlayer.Text = "Solo player";
            this.SoloPlayer.UseVisualStyleBackColor = true;
            this.SoloPlayer.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // Player1Ten
            // 
            this.Player1Ten.AutoSize = true;
            this.Player1Ten.Font = new System.Drawing.Font("Gill Sans Ultra Bold", 54F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Player1Ten.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.Player1Ten.Location = new System.Drawing.Point(218, 10);
            this.Player1Ten.Name = "Player1Ten";
            this.Player1Ten.Size = new System.Drawing.Size(104, 102);
            this.Player1Ten.TabIndex = 14;
            this.Player1Ten.Text = "1";
            this.Player1Ten.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.Player1Ten.Visible = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.WindowText;
            this.ClientSize = new System.Drawing.Size(784, 451);
            this.Controls.Add(this.PlayerMenu);
            this.Controls.Add(this.WinnerTag);
            this.Controls.Add(this.LeftPaddle);
            this.Controls.Add(this.RightPaddle);
            this.Controls.Add(this.Ball);
            this.Controls.Add(this.RestartLabel);
            this.Controls.Add(this.Score);
            this.Controls.Add(this.Player1Score);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.MidField);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.Player1Ten);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Pong";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.PlayerMenu.ResumeLayout(false);
            this.PlayerMenu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void GameTimer_Tick1(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        #endregion
        private System.Windows.Forms.Panel LeftPaddle;
        private System.Windows.Forms.Panel Ball;
        private System.Windows.Forms.Panel RightPaddle;
        private Label Player1Score;
        private Label WinnerTag;
        private Label RestartLabel;
        private Label Score;
        private Panel MidField;
        private Panel panel1;
        private Panel panel2;
        private Panel panel3;
        private Panel panel4;
        private Panel panel5;
        private GroupBox PlayerMenu;
        private RadioButton TwoPlayer;
        private RadioButton SoloPlayer;
        private Button StartButton;
        private Label Player1Ten;
    }
}

