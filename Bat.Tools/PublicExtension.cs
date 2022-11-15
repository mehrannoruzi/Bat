using System.Drawing;
using System.Drawing.Text;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;

namespace Bat.Tools;

public static class PublicExtension
{
    public static byte[] ToExcel<T>(this List<T> data) where T : class
    {
        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
        using var package = new ExcelPackage();
        var workSheet = package.Workbook.Worksheets.Add("ReportData");
        var reportFields = data.Count > 0 ? data.First().GetType().GetProperties() : typeof(T).GetProperties();
        var row = 1;
        var cell = 1;
        foreach (var field in reportFields)
        {
            workSheet.Cells[row, cell].Value = field.Name;
            workSheet.Cells[row, cell].Style.Font.Size = 16;
            workSheet.Cells[row, cell].Style.Font.Bold = true;
            workSheet.Cells[row, cell].AutoFitColumns(15, 20);
            cell++;
        }

        if (data.Any())
        {
            row = 2;
            foreach (var record in data)
            {
                cell = 1;
                foreach (var field in reportFields)
                {
                    var value = field.GetValue(record);
                    workSheet.Cells[row, cell].Value = value == null
                        ? string.Empty
                        : (value.GetType().Name.Contains("Anonymous") ? value?.SerializeToJson() : value.ToString());
                    cell++;
                }
                row++;
            }
        }

        workSheet.Protection.IsProtected = false;
        workSheet.Protection.AllowSelectLockedCells = false;
        using var fileStream = new MemoryStream();
        package.SaveAs(fileStream);
        return fileStream.ToArray();
    }

    public static bool IsPicture(this string fileNameWithExtension)
    {
        var fileExtention = Path.GetExtension(fileNameWithExtension);
        if (!fileExtention.ToLower().Contains("jpg") ||
                !fileExtention.ToLower().Contains("jpeg") ||
                !fileExtention.ToLower().Contains("png") ||
                !fileExtention.ToLower().Contains("gif") ||
                !fileExtention.ToLower().Contains("bmp")) return false;

        return true;
    }

    public static bool IsValidPersianDate(this string persianDate)
    {
        if (string.IsNullOrWhiteSpace(persianDate)) return false;

        var regex = new Regex(@"(1\d{3})[-/](\d{1,2})[-/](\d{1,2})([ -]*(\d{1,2}:?){2,3})?");
        if (!regex.Match(persianDate).Success) return false;

        return true;
    }


    [System.Diagnostics.CodeAnalysis.SuppressMessage("Interoperability", "CA1416:Validate platform compatibility", Justification = "<Pending>")]
    public static string GetCaptcha(string text)
    {
        var bitmap = new Bitmap(1, 1);
        var font = new Font("Arial", 25, FontStyle.Regular, GraphicsUnit.Pixel);
        var graphics = Graphics.FromImage(bitmap);
        int width = (int)graphics.MeasureString(text, font).Width;
        int height = (int)graphics.MeasureString(text, font).Height;
        bitmap = new Bitmap(bitmap, new Size(width, height));
        graphics = Graphics.FromImage(bitmap);
        graphics.Clear(Color.White);

        var objRandom = new Random();
        graphics.DrawLine(Pens.Black, objRandom.Next(0, 50), objRandom.Next(10, 30), objRandom.Next(0, 200), objRandom.Next(0, 50));
        graphics.DrawRectangle(Pens.Blue, objRandom.Next(0, 20), objRandom.Next(0, 20), objRandom.Next(50, 80), objRandom.Next(0, 20));
        graphics.DrawLine(Pens.Blue, objRandom.Next(0, 20), objRandom.Next(10, 50), objRandom.Next(100, 200), objRandom.Next(0, 80));
        HatchStyle[] aHatchStyles = new HatchStyle[]
        {
                HatchStyle.BackwardDiagonal, HatchStyle.Cross, HatchStyle.DashedDownwardDiagonal, HatchStyle.DashedHorizontal, HatchStyle.DashedUpwardDiagonal, HatchStyle.DashedVertical,
                HatchStyle.DiagonalBrick, HatchStyle.DiagonalCross, HatchStyle.Divot, HatchStyle.DottedDiamond, HatchStyle.DottedGrid, HatchStyle.ForwardDiagonal, HatchStyle.Horizontal,
                HatchStyle.HorizontalBrick, HatchStyle.LargeCheckerBoard, HatchStyle.LargeConfetti, HatchStyle.LargeGrid, HatchStyle.LightDownwardDiagonal, HatchStyle.LightHorizontal
        };
        var oRectangleF = new RectangleF(0, 0, 300, 300);
        var objBrush = new HatchBrush(aHatchStyles[objRandom.Next(aHatchStyles.Length - 3)], Color.FromArgb(objRandom.Next(100, 255), objRandom.Next(100, 255), objRandom.Next(100, 255)), Color.White);
        graphics.FillRectangle(objBrush, oRectangleF);

        graphics.SmoothingMode = SmoothingMode.AntiAlias;
        graphics.TextRenderingHint = TextRenderingHint.AntiAlias;
        graphics.DrawString(text, font, new SolidBrush(Color.FromArgb(255, 0, 0)), 0, 0);
        graphics.Flush();
        graphics.Dispose();

        using var ms = new MemoryStream();
        bitmap.Save(ms, ImageFormat.Png);
        return Convert.ToBase64String(ms.ToArray());
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Interoperability", "CA1416:Validate platform compatibility", Justification = "<Pending>")]
    public static string GetCaptcha2(string text)
    {
        var bitmap = new Bitmap(1, 1);
        var font = new Font("Arial", 25, FontStyle.Regular, GraphicsUnit.Pixel);
        var graphics = Graphics.FromImage(bitmap);
        int width = (int)graphics.MeasureString(text, font).Width;
        int height = (int)graphics.MeasureString(text, font).Height;
        bitmap = new Bitmap(bitmap, new Size(width, height));
        graphics = Graphics.FromImage(bitmap);
        graphics.Clear(Color.White);
        graphics.SmoothingMode = SmoothingMode.AntiAlias;
        graphics.TextRenderingHint = TextRenderingHint.AntiAlias;
        graphics.DrawString(text, font, new SolidBrush(Color.FromArgb(255, 0, 0)), 0, 0);
        graphics.Flush();
        graphics.Dispose();

        using var ms = new MemoryStream();
        bitmap.Save(ms, ImageFormat.Png);
        return Convert.ToBase64String(ms.ToArray());
    }

}