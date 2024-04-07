using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuestPDF;
using QuestPDF.Fluent;
using QuestPDF.Previewer;

namespace InstaInvLibrary
{
    public class InvoiceGenerator
    {
        public static void GeneratePdf(string filePath, List<InvoiceItem> invoiceItems)
        {
            Settings.License = QuestPDF.Infrastructure.LicenseType.Community;
            Random Random = new Random();

            var model = new InvoiceModel
            {
                InvoiceNumber = Random.Next(1_000, 10_000),
                IssueDate = DateTime.Now,
                DueDate = DateTime.Now + TimeSpan.FromDays(14),
                Items = invoiceItems
            };

            var document = new InvoiceDocument(model);
            document.GeneratePdf(filePath);

        }
    }
}
