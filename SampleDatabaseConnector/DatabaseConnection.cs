using System;
using System.Data;
using System.Data.SqlClient;

namespace SampleDatabaseConnector
{
    public class DatabaseConnection: IDisposable
    {
        private static readonly SqlConnectionStringBuilder LocalConnectionString =
            new SqlConnectionStringBuilder
            {
                ApplicationName = "LocalConnection",
                DataSource = "AJKERDEAL-SERVE",
                InitialCatalog = "AjkerDeal",
                IntegratedSecurity = false,
                Password = "Rony@Deal",
                PersistSecurityInfo = false,
                Pooling = true,
                UserID = "rony"
            };

        private static readonly SqlConnectionStringBuilder LiveConnectionString =
            new SqlConnectionStringBuilder
            {
                //ApplicationName = "LiveConnection",
                //DataSource = "50.28.38.161",
                ////DataSource = "192.168.0.4",
                //InitialCatalog = "AjkerDeal",
                //IntegratedSecurity = false,
                //Password = "AD#RS@Dl+016",
                //PersistSecurityInfo = false,
                //Pooling = true,
                //UserID = "AjkerD"

                ApplicationName = "LiveConnection",
                //DataSource = "69.16.228.137",
                DataSource = "192.168.0.5",
                InitialCatalog = "AjkerDeal",
                IntegratedSecurity = false,
                Password = "b01shakH@23#R",
                PersistSecurityInfo = false,
                Pooling = true,
                UserID = "AjkerD"
            };

        private static readonly SqlConnectionStringBuilder AccountingLocalConnectionString =
            new SqlConnectionStringBuilder
            {
                ApplicationName = "AccountingLocalConnection",
                DataSource = "AJKERDEAL-SERVE",
                InitialCatalog = "AccountingAD",
                IntegratedSecurity = false,
                Password = "Rony@Deal",
                PersistSecurityInfo = false,
                Pooling = true,
                UserID = "rony"
            };
        private static readonly SqlConnectionStringBuilder AccountingLiveConnectionString =
            new SqlConnectionStringBuilder
            {
                ApplicationName = "AccountingLiveConnection",
                DataSource = "103.36.100.156",
                //DataSource = "192.168.0.4",
                InitialCatalog = "AccountingAD",
                IntegratedSecurity = false,
                Password = "AD_Acc_Pr2",
                PersistSecurityInfo = false,
                Pooling = true,
                UserID = "AccountingAD"
            };


        private static readonly SqlConnectionStringBuilder AjkerDealLogsLiveConnectionString =
    new SqlConnectionStringBuilder
    {
        ApplicationName = "AjkerDealLogsLiveConnection",
        //DataSource = "69.16.228.137",
        DataSource = "192.168.0.5",
        InitialCatalog = "AjkerDealLogs",
        IntegratedSecurity = false,
        Password = "Rony@Ajker#Deal",
        PersistSecurityInfo = false,
        Pooling = true,
        UserID = "AjkerDealLog"
    };
        private SqlConnection _databaseConnection;

        public IDbConnection GetConnection(bool isLiveConnection)
        {
            _databaseConnection = isLiveConnection ? 
                new SqlConnection(LiveConnectionString.ConnectionString) : 
                new SqlConnection(LocalConnectionString.ConnectionString);
            try
            {
                _databaseConnection?.Open();
            }
            catch (Exception)
            {
                // ignored
            }
            return _databaseConnection;
        }

        public IDbConnection GetConnectionAccounting(bool isLiveConnection)
        {
            _databaseConnection = isLiveConnection ?
                new SqlConnection(AccountingLiveConnectionString.ConnectionString) :
                new SqlConnection(AccountingLocalConnectionString.ConnectionString);
            try
            {
                _databaseConnection?.Open();
            }
            catch (Exception)
            {
                // ignored
            }
            return _databaseConnection;
        }

        public IDbConnection GetConnectionAjkerDealLogs()
        {
            _databaseConnection = new SqlConnection(AjkerDealLogsLiveConnectionString.ConnectionString); 

            try
            {
                _databaseConnection?.Open();
            }
            catch (Exception)
            {
                // ignored
            }
            return _databaseConnection;
        }

        public void Dispose()
        {
            try
            {
                if (_databaseConnection.State != ConnectionState.Closed)
                {
                    _databaseConnection.Close();
                }
            }
            catch (Exception)
            {
                // ignored
            }
            SqlConnection.ClearPool(_databaseConnection);
        }
    }
}