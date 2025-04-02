using DescargarPDF.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using QuestPDF.Previewer;
using System.Reflection.Emit;

namespace DescargarPDF.Controllers
{
    public class HomeController : Controller
    {
        private readonly IWebHostEnvironment _host; //Esto nos permite acceder a la imagen que llevara el PDF
        private readonly ILogger<HomeController> _logger; 

        public HomeController(IWebHostEnvironment host, ILogger<HomeController> logger)
        {
            _host = host;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult DescargarPDF()
        {
            QuestPDF.Settings.License = LicenseType.Community;

            //Data almacena un array de bits que forman el documento PDF
        
            var data = Document.Create(document =>
                {
                    document.Page(page =>
                    {
                        page.Margin(30);
                        page.Header().ShowOnce().Row(row =>
                        {
                        
                            var rutaImagen = Path.Combine(_host.WebRootPath, "Images","LogoNet.png");
                            if(!System.IO.File.Exists(rutaImagen)) 
                            {
                                throw new FileNotFoundException($"No se encontro el archivo {rutaImagen}");
                            }
                            var imageData = System.IO.File.ReadAllBytes(rutaImagen);
                           
                            row.ConstantItem(150).Image(imageData);
                            //row.ConstantItem(140).Border(1).Height(60).Placeholder();

                            row.RelativeItem().Border(1).Column(col =>
                            {
                                col.Item().AlignCenter().Text("NOMBRE EMPRESA S.A").Bold().FontSize(14);
                                col.Item().AlignCenter().Text("AV H.YRIGOYEN 123").FontSize(9);
                                col.Item().AlignCenter().Text("1145903321").FontSize(9);
                                col.Item().AlignCenter().Text("ejemplo@gmail.com").FontSize(9);
                            });

                            row.RelativeItem().Border(1).Column(col =>
                            {
                                col.Item().Border(1).BorderColor("#257272")
                                .AlignCenter().Text("RUC 1231244124").FontSize(14);

                                col.Item().Background("#257272").Border(1)
                                .BorderColor("#257272").AlignCenter().
                                Text("Boleta de venta").FontColor("#fff");

                                col.Item().AlignCenter().Text("B002-454");
                            });

                        });

                        page.Content().PaddingVertical(10).Column(col1 =>
                        {
                            col1.Item().Text("Datos del cliente").Underline().Bold();

                            col1.Item().Text(txt =>
                            {
                                txt.Span("Nombre: ").SemiBold().FontSize(10);
                                txt.Span("Mario Casas").SemiBold().FontSize(10);
                            });
                            col1.Item().Text(txt =>
                            {
                                txt.Span("DNI: ").SemiBold().FontSize(10);
                                txt.Span("24234234").SemiBold().FontSize(10);
                            });
                            col1.Item().Text(txt =>
                            {
                                txt.Span("Direccion: ").SemiBold().FontSize(10);
                                txt.Span("Del Valle 12344").SemiBold().FontSize(10);
                            });

                            col1.Item().LineHorizontal(0.5f);

                            col1.Item().Table(tabla =>
                            {
                                tabla.ColumnsDefinition(columns =>
                                {
                                    columns.RelativeColumn(3);
                                    columns.RelativeColumn();
                                    columns.RelativeColumn();
                                    columns.RelativeColumn();

                                });
                                tabla.Header(header =>
                                {
                                    header.Cell().Background("#257272")
                                    .Padding(2).Text("Producto").FontColor("#fff");
                                    header.Cell().Background("#257272")
                                    .Padding(2).Text("Precio Unit").FontColor("#fff");
                                    header.Cell().Background("#257272")
                                    .Padding(2).Text("Cantidad").FontColor("#fff");
                                    header.Cell().Background("#257272")
                                    .Padding(2).Text("Total").FontColor("#fff");
                                });

                                foreach (var item in Enumerable.Range(1, 25))
                                {

                                    var cantidad = Placeholders.Random.Next(1, 10);
                                    var precio = Placeholders.Random.Next(5, 15);
                                    var total = cantidad * precio;

                                    tabla.Cell().BorderBottom(0.5f).BorderColor("#d9d9d9")
                                    .Padding(2).Text(Placeholders.Label()).FontSize(10);

                                    tabla.Cell().BorderBottom(0.5f).BorderColor("#d9d9d9")
                                    .Padding(2).Text(cantidad.ToString()).FontSize(10);

                                    tabla.Cell().BorderBottom(0.5f).BorderColor("#d9d9d9")
                                    .Padding(2).Text($" $.{precio}").FontSize(10);

                                    tabla.Cell().BorderBottom(0.5f).BorderColor("#d9d9d9")
                                    .Padding(2).Text($" $.{precio}").FontSize(10);

                                }
                            });

                            col1.Item().AlignRight().Text("Total 1500").FontSize(12);


                        });


                    });






                }).GeneratePdf();
            

            

            //?
            Stream stream= new MemoryStream(data);

            return File(stream, "aplication/pdf", "detalleVenta.pdf");

        }

        public IActionResult VerPDF()
        {
            QuestPDF.Settings.License = LicenseType.Community;

            //Data almacena un array de bits que forman el documento PDF

            var data = Document.Create(document =>
            {
                document.Page(page =>
                {
                    page.Margin(30);
                    page.Header().ShowOnce().Row(row =>
                    {

                        var rutaImagen = Path.Combine(_host.WebRootPath, "Images", "LogoNet.png");
                        if (!System.IO.File.Exists(rutaImagen))
                        {
                            throw new FileNotFoundException($"No se encontro el archivo {rutaImagen}");
                        }
                        var imageData = System.IO.File.ReadAllBytes(rutaImagen);

                        //row.ConstantItem(140).Border(1).Height(60).Placeholder();
                        row.ConstantItem(150).Image(imageData);

                        row.RelativeItem().Border(1).Column(col =>
                        {
                            col.Item().AlignCenter().Text("NOMBRE EMPRESA S.A").Bold().FontSize(14);
                            col.Item().AlignCenter().Text("AV H.YRIGOYEN 123").FontSize(9);
                            col.Item().AlignCenter().Text("1145903321").FontSize(9);
                            col.Item().AlignCenter().Text("ejemplo@gmail.com").FontSize(9);
                        });

                        row.RelativeItem().Border(1).Column(col =>
                        {
                            col.Item().Border(1).BorderColor("#257272")
                            .AlignCenter().Text("RUC 1231244124").FontSize(14);

                            col.Item().Background("#257272").Border(1)
                            .BorderColor("#257272").AlignCenter().
                            Text("Boleta de venta").FontColor("#fff");

                            col.Item().AlignCenter().Text("B002-454");
                        });

                    });

                    page.Content().PaddingVertical(10).Column(col1 =>
                    {
                        col1.Item().Text("Datos del cliente").Underline().Bold();

                        col1.Item().Text(txt =>
                        {
                            txt.Span("Nombre: ").SemiBold().FontSize(10);
                            txt.Span("Mario Casas").SemiBold().FontSize(10);
                        });
                        col1.Item().Text(txt =>
                        {
                            txt.Span("DNI: ").SemiBold().FontSize(10);
                            txt.Span("24234234").SemiBold().FontSize(10);
                        });
                        col1.Item().Text(txt =>
                        {
                            txt.Span("Direccion: ").SemiBold().FontSize(10);
                            txt.Span("Del Valle 12344").SemiBold().FontSize(10);
                        });

                        col1.Item().LineHorizontal(0.5f);

                        col1.Item().Table(tabla =>
                        {
                            tabla.ColumnsDefinition(columns =>
                            {
                                columns.RelativeColumn(3);
                                columns.RelativeColumn();
                                columns.RelativeColumn();
                                columns.RelativeColumn();

                            });
                            tabla.Header(header =>
                            {
                                header.Cell().Background("#257272")
                                .Padding(2).Text("Producto").FontColor("#fff");
                                header.Cell().Background("#257272")
                                .Padding(2).Text("Precio Unit").FontColor("#fff");
                                header.Cell().Background("#257272")
                                .Padding(2).Text("Cantidad").FontColor("#fff");
                                header.Cell().Background("#257272")
                                .Padding(2).Text("Total").FontColor("#fff");
                            });

                            foreach (var item in Enumerable.Range(1, 25))
                            {

                                var cantidad = Placeholders.Random.Next(1, 10);
                                var precio = Placeholders.Random.Next(5, 15);
                                var total = cantidad * precio;

                                tabla.Cell().BorderBottom(0.5f).BorderColor("#d9d9d9")
                                .Padding(2).Text(Placeholders.Label()).FontSize(10);

                                tabla.Cell().BorderBottom(0.5f).BorderColor("#d9d9d9")
                                .Padding(2).Text(cantidad.ToString()).FontSize(10);

                                tabla.Cell().BorderBottom(0.5f).BorderColor("#d9d9d9")
                                .Padding(2).Text($" $.{precio}").FontSize(10);

                                tabla.Cell().BorderBottom(0.5f).BorderColor("#d9d9d9")
                                .Padding(2).Text($" $.{precio}").FontSize(10);

                            }
                        });

                        col1.Item().AlignRight().Text("Total 1500").FontSize(12);


                    });


                });






            }).GeneratePdf();

          

            return File(data, "application/pdf");

        }

    }
        
}
