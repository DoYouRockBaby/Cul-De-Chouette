namespace Client
{
    partial class Form1
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.GrelotteCaPicoteButton = new System.Windows.Forms.Button();
            this.PasMouLeCaillouButton = new System.Windows.Forms.Button();
            this.ThrowCubesButton = new System.Windows.Forms.Button();
            this.ListBox = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // GrelotteCaPicoteButton
            // 
            this.GrelotteCaPicoteButton.Location = new System.Drawing.Point(12, 12);
            this.GrelotteCaPicoteButton.Name = "GrelotteCaPicoteButton";
            this.GrelotteCaPicoteButton.Size = new System.Drawing.Size(96, 62);
            this.GrelotteCaPicoteButton.TabIndex = 0;
            this.GrelotteCaPicoteButton.Text = "Grelotte ça picotte !";
            this.GrelotteCaPicoteButton.UseVisualStyleBackColor = true;
            this.GrelotteCaPicoteButton.Click += new System.EventHandler(this.GrelotteCaPicoteButton_Click);
            // 
            // PasMouLeCaillouButton
            // 
            this.PasMouLeCaillouButton.Location = new System.Drawing.Point(114, 12);
            this.PasMouLeCaillouButton.Name = "PasMouLeCaillouButton";
            this.PasMouLeCaillouButton.Size = new System.Drawing.Size(96, 62);
            this.PasMouLeCaillouButton.TabIndex = 1;
            this.PasMouLeCaillouButton.Text = "Pas mou le caillou !";
            this.PasMouLeCaillouButton.UseVisualStyleBackColor = true;
            this.PasMouLeCaillouButton.Click += new System.EventHandler(this.PasMouLeCaillouButton_Click);
            // 
            // ThrowCubesButton
            // 
            this.ThrowCubesButton.Location = new System.Drawing.Point(216, 12);
            this.ThrowCubesButton.Name = "ThrowCubesButton";
            this.ThrowCubesButton.Size = new System.Drawing.Size(96, 62);
            this.ThrowCubesButton.TabIndex = 2;
            this.ThrowCubesButton.Text = "Jeter les dés";
            this.ThrowCubesButton.UseVisualStyleBackColor = true;
            this.ThrowCubesButton.Click += new System.EventHandler(this.ThrowCubesButton_Click);
            // 
            // ListBox
            // 
            this.ListBox.FormattingEnabled = true;
            this.ListBox.Location = new System.Drawing.Point(13, 81);
            this.ListBox.Name = "ListBox";
            this.ListBox.Size = new System.Drawing.Size(299, 290);
            this.ListBox.TabIndex = 3;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(324, 380);
            this.Controls.Add(this.ListBox);
            this.Controls.Add(this.ThrowCubesButton);
            this.Controls.Add(this.PasMouLeCaillouButton);
            this.Controls.Add(this.GrelotteCaPicoteButton);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button GrelotteCaPicoteButton;
        private System.Windows.Forms.Button PasMouLeCaillouButton;
        private System.Windows.Forms.Button ThrowCubesButton;
        private System.Windows.Forms.ListBox ListBox;
    }
}

