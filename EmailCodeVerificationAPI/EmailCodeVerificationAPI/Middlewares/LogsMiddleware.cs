namespace EmailCodeVerificationAPI.Middlewares
{
    public class LogsMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly string FilePath;
        private static int counter = 0;
        private static DateTime ResetDate = DateTime.Today.AddDays(-1);
        private int thisRequestCounter;
        //private readonly string cycleSeparator = $"#####################################################################{Environment.NewLine}";
        public LogsMiddleware(RequestDelegate next)
        {
            _next = next;
            FilePath = Path.Combine(Environment.CurrentDirectory, "Logs", $"Logs_{DateTime.Today:dd-MM-yyyy}.txt");
            //$"C:\\Temp\\Logs_{DateTime.Today:dd-MM-yyyy}.txt";

            //ResetDate = DateTime.Today;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            context.Request.EnableBuffering();
            string bodyAsString = "";
            using (var reader = new StreamReader(
                context.Request.Body,
                encoding: System.Text.Encoding.UTF8,
                detectEncodingFromByteOrderMarks: false,
                bufferSize: 1024,
                leaveOpen: true))
            {
                bodyAsString = await reader.ReadToEndAsync();
                context.Request.Body.Position = 0;
            }
            if (ResetDate != DateTime.Today)
            {
                counter = 0;
                ResetDate = DateTime.Today;
                thisRequestCounter = Interlocked.Increment(ref counter);
            }
            else
            {
                thisRequestCounter = Interlocked.Increment(ref counter);
            }
            string RequestLog = $"--> Requete N'{thisRequestCounter} faite le {DateTime.Now} -- Par: {context.Connection.RemoteIpAddress?.ToString()} -- Path: {context.Request.Path} -- Methode: {context.Request.Method} -- Contenu: {bodyAsString}{Environment.NewLine}";
            await File.AppendAllTextAsync(FilePath, RequestLog);


            var originalResponseBody = context.Response.Body;
            using (var memoryStream = new MemoryStream())
            {
                context.Response.Body = memoryStream;
                await _next(context);

                memoryStream.Position = 0;
                string responseBody = await new StreamReader(memoryStream).ReadToEndAsync();
                memoryStream.Position = 0;
                await memoryStream.CopyToAsync(originalResponseBody);
                string ResponseLog = $"<-- Reponse N'{thisRequestCounter} faite le {DateTime.Now} -- Code: {context.Response.StatusCode} -- Contenu: {responseBody}{Environment.NewLine}";
                await File.AppendAllTextAsync(FilePath, ResponseLog);
                //await File.AppendAllTextAsync(FilePath, cycleSeparator);
            }
        }
    }
}
