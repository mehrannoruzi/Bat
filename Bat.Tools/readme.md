For use Bat.Tools just do it :

1- Install Bat.Tools on your project

2- Use it in bussiness logic
for example :



    public class BaseService : IBaseService
    {
        public void UseBatToolsSample()
        {
            //Excel extention methods
            var userList = await _appUow.UserRepo
                .Include(x => x.Families)
                .Select(x => new { x.UserId, x.Name })
                .ToListAsync();

            var excelByteArray = userList.ToExcel(sheetName: "UserReport");


            using var package = new ExcelPackage("files/UserReport.xlsx")
            var data = package.Workbook.Worksheets["UserReport"]
                .Extract<RowDataWithColumnBeingRow>()
                .WithProperty(p => p.UserId, "B")
                .WithProperty(p => p.Name, "C")

                // Here, the collection property is defined using the "WithCollectionProperty" method.
                // The following parameter is the expression indicating the property of "ColumnData"
                // that will be used to receive the header data followed by an integer indicating the row
                // that contains the header.
                // The last expression indicates the other "ColumnData" property, this one will receive
                // the row data. The two last strings are the start and end column from where
                // this data will be extracted.
                .WithCollectionProperty(p => p.Families,
                    item => item.FirstName, 1,
                    item => item.LastName, "E", "S")
                .GetData(2, 10)
                .ToList();

            var base64ImageString = "569821".GetCaptchaImage();

        }
    }
