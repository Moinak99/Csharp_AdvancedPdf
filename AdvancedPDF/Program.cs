using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text;
using System;

namespace AdvancedPDF
{

    class AdvancedPDFOperations
    {
        //FUNCTION FOR MERGING MULTIPLE PDF FILES

        public void Merge(string files)
        {
            try
            {
                // create PdfFileEditor object
                PdfFileEditor pdfEditor = new PdfFileEditor();


                //splitting from pipe to get files
                string[] filelist = files.Split("|");
                foreach (string data in filelist)
                    Console.WriteLine(data);
                //splitting to extract the location and first file name
                string[] firstfileoperation = filelist[0].Split(@"\");

                var l = firstfileoperation.Length;
                // revoming last 4 i.e .pdf
                string a = firstfileoperation[l - 1].Remove(firstfileoperation[l - 1].Length - 4);
                // getting the dir 
                string locationdir = "";
                for (int i = 0; i < firstfileoperation.Length - 1; i++)
                {
                    locationdir = locationdir + firstfileoperation[i] + @"\";
                }
                // final file name
                string resultfilename = a + "_treated.pdf";
                Console.WriteLine(locationdir + resultfilename);
                pdfEditor.Concatenate(filelist, locationdir + resultfilename);
                Console.WriteLine("Merge Done");
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine("MERGE FAILED!   Run Again With Proper Input");
            }
            

        }

        //FUNCTION TO ADD PAGE NUMBERS

        public void Pagination(string files)
        {
            try
            {
                string[] filelist = files.Split("|");
                string[] ans = filelist[0].Split(@"\");
                var l = ans.Length;
                string a = ans[l - 1].Remove(ans[l - 1].Length - 4);
                string resultfilename = a + "_treated.pdf";
                string locationdir = "";
                for (int i = 0; i < ans.Length - 1; i++)
                {
                    locationdir = locationdir + ans[i] + @"\";
                }
                // The path to the documents directory.
                string dataDir = locationdir;
                // Open document
                Document pdfDocument = new Document(dataDir + resultfilename);

                var countpages = pdfDocument.Pages.Count;
                // Create page number stamp
                PageNumberStamp pageNumberStamp = new PageNumberStamp();
                // Whether the stamp is background
                pageNumberStamp.Background = false;
                pageNumberStamp.Format = "Page # of " + pdfDocument.Pages.Count;
                pageNumberStamp.BottomMargin = 10;
                pageNumberStamp.HorizontalAlignment = HorizontalAlignment.Center;
                pageNumberStamp.StartingNumber = 1;
                // Set text properties
                pageNumberStamp.TextState.Font = FontRepository.FindFont("Arial");
                pageNumberStamp.TextState.FontSize = 9.0F;
                pageNumberStamp.TextState.FontStyle = FontStyles.Bold;
                pageNumberStamp.TextState.FontStyle = FontStyles.Italic;
                pageNumberStamp.TextState.ForegroundColor = Color.Black;

                // Add stamp to particular page
                for (int i = 1; i <= countpages; i++)
                {
                    pdfDocument.Pages[i].AddStamp(pageNumberStamp);

                }


                dataDir = dataDir + resultfilename;
                // Save output document
                pdfDocument.Save(dataDir);
                Console.WriteLine("Pagination Done");
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine("PAGINATION FAILED!   Run Again With Proper Input");

            }

        }
        // FUNCTION TO ADD LOCATION OF BASE FILES

        public void AddPageLocation(string files)
        {
            try
            {
                string[] filelist = files.Split("|");
                string[] ans = filelist[0].Split(@"\");
                var l = ans.Length;
                string a = ans[l - 1].Remove(ans[l - 1].Length - 4);
                string resultfilename = a + "_treated.pdf";
                string locationdir = "";
                for (int i = 0; i < ans.Length - 1; i++)
                {
                    locationdir = locationdir + ans[i] + @"\";
                }
                int x = 100, y = 600;
                // The path to the documents directory.
                string dataDir = locationdir;

                // Open document
                Document pdfDocument = new Document(dataDir + resultfilename);

                pdfDocument.Pages.Insert(1);

                // Get particular page
                Page pdfPage = (Page)pdfDocument.Pages[1];

                for (int i = 0; i < filelist.Length; i++)
                {
                    TextFragment textFragment = new TextFragment("PDF locations are:--" + filelist[0]);
                    // Create text fragment
                    textFragment.Position = new Position(x, y);
                    x = x + 10;
                    y = y + 10;
                    // Set text properties
                    textFragment.TextState.FontSize = 12;
                    textFragment.TextState.Font = FontRepository.FindFont("TimesNewRoman");
                    textFragment.TextState.BackgroundColor = Aspose.Pdf.Color.FromRgb(System.Drawing.Color.LightGray);
                    textFragment.TextState.ForegroundColor = Aspose.Pdf.Color.FromRgb(System.Drawing.Color.Blue);

                    // Create TextBuilder object
                    TextBuilder textBuilder = new TextBuilder(pdfPage);

                    // Append the text fragment to the PDF page
                    textBuilder.AppendText(textFragment);
                }



                dataDir = dataDir + resultfilename;

                // Save resulting PDF document.
                pdfDocument.Save(dataDir);
                Console.WriteLine("Summary Done");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine("SUMMARY FAILED!   Run Again With Proper Input");

            }


        }
    }
    class Program
    {

       
        static void Main(string[] args)
        {
            
            Console.WriteLine("Hello World!");
            Console.WriteLine("Enter Locations followed by a |  & add .pdf after file name");
            string files = Console.ReadLine();
            Console.WriteLine("Enter Operations! M-merge ,P-pagination,S-summary,Any-summary" );
            var userinp = Console.ReadLine();
            AdvancedPDFOperations p = new AdvancedPDFOperations();

            if(userinp=="m" || userinp=="M")
            {
                p.Merge(files);

            }

            else if(userinp=="p" || userinp=="P")
            {
                p.Merge(files);
                p.Pagination(files);
            }

            else if (userinp == "s" || userinp=="S")
            {
                p.Merge(files);
               
                p.AddPageLocation(files);
                p.Pagination(files);
            }

            else
            {
                p.Merge(files);
                p.Pagination(files);
                p.AddPageLocation(files);
            }



        }
    }
}
