using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.IO;
using System.Text;
using System.Threading;

namespace asgn5v1
{
    /// <summary>
    /// Summary description for Transformer.
    /// </summary>
    public class Transformer : System.Windows.Forms.Form
    {
        private System.ComponentModel.IContainer components;

        // basic data for Transformer
        Thread rotationThread;
        double shapeCenterX = 0;
        double shapeCenterY = 0;
        double shapeCenterZ = 0;
        double minY;
        double maxY;
        double objectHeight;
        bool firstDataPoint = true;
        int numpts = 0;
        int numlines = 0;
        bool gooddata = false;
        public const int MATRIX_SIZE = 4;
        double[,] vertices;
        double[,] scrnpts;
        double[,] ctrans = new double[MATRIX_SIZE, MATRIX_SIZE];  //your main transformation matrix
        private System.Windows.Forms.ImageList tbimages;
        private System.Windows.Forms.ToolBar toolBar1;
        private System.Windows.Forms.ToolBarButton transleftbtn;
        private System.Windows.Forms.ToolBarButton transrightbtn;
        private System.Windows.Forms.ToolBarButton transupbtn;
        private System.Windows.Forms.ToolBarButton transdownbtn;
        private System.Windows.Forms.ToolBarButton toolBarButton1;
        private System.Windows.Forms.ToolBarButton scaleupbtn;
        private System.Windows.Forms.ToolBarButton scaledownbtn;
        private System.Windows.Forms.ToolBarButton toolBarButton2;
        private System.Windows.Forms.ToolBarButton rotxby1btn;
        private System.Windows.Forms.ToolBarButton rotyby1btn;
        private System.Windows.Forms.ToolBarButton rotzby1btn;
        private System.Windows.Forms.ToolBarButton toolBarButton3;
        private System.Windows.Forms.ToolBarButton rotxbtn;
        private System.Windows.Forms.ToolBarButton rotybtn;
        private System.Windows.Forms.ToolBarButton rotzbtn;
        private System.Windows.Forms.ToolBarButton toolBarButton4;
        private System.Windows.Forms.ToolBarButton shearrightbtn;
        private System.Windows.Forms.ToolBarButton shearleftbtn;
        private System.Windows.Forms.ToolBarButton toolBarButton5;
        private System.Windows.Forms.ToolBarButton resetbtn;
        private System.Windows.Forms.ToolBarButton exitbtn;
        int[,] lines;

        public Transformer()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();

