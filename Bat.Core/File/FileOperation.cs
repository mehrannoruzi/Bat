namespace Bat.Core;

public static partial class FileOperation
{
    public static bool Save(string fileName, byte[] file, string absolatePath)
    {
        string path = Path.Combine(absolatePath, fileName);
        File.WriteAllBytes(path, file);
        if (File.Exists(path)) return true;

        return false;
    }

    public static string Save(string absolatePath, byte[] file)
    {
        var fileName = DateTime.Now.Ticks.ToString();
        string path = Path.Combine(absolatePath, fileName);
        File.WriteAllBytes(path, file);
        if (File.Exists(path)) return fileName;

        return string.Empty;
    }

    public static bool Delete(string fileName, string absolatePath)
    {
        string path = Path.Combine(absolatePath, fileName);
        File.Delete(path);
        if (File.Exists(path)) return false;

        return true;
    }

    public static bool Exist(string fileName, string absolatePath)
    {
        string path = Path.Combine(absolatePath, fileName);
        if (File.Exists(path)) return true;

        return false;
    }

    public static bool CreateDirectory(string path)
    {
        if (!string.IsNullOrEmpty(path))
        {
            string[] dirs = path.Split(new char[] { '/', '\\' }, StringSplitOptions.RemoveEmptyEntries);
            if (dirs.Length > 1)
            {
                string stepByStepPath = $"{dirs[0]}";
                for (int i = 1; i <= dirs.Length - 1; i++)
                {
                    if (!string.IsNullOrEmpty(dirs[i])) { stepByStepPath = $@"{stepByStepPath}\{dirs[i]}"; }
                    if (!Directory.Exists(stepByStepPath)) Directory.CreateDirectory(stepByStepPath);
                }
                return true;
            }
            return false;
        }
        return false;
    }

    public static bool CheckExtention(FileType fileType, string fileName)
    {
        switch (fileType)
        {
            case FileType.Image:
                return new string[] { ".png", ".jpg", ".jpeg", ".gif", ".tiff" }.Contains(Path.GetExtension(fileName));
            case FileType.Audio:
                return new string[] { ".mp3", ".wav", ".flm", ".fsm", ".ogg", ".m4a", ".m4b", ".m4p", ".m4r" }.Contains(Path.GetExtension(fileName));
            case FileType.Video:
                return new string[] { ".mp4", ".mkv", ".avi", ".ts", ".m4v", ".flv" }.Contains(Path.GetExtension(fileName));
            case FileType.Archive:
                return new string[] { ".zip", ".rar", ".iso", ".tar" }.Contains(Path.GetExtension(fileName));
            case FileType.Document:
                return new string[] { ".pdf", ".doc", ".docx", ".xln", ".txt", ".xls", ".xlm", ".josn", ".xlsx", ".pptx" }.Contains(Path.GetExtension(fileName));
            default:
                return false;

        }
    }

    public static FileType GetFileType(string fileName)
    {
        switch (Path.GetExtension(fileName))
        {
            case ".png":
            case ".jpg":
            case ".jpeg":
            case ".gif":
            case ".tiff":
                return FileType.Image;
            case ".mp3":
            case ".wav":
            case ".flm":
            case ".fsm":
            case ".ogg":
            case ".m4a":
            case ".m4b":
            case ".m4p":
            case ".m4r":
                return FileType.Audio;
            case ".mp4":
            case ".mkv":
            case ".avi":
            case ".ts":
            case ".m4v":
            case ".flv":
                return FileType.Video;
            case ".zip":
            case ".rar":
            case ".iso":
            case ".tar":
            case ".jar":
                return FileType.Archive;
            case ".pdf":
            case ".doc":
            case ".docx":
            case ".txt":
            case ".xls":
            case ".xlsx":
            case ".josn":
            case ".pptx":
                return FileType.Document;
            default:
                return FileType.Unknown;
        }
    }

    public static FileInfo GetFileInfo(string fullPath) => new FileInfo(fullPath);
}