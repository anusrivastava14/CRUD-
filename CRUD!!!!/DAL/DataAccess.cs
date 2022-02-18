using CRUD____.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CRUD____.DAL
{
    public class DataAccess:DbContext
    {
        public IEnumerable<MODEL>Reach(MODEL model,int procid)
        {

            var param = new SqlParameter[]
            {
                new SqlParameter {ParameterName="@ID",Value=model.ID==null?0:model.ID },
                new SqlParameter {ParameterName="@Name",Value=model.Name??string.Empty },
                new SqlParameter {ParameterName="@FatherName",Value=model.Fname??string.Empty },
                new SqlParameter {ParameterName="@MotherName",Value=model.Mname??string.Empty },
                new SqlParameter {ParameterName="@Mobile",Value=model.MOB??string.Empty },
                new SqlParameter {ParameterName="@Qualification",Value=model.Qf??string.Empty },
                new SqlParameter {ParameterName="@ImagePath",Value=model.ImagePath??string.Empty },
                new SqlParameter {ParameterName="@Gender",Value=model.gender??string.Empty },
                new SqlParameter {ParameterName="@cityid",Value=model.CityId==null?0:model.CityId },
                new SqlParameter {ParameterName="@stateid",Value=model.StateId==null?0:model.StateId },
                new SqlParameter {ParameterName="@procid",Value=procid }
                };
            var com = @"SP_STU_REG @ID,@Name,@FatherName,@MotherName,@Mobile,@Qualification,@ImagePath,@Gender,@cityid,@stateid,@procid";
            return this.Database.SqlQuery<MODEL>(com, param);

        }
        public IEnumerable<MODEL1>Reach1(int procid)
        {
            var param = new SqlParameter[]
                { new SqlParameter {ParameterName="@procid",Value=procid }};
            var com = @"SP_STATE @procid";
            return this.Database.SqlQuery<MODEL1>(com, param);
        }
        public IEnumerable<MODEL1>Reach2(int value, int procid)
        {
            var param = new SqlParameter[]
            {
                new SqlParameter {ParameterName="@ID",Value=value },
                new SqlParameter {ParameterName="@procid",Value=procid }
            };
            var com = @"SP_STATEBYID @ID,@procid";
            return this.Database.SqlQuery<MODEL1>(com, param);
        }
    }
}