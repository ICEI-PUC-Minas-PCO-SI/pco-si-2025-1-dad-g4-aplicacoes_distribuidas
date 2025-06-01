<<<<<<< HEAD
<<<<<<< HEAD
=======
>>>>>>> 6c2bb15 (metodos sendstatuspurchase e SendWelcomeEmail)
﻿using MailKit.Net.Smtp;
using MimeKit;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using Model.Notification;
using Model.Order;
using Model;
using API.ViewModel;
<<<<<<< HEAD
=======
﻿using RazorLight;
using static System.Net.WebRequestMethods;
>>>>>>> 6f91623 (send email)
=======
>>>>>>> 6c2bb15 (metodos sendstatuspurchase e SendWelcomeEmail)

namespace API.Service
{
    public static class EmailService
    {
<<<<<<< HEAD
<<<<<<< HEAD
        public static string WelcomeEmailTemplate(string nomeCliente, string codigoCupom)
=======
        public static string WelcomeEmail(string nomeCliente, string codigoCupom)
>>>>>>> 6f91623 (send email)
=======
        public static string WelcomeEmailTemplate(string nomeCliente, string codigoCupom)
>>>>>>> 6c2bb15 (metodos sendstatuspurchase e SendWelcomeEmail)
        {
            return $@"
        <!DOCTYPE html PUBLIC ""-//W3C//DTD XHTML 1.0 Transitional//EN"" ""http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd"">
        <html xmlns=""http://www.w3.org/1999/xhtml"">
        <head>
            <meta http-equiv=""Content-Type"" content=""text/html; charset=utf-8"" />
            <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"" />
            <title>Bem-vindo ao Puro Osso</title>
            <style type=""text/css"">
                body {{
                    font-family: Arial, sans-serif;
                    line-height: 1.6;
                    color: #333333;
                    margin: 0;
                    padding: 0;
                }}
                .container {{
                    max-width: 600px;
                    margin: 0 auto;
                    padding: 20px;
                }}
                .header {{
<<<<<<< HEAD
<<<<<<< HEAD
                    color: white;
                    background-color: #8b4513;
=======
                    background-color: #2c3e50;
>>>>>>> 6f91623 (send email)
=======
                    color: white;
                    background-color: #8b4513;
>>>>>>> 6c2bb15 (metodos sendstatuspurchase e SendWelcomeEmail)
                    padding: 20px;
                    text-align: center;
                }}
                .header h1 {{
                    color: #ffffff;
                    margin: 0;
                }}
                .coupon {{
<<<<<<< HEAD
<<<<<<< HEAD
                    background-color: #f8f1e5;
=======
                    background-color: #f39c12;
>>>>>>> 6f91623 (send email)
=======
                    background-color: #f8f1e5;
>>>>>>> 6c2bb15 (metodos sendstatuspurchase e SendWelcomeEmail)
                    padding: 15px;
                    text-align: center;
                    margin: 20px 0;
                    border-radius: 4px;
                    font-weight: bold;
                }}
                .footer {{
                    background-color: #f5f5f5;
                    padding: 15px;
                    text-align: center;
                    font-size: 12px;
                }}
                .button {{
                    display: inline-block;
<<<<<<< HEAD
<<<<<<< HEAD
                    background-color: #f8f1e5;
                    color: white;
=======
                    background-color: #2c3e50;
                    color: #ffffff;
>>>>>>> 6f91623 (send email)
=======
                    background-color: #f8f1e5;
                    color: white;
>>>>>>> 6c2bb15 (metodos sendstatuspurchase e SendWelcomeEmail)
                    padding: 10px 20px;
                    text-decoration: none;
                    border-radius: 4px;
                    margin: 15px 0;
                }}
            </style>
        </head>
        <body>
            <div class=""container"">
                <div class=""header"">
                    <h1>Bem-vindo ao Purro Osso</h1>
                </div>
        
                <p>Prezado {nomeCliente},</p>
        
                <p>É com grande satisfação que damos as boas-vindas ao nosso exclusivo clube de colecionadores de relíquias pré-históricas!</p>
        
                <div class=""coupon"">
                    Presente de boas-vindas: Use o código <strong>{codigoCupom}</strong> para 10% de desconto em sua primeira compra.
                </div>
        
                <p>Em nosso acervo digital você encontrará:</p>
                <ul>
                    <li>Fósseis autênticos certificados</li>
                    <li>Réplicas de museu com precisão científica</li>
                    <li>Kits para entusiastas da paleontologia</li>
                </ul>
        
                <p style=""text-align: center;"">
                    <a href=""https://seusite.com"" class=""button"">Explorar Acervo Completo</a>
                </p>
        
                <p>Atenciosamente,<br/>
                Equipe Fossil Relics<br/>
                Desenterrando o passado, presente após presente</p
                        < div class=""footer"">
                    © {DateTime.Now.Year} Fossil Relics.Todos os direitos reservados.<br/>
                    <a href = ""[UNSUBSCRIBE_LINK]"">Cancelar inscrição</a> | 
                    <a href = ""https://seusite.com/privacidade"">Política de Privacidade</a>
                </div>
            </div>
        </body>
        </html>";
        }
<<<<<<< HEAD
<<<<<<< HEAD
=======
>>>>>>> 6c2bb15 (metodos sendstatuspurchase e SendWelcomeEmail)
        public static string OrderStatusTemplate(NotificationViewModel notificationViewModel)
        {
            return @$"
                <!DOCTYPE html>
                <html lang=""pt-BR"">
                <head>
                    <meta charset=""UTF-8"">
                    <meta name=""viewport"" content="" width=device-width, initial-scale=1.0"">
                    <title>Seu Pedido na Puro Osso</title>
                    <style>
                        body {{
                            font-family: 'Arial', sans-serif;
                            background-color: #f5f5f5;
                            margin: 0;
                            padding: 0;
                            color: #333;
                        }}
                        .container {{
                            max-width: 600px;
                            margin: 0 auto;
                            background-color: #fff;
                            border-radius: 10px;
                            overflow: hidden;
                            box-shadow: 0 0 15px rgba(0,0,0,0.1);
                        }}
                        .header {{
                            background-color: #2c3e50;
                            padding: 30px;
                            text-align: center;
                            color: #fff;
                            position: relative;
                        }}
                        .header h1 {{
                            margin: 0;
                            font-size: 28px;
                            font-weight: bold;
                        }}
                        .header img {{
                            width: 80px;
                            position: absolute;
                            top: 10px;
                            left: 20px;
                        }}
                        .content {{
                            padding: 30px;
                        }}
                        .status {{
                            background-color: #f8f1e5;
                            border-left: 5px solid #d4a017;
                            padding: 15px;
                            margin-bottom: 20px;
                            border-radius: 0 5px 5px 0;
                        }}
                        .status h2 {{
                            margin-top: 0;
                            color: #8B4513;
                        }}
                        .order-details {{
                            border: 1px dashed #8B4513;
                            padding: 15px;
                            margin-bottom: 20px;
                            border-radius: 5px;
                        }}
                        .product {{
                            display: flex;
                            margin-bottom: 15px;
                            padding-bottom: 15px;
                            border-bottom: 1px solid #eee;
                        }}
                        .product:last-child {{
                            border-bottom: none;
                            margin-bottom: 0;
                            padding-bottom: 0;
                        }}
                        .product-image {{
                            width: 80px;
                            height: 80px;
                            object-fit: cover;
                            border-radius: 5px;
                            margin-right: 15px;
                            border: 1px solid #ddd;
                        }}
                        .product-info {{
                            flex: 1;
                        }}
                        .footer {{
                            background-color: #f8f1e5;
                            padding: 20px;
                            text-align: center;
                            font-size: 12px;
                            color: #666;
                        }}
                        .dino-track {{
                            height: 30px;
                            background-image: url('data:image/svg+xml;utf8,<svg xmlns=""http://www.w3.org/2000/svg"" viewBox=""0 0 100 10""><path d=""M0,5 Q10,0 20,5 T40,5 T60,5 T80,5 T100,5"" fill=""none"" stroke=""%238B4513"" stroke-width=""1""/></svg>');
                            background-repeat: repeat-x;
                            opacity: 0.6;
                        }}
                        .button {{
                            display: inline-block;
                            padding: 12px 25px;
                            background-color: #d4a017;
                            color: #fff !important;
                            text-decoration: none;
                            border-radius: 5px;
                            font-weight: bold;
                            margin-top: 15px;
                        }}
                        .fun-fact {{
                            background-color: #e8d5b5;
                            padding: 15px;
                            border-radius: 5px;
                            margin-top: 20px;
                            font-style: italic;
                        }}
                    </style>
                </head>
                <body>
                    <div class=""container"">
                        <div class=""header"">
                            <h1>Puro Osso - Seu Pedido está a caminho!</h1>
                        </div>
        
                        <div class=""dino-track""></div>
        
                        <div class=""content"">
                            <p>Olá, {notificationViewModel.Customer}</p>
            
                            <div class=""status"">
                                <h2>Status do Pedido:  </h2>
                                <p>{notificationViewModel.Status}</p>
                                {(notificationViewModel.Status == DefaultValues.StatusPedido.Processando ? $"<p><strong>Pedido sendo processado com muito carinho!" : "")}
                                {(notificationViewModel.Status == DefaultValues.StatusPedido.EmSeparacao ? $"<p><strong>Pedido em separação!" : "")}
                                {(notificationViewModel.Status == DefaultValues.StatusPedido.EmRotaDeEntrega ? $"<p><strong>Previsão de entrega:</strong> {notificationViewModel.previsaoEntrega}</p>" : "")}
                                {(notificationViewModel.Status == DefaultValues.StatusPedido.Concluido ? $"<p><strong>Pedido concludo!" : "")}
                            </div>
            
                            <div style=""text-align: center;"">
                                <a href=""#"" class=""button"">Acompanhar Pedido</a>
                            </div>
            
                            <div class=""fun-fact"">
                                <h4>Você sabia?</h4>
                                <p>O T-Rex tinha cerca de 50 dentes, cada um com até 20 cm de comprimento! Mas não se preocupe, os dentes do seu Velociraptor são réplicas inofensivas.</p>
                            </div>
                        </div>
        
                        <div class=""footer"">
                            <p>Puro Osso - Transformando seu hobby em uma expedição paleontológica!</p>
                            <p>© 2025 Puro Osso. Todos os direitos reservados.</p>
                            <p><small>Este é um e-mail automático, por favor não responda.</small></p>
                        </div>
                    </div>
                </body>
                </html>
                ";
        }
        public static async void SendEmail(Notification model, EmailSettings _emailSettings)
        {
            // Mockandos os dados se não tiver paramêtro
            if (String.IsNullOrEmpty(model.Sender) || model.Sender == "string")
            {
                model.Sender = "Kleber, me deu ATP, professor gente boa dms";
                model.cupomDeDesconto = "BONE-15";
            }
            var htmlContent = WelcomeEmailTemplate(model.Sender, model.cupomDeDesconto);

            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(_emailSettings.FromName, _emailSettings.FromAddress));
            message.To.Add(new MailboxAddress("Destinatário", "matheushenriquecanuto77@gmail.com"));
            message.Subject = "Bem-vindo(a) ao Time dos Caçadores de Relíquias!";