            //
            // TODO: Add any constructor code after InitializeComponent call
            //
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.DoubleBuffer, true);
            Text = "COMP 4560:  Assignment 5 (200830) Jacob Frank - A00919081";
            ResizeRedraw = true;
            BackColor = Color.Black;
            MenuItem miNewDat = new MenuItem("New &Data...",
                new EventHandler(MenuNewDataOnClick));
            MenuItem miExit = new MenuItem("E&xit",
                new EventHandler(MenuFileExitOnClick));
            MenuItem miDash = new MenuItem("-");
            MenuItem miFile = new MenuItem("&File",
                new MenuItem[] { miNewDat, miDash, miExit });
            MenuItem miAbout = new MenuItem("&About",
                new EventHandler(MenuAboutOnClick));
            Menu = new MainMenu(new MenuItem[] { miFile, miAbout });
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Transformer));
            this.tbimages = new System.Windows.Forms.ImageList(this.components);
            this.toolBar1 = new System.Windows.Forms.ToolBar();
            this.transleftbtn = new System.Windows.Forms.ToolBarButton();
            this.transrightbtn = new System.Windows.Forms.ToolBarButton();
            this.transupbtn = new System.Windows.Forms.ToolBarButton();
            this.transdownbtn = new System.Windows.Forms.ToolBarButton();
            this.toolBarButton1 = new System.Windows.Forms.ToolBarButton();
            this.scaleupbtn = new System.Windows.Forms.ToolBarButton();
            this.scaledownbtn = new System.Windows.Forms.ToolBarButton();
            this.toolBarButton2 = new System.Windows.Forms.ToolBarButton();
            this.rotxby1btn = new System.Windows.Forms.ToolBarButton();
            this.rotyby1btn = new System.Windows.Forms.ToolBarButton();
            this.rotzby1btn = new System.Windows.Forms.ToolBarButton();
            this.toolBarButton3 = new System.Windows.Forms.ToolBarButton();
            this.rotxbtn = new System.Windows.Forms.ToolBarButton();
            this.rotybtn = new System.Windows.Forms.ToolBarButton();
            this.rotzbtn = new System.Windows.Forms.ToolBarButton();
            this.toolBarButton4 = new System.Windows.Forms.ToolBarButton();
            this.shearrightbtn = new System.Windows.Forms.ToolBarButton();
            this.shearleftbtn = new System.Windows.Forms.ToolBarButton();
            this.toolBarButton5 = new System.Windows.Forms.ToolBarButton();
            this.resetbtn = new System.Windows.Forms.ToolBarButton();
            this.exitbtn = new System.Windows.Forms.ToolBarButton();
            this.SuspendLayout();
            // 
            // tbimages
            // 
            this.tbimages.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("tbimages.ImageStream")));
            this.tbimages.TransparentColor = System.Drawing.Color.Transparent;
            this.tbimages.Images.SetKeyName(0, "");
            this.tbimages.Images.SetKeyName(1, "");
            this.tbimages.Images.SetKeyName(2, "");
            this.tbimages.Images.SetKeyName(3, "");
            this.tbimages.Images.SetKeyName(4, "");
            this.tbimages.Images.SetKeyName(5, "");
            this.tbimages.Images.SetKeyName(6, "");
            this.tbimages.Images.SetKeyName(7, "");
            this.tbimages.Images.SetKeyName(8, "");
            this.tbimages.Images.SetKeyName(9, "");
            this.tbimages.Images.SetKeyName(10, "");
            this.tbimages.Images.SetKeyName(11, "");
            this.tbimages.Images.SetKeyName(12, "");
            this.tbimages.Images.SetKeyName(13, "");
            this.tbimages.Images.SetKeyName(14, "");
            this.tbimages.Images.SetKeyName(15, "");
            // 
            // toolBar1
            // 
            this.toolBar1.Buttons.AddRange(new System.Windows.Forms.ToolBarButton[] {
            this.transleftbtn,
            this.transrightbtn,
            this.transupbtn,
            this.transdownbtn,
            this.toolBarButton1,
            this.scaleupbtn,
            this.scaledownbtn,
            this.toolBarButton2,
            this.rotxby1btn,
            this.rotyby1btn,
            this.rotzby1btn,
            this.toolBarButton3,
            this.rotxbtn,
            this.rotybtn,
            this.rotzbtn,
            this.toolBarButton4,
            this.shearrightbtn,
            this.shearleftbtn,
            this.toolBarButton5,
            this.resetbtn,
            this.exitbtn});
            this.toolBar1.Dock = System.Windows.Forms.DockStyle.Right;
            this.toolBar1.DropDownArrows = true;
            this.toolBar1.ImageList = this.tbimages;
            this.toolBar1.Location = new System.Drawing.Point(484, 0);
            this.toolBar1.Name = "toolBar1";
            this.toolBar1.ShowToolTips = true;
            this.toolBar1.Size = new System.Drawing.Size(24, 306);
            this.toolBar1.TabIndex = 0;
            this.toolBar1.ButtonClick += new System.Windows.Forms.ToolBarButtonClickEventHandler(this.toolBar1_ButtonClick);
            // 
            // transleftbtn
            // 
            this.transleftbtn.ImageIndex = 1;
            this.transleftbtn.Name = "transleftbtn";
            this.transleftbtn.ToolTipText = "translate left";
            // 
            // transrightbtn
            // 
            this.transrightbtn.ImageIndex = 0;
            this.transrightbtn.Name = "transrightbtn";
            this.transrightbtn.ToolTipText = "translate right";
            // 
            // transupbtn
            // 
            this.transupbtn.ImageIndex = 2;
            this.transupbtn.Name = "transupbtn";
            this.transupbtn.ToolTipText = "translate up";
            // 
            // transdownbtn
            // 
            this.transdownbtn.ImageIndex = 3;
            this.transdownbtn.Name = "transdownbtn";
            this.transdownbtn.ToolTipText = "translate down";
            // 
            // toolBarButton1
            // 
            this.toolBarButton1.Name = "toolBarButton1";
            this.toolBarButton1.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            // 
            // scaleupbtn
            // 
            this.scaleupbtn.ImageIndex = 4;
            this.scaleupbtn.Name = "scaleupbtn";
            this.scaleupbtn.ToolTipText = "scale up";
            // 
            // scaledownbtn
            // 
            this.scaledownbtn.ImageIndex = 5;
            this.scaledownbtn.Name = "scaledownbtn";
            this.scaledownbtn.ToolTipText = "scale down";
            // 
            // toolBarButton2
            // 
            this.toolBarButton2.Name = "toolBarButton2";
            this.toolBarButton2.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            // 
            // rotxby1btn
            // 
            this.rotxby1btn.ImageIndex = 6;
            this.rotxby1btn.Name = "rotxby1btn";
            this.rotxby1btn.ToolTipText = "rotate about x by 1";
            // 
            // rotyby1btn
            // 
            this.rotyby1btn.ImageIndex = 7;
            this.rotyby1btn.Name = "rotyby1btn";
            this.rotyby1btn.ToolTipText = "rotate about y by 1";
            // 
            // rotzby1btn
            // 
            this.rotzby1btn.ImageIndex = 8;
            this.rotzby1btn.Name = "rotzby1btn";
            this.rotzby1btn.ToolTipText = "rotate about z by 1";
            // 
            // toolBarButton3
            // 
            this.toolBarButton3.Name = "toolBarButton3";
            this.toolBarButton3.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            // 
            // rotxbtn
            // 
            this.rotxbtn.ImageIndex = 9;
            this.rotxbtn.Name = "rotxbtn";
            this.rotxbtn.ToolTipText = "rotate about x continuously";
            // 
            // rotybtn
            // 
            this.rotybtn.ImageIndex = 10;
            this.rotybtn.Name = "rotybtn";
            this.rotybtn.ToolTipText = "rotate about y continuously";
            // 
            // rotzbtn
            // 
            this.rotzbtn.ImageIndex = 11;
            this.rotzbtn.Name = "rotzbtn";
            this.rotzbtn.ToolTipText = "rotate about z continuously";
            // 
            // toolBarButton4
            // 
            this.toolBarButton4.Name = "toolBarButton4";
            this.toolBarButton4.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            // 
            // shearrightbtn
            // 
            this.shearrightbtn.ImageIndex = 12;
            this.shearrightbtn.Name = "shearrightbtn";
            this.shearrightbtn.ToolTipText = "shear right";
            // 
            // shearleftbtn
            // 
            this.shearleftbtn.ImageIndex = 13;
            this.shearleftbtn.Name = "shearleftbtn";
            this.shearleftbtn.ToolTipText = "shear left";
            // 
            // toolBarButton5
            // 
            this.toolBarButton5.Name = "toolBarButton5";
            this.toolBarButton5.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            // 
            // resetbtn
            // 
            this.resetbtn.ImageIndex = 14;
            this.resetbtn.Name = "resetbtn";
            this.resetbtn.ToolTipText = "restore the initial image";
            // 
            // exitbtn
            // 
            this.exitbtn.ImageIndex = 15;
            this.exitbtn.Name = "exitbtn";
            this.exitbtn.ToolTipText = "exit the program";
            // 
            // Transformer
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(508, 306);
            this.Controls.Add(this.toolBar1);
            this.Name = "Transformer";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Transformer_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.Run(new Transformer());
        }

        protected override void OnPaint(PaintEventArgs pea)
        {
            Graphics grfx = pea.Graphics;
            Pen pen = new Pen(Color.White, 3);
            double temp;
            int k;

            if (gooddata)
            {
                //create the screen coordinates:
                // scrnpts = vertices*ctrans

                for (int i = 0; i < numpts; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        temp = 0.0d;
                        for (k = 0; k < 4; k++)
                            temp += vertices[i, k] * ctrans[k, j];
                        scrnpts[i, j] = temp;
                    }
                }

                //now draw the lines

                for (int i = 0; i < numlines; i++)
                {
                    grfx.DrawLine(pen, (int)scrnpts[lines[i, 0], 0], (int)scrnpts[lines[i, 0], 1],
                        (int)scrnpts[lines[i, 1], 0], (int)scrnpts[lines[i, 1], 1]);
                }


            } // end of gooddata block	
        } // end of OnPaint

        //USer selects the menu option to display new data
        //after selecting files, calls function to display initial image
        void MenuNewDataOnClick(object obj, EventArgs ea)
        {
            //MessageBox.Show("New Data item clicked.");
            minY = 0;
            maxY = 0;
            firstDataPoint = true;
            gooddata = GetNewData();
            RestoreInitialImage();
        }

        //User selects the menu option to exit the application
        void MenuFileExitOnClick(object obj, EventArgs ea)
        {
            Close();
        }

        //User selects the menu option to display teh about info
        void MenuAboutOnClick(object obj, EventArgs ea)
        {
            AboutDialogBox dlg = new AboutDialogBox();
            dlg.ShowDialog();
        }


        //Function to display the original image
        //and restor a transformed image to it's original state
        void RestoreInitialImage()
        {
            double initialScaleFactor = (this.Height / 2 / objectHeight);
            double[] centerScreen = { this.Width / 2, this.Height / 2, 0 };
            double[] initialScale = { initialScaleFactor, -initialScaleFactor, initialScaleFactor };

            setIdentity(ctrans, MATRIX_SIZE, MATRIX_SIZE);
            scale(initialScale);            

            double[] currentOffset = findShapeCenterOffset();


            translate(centerScreen);
            translateBack(currentOffset);
            Invalidate();
        } // end of RestoreInitialImage

        bool GetNewData()
        {
            string strinputfile, text;
            ArrayList coorddata = new ArrayList();
            ArrayList linesdata = new ArrayList();
            OpenFileDialog opendlg = new OpenFileDialog();
            opendlg.Title = "Choose File with Coordinates of Vertices";
            if (opendlg.ShowDialog() == DialogResult.OK)
            {
                strinputfile = opendlg.FileName;
                FileInfo coordfile = new FileInfo(strinputfile);
                StreamReader reader = coordfile.OpenText();
                do
                {
                    text = reader.ReadLine();
                    if (text != null) coorddata.Add(text);
                } while (text != null);
                reader.Close();
                DecodeCoords(coorddata);
            }
            else
            {
                MessageBox.Show("***Failed to Open Coordinates File***");
                return false;
            }

            opendlg.Title = "Choose File with Data Specifying Lines";
            if (opendlg.ShowDialog() == DialogResult.OK)
            {
                strinputfile = opendlg.FileName;
                FileInfo linesfile = new FileInfo(strinputfile);
                StreamReader reader = linesfile.OpenText();
                do
                {
                    text = reader.ReadLine();
                    if (text != null) linesdata.Add(text);
                } while (text != null);
                reader.Close();
                DecodeLines(linesdata);
            }
            else
            {
                MessageBox.Show("***Failed to Open Line Data File***");
                return false;
            }
            scrnpts = new double[numpts, 4];
            setIdentity(ctrans, 4, 4);  //initialize transformation matrix to identity
            return true;
        } // end of GetNewData

        void DecodeCoords(ArrayList coorddata)
        {
            //this may allocate slightly more rows that necessary
            vertices = new double[coorddata.Count, 4];
            numpts = 0;
            string[] text = null;
            for (int i = 0; i < coorddata.Count; i++)
            {
                text = coorddata[i].ToString().Split(' ', ',');
                vertices[numpts, 0] = double.Parse(text[0]);
                if (vertices[numpts, 0] < 0.0d) break;
                vertices[numpts, 1] = double.Parse(text[1]);
                vertices[numpts, 2] = double.Parse(text[2]);
                vertices[numpts, 3] = 1.0d;

                if (firstDataPoint)
                {
                    minY = vertices[0, 1];
                    maxY = vertices[0, 1];
                    firstDataPoint = false;
                } else
                {
                    if (vertices[numpts, 1] < minY)
                    {
                        minY = vertices[numpts, 1];
                    }

                    if (vertices[numpts, 1] > maxY)
                    {
                        maxY = vertices[numpts, 1];
                    }
                }

                numpts++;
            }

            shapeCenterX = vertices[0, 0];
            shapeCenterY = vertices[0, 1];
            shapeCenterZ = vertices[0, 2];
            objectHeight = maxY - minY;

        }// end of DecodeCoords

        void DecodeLines(ArrayList linesdata)
        {
            //this may allocate slightly more rows that necessary
            lines = new int[linesdata.Count, 2];
            numlines = 0;
            string[] text = null;
            for (int i = 0; i < linesdata.Count; i++)
            {
                text = linesdata[i].ToString().Split(' ', ',');
                lines[numlines, 0] = int.Parse(text[0]);
                if (lines[numlines, 0] < 0) break;
                lines[numlines, 1] = int.Parse(text[1]);
                numlines++;
            }
        } // end of DecodeLines


        // Resets matrix passed into function to default values
        // (1's along the diagonal and 0's everywhere else).
        // |1000|
        // |0100|
        // |0010|
        // |0001|
        void setIdentity(double[,] A, int nrow, int ncol)
        {
            for (int i = 0; i < nrow; i++)
            {
                for (int j = 0; j < ncol; j++) A[i, j] = 0.0d;
                A[i, i] = 1.0d;
            }
        }// end of setIdentity


        private void Transformer_Load(object sender, System.EventArgs e)
        {

        }

        /**********************************************/
        /////// Helper Transformation Functions ///////
        /*********************************************/

        /// <summary>
        /// Calculates the shapes current offset from the center 
        /// of the screen based on it's current coordinates and transformation
        /// </summary>
        /// <returns>the x, y, and z axis offset from the center of the screen</returns>
        private double[] findShapeCenterOffset()
        {
            double[] offsetFromCentre = new double[3];

            offsetFromCentre[0] = shapeCenterX * ctrans[0, 0] + shapeCenterY * ctrans[1, 0] + shapeCenterZ * ctrans[2, 0] + ctrans[3,0];
            offsetFromCentre[1] = shapeCenterX * ctrans[0, 1] + shapeCenterY * ctrans[1, 1] + shapeCenterZ * ctrans[2, 1] + ctrans[3, 1];
            offsetFromCentre[2] = shapeCenterX * ctrans[0, 2] + shapeCenterY * ctrans[1, 2] + shapeCenterZ * ctrans[2, 2] + ctrans[3, 2];

            return offsetFromCentre;
        }
        
        /// <summary>
        /// Translates the object along the X, Y, and Z axis based on input
        /// </summary>
        /// <param name="xyz">Translation coordinates for the x, y, and z axis</param>                               
        private void translate(double[] xyz)
        {
            double[,] translationMatrix = new double[MATRIX_SIZE, MATRIX_SIZE];

            //Initialize the translation matrix to default values
            setIdentity(translationMatrix, MATRIX_SIZE, MATRIX_SIZE);

            //Create new translation matrix
            for (int i = 0; i < MATRIX_SIZE - 1; i++)
            {
                translationMatrix[3, i] = xyz[i];
            }

            //compute new Tnet
            performTransformation(translationMatrix);
        }


        /// <summary>
        /// Translates the object along the X, Y, and Z axis based on input
        /// </summary>
        /// <param name="xyz">Translation coordinates for the x, y, and z axis</param>                               
        private void translateBack(double[] xyz)
        {
            double[,] translationMatrix = new double[MATRIX_SIZE, MATRIX_SIZE];

            //Initialize the translation matrix to default values
            setIdentity(translationMatrix, MATRIX_SIZE, MATRIX_SIZE);

            //Create new translation matrix
            for (int i = 0; i < MATRIX_SIZE - 1; i++)
            {
                translationMatrix[3, i] = -xyz[i];
            }

            //compute new Tnet
            performTransformation(translationMatrix);
        }



        /// <summary>
        /// Scales the object along the x, y and z axis based on input
        /// </summary>
        /// <param name="xyz">Scale factors for the x, y and z axis</param>
        private void scale(double[] xyz)
        {
            double[,] scaleMatrix = new double[MATRIX_SIZE, MATRIX_SIZE];

            //Initialize the scaling matrix to default values
            setIdentity(scaleMatrix, MATRIX_SIZE, MATRIX_SIZE);

            //Create new translation matrix
            for (int i = 0; i < MATRIX_SIZE - 1; i++)
            {
                scaleMatrix[i, i] = xyz[i];
            }
            scaleMatrix[3, 3] = 1;

            //compute new Tnet
            performTransformation(scaleMatrix);

        }

        /// <summary>
        /// Rotates the object by an angle in radians specified
        /// around the x-axis
        /// </summary>
        /// <param name="radianAngle">The angle of rotation (in radians)</param>
        private void rotateX(double radianAngle)
        {
            double[,] rotationMatrix = new double[MATRIX_SIZE, MATRIX_SIZE];

            //Initialize the rotation matrix to default values
            setIdentity(rotationMatrix, MATRIX_SIZE, MATRIX_SIZE);

            //Create the new rotation matrix
            rotationMatrix[1, 1] = Math.Cos(radianAngle);
            rotationMatrix[1, 2] = -Math.Sin(radianAngle);
            rotationMatrix[2, 1] = Math.Sin(radianAngle);
            rotationMatrix[2, 2] = Math.Cos(radianAngle);

            //compute new Tnet
            performTransformation(rotationMatrix);
        }

        /// <summary>
        /// Rotates the object by an angle in radians specified
        /// around the y-axis
        /// </summary>
        /// <param name="radianAngle">The angle of rotation (in radians)</param>
        private void rotateY(double radianAngle)
        {
            double[,] rotationMatrix = new double[MATRIX_SIZE, MATRIX_SIZE];

            //Initialize the rotation matrix to default values
            setIdentity(rotationMatrix, MATRIX_SIZE, MATRIX_SIZE);

            //Create the new rotation matrix
            rotationMatrix[0, 0] = Math.Cos(radianAngle);
            rotationMatrix[0, 2] = -Math.Sin(radianAngle);
            rotationMatrix[2, 0] = Math.Sin(radianAngle);
            rotationMatrix[2, 2] = Math.Cos(radianAngle);

            //compute new Tnet
            performTransformation(rotationMatrix);
        }

        /// <summary>
        /// Rotates the object by an angle in radians specified
        /// around the z-axis
        /// </summary>
        /// <param name="radianAngle">The angle of rotation (in radians)</param>
        private void rotateZ(double radianAngle)
        {
            double[,] rotationMatrix = new double[MATRIX_SIZE, MATRIX_SIZE];

            //Initialize the rotation matrix to default values
            setIdentity(rotationMatrix, MATRIX_SIZE, MATRIX_SIZE);

            //Create the new rotation matrix
            rotationMatrix[0, 0] = Math.Cos(radianAngle);
            rotationMatrix[0, 1] = -Math.Sin(radianAngle);
            rotationMatrix[1, 0] = Math.Sin(radianAngle);
            rotationMatrix[1, 1] = Math.Cos(radianAngle);

            //compute new Tnet
            performTransformation(rotationMatrix);
        }

        /// <summary>
        /// Rotates the object by an angle in radians specified
        /// around the x-axis continusously.
        /// performs a new rotation ever 50 miliseconds
        /// </summary>
        /// <param name="radianAngle">The angle of rotation (in radians)</param>
        private void rotateXContinuously(double radianAngle)
        {
            double[] currentOffset = findShapeCenterOffset();

            while (true)
            {
                //rotate by an angle of 0.05 radians continuously
                translateBack(currentOffset);   //translate to 0,0,0
                rotateX(radianAngle);           //roatate by an angle of 0.5 radians around the Z axis
                translate(currentOffset);       //translate back to previous location

                Invalidate();
                Thread.Sleep(50);
            }
        }

        /// <summary>
        /// Rotates the object by an angle in radians specified
        /// around the y-axis continusously.
        /// performs a new rotation ever 50 miliseconds
        /// </summary>
        /// <param name="radianAngle">The angle of rotation (in radians)</param>
        private void rotateYContinuously(double radianAngle)
        {
            double[] currentOffset = findShapeCenterOffset();

            while (true)
            {
                //rotate by an angle of 0.05 radians continuously
                translateBack(currentOffset);   //translate to 0,0,0
                rotateY(radianAngle);           //roatate by an angle of 0.5 radians around the Z axis
                translate(currentOffset);       //translate back to previous location

                Invalidate();
                Thread.Sleep(50);
            }
        }

        /// <summary>
        /// Rotates the object by an angle in radians specified
        /// around the z-axis continusously.
        /// performs a new rotation ever 50 miliseconds
        /// </summary>
        /// <param name="radianAngle">The angle of rotation (in radians)</param>
        private void rotateZContinuously(double radianAngle)
        {
            double[] currentOffset = findShapeCenterOffset();

            while (true)
            {
                //rotate by an angle of 0.05 radians continuously
                translateBack(currentOffset);   //translate to 0,0,0
                rotateZ(radianAngle);           //roatate by an angle of 0.5 radians around the Z axis
                translate(currentOffset);       //translate back to previous location

                Invalidate();
                Thread.Sleep(50);
            }
        }

        /// <summary>
        /// Shears the object horizontally along the X-axis with respect to Y
        /// by a factor specified.  
        /// Negative shear factors will shear the object right, while
        /// positive shear factors will shear the object left.
        /// </summary>
        /// <param name="shearFactor">The factor by which the object will be sheared</param>
        private void horizontalShear(double shearFactor)
        {
            //The objects current offset from 0,0,0 with respect to the objects center
            double[] currentOffset = findShapeCenterOffset();

            //The offset between the center and bottom of the object
            double[] yOffset = new double[3];
            double[,] shearMatrix = new double[MATRIX_SIZE,MATRIX_SIZE];

            //Initialize the shear matrix to default values
            setIdentity(shearMatrix, MATRIX_SIZE, MATRIX_SIZE);

            //Calculation of the offset between the center and bottom of the object
            yOffset[1] = -shapeCenterX * ctrans[0, 1] + -shapeCenterY * ctrans[1, 1] + -shapeCenterZ * ctrans[2, 1];

            //Create the new shear matrix
            shearMatrix[1, 0] = shearFactor;

            //Translate the bottom line of the object to fall along the x-axis
            translateBack(currentOffset);
            translateBack(yOffset);

            //Perform the shear
            performTransformation(shearMatrix);

            //Translate the object back to it's original position
            translate(yOffset);
            translate(currentOffset);

        }


        /// <summary>
        /// computes the new net transformation and overwrite the current net transformation
        /// </summary>
        /// <param name="transMatrix"> The transformation matrix to be multiplied into the current transformation matrix (ctrans)</param>
        private void performTransformation(double[,] transMatrix)
        {
            //Temporary matrix that will contain the new Tnet
            double[,] newTransMatrix = new double[4, 4];

            for (int i = 0; i < MATRIX_SIZE; i++)
            {
                for (int j = 0; j < MATRIX_SIZE; j++)
                {
                    for (int k = 0; k < MATRIX_SIZE; k++)
                    {
                        newTransMatrix[i, j] += ctrans[i, k] * transMatrix[k, j];
                    }
                }
            }

            //overwrite the current transformation with the newly created one
            ctrans = newTransMatrix;
        }

        private void toolBar1_ButtonClick(object sender, System.Windows.Forms.ToolBarButtonClickEventArgs e)
        {
            if (rotationThread != null)
            {
                rotationThread.Abort();
            }

            if (e.Button == transleftbtn)
            {
                //Translate 75 pixels left
                double[] translateLeft = {-75,0,0 };
                translate(translateLeft);
                
                Refresh();
            }
            if (e.Button == transrightbtn)
            {
                //Translate 75 pixels right
                double[] translateright = { 75, 0, 0 };
                translate(translateright);

                Refresh();
            }
            if (e.Button == transupbtn)
            {
                //translate 35 pixels up
                double[] translateup = { 0, -35, 0 };
                translate(translateup);
                Refresh();
            }

            if (e.Button == transdownbtn)
            {
                //translate 35 pixels down
                double[] translatedown = { 0, 35, 0 };
                translate(translatedown);
                Refresh();
            }
            if (e.Button == scaleupbtn)
            {
                double[] currentOffset = findShapeCenterOffset();
                double[] scaleUp = { 1.1, 1.1, 1.1 };

                
                translateBack(currentOffset);   //translate to 0,0,0
                scale(scaleUp);                 //scale up by 10%
                translate(currentOffset);       //translate back to previous location

                Refresh();
            }
            if (e.Button == scaledownbtn)
            {
                double[] currentOffset = findShapeCenterOffset();
                double[] scaleDown = {.9, .9, .9 };

                //scale down by 10%
                translateBack(currentOffset);   //translate to 0,0,0
                scale(scaleDown);               //scale up by 10%
                translate(currentOffset);       //translate back to previous location

                Refresh();
            }
            if (e.Button == rotxby1btn)
            {
                double[] currentOffset = findShapeCenterOffset();

                //rotate by an angle of 0.05 radians
                translateBack(currentOffset);   //translate to 0,0,0
                rotateX(0.05);                   //roatate by an angle of 0.5 radians around the X axis
                translate(currentOffset);       //translate back to previous location

                Refresh();
            }
            if (e.Button == rotyby1btn)
            {
                double[] currentOffset = findShapeCenterOffset();

                //rotate by an angle of 0.05 radians
                translateBack(currentOffset);   //translate to 0,0,0
                rotateY(0.05);                   //roatate by an angle of 0.5 radians around the Y axis
                translate(currentOffset);       //translate back to previous location

                Refresh();
            }
            if (e.Button == rotzby1btn)
            {
                double[] currentOffset = findShapeCenterOffset();

                //rotate by an angle of 0.05 radians
                translateBack(currentOffset);   //translate to 0,0,0
                rotateZ(0.05);                   //roatate by an angle of 0.5 radians around the Z axis
                translate(currentOffset);       //translate back to previous location

                Refresh();
            }

            if (e.Button == rotxbtn)
            {
                //rotate by an angle of 0.05 radians continuously around the X axis
                rotationThread = new Thread(() => rotateXContinuously(0.05));
                rotationThread.Start();
            }
            if (e.Button == rotybtn)
            {
                //rotate by an angle of 0.05 radians continuously around the Y axis
                rotationThread = new Thread(() => rotateYContinuously(0.05));
                rotationThread.Start();
            }

            if (e.Button == rotzbtn)
            {
                //rotate by an angle of 0.05 radians continuously around the Z axis
                rotationThread = new Thread(() => rotateZContinuously(0.05));
                rotationThread.Start();
            }

            if (e.Button == shearleftbtn)
            {
                //shear the character in the x-direction with respect to y, leftwards at the top by 10%
                horizontalShear(0.1);
                Refresh();
            }

            if (e.Button == shearrightbtn)
            {
                //shear the character in the x-direction with respect to y, rightwards at the top by 10 %
                horizontalShear(-0.1);
                Refresh();
            }

            if (e.Button == resetbtn)
            {
                RestoreInitialImage();
            }

            if (e.Button == exitbtn)
            {
                Close();
            }

        }


    }


}
