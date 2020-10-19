using System.Linq;
using System.Collections;
using System.Data.SqlTypes;
using Microsoft.SqlServer.Server;

namespace Bat.Sql
{
    public partial class StringFunction
    {
        public static void GetSplitedItems(object text, out SqlString Items) => Items = text.ToString();

        [SqlFunction(DataAccess = DataAccessKind.Read, 
                    FillRowMethodName = "GetSplitedItems", 
                    TableDefinition = "Items NVARCHAR(MAX)")]
        public static IEnumerable SplitText(string text, char spliter)
        {
            var result = new ArrayList();
            if (IsNullOrEmpty(text)) return result;

            return text.Split(spliter).Select(Items => (SqlString)Items.Trim()).ToList();
        }
    }
}
