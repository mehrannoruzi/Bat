namespace Bat.Core;

public class FileLoger
{
    private static readonly object _infoLock = new object();
    private static readonly object _errorLock = new object();
    private static readonly object _messageLock = new object();

    public static void Info(string log, string path = "")
    {
        if (string.IsNullOrEmpty(path)) path = $"{AppDomain.CurrentDomain.BaseDirectory}\\Log";
        if (!Directory.Exists(path)) Directory.CreateDirectory(path);
        Monitor.Enter(_infoLock);
        try
        {
            using (var stream = File.AppendText($"{path}\\Info-{PersianDateTime.Now.ToString(PersianDateTimeFormat.Date).Replace("/", "-")}.txt"))
            {
                stream.WriteLine($"{PersianDateTime.Now.ToString(PersianDateTimeFormat.DateTime)} :: {log}");
                stream.Close();
            }
        }
        finally
        {
            Monitor.Exit(_infoLock);
        }
    }

    public static void CriticalInfo(string log, string path = "")
    {
        if (string.IsNullOrEmpty(path)) path = $"{AppDomain.CurrentDomain.BaseDirectory}\\Log";
        if (!Directory.Exists(path)) Directory.CreateDirectory(path);
        Monitor.Enter(_infoLock);
        try
        {
            using (var stream = File.AppendText($"{path}\\CriticalInfo-{PersianDateTime.Now.ToString(PersianDateTimeFormat.Date).Replace("/", "-")}.txt"))
            {
                stream.WriteLine($"{PersianDateTime.Now.ToString(PersianDateTimeFormat.DateTime)} :: {log}");
                stream.Close();
            }
        }
        finally
        {
            Monitor.Exit(_infoLock);
        }
    }

    public static void Message(string log, string path = "")
    {
        if (string.IsNullOrEmpty(path)) path = $"{AppDomain.CurrentDomain.BaseDirectory}\\Log";
        if (!Directory.Exists(path)) Directory.CreateDirectory(path);
        Monitor.Enter(_messageLock);
        try
        {
            using (var stream = File.AppendText($"{path}\\Message-{PersianDateTime.Now.ToString(PersianDateTimeFormat.Date).Replace("/", "-")}.txt"))
            {
                stream.WriteLine($"{PersianDateTime.Now.ToString(PersianDateTimeFormat.DateTime)} :: {log}");
                stream.Close();
            }
        }
        finally
        {
            Monitor.Exit(_messageLock);
        }
    }

    public static void Error(Exception e, string path = "")
    {
        var exceptionDetails = ExceptionBusiness.GetCallerMethodName(e);
        if (string.IsNullOrEmpty(path)) path = $"{AppDomain.CurrentDomain.BaseDirectory}\\Log";
        if (!Directory.Exists(path)) Directory.CreateDirectory(path);
        Monitor.Enter(_errorLock);
        try
        {
            using (var stream = File.AppendText($"{path}\\Error-{PersianDateTime.Now.ToString(PersianDateTimeFormat.Date).Replace("/", "-")}.txt"))
            {
                stream.WriteLine(
                    $" DateTime : {PersianDateTime.Now.ToString(PersianDateTimeFormat.DateTime)}" + Environment.NewLine +
                    $" MethodName : {exceptionDetails.MethodName}" + Environment.NewLine +
                    $" Parameters : {exceptionDetails.Parameters}" + Environment.NewLine +
                    $" ExceptionLineNumber : {exceptionDetails.ExceptionLineNumber}" + Environment.NewLine +
                    $" Message : {e.Message}" + Environment.NewLine +
                    $" InnerException : {e.InnerException}" + Environment.NewLine + Environment.NewLine);
                stream.Close();
            }
        }
        finally
        {
            Monitor.Exit(_errorLock);
        }
    }

    public static void CriticalError(Exception e, string path = "")
    {
        var exceptionDetails = ExceptionBusiness.GetCallerMethodName(e);
        if (string.IsNullOrEmpty(path)) path = $"{AppDomain.CurrentDomain.BaseDirectory}\\Log";
        if (!Directory.Exists(path)) Directory.CreateDirectory(path);
        Monitor.Enter(_errorLock);
        try
        {
            using (var stream = File.AppendText($"{path}\\CriticalError-{PersianDateTime.Now.ToString(PersianDateTimeFormat.Date).Replace("/", "-")}.txt"))
            {
                stream.WriteLine(
                    $" DateTime : {PersianDateTime.Now.ToString(PersianDateTimeFormat.DateTime)}" + Environment.NewLine +
                    $" MethodName : {exceptionDetails.MethodName}" + Environment.NewLine +
                    $" Parameters : {exceptionDetails.Parameters}" + Environment.NewLine +
                    $" ExceptionLineNumber : {exceptionDetails.ExceptionLineNumber}" + Environment.NewLine +
                    $" Message : {e.Message}" + Environment.NewLine +
                    $" InnerException : {e.InnerException}" + Environment.NewLine + Environment.NewLine);
                stream.Close();
            }
        }
        finally
        {
            Monitor.Exit(_errorLock);
        }
    }
}