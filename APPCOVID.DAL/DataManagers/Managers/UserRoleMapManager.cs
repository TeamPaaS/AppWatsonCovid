using APPCOVID.DAL.DataManagers.Core;
using APPCOVID.DAL.DataManagers.Helper;
using System.Collections.Generic;
using APPCOVID.Entity.DTO;

namespace APP27062020.DAL.DataManagers
{
    public class UserRoleMapManager: DataManager<UserRoleMapDto>
    {
        #region Db Queries 
        private readonly string createTableQuery = $@"CREATE TABLE {DalCostants.TABLE_SCHEMA}.{DalCostants.USERROLEMAP_TABLE}" +
           "( ROLEID INT  NOT NULL, " +
           "USERID INT NOT NULL );";

        private readonly string getAllDataQuery = $@"select * from {DalCostants.TABLE_SCHEMA}.{DalCostants.USERROLEMAP_TABLE}";

        #endregion

        private DataManager<UserRoleMapDto> _dataManager;

        public UserRoleMapManager()
        {
            _dataManager = new DataManager<UserRoleMapDto>();
            if (!_dataManager.IsTableExistsinDb(DalCostants.TABLE_SCHEMA, DalCostants.USERROLEMAP_TABLE))
            {
                _dataManager.CreateNewTable(createTableQuery, DalCostants.TABLE_SCHEMA, DalCostants.USERROLEMAP_TABLE);
            }
        }
        public List<UserRoleMapDto> GetUserRoleMapData()
        {
            return _dataManager.GetData(getAllDataQuery);
        }

        public bool CreateUserRoleMap(UserRoleMapDto map)
        {
            string prepareQuery = $@"insert into {DalCostants.TABLE_SCHEMA}.{DalCostants.USERROLEMAP_TABLE} (ROLEID,USERID) values({map.ROLEID},{map.USERID})";
            return _dataManager.InsertData(prepareQuery);
        }
    }
}