            message.Body = new TextPart("html")
            {
                Text = htmlContent
            };

            using (var client = new SmtpClient())
            {
                await client.ConnectAsync(
                _emailSettings.SmtpServer,
                    _emailSettings.SmtpPort,
                    false
                );
                await client.AuthenticateAsync(
                _emailSettings.SmtpUsername,    
                    _emailSettings.SmtpPassword
                );
                await client.SendAsync(message);
                await client.DisconnectAsync(true);
            }
        }

        public static async void SendStatusEmail(NotificationViewModel notificationViewModel, EmailSettings _emailSettings)
        {
            var htmlContent = OrderStatusTemplate(notificationViewModel);

            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(_emailSettings.FromName, _emailSettings.FromAddress));
            message.To.Add(new MailboxAddress("Destinatário", "matheushenriquecanuto77@gmail.com"));
            message.Subject = "Bem-vindo(a) ao Time dos Caçadores de Relíquias!";

            message.Body = new TextPart("html")
            {
                Text = htmlContent
            };

            using (var client = new SmtpClient())
            {
                await client.ConnectAsync(
                _emailSettings.SmtpServer,
                    _emailSettings.SmtpPort,
                    false
                );
                await client.AuthenticateAsync(
                _emailSettings.SmtpUsername,
                    _emailSettings.SmtpPassword
                );
                await client.SendAsync(message);
                await client.DisconnectAsync(true);
            }
        }
<<<<<<< HEAD
=======
>>>>>>> 6f91623 (send email)
=======
>>>>>>> 6c2bb15 (metodos sendstatuspurchase e SendWelcomeEmail)
    }
}
