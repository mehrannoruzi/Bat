namespace Bat.AspNetCore;

public static class FileExtension
{
    public static byte[] ToByteArray(this IFormFile file)
    {
        using var target = new MemoryStream();
        file.OpenReadStream().CopyTo(target);
        return target.ToArray();
    }

    public static string ToBase64(this IFormFile file)
    {
        using var target = new MemoryStream();
        file.OpenReadStream().CopyTo(target);
        return Convert.ToBase64String(target.ToArray());
    }

    public static string Save(this IFormFile file, string fullPath)
    {
        if (file is null || file.Length <= 0) return null;

        using (var stream = new FileStream(fullPath, FileMode.Create))
        {
            file.CopyTo(stream);
        }
        if (File.Exists(fullPath)) return fullPath.Contains("wwwroot/") ? fullPath.Remove(0, 8) : fullPath;
        return null;
    }

    public static string SaveFile(this byte[] fileBytes, string fullPath)
    {
        if (fileBytes is null || fileBytes.Length <= 0) return null;

        var file = new FormFile(null, 0, fileBytes.Length, "", "");

        using (var stream = new FileStream(fullPath, FileMode.Create))
        {
            file.CopyTo(stream);
        }
        if (File.Exists(fullPath)) return fullPath;
        return null;
    }
}