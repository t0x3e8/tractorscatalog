using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Enceladus.Api;
using Enceladus;
using System.IO;

namespace PDFsCheck
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnRun_Click(object sender, RoutedEventArgs e)
        {
            ConstantsReader contantsReader = new ConstantsReader();
            DatabaseStorage db = new DatabaseStorage();
            int numberOfTractors = contantsReader.GetTotalTractorsNumber();

            IList<string> missingPDFs = new List<string>();
            IList<string> correctPDFs = new List<string>();
            IList<string> missingPictures = new List<string>();
            IList<string> correctPictures = new List<string>();
            

            for (int i = 1; i < numberOfTractors; i++ )
            {
                Tractor tractor = db.Get(i);

                if (!string.IsNullOrEmpty(tractor.Seitencodeprofi.ToUpper()))
                {
                    if (!ResourcesFinder.ResourceExist(tractor.Seitencodeprofi.ToUpper(), ResourceType.PDF))
                    {
                        if (!missingPDFs.Contains(tractor.Seitencodeprofi.ToUpper()))
                            missingPDFs.Add(tractor.Seitencodeprofi.ToUpper());
                    }
                    else
                    {
                        if (!correctPDFs.Contains(tractor.Seitencodeprofi.ToUpper()))
                            correctPDFs.Add(tractor.Seitencodeprofi.ToUpper());
                    }
                }

                if (!string.IsNullOrEmpty(tractor.Seitencodetop.ToUpper()))
                {
                    if (!ResourcesFinder.ResourceExist(tractor.Seitencodetop.ToUpper(), ResourceType.PDF))
                    {
                        if (!missingPDFs.Contains(tractor.Seitencodetop.ToUpper()))
                            missingPDFs.Add(tractor.Seitencodetop.ToUpper());
                    }
                    else
                    {
                        if (!correctPDFs.Contains(tractor.Seitencodetop.ToUpper()))
                            correctPDFs.Add(tractor.Seitencodetop.ToUpper());
                    }
                }


                if (!string.IsNullOrEmpty(tractor.Bild1.ToUpper()))
                {
                    if (!ResourcesFinder.ResourceExist(tractor.Bild1.ToUpper(), ResourceType.Picture))
                    {
                        if (!missingPictures.Contains(tractor.Bild1.ToUpper()))
                            missingPictures.Add(tractor.Bild1.ToUpper());
                    }
                    else
                    {
                        if (!correctPictures.Contains(tractor.Bild1.ToUpper()))
                            correctPictures.Add(tractor.Bild1.ToUpper());
                    }
                }

                this.Title = i.ToString();
            }

            this.MissingPDFFiles.Text = "Missing PDFs: " + missingPDFs.Count.ToString() + Environment.NewLine;
            foreach (var missingPDF in missingPDFs)
            {
                this.MissingPDFFiles.Text += missingPDF + Environment.NewLine;
            }

            this.MissingPDFFiles.Text += "Missing Pictures: " + missingPictures.Count.ToString() + Environment.NewLine;
            foreach (var missingPicture in missingPictures)
            {
                this.MissingPDFFiles.Text += missingPicture + Environment.NewLine;
            }

            this.MissingPictureFiles.Text += "Correct PDFs: " + correctPDFs.Count.ToString() + Environment.NewLine;
            foreach (var correctPDF in correctPDFs)
            {
                this.MissingPictureFiles.Text += correctPDF + Environment.NewLine;
                //this.Copyfile(correctPDF, "c:\\temp\\pdf", ResourceType.PDF);
            }

            this.MissingPictureFiles.Text += "Correct Pictures: " + correctPictures.Count.ToString() + Environment.NewLine;
            foreach (var correctPicture in correctPictures)
            {
                //this.Copyfile(correctPicture, "c:\\temp\\pictures", ResourceType.Picture);
            }
        }

        private void Copyfile(string fileToCopy, string destDir, ResourceType type)
        {
            FileInfo fi = ResourcesFinder.ResourcePath(fileToCopy, type);
            fi.CopyTo(destDir + "\\" + fi.Name, false);
        }
    }
}
