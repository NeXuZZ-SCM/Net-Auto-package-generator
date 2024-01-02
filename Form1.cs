using Business;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO.Compression;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Business.Form1Service;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace AutoZipDeploy
{
    public partial class Form1 : Form
    {
        private IConfiguration Configuration { get; set; }

        private System.Windows.Forms.Timer debounceTimerBackup;
        private System.Windows.Forms.Timer debounceTimerDeploy;

        private string folderToZip;
        private string zipFilePath;
        private string folderSelectedDestiny;
        private readonly Form1Service _form1Service;
        private List<string> selectedDeleteFiles = new List<string>();// Crear una lista para almacenar los elementos seleccionados.
        private List<string> selectedTypeBackup = new List<string>();// Crear una lista para almacenar los elementos seleccionados.


        public Form1()
        {
            // Construir la configuración
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            Configuration = builder.Build();



            // Leer una configuración específica
            List<string> patternsListConfig = Configuration.GetSection("ListOfFilesToDelete").Get<List<string>>();

            List<string> ProjectNamesList = Configuration.GetSection("ProjectNames").Get<List<string>>();

            int? defaultOptionProjectName = Configuration.GetValue<int>("DefaultOptionProjectName");



            InitializeComponent();
            _form1Service = new Form1Service();
            TxtBoxNameProject.Enabled = false;

            lblCompleteFolderPath.AutoEllipsis = true;
            BtnZipFolder.Enabled = false;

            CmbboxProjectName.Enabled = false;


            // Establece el texto del ToolTip que se mostrará
            toolTip1.SetToolTip(lblCompleteFolderPath, "");

            // Opcional: Configura otras propiedades
            toolTip1.AutoPopDelay = 5000;    // Tiempo en milisegundos que el ToolTip permanece visible
            toolTip1.ReshowDelay = 500;      // Tiempo en milisegundos antes de que el ToolTip se muestre de nuevo
            toolTip1.ShowAlways = true;      // Muestra el ToolTip incluso si el formulario no está activo

            // Opcional: Configura otras propiedades
            toolTip2.AutoPopDelay = 5000;    // Tiempo en milisegundos que el ToolTip permanece visible
            toolTip2.ReshowDelay = 500;      // Tiempo en milisegundos antes de que el ToolTip se muestre de nuevo
            toolTip2.ShowAlways = true;      // Muestra el ToolTip incluso si el formulario no está activo

            // Opciones disponibles de zipeado.
            var typeZipped = new Dictionary<string, string>
            {
                {"1", "Backup"},
                {"2", "Deploy"}
            };

            #region Completo Listas de opciones
            foreach (string pattern in patternsListConfig)//web.config, app.config, etc.
            {
                ChkBLOptionsDelete.Items.Add(pattern, true);
            }

            foreach (string type in typeZipped.Values)//Backup, Deploy
            {
                ChkBLOptionsBackup.Items.Add(type, true);
            }



            CmbboxProjectName.Items.Add("");//opcion en blanco

            foreach (string project in ProjectNamesList)//DirectMarketing, IM, Eflow, etc.
            {
                CmbboxProjectName.Items.Add(project);
            }

            try
            {
                CmbboxProjectName.SelectedIndex = defaultOptionProjectName ?? 0;//podria estar fuera de intervalo
            }
            catch (Exception)
            {
                CmbboxProjectName.SelectedIndex = 0;
            }

            #endregion





            foreach (object item in ChkBLOptionsDelete.CheckedItems)
            {
                selectedDeleteFiles.Add(item.ToString());
            }

            foreach (object item in ChkBLOptionsBackup.CheckedItems)
            {
                selectedTypeBackup.Add(item.ToString());
            }
        }

        private void BtnOpenDirectory_Click(object sender, EventArgs e)
        {
            // Crear una instancia del FolderBrowserDialog
            using (FolderBrowserDialog folderDialog = new FolderBrowserDialog())
            {
                // Opcional: Configurar la descripción y el path inicial
                folderDialog.Description = "Selecciona una carpeta";
                folderDialog.RootFolder = Environment.SpecialFolder.MyComputer; // Puedes cambiar esto según tus necesidades

                // Mostrar el diálogo al usuario
                DialogResult result = folderDialog.ShowDialog();

                // Verificar si el usuario hizo clic en el botón OK
                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(folderDialog.SelectedPath))
                {

                    #region chequear si la carpeta parece ser una carpeta valida
                    string pathToCheck = folderDialog.SelectedPath; // Modifica esto con la ruta de tu carpeta
                    if (IsPublishFolder(pathToCheck))
                    {
                        // Hacer algo con la ruta seleccionada
                        this.folderToZip = folderDialog.SelectedPath;
                        BtnZipFolder.Enabled = true;
                        TxtBoxNameProject.Enabled = true;
                        CmbboxProjectName.Enabled = true;
                        toolTip1.SetToolTip(lblCompleteFolderPath, this.folderToZip);
                        lblCompleteFolderPath.Text = folderDialog.SelectedPath;
                    }
                    else
                    {
                        // Muestra el MessageBox
                        DialogResult resultYesNo = MessageBox.Show($"La carpeta {pathToCheck} no parece ser una carpeta de publicación válida ¿Deseas continuar?", "Confirmación", MessageBoxButtons.YesNo);

                        // Verifica la respuesta del usuario
                        if (resultYesNo == DialogResult.Yes)
                        {
                            BtnZipFolder.Enabled = true;
                            TxtBoxNameProject.Enabled = true;
                            CmbboxProjectName.Enabled = true;
                        }
                        else if (resultYesNo == DialogResult.No)
                        {
                            BtnZipFolder.Enabled = false;
                            TxtBoxNameProject.Enabled = false;
                            CmbboxProjectName.Enabled = false;
                        }
                    }
                    #endregion

                }
            }
        }

        private void BtnZipFolder_Click(object sender, EventArgs e)
        {
            string nameBackup = $"Backup {DateTime.Now.ToString("dd-MM-yyyy HH mm ss")}";
            string nameDeploy = $"Deploy {DateTime.Now.ToString("dd-MM-yyyy HH mm ss")}";

            if (CmbboxProjectName.Text != string.Empty && TxtBoxNameProject.Text != string.Empty)
            {
                nameBackup = $"Backup {CmbboxProjectName.Text} {TxtBoxNameProject.Text} {DateTime.Now.ToString("dd-MM-yyyy HH mm ss")}";
                nameDeploy = $"Deploy {CmbboxProjectName.Text} {TxtBoxNameProject.Text} {DateTime.Now.ToString("dd-MM-yyyy HH mm ss")}";
            }
            else
            {
                if (TxtBoxNameProject.Text != string.Empty)
                {
                    nameBackup = $"Backup {TxtBoxNameProject.Text} {DateTime.Now.ToString("dd-MM-yyyy HH mm ss")}";
                    nameDeploy = $"Deploy {TxtBoxNameProject.Text} {DateTime.Now.ToString("dd-MM-yyyy HH mm ss")}";
                }
                if (CmbboxProjectName.Text != string.Empty)
                {
                    nameBackup = $"Backup {CmbboxProjectName.Text} {DateTime.Now.ToString("dd-MM-yyyy HH mm ss")}";
                    nameDeploy = $"Deploy {CmbboxProjectName.Text} {DateTime.Now.ToString("dd-MM-yyyy HH mm ss")}";
                }
            }


            foreach (var item in selectedTypeBackup)
            {
                if (item.Contains("Backup"))
                {
                    #region BackupDeployFiles
                    ZipPackage($"{nameBackup}", ".zip", typeOfZip.Backup);
                    #endregion
                }
                if (item.Contains("Deploy"))
                {
                    #region Zip to deploy
                    ZipPackage($"{nameDeploy}", ".zip", typeOfZip.Deploy);
                    #endregion
                }
            }
        }

        private void ZipPackage(string fileName, string extension, typeOfZip type)
        {

            try
            {
                if (folderSelectedDestiny is not null)
                {
                    this.zipFilePath = Path.Combine(folderSelectedDestiny, fileName + extension);// Ruta y nombre del archivo zip resultante
                }
                else
                {
                    this.zipFilePath = Path.Combine(Path.GetDirectoryName(folderToZip), fileName + extension);// Ruta y nombre del archivo zip resultante
                }

                int cont = 0;
                while (File.Exists(this.zipFilePath))
                {
                    this.zipFilePath = Path.Combine(Path.GetDirectoryName(folderToZip), fileName + $"({cont})" + extension);
                    cont++;
                }


                _form1Service.ZipAndBackoupFiles(folderToZip, this.zipFilePath, selectedDeleteFiles, type);// Crear el archivo zip desde la carpeta

                //this.folderSelectedDestiny = string.Empty;

                MessageBox.Show($"Carpeta {type} zipeada exitosamente en: " + this.zipFilePath);
            }
            catch (Exception ex)
            {
                //MessageBox.Show($"Error al zipear la carpeta {type} : " + ex.Message);
                if (ex.Message.Contains("path1"))
                {
                    MessageBox.Show($"Error al zipear la carpeta {type} : " + "No se ha seleccionado ninguna carpeta");
                }
                else
                {
                    MessageBox.Show($"Error al zipear la carpeta {type} : " + ex.Message);
                }
            }
        }


        private void ChkBLOptionsBackup_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (debounceTimerBackup == null)
            {
                debounceTimerBackup = new System.Windows.Forms.Timer();
                debounceTimerBackup.Interval = 100; // Tiempo de espera antes de procesar el evento
                debounceTimerBackup.Tick += (s, args) =>
                {
                    debounceTimerBackup.Stop();
                    // Procesar el cambio aquí
                    selectedTypeBackup = new List<string>();// Crear una lista para almacenar los elementos seleccionados.
                    foreach (object item in ChkBLOptionsBackup.CheckedItems)
                    {
                        selectedTypeBackup.Add(item.ToString());
                    }
                    if (selectedTypeBackup.Count == 0)
                    {
                        BtnZipFolder.Enabled = false;
                    }
                    else
                    {
                        if (this.folderToZip is not null)
                        {
                            BtnZipFolder.Enabled = true;
                        }
                    }
                };
            }



            debounceTimerBackup.Stop();
            debounceTimerBackup.Start(); // Reinicia el temporizador cada vez que se dispara el evento
        }

        private void ChkBLOptionsDelete_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (debounceTimerDeploy == null)
            {
                debounceTimerDeploy = new System.Windows.Forms.Timer();
                debounceTimerDeploy.Interval = 100; // Tiempo de espera antes de procesar el evento
                debounceTimerDeploy.Tick += (s, args) =>
                {
                    debounceTimerDeploy.Stop();
                    // Procesar el cambio aquí
                    selectedDeleteFiles = new List<string>();// Crear una lista para almacenar los elementos seleccionados.

                    foreach (object item in ChkBLOptionsDelete.CheckedItems)
                    {
                        selectedDeleteFiles.Add(item.ToString());
                    }
                };
            }

            debounceTimerDeploy.Stop();
            debounceTimerDeploy.Start(); // Reinicia el temporizador cada vez que se dispara el evento
        }

        private void TxtBoxNameProject_TextChanged(object sender, EventArgs e)
        {
            LblVistaPreviaNamePackage.Text = $"Backup {CmbboxProjectName.Text} {TxtBoxNameProject.Text} {DateTime.Now.ToString("dd-MM-yyyy HH mm ss")}";
            toolTip2.SetToolTip(LblVistaPreviaNamePackage, LblVistaPreviaNamePackage.Text);
        }

        private void CmbboxProjectName_SelectedIndexChanged(object sender, EventArgs e)
        {
            LblVistaPreviaNamePackage.Text = $"Backup {CmbboxProjectName.Text} {TxtBoxNameProject.Text} {DateTime.Now.ToString("dd-MM-yyyy HH mm ss")}";
            toolTip2.SetToolTip(LblVistaPreviaNamePackage, LblVistaPreviaNamePackage.Text);

        }



        static bool IsPublishFolder(string path)
        {
            // Verifica la existencia de archivos .dll
            bool hasDllFiles = Directory.GetFiles(path, "*.dll").Length > 0;

            // Verifica la existencia de archivos de configuración específicos
            bool hasConfigFiles = File.Exists(Path.Combine(path, "web.config")) || File.Exists(Path.Combine(path, "appsettings.json"));

            // Agrega más verificaciones según sea necesario

            return hasDllFiles && hasConfigFiles; // Modifica esta lógica según tus necesidades
        }

        private void BtnDestinyFolder_Click(object sender, EventArgs e)
        {
            // Crear una instancia del FolderBrowserDialog
            using (FolderBrowserDialog folderDialog = new FolderBrowserDialog())
            {
                // Opcional: Configurar la descripción y el path inicial
                folderDialog.Description = "Selecciona una carpeta";
                folderDialog.RootFolder = Environment.SpecialFolder.MyComputer; // Puedes cambiar esto según tus necesidades

                // Mostrar el diálogo al usuario
                DialogResult result = folderDialog.ShowDialog();

                // Verificar si el usuario hizo clic en el botón OK
                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(folderDialog.SelectedPath))
                {

                    #region chequear si la carpeta parece ser una carpeta valida
                    LblDestinyFolder.Text = folderDialog.SelectedPath;
                    this.folderSelectedDestiny = folderDialog.SelectedPath;
                    #endregion

                }
            }
        }
    }

}
