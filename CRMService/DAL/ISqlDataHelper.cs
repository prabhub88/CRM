using System.Data.SqlClient;
using System.Data;
namespace DAL
{
    public interface ISqlDataHelper
    {
        bool InsertOrUpdate(string SP_Name, params SqlParameter[] Parameter_Values);

        DataSet GetDataset(string SP_Name, params SqlParameter[] Parameter_Values);

    }
}
