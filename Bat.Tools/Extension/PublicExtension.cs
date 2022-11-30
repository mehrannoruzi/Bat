namespace Bat.Tools;

public static class PublicExtension
{
    public static byte[] ToExcel<T>(this List<T> data, bool withAnonymousObject = true,
        bool withCollectionsObject = true, string sheetName = "ReportData") where T : class
    {
        ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
        using var package = new ExcelPackage();
        var workSheet = package.Workbook.Worksheets.Add(sheetName);
        var reportFields = data.Count > 0 ? data.First().GetType().GetProperties() : typeof(T).GetProperties();
        var row = 1;
        var cell = 1;
        foreach (var field in reportFields)
        {
            workSheet.Cells[row, cell].Value = field.Name;
            workSheet.Cells[row, cell].Style.Font.Size = 16;
            workSheet.Cells[row, cell].Style.Font.Bold = true;
            workSheet.Cells[row, cell].AutoFitColumns(15, 50);
            cell++;
        }

        if (data.Any())
        {
            row = 2;
            foreach (var record in data)
            {
                #region Fill Data
                cell = 1;
                foreach (var field in reportFields)
                {
                    var value = field.GetValue(record);
                    var typeName = field.PropertyType.Name;

                    if (value == null)
                    {
                        workSheet.Cells[row, cell].Value = string.Empty;
                    }
                    else
                    {
                        if (typeName.Contains("Anonymous"))
                        {
                            workSheet.Cells[row, cell].Value = withAnonymousObject == false
                                ? string.Empty
                                : value.SerializeToJson();
                        }
                        else if (typeName.Contains("Enumerable"))
                        {
                            if (withCollectionsObject == false)
                            {
                                workSheet.Cells[row, cell].Value = string.Empty;
                            }
                            else
                            {
                                foreach (var item in value as IEnumerable<object>)
                                {
                                    workSheet.Cells[row, cell].Value += item.SerializeToJson() + Environment.NewLine;
                                    row++;
                                }
                                row--;
                            }
                        }
                        else
                        {
                            workSheet.Cells[row, cell].Value = value.ToString();
                        }
                    }
                    cell++;
                }
                row++;
                #endregion
            }
        }

        workSheet.Protection.IsProtected = false;
        workSheet.Protection.AllowSelectLockedCells = false;
        using var fileStream = new MemoryStream();
        package.SaveAs(fileStream);
        return fileStream.ToArray();
    }

    public static byte[] ToExcel<T>(this List<T> data, bool withAnonymousObject = true,
        bool withCollectionsObject = true, string sheetName = "ReportData", List<string> excludeProperties = null) where T : class
    {
        ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
        using var package = new ExcelPackage();
        var workSheet = package.Workbook.Worksheets.Add(sheetName);
        var reportFields = data.Count > 0 ? data.First().GetType().GetProperties().ToList() : typeof(T).GetProperties().ToList();
        if (excludeProperties.Any()) reportFields = reportFields.Except(reportFields.Where(x => excludeProperties.Contains(x.Name)).ToList()).ToList();

        var row = 1;
        var cell = 1;
        foreach (var field in reportFields)
        {
            workSheet.Cells[row, cell].Value = field.Name;
            workSheet.Cells[row, cell].Style.Font.Size = 16;
            workSheet.Cells[row, cell].Style.Font.Bold = true;
            workSheet.Cells[row, cell].AutoFitColumns(15, 50);
            cell++;
        }

        if (data.Any())
        {
            row = 2;
            foreach (var record in data)
            {
                #region Fill Data
                cell = 1;
                foreach (var field in reportFields)
                {
                    var value = field.GetValue(record);
                    var typeName = field.PropertyType.Name;

                    if (value == null)
                    {
                        workSheet.Cells[row, cell].Value = string.Empty;
                    }
                    else
                    {
                        if (typeName.Contains("Anonymous"))
                        {
                            workSheet.Cells[row, cell].Value = withAnonymousObject == false
                                ? string.Empty
                                : value.SerializeToJson();
                        }
                        else if (typeName.Contains("Enumerable"))
                        {
                            if (withCollectionsObject == false)
                            {
                                workSheet.Cells[row, cell].Value = string.Empty;
                            }
                            else
                            {
                                foreach (var item in value as IEnumerable<object>)
                                {
                                    workSheet.Cells[row, cell].Value += item.SerializeToJson() + Environment.NewLine;
                                    row++;
                                }
                                row--;
                            }
                        }
                        else
                        {
                            workSheet.Cells[row, cell].Value = value.ToString();
                        }
                    }
                    cell++;
                }
                row++;
                #endregion
            }
        }

        workSheet.Protection.IsProtected = false;
        workSheet.Protection.AllowSelectLockedCells = false;
        using var fileStream = new MemoryStream();
        package.SaveAs(fileStream);
        return fileStream.ToArray();
    }


}