using RazorLight;
using static System.Net.WebRequestMethods;

namespace API.Service
{
    public static class EmailService
    {
        public static string WelcomeEmail(string nomeCliente, string codigoCupom)
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
                    background-color: #2c3e50;
                    padding: 20px;
                    text-align: center;
                }}
                .header h1 {{
                    color: #ffffff;
                    margin: 0;
                }}
                .coupon {{
                    background-color: #f39c12;
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
                    background-color: #2c3e50;
                    color: #ffffff;
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
    }
}
