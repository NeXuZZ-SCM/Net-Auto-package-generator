namespace AutoZipDeploy
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
            components = new System.ComponentModel.Container();
            BtnOpenDirectory = new Button();
            lblCompleteFolderPath = new Label();
            BtnZipFolder = new Button();
            ChkBLOptionsDelete = new CheckedListBox();
            TxtBoxNameProject = new TextBox();
            ChkBLOptionsBackup = new CheckedListBox();
            LblExcludeFiles = new Label();
            LblZipedType = new Label();
            groupBox1 = new GroupBox();
            groupBox2 = new GroupBox();
            toolTip1 = new ToolTip(components);
            groupBox3 = new GroupBox();
            LblVistaPreviaNamePackage = new Label();
            toolTip2 = new ToolTip(components);
            CmbboxProjectName = new ComboBox();
            groupBox4 = new GroupBox();
            groupBox5 = new GroupBox();
            LblDestinyFolder = new Label();
            BtnDestinyFolder = new Button();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            groupBox3.SuspendLayout();
            groupBox4.SuspendLayout();
            groupBox5.SuspendLayout();
            SuspendLayout();
            // 
            // BtnOpenDirectory
            // 
            BtnOpenDirectory.Location = new Point(12, 60);
            BtnOpenDirectory.Name = "BtnOpenDirectory";
            BtnOpenDirectory.Size = new Size(323, 29);
            BtnOpenDirectory.TabIndex = 0;
            BtnOpenDirectory.Text = "1- Seleccionar carpeta";
            BtnOpenDirectory.UseVisualStyleBackColor = true;
            BtnOpenDirectory.Click += BtnOpenDirectory_Click;
            // 
            // lblCompleteFolderPath
            // 
            lblCompleteFolderPath.AutoSize = true;
            lblCompleteFolderPath.Location = new Point(7, 19);
            lblCompleteFolderPath.MaximumSize = new Size(400, 20);
            lblCompleteFolderPath.Name = "lblCompleteFolderPath";
            lblCompleteFolderPath.Size = new Size(128, 15);
            lblCompleteFolderPath.TabIndex = 2;
            lblCompleteFolderPath.Text = "Seleccione una carpeta";
            // 
            // BtnZipFolder
            // 
            BtnZipFolder.Location = new Point(7, 175);
            BtnZipFolder.Name = "BtnZipFolder";
            BtnZipFolder.Size = new Size(306, 23);
            BtnZipFolder.TabIndex = 3;
            BtnZipFolder.Text = "2- Empaquetar";
            BtnZipFolder.UseVisualStyleBackColor = true;
            BtnZipFolder.Click += BtnZipFolder_Click;
            // 
            // ChkBLOptionsDelete
            // 
            ChkBLOptionsDelete.FormattingEnabled = true;
            ChkBLOptionsDelete.Location = new Point(6, 39);
            ChkBLOptionsDelete.Name = "ChkBLOptionsDelete";
            ChkBLOptionsDelete.Size = new Size(159, 130);
            ChkBLOptionsDelete.TabIndex = 4;
            ChkBLOptionsDelete.ItemCheck += ChkBLOptionsDelete_ItemCheck;
            // 
            // TxtBoxNameProject
            // 
            TxtBoxNameProject.Location = new Point(176, 22);
            TxtBoxNameProject.MaxLength = 200;
            TxtBoxNameProject.Name = "TxtBoxNameProject";
            TxtBoxNameProject.Size = new Size(141, 23);
            TxtBoxNameProject.TabIndex = 5;
            TxtBoxNameProject.TextChanged += TxtBoxNameProject_TextChanged;
            // 
            // ChkBLOptionsBackup
            // 
            ChkBLOptionsBackup.FormattingEnabled = true;
            ChkBLOptionsBackup.Location = new Point(176, 39);
            ChkBLOptionsBackup.Name = "ChkBLOptionsBackup";
            ChkBLOptionsBackup.Size = new Size(137, 76);
            ChkBLOptionsBackup.TabIndex = 8;
            ChkBLOptionsBackup.ItemCheck += ChkBLOptionsBackup_ItemCheck;
            // 
            // LblExcludeFiles
            // 
            LblExcludeFiles.AutoSize = true;
            LblExcludeFiles.Location = new Point(6, 19);
            LblExcludeFiles.Name = "LblExcludeFiles";
            LblExcludeFiles.Size = new Size(155, 15);
            LblExcludeFiles.TabIndex = 9;
            LblExcludeFiles.Text = "Archivos a excluir en deploy";
            // 
            // LblZipedType
            // 
            LblZipedType.AutoSize = true;
            LblZipedType.Location = new Point(176, 19);
            LblZipedType.Name = "LblZipedType";
            LblZipedType.Size = new Size(90, 15);
            LblZipedType.TabIndex = 10;
            LblZipedType.Text = "Tipo de zipeado";
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(ChkBLOptionsBackup);
            groupBox1.Controls.Add(LblExcludeFiles);
            groupBox1.Controls.Add(LblZipedType);
            groupBox1.Controls.Add(ChkBLOptionsDelete);
            groupBox1.Controls.Add(BtnZipFolder);
            groupBox1.Location = new Point(12, 300);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(323, 207);
            groupBox1.TabIndex = 11;
            groupBox1.TabStop = false;
            groupBox1.Text = "Opciones";
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(lblCompleteFolderPath);
            groupBox2.Location = new Point(12, 12);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(323, 42);
            groupBox2.TabIndex = 12;
            groupBox2.TabStop = false;
            groupBox2.Text = "Ruta del paquete publicado";
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(LblVistaPreviaNamePackage);
            groupBox3.Location = new Point(12, 179);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(323, 47);
            groupBox3.TabIndex = 13;
            groupBox3.TabStop = false;
            groupBox3.Text = "Vista previa del nombre del paquete";
            // 
            // LblVistaPreviaNamePackage
            // 
            LblVistaPreviaNamePackage.AutoSize = true;
            LblVistaPreviaNamePackage.Location = new Point(7, 19);
            LblVistaPreviaNamePackage.Name = "LblVistaPreviaNamePackage";
            LblVistaPreviaNamePackage.Size = new Size(274, 15);
            LblVistaPreviaNamePackage.TabIndex = 0;
            LblVistaPreviaNamePackage.Text = "Deploy/Backup Nombre_Del_Proyecto Fecha_Hora";
            // 
            // CmbboxProjectName
            // 
            CmbboxProjectName.DropDownStyle = ComboBoxStyle.DropDownList;
            CmbboxProjectName.FormattingEnabled = true;
            CmbboxProjectName.Location = new Point(7, 22);
            CmbboxProjectName.Name = "CmbboxProjectName";
            CmbboxProjectName.Size = new Size(158, 23);
            CmbboxProjectName.TabIndex = 15;
            CmbboxProjectName.SelectedIndexChanged += CmbboxProjectName_SelectedIndexChanged;
            // 
            // groupBox4
            // 
            groupBox4.Controls.Add(TxtBoxNameProject);
            groupBox4.Controls.Add(CmbboxProjectName);
            groupBox4.Location = new Point(12, 232);
            groupBox4.Name = "groupBox4";
            groupBox4.Size = new Size(323, 61);
            groupBox4.TabIndex = 16;
            groupBox4.TabStop = false;
            groupBox4.Text = "Nombre del proyecto";
            // 
            // groupBox5
            // 
            groupBox5.Controls.Add(LblDestinyFolder);
            groupBox5.Location = new Point(12, 96);
            groupBox5.Name = "groupBox5";
            groupBox5.Size = new Size(323, 42);
            groupBox5.TabIndex = 17;
            groupBox5.TabStop = false;
            groupBox5.Text = "Ruta de destino";
            // 
            // LblDestinyFolder
            // 
            LblDestinyFolder.AutoSize = true;
            LblDestinyFolder.Location = new Point(7, 19);
            LblDestinyFolder.Name = "LblDestinyFolder";
            LblDestinyFolder.Size = new Size(228, 15);
            LblDestinyFolder.TabIndex = 0;
            LblDestinyFolder.Text = "Por defecto el path del paquete publicado";
            // 
            // BtnDestinyFolder
            // 
            BtnDestinyFolder.Location = new Point(12, 144);
            BtnDestinyFolder.Name = "BtnDestinyFolder";
            BtnDestinyFolder.Size = new Size(323, 23);
            BtnDestinyFolder.TabIndex = 18;
            BtnDestinyFolder.Text = "1.1- Opcional Carpeta de destino";
            BtnDestinyFolder.UseVisualStyleBackColor = true;
            BtnDestinyFolder.Click += BtnDestinyFolder_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(351, 518);
            Controls.Add(BtnDestinyFolder);
            Controls.Add(groupBox5);
            Controls.Add(groupBox4);
            Controls.Add(groupBox3);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            Controls.Add(BtnOpenDirectory);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "Form1";
            Text = "Auto deploy package";
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            groupBox3.ResumeLayout(false);
            groupBox3.PerformLayout();
            groupBox4.ResumeLayout(false);
            groupBox4.PerformLayout();
            groupBox5.ResumeLayout(false);
            groupBox5.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Button BtnOpenDirectory;
        private Label lblCompleteFolderPath;
        private Button BtnZipFolder;
        private CheckedListBox ChkBLOptionsDelete;
        private TextBox TxtBoxNameProject;
        private CheckedListBox ChkBLOptionsBackup;
        private Label LblExcludeFiles;
        private Label LblZipedType;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private ToolTip toolTip1;
        private GroupBox groupBox3;
        private Label LblVistaPreviaNamePackage;
        private ToolTip toolTip2;
        private ComboBox CmbboxProjectName;
        private GroupBox groupBox4;
        private GroupBox groupBox5;
        private Label LblDestinyFolder;
        private Button BtnDestinyFolder;
    }
}