// See https://aka.ms/new-console-template for more information




using PracticaCrearPDF;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using QuestPDF.Previewer;
using System.Reflection.Emit;

List<Producto> productos = new List<Producto>();

productos.Add(new Producto { Nombre = "Caramelo", Precio = 20, Cantidad = 2, Total = 40 });
productos.Add(new Producto { Nombre = "Alfajor", Precio = 60, Cantidad = 1, Total = 60 });
productos.Add(new Producto { Nombre = "Papas", Precio = 15, Cantidad = 3, Total = 45 });
productos.Add(new Producto { Nombre = "Cocucha", Precio = 10, Cantidad = 6, Total = 60 });
productos.Add(new Producto { Nombre = "Pitusas", Precio = 12, Cantidad = 1, Total = 12 });
productos.Add(new Producto { Nombre = "RedPoint", Precio = 30, Cantidad = 2, Total = 60 });



QuestPDF.Settings.License = LicenseType.Community;





Document.Create(document =>
{
    document.Page(page =>
    {
        page.Margin(30);
        page.Header().ShowOnce().Row(row =>
        {
            row.ConstantItem(140).Border(1).Height(60).Placeholder();

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

                foreach(var item in Enumerable.Range(1,25))
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

    

    


}).ShowInPreviewer();


/*document.Page(page =>
    {
        page.Content().Column(col1 =>
        {
            var precioFinal = 0.0;
            col1.Item().Row(row =>
            {
                row.RelativeItem().Height(50).Border(1).Background(Colors.DeepOrange.Medium);
                row.RelativeItem().Height(50).Border(1).Background(Colors.BlueGrey.Medium);
                row.RelativeItem().Height(50).Border(1).Background(Colors.LightGreen.Medium);

            });

            col1.Item().PaddingTop(50).Table(tabla =>
            {
                tabla.ColumnsDefinition(columns => 
                {
                    columns.RelativeColumn(2);
                    columns.RelativeColumn();
                    columns.RelativeColumn();
                    columns.RelativeColumn();
                });
                tabla.Header(header => 
                {
                    header.Cell().Background("#257272")
                    .Padding(2).Text("Producto").FontColor("#fff");
                    header.Cell().Background("#257272")
                    .Padding(2).Text("Precio").FontColor("#fff");
                    header.Cell().Background("#257272")
                    .Padding(2).Text("Cantidad").FontColor("#fff");
                    header.Cell().Background("#257272")
                    .Padding(2).Text("Total").FontColor("#fff");

                });

                
                foreach(var item in productos)
                {
                    precioFinal = precioFinal + item.Total;
                    tabla.Cell().Text(item.Nombre.ToString());

                    tabla.Cell().Text(item.Precio.ToString());

                    tabla.Cell().Text(item.Cantidad.ToString());

                    tabla.Cell().Text(item.Total.ToString());
                }

            });
            col1.Item().Text($" TOTAL: {precioFinal}").AlignRight();
            



        });

       

    });*/