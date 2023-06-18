using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IronPdf;
using IronPdf.Pages;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IronPdf.ChromePdfRenderer Renderer = new IronPdf.ChromePdfRenderer();
            string text = "rtr<p>rtrtr\n";
            var Pdf = Renderer.RenderHtmlAsPdf(text);
            Pdf.SaveAs("result.pdf");
        }
    }
}
