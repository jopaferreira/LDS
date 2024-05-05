using System;
using System.Reflection.PortableExecutable;
using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;

namespace UnirPDFs
{
    class Programa
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Que unir quantos ficheiros PDF? :");
            int numeroDeFicheiros = int.Parse(Console.ReadLine());

            string[] ficheirosPDF = new string[numeroDeFicheiros];

            for (int i = 0; i < numeroDeFicheiros; i++)
            {
                Console.WriteLine($"Introduza nome do {i + 1}º ficheiro PDF :");
                ficheirosPDF[i] = Console.ReadLine();
            }

            Console.WriteLine("Qual o nome do ficheiro final? :");
            string ficheiroPDFSaida = Console.ReadLine();

            UnirPDFs(ficheirosPDF, ficheiroPDFSaida);
        }

        static void UnirPDFs(string[] ficheirosPDF, string ficheiroPDFSaida)
        {
            PdfDocument documentoPDF = new PdfDocument();

            foreach (string ficheiroPDF in ficheirosPDF)
            {
                PdfDocument pdf = PdfReader.Open(ficheiroPDF, PdfDocumentOpenMode.Import);

                foreach (PdfPage pagina in pdf.Pages)
                {
                    documentoPDF.AddPage(pagina);
                }
            }

            documentoPDF.Save(ficheiroPDFSaida);
        }
    }
}
